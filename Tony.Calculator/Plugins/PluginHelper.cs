namespace Tony.Calculator.Plugins
{
    public static class PluginHelper
    {
        public static bool TryCastDouble(object arg, out double value)
        {
            value = 0;
            if (arg is double)
            {
                value = (double)arg;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool TryCastDouble(object arg1, object arg2, out double value1, out double value2)
        {
            value1 = 0;
            value2 = 0;
            if (arg1 is double)
            {
                value1 = (double)arg1;
                if (arg2 is double)
                {
                    value2 = (double)arg2;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public static bool TryCastDouble(object[] args, out double[] values)
        {
            values = new double[args.Length];
            for(int i = 0; i < args.Length; i++)
            {
                if(args[i] is double value)
                {
                    values[i] = value;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool TryCastLong(object arg, out long value)
        {
            value = 0;
            if (arg is long)
            {
                value = (long)arg;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool TryCastLong(object arg1, object arg2, out long value1, out long value2)
        {
            value1 = 0;
            value2 = 0;
            if (arg1 is long)
            {
                value1 = (long)arg1;
                if (arg2 is long)
                {
                    value2 = (long)arg2;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public static bool TryCastLong(object[] args, out long[] values)
        {
            values = new long[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is long value)
                {
                    values[i] = value;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
