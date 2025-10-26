using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tony.Calculator.SemanticAnalysis
{
    public struct SemanticError
    {
        public int Index { get; init; }
        public string Message { get; init; }
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
            List<Token> filteredTokens = new List<Token>();
            for(int i = 0; i < tokens.Count; i++)
            {
                TokenTypes type = tokens[i].Type;
                if(type == TokenTypes.Unknown)
                {
                    throw new NotSupportedException($"Could not parse unknown token: \"{tokens[i].Text}\".");
                }
                else if(type != TokenTypes.Whitespace)
                {
                    filteredTokens.Add(tokens[i]);
                }
            }
            errors = new List<SemanticError>();
            return Parse(filteredTokens, 0, filteredTokens.Count, errors);
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
                | (E) ~
                | T
            P -> E,P 
                | E
            T -> v ~
                | id
        */
        private IParseNode Parse(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
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
                        Index = startToken.Index,
                        Message = $"Unexepected token: {startToken}.",
                    });
                    return null;
            }
        }

        private IParseNode ParseNumber(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token numberToken = tokens[start];
            NumberNode numberNode = new NumberNode(numberToken);
            int potentialBinaryOperatorIndex = start + 1;
            if (RequireBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors))
            {
                return ParseBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors, numberNode);
            }
            return numberNode;
        }

        private IParseNode ParseVariable(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token variableToken = tokens[start];

            if (!Definitions.Variables.TryGetValue(variableToken.Text.ToString(), out VariableDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = variableToken.Index,
                    Message = $"Failed to find definition for variable: {definition.Name}.",
                });
            }

            VariableNode variableNode = new VariableNode(variableToken, definition);
            int potentialBinaryOperatorIndex = start + 1;
            if (RequireBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors))
            {
                return ParseBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors, variableNode);
            }
            return variableNode;
        }

        private IParseNode ParseFunction(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            return null;
        }

        private IParseNode ParseUnaryOperator(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            Token unaryOperatorToken = tokens[start];

            if (!Definitions.UnaryOperators.TryGetValue(unaryOperatorToken.Text.ToString(), out UnaryOperatorDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = unaryOperatorToken.Index,
                    Message = $"Failed to find definition for unary operator symbol: {definition.Symbol}.",
                });
            }


            IParseNode operand = Parse(tokens, start + 1, end, errors);
            UnaryOperatorNode unaryOperatorNode = new UnaryOperatorNode(unaryOperatorToken, definition, operand);
            return unaryOperatorNode;
            int potentialBinaryOperatorIndex = start + 1;
            if (RequireBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors))
            {
                return ParseBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors, unaryOperatorNode);
            }
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
                    Index = potentialBinaryOperatorToken.Index,
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
                    Index = binaryOperatorToken.Index,
                    Message = "Missing right side of binary operator.",
                });
                right = null;
            }
            if (!Definitions.BinaryOperators.TryGetValue(binaryOperatorToken.Text.ToString(), out BinaryOperatorDefinition definition))
            {
                errors.Add(new SemanticError()
                {
                    Index = binaryOperatorToken.Index,
                    Message = $"Failed to find definition for binary operator symbol: {definition.Symbol}.",
                });
            }
            return new BinaryOperatorNode(binaryOperatorToken, definition, left, right);
        }

        private IParseNode ParseParentheses(IReadOnlyList<Token> tokens, int start, int end, List<SemanticError> errors)
        {
            int closingParenthesisIndex = FindClosingParenthesisIndex(tokens, start, end);
            IParseNode parenthesisNode = Parse(tokens, start + 1, closingParenthesisIndex - 1, errors);
            int potentialBinaryOperatorIndex = closingParenthesisIndex + 1;
            if (RequireBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors))
            {
                return ParseBinaryOperator(tokens, potentialBinaryOperatorIndex, end, errors, parenthesisNode);
            }
            return parenthesisNode;
        }

        private int FindClosingParenthesisIndex(IReadOnlyList<Token> tokens, int start, int end)
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
                throw new Exception("Missing closing parenthesis.");
            }
            return closingParenthesisIndex;
        }
    }
}
