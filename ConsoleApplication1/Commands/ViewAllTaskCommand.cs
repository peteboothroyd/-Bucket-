using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewAllTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ViewAllTaskCommand(ToDoApplication toDoApplication)
            : base("View All")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            mApplication.Hub.Invoke("ClientViewAllTasksRequest").Wait();
        }
    }
}
