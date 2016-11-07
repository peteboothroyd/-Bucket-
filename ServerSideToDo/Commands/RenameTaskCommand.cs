using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class RenameTaskCommand : CommandBase
    {
        private User mUser;
        private int mID;
        private string mNewName;

        public RenameTaskCommand(User user, int ID, string NewName)
            : base("Rename")
        {
            mUser = user;
            mID = ID;
            mNewName = NewName;
        }

        public override void Execute()
        {
            ToDoTrainingGit.Task taskItem = mUser.Tasks.FirstOrDefault<ToDoTrainingGit.Task>(task => task.ID == mID);
            
            if (taskItem != null)
            {
                taskItem.Name = mNewName;
            }
            else
            {
                Console.WriteLine("Could not find task to rename.");
            }
        }
    }
}
