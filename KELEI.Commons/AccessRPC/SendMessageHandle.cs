using KELEI.Commons.Helper;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KELEI.Commons.AccessRPC
{
    public class SendMessageHandle
    {
        private const int bodyLengthToCompress = 250000;
        private BaseMessage message = new BaseMessage();
        public SendMessageHandle()
        {
            message.Id = Guid.NewGuid();
            message.MessageType = BaseMessageType.Unknown;
        }
        /// <summary>
        /// Creates new instance of BaseMessage which contains parameters.
        /// </summary>
        /// <param name="subject">Subject of the message.</param>
        /// <param name="bodyParameters">List of parameters to put int message body.
        /// If the parameters list contains only one parameter, then it will be stored directly in message body, 
        /// otherwise in object[] array in the same order.</param>
        public SendMessageHandle(string subject, params object[] bodyParameters)
        {
            message.Id = Guid.NewGuid();
            message.Subject = subject;
            if (bodyParameters != null)
            {
                message.Body = SetMessageBody(bodyParameters);
                message.MessageType = BaseMessageType.ListOfParameters;
            }
        }

        public BaseMessage GetMessage
        {
            get { return message; }
        }
        private byte[] SetMessageBody(object[] bodyObject)
        {
            if (bodyObject == null)
                return null;

            byte[] bodyArray = null;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, bodyObject);

                bodyArray = ms.ToArray();
            }

            if (bodyArray.Length > bodyLengthToCompress)
            {
                message.CompressionAlgorithm = CompressionAlgorithm.GZipStream;
                bodyArray = ZipHelper.CompressMessageUsingGZipStream(bodyArray);
            }
            else
            {
                message.CompressionAlgorithm = CompressionAlgorithm.None;
            }
            return bodyArray;
        }
    }
}