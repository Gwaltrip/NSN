using System;
using Nancy.Hosting.Self;
using NSN.Core.Modules;

namespace NSN.Core
{
    public interface IRunner
    {
        void Start();
        void Start(string url);
        void Stop();
    }
    
    public class Runner: IRunner
    {
        private string _url;
        private NancyHost _host;
        public Runner(object coreObject, string url)
        {
            _url = url;
            CoreModule.CoreObject = coreObject;
        }
        public Runner(object coreObject)
        {
            CoreModule.CoreObject = coreObject;
        }

        public void Start()
        {
            _host = new NancyHost(new Uri(_url));
            _host.Start();
        }
        public void Start(string url)
        {
            this._url = url;
            _host = new NancyHost(new Uri(url));
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
        }
    }
}
