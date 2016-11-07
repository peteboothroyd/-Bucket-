using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class RefreshTasksCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public RefreshTasksCommand(MainWindow mainwindow)
            : base("Refresh")
        {
            mMainWindow = mainwindow;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientRequestTaskList").Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
