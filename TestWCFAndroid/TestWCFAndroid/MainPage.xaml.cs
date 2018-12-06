using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using TestWCFAndroid.Data;

namespace TestWCFAndroid
{
    public partial class MainPage : ContentPage
    {
        Server server;

        public MainPage()
        {
            InitializeComponent();
        }
		
        private void PingServerButton_Clicked(object sender, EventArgs e)
        {            
			string url = URLInput.Text;
            string port = PortInput.Text;

            server = new Server(url,port);
            PingResponse pingResponse = new PingResponse();

            pingResponse = server.PingServer();
            Debug.WriteLine("" + pingResponse.Success);
            Debug.WriteLine("" + pingResponse.CurrentServerTimeGMT);

            if (pingResponse.Success == true)
            {
                CurrentTimestampResult.Text = "Server Local Time: " + pingResponse.CurrentServerTimeLocal;
                CurrentTimestampResult.TextColor = Color.Green;
            }
            else
            {
                CurrentTimestampResult.Text = "Unable to ping server";
                CurrentTimestampResult.TextColor = Color.Red;
            }
        }

        private void CheckStoresButton_Clicked(object sender, EventArgs e)
        {
            string storeResult;
            string username;

            //by default
            username = "admin";

            storeResult = server.GetStores(username);                        

            StoreResult.Text = storeResult;
        }
    }
}
