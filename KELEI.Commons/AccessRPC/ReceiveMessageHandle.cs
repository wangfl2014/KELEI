using KELEI.Commons.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KELEI.Commons.AccessRPC
{
    public class ReceiveMessageHandle
    {
        private BaseMessage message = new BaseMessage();
        private object Body;
        public ReceiveMessageHandle(BaseMessage mess)
        {
            message = mess;
            Body = GetMessageBody();
        }

        private object GetMessageBody()
        {
            if (message.Body == null)
                return null;

            byte[] bodyToDeserialize = message.Body;
            if (message.CompressionAlgorithm == CompressionAlgorithm.GZipStream)
                bodyToDeserialize = ZipHelper.DecompressMessageUsingGZipStream(message.Body);

            object serializedObject = null;
            using (MemoryStream ms = new MemoryStream(bodyToDeserialize))
            {
                BinaryFormatter bf = new BinaryFormatter();
                serializedObject = bf.Deserialize(ms);
            }
            return serializedObject;
        }

        /// <summary>
        /// Initializes provided parameters based on message body. Function checks number of parameters in the message and their types.
        /// </summary>
        /// <typeparam name="X">Type of the parameter.</typeparam>
        /// <param name="param1">Parameter to initialize.</param>
        public void GetParameters<X>(out X param1)
        {
            param1 = default(X);

            List<object> retVal = GetParametersValues(typeof(X));
            if (retVal == null)
                return;

            param1 = (X)retVal[0];
        }

        /// <summary>
        /// Initializes provided parameters based on message body. Function checks number of parameters in the message and their types.
        /// </summary>
        /// <typeparam name="X">Type of the 1 parameter.</typeparam>
        /// <typeparam name="Y">Type of the 2 parameter.</typeparam>
        /// <param name="param1">First parameter to initialize.</param>
        /// <param name="param2">Second parameter to initialize.</param>
        public void GetParameters<X, Y>(out X param1, out Y param2)
        {
            param1 = default(X);
            param2 = default(Y);

            List<object> retVal = GetParametersValues(typeof(X), typeof(Y));
            if (retVal == null)
                return;

            param1 = (X)retVal[0];
            param2 = (Y)retVal[1];
        }

        /// <summary>
        /// Initializes provided parameters based on message body. Function checks number of parameters in the message and their types.
        /// </summary>
        /// <typeparam name="X">Type of the 1 parameter.</typeparam>
        /// <typeparam name="Y">Type of the 2 parameter.</typeparam>
        /// <typeparam name="Z">Type of the 3 parameter.</typeparam>
        /// <param name="param1">First parameter to initialize.</param>
        /// <param name="param2">Second parameter to initialize.</param>
        /// <param name="param3">Third parameter to initialize.</param>
        public void GetParameters<X, Y, Z>(out X param1, out Y param2, out Z param3)
        {
            param1 = default(X);
            param2 = default(Y);
            param3 = default(Z);

            List<object> retVal = GetParametersValues(typeof(X), typeof(Y), typeof(Z));
            if (retVal == null)
                return;

            param1 = (X)retVal[0];
            param2 = (Y)retVal[1];
            param3 = (Z)retVal[2];
        }

        public void GetParameters<TP1, TP2, TP3, TP4>(out TP1 param1, out TP2 param2, out TP3 param3, out TP4 param4)
        {
            param1 = default(TP1);
            param2 = default(TP2);
            param3 = default(TP3);
            param4 = default(TP4);

            List<object> retVal = GetParametersValues(typeof(TP1), typeof(TP2), typeof(TP3), typeof(TP4));
            if (retVal == null)
                return;

            param1 = (TP1)retVal[0];
            param2 = (TP2)retVal[1];
            param3 = (TP3)retVal[2];
            param4 = (TP4)retVal[3];
        }

        public void GetParameters<TP1, TP2, TP3, TP4, TP5>(out TP1 param1, out TP2 param2, out TP3 param3, out TP4 param4, out TP5 param5)
        {
            param1 = default(TP1);
            param2 = default(TP2);
            param3 = default(TP3);
            param4 = default(TP4);
            param5 = default(TP5);

            List<object> retVal = GetParametersValues(typeof(TP1), typeof(TP2), typeof(TP3), typeof(TP4), typeof(TP5));
            if (retVal == null)
                return;

            param1 = (TP1)retVal[0];
            param2 = (TP2)retVal[1];
            param3 = (TP3)retVal[2];
            param4 = (TP4)retVal[3];
            param5 = (TP5)retVal[4];
        }

        public void GetParameters<TP1, TP2, TP3, TP4, TP5, TP6>(out TP1 param1, out TP2 param2, out TP3 param3, out TP4 param4, out TP5 param5, out TP6 param6)
        {
            param1 = default(TP1);
            param2 = default(TP2);
            param3 = default(TP3);
            param4 = default(TP4);
            param5 = default(TP5);
            param6 = default(TP6);

            List<object> retVal = GetParametersValues(typeof(TP1), typeof(TP2), typeof(TP3), typeof(TP4), typeof(TP5), typeof(TP6));
            if (retVal == null)
                return;

            param1 = (TP1)retVal[0];
            param2 = (TP2)retVal[1];
            param3 = (TP3)retVal[2];
            param4 = (TP4)retVal[3];
            param5 = (TP5)retVal[4];
            param6 = (TP6)retVal[5];
        }

        public void GetParameters<TP1, TP2, TP3, TP4, TP5, TP6, TP7>(out TP1 param1, out TP2 param2, out TP3 param3, out TP4 param4, out TP5 param5, out TP6 param6, out TP7 param7)
        {
            param1 = default(TP1);
            param2 = default(TP2);
            param3 = default(TP3);
            param4 = default(TP4);
            param5 = default(TP5);
            param6 = default(TP6);
            param7 = default(TP7);

            List<object> retVal = GetParametersValues(typeof(TP1), typeof(TP2), typeof(TP3), typeof(TP4), typeof(TP5), typeof(TP6), typeof(TP7));
            if (retVal == null)
                return;

            param1 = (TP1)retVal[0];
            param2 = (TP2)retVal[1];
            param3 = (TP3)retVal[2];
            param4 = (TP4)retVal[3];
            param5 = (TP5)retVal[4];
            param6 = (TP6)retVal[5];
            param7 = (TP7)retVal[6];
        }

        /// <summary>
        /// Returns list of parameters from the current message and checks wheather types and quantity of passed parameters are correct.
        /// </summary>
        /// <param name="parametersTypes">Message parameters.</param>
        /// <returns>List of parameters from the message body.</returns>
        public List<object> GetParametersValues(params Type[] parametersTypes)
        {
            return GetParametersValuesImp(Body, parametersTypes);
        }

        /// <summary>
        /// Gets all sent parameters without type checking
        /// </summary>
        /// <returns></returns>
        public object[] GetParameters()
        {
            object[] pars = Body as object[];
            return pars;
        }

        /// <summary>
        /// Checks if passed value can be cast to specified type. Throws an exception if not.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <exception cref="ArgumentException">If passed value cannot be cast to specified type.</exception>
        private static void CheckIfAssignableFrom(object obj, Type type)
        {
            if (type == null)
                return;
            if (obj == null)
            {
                if (type.IsValueType)
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        return;
                    throw new ArgumentException("ValueType value cannot be null");
                }
                return;
            }
            if (!type.IsAssignableFrom(obj.GetType()))
            {
                throw new ArgumentException("Parameter type is different then expected");
            }
        }

        /// <summary>
        /// Returns list of parameters from the current message and checks wheather types and quantity of passed parameters are correct.
        /// </summary>
        /// <param name="body">Message body.</param>
        /// <param name="parametersTypes">Message parameters.</param>
        /// <returns>List of parameters from the message body.</returns>
        public static List<object> GetParametersValuesImp(object obj, params Type[] parametersTypes)
        {
            if (obj == null)
                return null;
            if (parametersTypes == null)
                return null;

            object[] pars = obj as object[];
            if (pars == null)
                throw new ArgumentException("Messsage doesn't contain list of parameters. Check message type (should be set to 'ListOfParameters') and body structure.");
            if (pars.Length == 0)
                return null;
            if (pars.Length != parametersTypes.Length)
                throw new ArgumentException("Incorrect number of parameters in message body.");

            List<object> retVal = new List<object>(parametersTypes.Length);

            for (int i = 0; i < pars.Length; i++)
            {
                CheckIfAssignableFrom(pars[i], parametersTypes[i]);
                retVal.Add(pars[i]);
            }

            return retVal;
        }
    }
}