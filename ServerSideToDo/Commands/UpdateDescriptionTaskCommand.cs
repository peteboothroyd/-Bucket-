using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands 
{
    class UpdateDescriptionTaskCommand : CommandBase
    {
        private User mUser;
        private int mRandomID;
        private string mNewDescription;

        public UpdateDescriptionTaskCommand(User user, int RandomID, string NewDescription)
            : base("Update Description")
        {
            mUser = user;
            mRandomID = RandomID;
            mNewDescription = NewDescription;
        }

        public override void Execute()
        {
            ToDoTrainingGit.Task taskItem = mUser.Tasks.FirstOrDefault<ToDoTrainingGit.Task>(task => task.ID == mRandomID);
            
            if (taskItem != null) 
            {
                taskItem.Description = mNewDescription;
            }
            else
            {
                Console.WriteLine("Could not find task to change description.");
            }
        }
    }
}
