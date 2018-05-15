using System;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    abstract class RestProvider : Provider
    {
        private RestWrapper[] _restProvider;
        private Action<string> _message;

        protected void Init(string[] url, string providerName)
        {
            _message = OnMessage;
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
                p.OnMessage = _message;
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
