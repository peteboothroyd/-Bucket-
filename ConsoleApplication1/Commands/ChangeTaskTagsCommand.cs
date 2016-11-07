using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ChangeTaskTagsCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ChangeTaskTagsCommand(ToDoApplication toDoApplication)
            : base("Change tags")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mApplication))
                return;

            var taskName = Check.AskQuestionServer(mApplication, "Please enter name:", "ClientPresenceCheckRequest");
            if (Check.ExitCheck(taskName))
                return;

            string taskTags = Check.AskQuestion("Enter tags:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskTags))
                return;

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();

                Task taskToChange = mApplication.Todos.FirstOrDefault<Task>(task => task.Name == taskName);

                mApplication.Hub.Invoke("ClientChangeTaskTagsRequest", taskToChange.ID, taskTags).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }   
        }
    }
}
