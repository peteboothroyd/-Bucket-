using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewUnfinishedTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ViewUnfinishedTaskCommand(ToDoApplication toDoApplication)
            : base("View Unfinished")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            mApplication.Hub.Invoke("ClientViewCompletedOrUnfinishedTasksRequest", false).Wait();
        }
    }
}
