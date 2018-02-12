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
            while (true)
            {
                byte[] workload = receiver.ReceiveFrameBytes();
                Task.Run(() => {
                    ReceiveMessage p = ProtoSerialize.Deserialize<ReceiveMessage>(workload);
                    NetMqResultHash.AddHash(p.Id.ToString() + "." + p.Subject, p.Body);
                });
                Thread.Sleep(10);
            }
        }

    }
}
