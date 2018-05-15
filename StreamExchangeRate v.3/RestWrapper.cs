using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StreamExchangeRate_v._3
{
    public class RestWrapper : IDisposable
    {
        private readonly string _url;
        private HttpClient _client;
        private Timer _timer;
        private CancellationTokenSource _cancelation;
        private string _providerName;

        public Action<string> OnMessage;
        public RestWrapper(string url, string providerName)
        {
            _url = url;
            this._providerName = providerName;
        }

        public Task Start()
        {
            Console.WriteLine(L("Starting.."));
            _client = new HttpClient();
            _cancelation = new CancellationTokenSource();

            int minute = 1000;
            _timer = new Timer(async async => await UpdateData(), null, minute, minute);
            return Listen();
        }

        public void Stop()
        {
            _cancelation?.Cancel();
            _client.Dispose();
            _timer.Dispose();
        }

        public void Dispose()
        {
            _cancelation?.Dispose();
            _client.Dispose();
            _timer.Dispose();
        }
        private async Task Listen()
        {
            string json = string.Empty;
            try
            {
                var response = await _client.GetAsync(_url, _cancelation.Token);
                json = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error");
                }
                OnMessage(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(L("Exception.." + ex.Message));
            }
        }

        private async Task UpdateData()
        {
            await Listen();
        }

        private string L(string msg)
        {
            return $"[{_providerName} REST API] {msg}";
        }
    }
}