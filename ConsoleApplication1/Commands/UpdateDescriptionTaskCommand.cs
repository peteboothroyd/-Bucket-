using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands 
{
    class UpdateDescriptionTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public UpdateDescriptionTaskCommand(ToDoApplication toDoApplication)
            : base("Update Description")
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

            var taskDescription = Check.AskQuestion("Please enter new description:", Check.EmptyStringCheck);
            if (Check.ExitCheck(taskDescription))
                return;

            try
            {
                mApplication.Hub.Invoke("ClientRequestTaskList").Wait();

                Task taskToChange = mApplication.Todos.FirstOrDefault<Task>(task => task.Name == taskName);

                mApplication.Hub.Invoke("ClientUpdateTaskDescriptionRequest", taskToChange.ID, taskDescription).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
