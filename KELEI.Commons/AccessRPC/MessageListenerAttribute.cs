using System;

namespace KELEI.Commons.AccessRPC
{
    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MessageListenerAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconusingattributeclasses.asp
        private string messageSubject;
        private int executionPriority;

        // This is a positional argument.
        public MessageListenerAttribute(string messageSubject)
        {
            this.messageSubject = messageSubject;
            OutsideSessionScope = false;
        }

        /// <summary>
        /// Subject of message that method will handle.
        /// </summary>
        public string MessageSubject
        {
            get { return messageSubject; }
            set { messageSubject = value; }
        }

        /// <summary>
        /// Value, which determines order of methods call execution.
        /// </summary>
        public int ExecutionPriority
        {
            get { return executionPriority; }
            set { executionPriority = value; }
        }

        public bool OutsideSessionScope { get; set; }
    }
}