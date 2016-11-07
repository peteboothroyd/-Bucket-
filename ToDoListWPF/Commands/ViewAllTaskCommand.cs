using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewAllTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public ViewAllTaskCommand(MainWindow mainwindow)
            : base("View All")
        {
            mMainWindow = mainwindow;
        }

        public override void Execute()
        {
            mMainWindow.Hub.Invoke("ClientViewAllTasksRequest").Wait();
        }
    }
}
