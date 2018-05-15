using System;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    abstract class WebSocketProvider : Provider
    {
        private WebSocketWrapper _restProvider;
        private Action<string> _message;

        protected void Init(Uri url, string providerName)
        {
            _message = OnMessage;
            _restProvider = new WebSocketWrapper(url, providerName);
        }

        public override async Task Start()
        {
            _restProvider.OnMessage = _message;
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
