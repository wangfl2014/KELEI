using System;

namespace KELEI.Commons.Helper
{
    public static class DataConvertHelper
    {
        public static Tuple<bool, Int32> GetInt(string value)
        {
            Int32 valueint;
            if(Int32.TryParse(value,out valueint))
            {
                return new Tuple<bool, int>(true, valueint);
            }
            else
            {
                return new Tuple<bool, int>(false, 0);
            }
        }

        public static Tuple<bool,decimal> GetDecimal(string value)
        {
            decimal valuedec;
            if (decimal.TryParse(value, out valuedec))
            {
                return new Tuple<bool, decimal>(true, valuedec);
            }
            else
            {
                return new Tuple<bool, decimal>(false, 0);
            }
        }

        public static Tuple<bool, DateTime> GetDateTime(string value)
        {
            DateTime valueDate;
            if (DateTime.TryParse(value, out valueDate))
            {
                return new Tuple<bool, DateTime>(true, valueDate);
            }
            else
            {
                return new Tuple<bool, DateTime>(false, DateTime.Now);
            }
        }
    }
}