using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tony.Calculator.SemanticAnalysis
{
    public struct SemanticError
    {
        public int Index { get; init; }
        public string Message { get; init; }
        public override string ToString()
        {
            return $"{nameof(Index)}: {Index}, {nameof(Message)}: {Message}";
        }
    }
    public class SemanticAnalyser
    {
        public SemanticAnalyser(DefinitionCollection definitions)
        {
            Definitions = definitions;
        }

        public DefinitionCollection Definitions { get; }

        public IParseNode Parse(IReadOnlyList<Token> tokens, out List<SemanticError> errors)
        {
            for(int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if(token.Type == TokenTypes.Unknown)
                {
                    throw new NotSupportedException($"Could not parse unknown token: \"{token.Text}\".");
                }
            }
            errors = new List<SemanticError>();
            return Parse(tokens, 0, tokens.Count - 1, errors);
        }

        /*
            Grammar:

            non-terminals:
            E: expression
            T: term
            P: parameters
            
            terminals:
            v: value
            id: variable or function
            o: unary or binary operator
            (), : self explanatory
            
            E -> EoE 
                | oE 
                | id(P) 
                | (E) 
                | T 
            P -> E,P 
                | E 
            T -> v 
                | id 
        */
        private IParseNode Parse(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            if(TryParseBinaryOperator(tokens, start, end, errors, out BinaryOperatorNode binaryOperatorNode))
            {
                return binaryOperatorNode;
            }
            Token startToken = tokens[start];
            switch (startToken.Type)
            {
                case TokenTypes.Number:
                    return ParseNumber(tokens, start, end, errors);
                case TokenTypes.Identifier:
                    if (tokens.Count > start + 1 && tokens[start + 1].Type == TokenTypes.L_Parenthesis)
                    {
                        return ParseFunction(tokens, start, end, errors);
                    }
                    else
                    {
                        return ParseVariable(tokens, start, end, errors);
                    }
                case TokenTypes.Operator:
                    return ParseUnaryOperator(tokens, start, end, errors);
                case TokenTypes.L_Parenthesis:
                    return ParseParentheses(tokens, start, end, errors);
                default:
                    errors.Add(new SemanticError()
                    {
                        Index = startToken.TextIndex,
                        Message = $"Unexepected token: {startToken}.",
                    });
                    return null;
            }
        }

        private IParseNode ParseNumber(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token numberToken = tokens[start];
            NumberNode numberNode = new NumberNode(numberToken);
            return numberNode;
        }

        private IParseNode ParseVariable(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token variableToken = tokens[start];

            if (!Definitions.Variables.TryGetValue(variableToken.Text.ToString(), out VariableDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = variableToken.TextIndex,
                    Message = $"Failed to find definition for variable: {definition.Name}.",
                });
            }

            VariableNode variableNode = new VariableNode(variableToken, definition);
            return variableNode;
        }

        private IParseNode ParseFunction(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token functionToken = tokens[start];

            int parenthesesCount = 0;
            int parameterStart = start + 2;
            List<IParseNode> parameters = new List<IParseNode>();
            for (int i = parameterStart; i <= end; i++)
            {
                Token newToken = tokens[i];
                if (newToken.Type == TokenTypes.L_Parenthesis)
                {
                    parenthesesCount++;
                }
                else if (tokens[i].Type == TokenTypes.R_Parenthesis)
                {
                    if (parenthesesCount == 0)
                    {
                        IParseNode parameter = Parse(tokens, parameterStart, i - 1, errors);
                        parameters.Add(parameter);
                        break;
                    }
                    parenthesesCount--;
                }
                else if (newToken.Type == TokenTypes.Colon)
                {
                    if (parenthesesCount == 0)
                    {
                        IParseNode parameter = Parse(tokens, parameterStart, i - 1, errors);
                        parameters.Add(parameter);
                        parameterStart = i + 1;
                    }
                }
            }
            if (parenthesesCount != 0)
            {
                errors.Add(new SemanticError()
                {
                    Index = end,
                    Message = "Missing closing parenthesis.",
                });
            }

            if (!Definitions.Functions.TryGetValue(functionToken.Text.ToString(), out FunctionDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = functionToken.TextIndex,
                    Message = $"Failed to find definition for unary operator symbol: {definition.Name}.",
                });
            }

            FunctionNode functionNode = new FunctionNode(functionToken, definition, parameters);
            return functionNode;
        }

        private IParseNode ParseUnaryOperator(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token unaryOperatorToken = tokens[start];

            if (!Definitions.UnaryOperators.TryGetValue(unaryOperatorToken.Text.ToString(), out UnaryOperatorDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = unaryOperatorToken.TextIndex,
                    Message = $"Failed to find definition for unary operator symbol: {definition.Symbol}.",
                });
            }

            IParseNode operand = Parse(tokens, start + 1, end, errors);
            UnaryOperatorNode unaryOperatorNode = new UnaryOperatorNode(unaryOperatorToken, definition, operand);
            return unaryOperatorNode;
        }

        private bool RequireBinaryOperator(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            if (start > end)
            {
                return false;
            }
            Token potentialBinaryOperatorToken = tokens[start];
            if (potentialBinaryOperatorToken.Type != TokenTypes.Operator)
            {
                errors.Add(new SemanticError()
                {
                    Index = potentialBinaryOperatorToken.TextIndex,
                    Message = $"Unexpected token after expression: {potentialBinaryOperatorToken}.",
                });
                return false;
            }
            return true;
        }

        //doesn't take priority into account
        private IParseNode ParseBinaryOperator(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors, IParseNode left)
        {
            Token binaryOperatorToken = tokens[start];
            IParseNode right;
            if (end - start >= 1)
            {
                right = Parse(tokens, start + 1, end, errors);
            }
            else
            {
                errors.Add(new SemanticError()
                {
                    Index = binaryOperatorToken.TextIndex,
                    Message = "Missing right side of binary operator.",
                });
                right = null;
            }
            if (!Definitions.BinaryOperators.TryGetValue(binaryOperatorToken.Text.ToString(), out BinaryOperatorDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = binaryOperatorToken.TextIndex,
                    Message = $"Failed to find definition for binary operator symbol: {definition.Symbol}.",
                });
            }
            return new BinaryOperatorNode(binaryOperatorToken, definition, left, right);
        }

        private bool TryParseBinaryOperator(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors, out BinaryOperatorNode binaryOperatorNode)
        {
            binaryOperatorNode = null;

            int count = 0;
            Token highestPriorityToken = default;
            BinaryOperatorDefinition highestPriorityDefinition = null;
            for (int i = start; i <= end; i++)
            {
                Token token = tokens[i];
                switch (token.Type)
                {
                    case TokenTypes.L_Parenthesis:
                        count++;
                        break;
                    case TokenTypes.R_Parenthesis:
                        count--;
                        break;
                    case TokenTypes.Operator:
                        if (count == 0 && i != 0 && tokens[i - 1] is Token previousToken
                            && previousToken.Type != TokenTypes.L_Parenthesis
                            && previousToken.Type != TokenTypes.Colon
                            && previousToken.Type != TokenTypes.Operator)
                        {
                            if (!Definitions.BinaryOperators.TryGetValue(token.Text.ToString(), out BinaryOperatorDefinition definition))
                            {
                                errors.Add(new SemanticError()
                                {
                                    Index = token.TextIndex,
                                    Message = $"Failed to find definition for binary operator symbol: {definition.Symbol}.",
                                });
                            }
                            else if (highestPriorityDefinition == null || highestPriorityDefinition.Priority <= definition.Priority)
                            {
                                highestPriorityToken = token;
                                highestPriorityDefinition = definition;
                            }
                        }
                        break;
                }
            }

            if(highestPriorityDefinition == null)
            {
                return false;
            }

            IParseNode left;
            if (highestPriorityToken.TokenIndex - start >= 1)
            {
                left = Parse(tokens, start, highestPriorityToken.TokenIndex - 1, errors);
            }
            else
            {
                errors.Add(new SemanticError()
                {
                    Index = highestPriorityToken.TextIndex,
                    Message = "Missing left side of binary operator.",
                });
                left = null;
            }
            IParseNode right;
            if (end - highestPriorityToken.TokenIndex >= 1)
            {
                right = Parse(tokens, highestPriorityToken.TokenIndex + 1, end, errors);
            }
            else
            {
                errors.Add(new SemanticError()
                {
                    Index = highestPriorityToken.TextIndex,
                    Message = "Missing right side of binary operator.",
                });
                right = null;
            }

            binaryOperatorNode = new BinaryOperatorNode(highestPriorityToken, highestPriorityDefinition, left, right);
            return true;
        }

        private IParseNode ParseParentheses(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            int count = 0;
            int closingParenthesisIndex = -1;
            for (int i = start + 1; i <= end; i++)
            {
                if (tokens[i].Type == TokenTypes.L_Parenthesis)
                {
                    count++;
                }
                if (tokens[i].Type == TokenTypes.R_Parenthesis)
                {
                    if (count == 0)
                    {
                        closingParenthesisIndex = i;
                        break;
                    }
                    count--;
                }
            }
            if (count != 0)
            {
                closingParenthesisIndex = end;
                errors.Add(new SemanticError()
                {
                    Index = end,
                    Message = "Missing closing parenthesis.",
                });
            }
            IParseNode parenthesisNode = Parse(tokens, start + 1, closingParenthesisIndex - 1, errors);
            return parenthesisNode;
        }
    }
}
