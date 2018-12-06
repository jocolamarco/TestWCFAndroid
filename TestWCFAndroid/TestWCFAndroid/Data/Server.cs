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
        private DmImgrWebInterface client;


        public Server()
        {
            url  = _DEFAULT_URL_  ;
            port = _DEFAULT_PORT_ ;

            new Server(url,port);
        }

        public Server(string url, string port)
        {
            if(url==null) url = _DEFAULT_URL_;
            if(port==null) port = _DEFAULT_PORT_;

            this.url = url;
            this.port = port;
            client = new DmImgrWebInterface(url, port);
        }


        public PingResponse PingServer()
        {
            return PingServer(this.url,this.port);
        }

        public PingResponse PingServer(string url, string port)
        {
            PingRequest pingRequest = new PingRequest();
            PingResponse pingResponse = new PingResponse();            

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

        public string GetStores(string username)
        {
            GetUserStoresRequest getUserStoresRequest = new GetUserStoresRequest();        
            GetUserStoresResponse getUserStoresResponse = new GetUserStoresResponse();
            GetUserStoresResponseStore[] getUserStoresResponseStores;

            bool success;
            int length;
            string result = "Stores: ";

            try
            {
               getUserStoresRequest.username = username;
               getUserStoresResponse = client.GetUserStores(getUserStoresRequest);
               getUserStoresResponseStores = new GetUserStoresResponseStore[getUserStoresResponse.Stores.Length];
               success = getUserStoresResponse.Succeeded;
            
               if(success)
                {
                    length = getUserStoresResponseStores.Length;
                    Debug.WriteLine("Total Store Count: "+length);
                    for (int i = 0; i < length; i++)
                    {
                        Debug.WriteLine("Store: "+getUserStoresResponse.Stores[i].name);
                        result += getUserStoresResponse.Stores[i].name;
                        if (i < (length - 1))
                        {
                            result += ", ";
                        }
                    }                    
                }

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "Unable to retrieve Store List";
            }                       
        }
    }
}
