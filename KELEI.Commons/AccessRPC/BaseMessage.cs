using ProtoBuf;
using System;


namespace KELEI.Commons.AccessRPC
{
    [Serializable]
    [ProtoContract]
    public class BaseMessage
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        [ProtoMember(2)]
        public string Subject { get; set; }

        [ProtoMember(3)]
        public byte[] Body { get; set; }
        [ProtoMember(4)]
        public BaseMessageType MessageType { get; set; }
        [ProtoMember(5)]
        public CompressionAlgorithm CompressionAlgorithm { get; set; }
    }

    public enum BaseMessageType
    {
        Unknown = 0,            //created by calling a setter on body property
        ListOfParameters = 1,   //message body contains list of parameters
        NamedParameters = 2     //message body contains list of named parameters
    }

    public enum CompressionAlgorithm
    {
        /// <summary>
        /// Message body is not compressed.
        /// </summary>
        None = 0,
        /// <summary>
        /// Message body compressed using GZip stream method.
        /// </summary>
        GZipStream = 1
    }
}