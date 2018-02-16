using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KELEI.Commons.AccessRPC
{
    public class NetMqPull
    {
        PullSocket receiver = null;
        public NetMqPull(string ip)
        {
            receiver = new PullSocket($"@tcp://{ip}");
            Read();
        }

        private void Read()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    byte[] workload = receiver.ReceiveFrameBytes();
                    Task.Run(() =>
                    {
                        BaseMessage p = ProtoSerialize.Deserialize<BaseMessage>(workload);
                        NetMqResultHash.AddHash(p.Id.ToString() + "." + p.Subject, p.Body);
                    });
                    Thread.Sleep(10);
                }
            });
        }

        public void Dispose()
        {
            if (receiver != null)
            {
                receiver.Dispose();
                receiver = null;
            }
        }

        ~NetMqPull()
        {
            if (receiver != null)
            {
                receiver.Dispose();
                receiver = null;
            }
        }

    }
}
