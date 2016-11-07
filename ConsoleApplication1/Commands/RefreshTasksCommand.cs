using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class RefreshTasksCommand : CommandBase
    {
        private ToDoApplication mApplication;

        public RefreshTasksCommand(ToDoApplication Application)
            : base("Refresh")
        {
            mApplication = Application;
        }

        public override void Execute()
        {
            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
