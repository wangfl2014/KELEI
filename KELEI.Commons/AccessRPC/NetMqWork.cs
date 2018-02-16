using NetMQ.Sockets;
using NetMQ;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace KELEI.Commons.AccessRPC
{
    public class NetMqWork
    {
        PullSocket receiver = null;
        PushSocket sender = null;
        public NetMqWork(string pullIP, string pushIP)
        {
            try
            {
                receiver = new PullSocket($">tcp://{pullIP}");
                sender = new PushSocket($">tcp://{pushIP}");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (receiver != null)
            {
                receiver.Close();
                receiver.Dispose();
                receiver = null;
            }

            if (sender != null)
            {
                sender.Close();
                sender.Dispose();
                sender = null;
            }
        }

        public void Read()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    byte[] workload = receiver.ReceiveFrameBytes();
                    Task.Run(() =>
                    {
                        var ps = ProtoSerialize.Deserialize<BaseMessage>(workload);
                        var result = ServiceListenerObject.ExceMethod(ps);
                        var message = new BaseMessage()
                        {
                            Id = ps.Id,
                            Subject = ps.Subject,
                            Body= (byte[])result,
                            MessageType = BaseMessageType.ListOfParameters
                        };
                        var byteMessage = ProtoSerialize.Serialize<BaseMessage>(message);
                        sender.SendFrame(byteMessage);
                    });

                    Thread.Sleep(10);
                }
            });
        }

        ~NetMqWork()
        {
            if (receiver != null)
            {
                receiver.Close();
                receiver.Dispose();
                receiver = null;
            }

            if (sender != null)
            {
                sender.Close();
                sender.Dispose();
                sender = null;
            }
        }
    }
}
