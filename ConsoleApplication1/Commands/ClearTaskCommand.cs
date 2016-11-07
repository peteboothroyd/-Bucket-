using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ClearTaskCommand : CommandBase
    {
        private readonly ToDoApplication mApplication;

        public ClearTaskCommand(ToDoApplication toDoApplication)
            : base("Clear")
        {
            mApplication = toDoApplication;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mApplication))
                return;
                
            var yesnoanswer = Check.AskQuestion("Are you sure you want to clear, type yes to confirm or no to abort:",
                                possibleName => Check.EitherOrQuestionCheck(possibleName, ToDoApplication.CONFIRMATION_AFFIRMATION,
                                    ToDoApplication.CONFIRMATION_NEGATIVE));

            if (Check.ExitCheck(yesnoanswer))
                return;

            if (yesnoanswer == ToDoApplication.CONFIRMATION_AFFIRMATION)
            {
                try
                {
                    mApplication.Hub.Invoke("ClientClearTasksRequest").Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }  
                
            }
        }
    }
}
