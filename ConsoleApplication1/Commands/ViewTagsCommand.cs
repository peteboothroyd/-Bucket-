using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewTagsCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ViewTagsCommand(ToDoApplication toDoApplication)
            : base("View Tags")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            string taskTags = Check.AskQuestion("Enter tags:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskTags))
                return;

            mApplication.Hub.Invoke("ClientViewTagsRequest", taskTags).Wait();
        }
    }
}
