using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit
{
    class StartConnection
    {
        private readonly string mUrl;
        private readonly string mHubProxyName;
        private readonly ToDoApplication mApplication;

        public StartConnection(string Url, string HubProxyName, ToDoApplication Application)
        {
            mUrl = Url;
            mHubProxyName = HubProxyName;
            mApplication = Application;
        }

        public void Execute()
        {
            var hubconnection = new HubConnection(mUrl);
            mApplication.HubConnection = hubconnection;

            var mHub = hubconnection.CreateHubProxy(mHubProxyName);
            mApplication.Hub = mHub;
            mHub.On<List<Task>>("ReceiveUpdatedTasks", (x) =>
            {
                if (x != null)
                {
                    mApplication.Todos = x;

                }
            }
);
            mHub.On<string>("SendErrorMessage", (x) =>
            {
                if (x != null)
                {
                    Console.WriteLine(x);

                }
            }
);


            try
            {
                hubconnection.Start().Wait();
                Console.WriteLine(hubconnection.State);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
