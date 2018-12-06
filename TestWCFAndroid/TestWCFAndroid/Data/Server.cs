using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TestWCFAndroid.Data
{
    class Server
    {
        private string _DEFAULT_URL_ = "192.168.222.207";
        private string _DEFAULT_PORT_ = "8080";

        private string url;
        private string port;


        public Server()
        {
            url  = _DEFAULT_URL_  ;
            port = _DEFAULT_PORT_ ;

            new Server(url,port);
        }

        public Server(string url, string port)
        {
            this.url = url;
            this.port = port;
        }


        public PingResponse PingServer()
        {
            return PingServer(this.url,this.port);
        }

        public PingResponse PingServer(string url, string port)
        {
            DmImgrWebInterface client = new DmImgrWebInterface(url, port);
            PingResponse pingResponse = new PingResponse();
            PingRequest pingRequest = new PingRequest();
            try
            {
                pingResponse = client.Ping(pingRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return pingResponse;           
        }        
    }
}
