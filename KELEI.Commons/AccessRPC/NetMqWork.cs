using NetMQ.Sockets;
using NetMQ;
using System.Threading.Tasks;
using System.Threading;

namespace KELEI.Commons.AccessRPC
{
    public class NetMqWork
    {
        PullSocket receiver = null;
        PushSocket sender = null;
        public NetMqWork(string pullIP, string pushIP)
        {
            receiver = new PullSocket($">tcp://{pullIP}");
            sender = new PushSocket($">tcp://{pushIP}");
            Read();
        }

        private void Read()
        {
            while(true)
            {
                byte[] workload = receiver.ReceiveFrameBytes();
                Task.Run(() => {
                    var ps = ProtoSerialize.Deserialize<BaseMessage>(workload);
                    var result = ServiceListenerObject.ExceMethod(ps);
                    var message = new ReceiveMessage()
                    {
                        Id = ps.Id,
                        Subject=ps.Subject,
                        Body = result
                    };
                    var byteMessage = ProtoSerialize.Serialize<ReceiveMessage>(message);
                    sender.SendReady += (s, a) =>
                    {
                        a.Socket.SendFrame(byteMessage);
                    };
                });
                Thread.Sleep(10);
            }
        }
    }
}
