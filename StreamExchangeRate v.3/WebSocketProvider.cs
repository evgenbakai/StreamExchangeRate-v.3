using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    abstract class WebSocketProvider : Provider
    {
        private WebSocketWrapper _restProvider;
        private Action<string> Message;

        public void Init(Uri url, string providerName)
        {
            Message = OnMessage;
            _restProvider = new WebSocketWrapper(url, providerName);

        }

        public override async Task Start()
        {
            _restProvider.OnMessage = Message;
            await _restProvider.Start();
        }

        public override void Stop()
        {
            _restProvider.Stop();
        }

        public override void Dispose()
        {
            _restProvider.Dispose();
        }
    }
}
