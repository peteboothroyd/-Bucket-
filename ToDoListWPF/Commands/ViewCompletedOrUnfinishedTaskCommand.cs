using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewCompletedOrUnfinishedTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private bool mStatus;

        public ViewCompletedOrUnfinishedTaskCommand(MainWindow mainwindow, bool status)
            : base("View Completed")
        {
            mMainWindow = mainwindow;
            mStatus = status;
        }

        public override void Execute()
        {
            mMainWindow.Hub.Invoke("ClientViewCompletedOrUnfinishedTasksRequest", mStatus).Wait();
        }
    }
}
