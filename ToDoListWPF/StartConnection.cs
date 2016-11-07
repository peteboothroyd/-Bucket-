using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ToDoTrainingGit
{
    class StartConnection
    {
        private readonly string mUrl;
        private readonly string mHubProxyName;
        private readonly MainWindow mMainWindow;

        public StartConnection(string Url, string HubProxyName, MainWindow MainWindow)
        {
            mUrl = Url;
            mHubProxyName = HubProxyName;
            mMainWindow = MainWindow;
        }

        public void Execute()
        {
            var hubconnection = new HubConnection(mUrl);
            mMainWindow.HubConnection = hubconnection;
            var mHub = hubconnection.CreateHubProxy(mHubProxyName);
            mMainWindow.Hub = mHub;

            mHub.On<List<Task>>("ReceiveUpdatedTasks", (x) =>
            {
                if (x != null)
                {
                    var mTempTaskList = new ObservableCollection<ToDoTrainingGit.Task>(x);
                    mMainWindow.Tasks = mTempTaskList;
                    mMainWindow.Dispatcher.BeginInvoke(new Action(mMainWindow.UpdateViewSource), DispatcherPriority.Background);
                }
            }
);

            mHub.On<string>("SendErrorMessage", (x) =>
            {
                if (x != null)
                {
                    string messageBoxText = x;
                    string caption = "Error";
                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBox.Show(messageBoxText, caption, btnMessageBox, icnMessageBox);

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
