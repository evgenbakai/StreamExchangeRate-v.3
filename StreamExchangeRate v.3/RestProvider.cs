using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    abstract class RestProvider : Provider
    {
        private RestWrapper[] _restProvider;
        private Action<string> Message;

        public void Init(string[] url, string providerName)
        {
            Message = OnMessage;
            _restProvider = new RestWrapper[url.Length];

            for (int i = 0; i < url.Length; i++)
            {
                _restProvider[i] = new RestWrapper(url[i], providerName);
            }
        }

        public override async Task Start()
        {
            foreach (var p in _restProvider)
            {
                p.OnMessage = Message;
                await p.Start();
            }
        }

        public override void Stop()
        {
            foreach (var p in _restProvider)
                p.Stop();
        }

        public override void Dispose()
        {
            foreach (var p in _restProvider)
                p.Dispose();
        }
    }
}
