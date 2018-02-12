using System;
using System.Reflection;

namespace KELEI.Commons.Helper
{
    /// <summary>
	/// 自定义的异常类，该类中对异常不做任何处理，仅仅提供该类的定义
	/// </summary>
	/// <remarks>
	/// 自定义的异常类，该类中对异常不做任何处理，仅仅提供该类的定义
	/// </remarks>
	/// <example>
	/// <code>
	/// try
	/// {
	///		.......
	/// }
	/// catch (DmcSystemAbortException)
	/// {
	///		
	/// }
	/// </code>
	/// </example>
	public class DmcSystemAbortException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public DmcSystemAbortException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        public DmcSystemAbortException(string strMessage) : base(strMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="ex"></param>
        public DmcSystemAbortException(string strMessage, Exception ex)
            : base(strMessage, ex)
        {
        }
    }

    /// <summary>
    /// 定制的异常类。这种异常类会提醒前端程序显示出技术支持信息的提示信息
    /// </summary>
    public class DmcSystemSupportException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public DmcSystemSupportException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        public DmcSystemSupportException(string strMessage) : base(strMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="ex"></param>
        public DmcSystemSupportException(string strMessage, Exception ex)
            : base(strMessage, ex)
        {
        }
    }

    /// <summary>
    /// 定制的异常类。这种异常类发生在对Config文件进行解析的时候。
    /// </summary>
    public class DmcConfigException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public DmcConfigException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        public DmcConfigException(string strMessage) : base(strMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="ex"></param>
        public DmcConfigException(string strMessage, Exception ex)
            : base(strMessage, ex)
        {
        }
    }

    /// <summary>
    /// Config Handler执行时抛出的异常
    /// </summary>
    public class DmcConfigHandlerException : ApplicationException
    {
        public DmcConfigHandlerException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        public DmcConfigHandlerException(string strMessage) : base(strMessage)
        {
        }

        public DmcConfigHandlerException(string strMessage, Exception ex)
            : base(strMessage, ex)
        {
        }
    }

    /// <summary>
    /// 定制的异常类。这种异常类发生在对Config文件进行解析时，未能发现要寻找的节点时。
    /// </summary>
    public class DmcConfigMissNodeException : DmcConfigException
    {
        /// <summary>
        /// 
        /// </summary>
        public DmcConfigMissNodeException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        public DmcConfigMissNodeException(string strMessage) : base(strMessage)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="ex"></param>
        public DmcConfigMissNodeException(string strMessage, Exception ex) : base(strMessage, ex)
        {
        }
    }

    /// <summary>
    /// Summary description for Exception.
    /// </summary>
    public class DmcExceptionTools
    {

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出strMessage指定的错误信息
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <param name="strMessage">错误信息</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出strMessage指定的错误信息
        /// </remarks>
        /// <example>
        /// <code>
        /// DBAccess dba = new DBAccess();
        /// string strSql = "SELECT * FROM TYPE_DEFINE WHERE GUID='f92659c8-df7f-476d-ac26-83e3956fdb7d'";
        /// object obj = dba.RunSQLExecuteScalar(strSql);
        /// MCSException.TrueThrow(obj == null, "对不起，没有该用户的信息！");
        /// </code>
        /// </example>
        /// <seealso cref="FalseThrow"/>
        public static void TrueThrow(bool boolExpression, string strMessage)
        {
            TrueThrow(boolExpression, typeof(DmcSystemSupportException), strMessage);
        }

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出strMessage指定的错误信息
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <param name="exceptionType">异常的类型</param>
        /// <param name="strMessage">错误信息</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出strMessage指定的错误信息
        /// </remarks>
        /// <example>
        /// <code>
        /// DBAccess dba = new DBAccess();
        /// string strSql = "SELECT * FROM TYPE_DEFINE WHERE GUID='f92659c8-df7f-476d-ac26-83e3956fdb7d'";
        /// object obj = dba.RunSQLExecuteScalar(strSql);
        /// MCSException.TrueThrow(obj == null, "对不起，没有该用户的信息！");
        /// </code>
        /// </example>
        /// <seealso cref="FalseThrow"/>
        public static void TrueThrow(bool boolExpression, Type exceptionType, string strMessage)
        {
            if (boolExpression == true)
            {
                Object obj = Activator.CreateInstance(exceptionType);

                Type[] types = new Type[1];
                types[0] = typeof(string);

                ConstructorInfo constructorInfoObj = exceptionType.GetConstructor(
                    BindingFlags.Instance | BindingFlags.Public, null,
                    CallingConventions.HasThis, types, null);

                Object[] args = new Object[1];

                args[0] = strMessage;

                constructorInfoObj.Invoke(obj, args);

                throw (Exception)obj;
            }
        }

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为假（false），则抛出strMessage指定的错误信息
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <param name="strMessage">错误信息</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为假（false），则抛出strMessage指定的错误信息
        /// </remarks>
        /// <example>
        /// <code>
        /// DBAccess dba = new DBAccess();
        /// string strSql = "SELECT * FROM TYPE_DEFINE WHERE GUID='f92659c8-df7f-476d-ac26-83e3956fdb7d'";
        /// object obj = dba.RunSQLExecuteScalar(strSql);
        /// MCSException.FalseThrow(obj != null, "对不起，没有该用户的信息！");
        /// </code>
        /// </example>
        /// <seealso cref="TrueThrow"/>
        public static void FalseThrow(bool boolExpression, string strMessage)
        {
            TrueThrow(!boolExpression, strMessage);
        }

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为假（false），则抛出strMessage指定的错误信息
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <param name="exceptionType">异常的类型</param>
        /// <param name="strMessage">错误信息</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为假（false），则抛出strMessage指定的错误信息
        /// </remarks>
        /// <example>
        /// <code>
        /// DBAccess dba = new DBAccess();
        /// string strSql = "SELECT * FROM TYPE_DEFINE WHERE GUID='f92659c8-df7f-476d-ac26-83e3956fdb7d'";
        /// object obj = dba.RunSQLExecuteScalar(strSql);
        /// MCSException.FalseThrow(obj != null, "对不起，没有该用户的信息！");
        /// </code>
        /// </example>
        /// <seealso cref="TrueThrow"/>
        public static void FalseThrow(bool boolExpression, Type exceptionType, string strMessage)
        {
            TrueThrow(!boolExpression, exceptionType, strMessage);
        }

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出Abort(忽略的异常，不用作任何处理)
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为真(true)，则抛出Abort(忽略的异常，不用作任何处理)
        /// </remarks>
        /// <seealso cref="FalseThrowAbort"/>
        public static void TrueThrowAbort(bool boolExpression)
        {
            if (boolExpression == true)
                throw new DmcSystemAbortException();
        }

        /// <summary>
        /// 如果条件表达式boolExpression的结果值为假(false)，则抛出Abort(忽略的异常，不用作任何处理)
        /// </summary>
        /// <param name="boolExpression">条件表达式</param>
        /// <remarks>
        /// 如果条件表达式boolExpression的结果值为假(false)，则抛出Abort(忽略的异常，不用作任何处理)
        /// </remarks>
        /// <seealso cref="TrueThrowAbort"/>
        public static void FalseThrowAbort(bool boolExpression)
        {
            TrueThrowAbort(!boolExpression);
        }

        /// <summary>
        /// 检查字符串是否为空（空串或NULL），如果为空，则抛出异常
        /// </summary>
        /// <param name="strData">待检查的字符串</param>
        public static void CheckStringEmpty(string strData)
        {
            CheckStringEmpty(strData, string.Empty);
        }

        /// <summary>
        /// 检查字符串是否为空（空串或null），如果为空，则抛出异常
        /// </summary>
        /// <param name="strData">待检查的字符串</param>
        /// <param name="strParamName">为空的参数名称</param>
        public static void CheckStringEmpty(string strData, string strParamName)
        {
            System.ArgumentException ae = null;
            string strMessage = string.Empty;

            if (strData == null)
            {
                if (strParamName != string.Empty)
                    strMessage = string.Format("参数\"{0}\"不能为null", strParamName);
                else
                    strMessage = string.Format("字符串参数不能为null");

                ae = new ArgumentNullException(strParamName);
            }
            else
            if (strData == string.Empty)
            {
                if (strParamName != string.Empty)
                    strMessage = string.Format("参数\"{0}\"不能为空字符串", strParamName);
                else
                    strMessage = string.Format("字符串参数不能为空字符串");

                ae = new ArgumentException(strMessage);
            }

            if (ae != null)
                throw ae;
        }

        /// <summary>
        /// 检查参数是否为null，如果为null，则抛出异常
        /// </summary>
        /// <param name="oValue">需要检查的参数</param>
        public static void CheckNull(object oValue)
        {
            CheckNull(oValue, string.Empty);
        }

        /// <summary>
        /// 检查参数是否为null，如果为null，则抛出异常
        /// </summary>
        /// <param name="oValue">需要检查的参数</param>
        /// <param name="strParamName">参数的名称</param>
        public static void CheckNull(object oValue, string strParamName)
        {
            if (oValue == null)
            {
                string strMessage = string.Empty;

                if (strParamName != string.Empty)
                    strMessage = string.Format("参数\"{0}\"不能为null", strParamName);
                else
                    strMessage = "参数不能为null";

                throw new ArgumentNullException(strMessage);
            }
        }
    }
}
