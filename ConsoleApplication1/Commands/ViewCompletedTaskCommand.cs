using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewCompletedTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ViewCompletedTaskCommand(ToDoApplication toDoApplication)
            : base("View Completed")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            mApplication.Hub.Invoke("ClientViewCompletedOrUnfinishedTasksRequest", true).Wait();
        }
    }
}
