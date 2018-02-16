using NetMQ;
using NetMQ.Sockets;
using System.Threading.Tasks;

namespace KELEI.Commons.AccessRPC
{
    public class NetMqPush
    {
        PushSocket sender =null;
        public NetMqPush(string ip)
        {
            sender= new PushSocket($"@tcp://{ip}");
        }

        public T Send<T>(BaseMessage msg)
        {
            byte[] obj = ProtoSerialize.Serialize<BaseMessage>(msg);
            object result = null;
            sender.SendFrame(obj);

            while (true)
            {
                var value = NetMqResultHash.GetHash(msg.Id.ToString() + "." + msg.Subject);
                if (value.Item1)
                {
                    result = ProtoSerialize.Deserialize<T>((byte[])value.Item2);
                    //删除结果
                    Task.Run(() =>
                    {
                        NetMqResultHash.DeleteHash(msg.Id.ToString() + "." + msg.Subject);
                    });
                    break;
                }
            }

            return (T)result;
        }

        public void Dispose()
        {
            if(sender!=null)
            {
                sender.Dispose();
                sender = null;
            }
        }

        ~ NetMqPush()
        {
            if (sender != null)
            {
                sender.Dispose();
                sender = null;
            }
        }
    }
}
