using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class MarkTaskCommand : CommandBase
    {
        private User mUser;
        private int mRandomID;
        private bool mIsChecked;

        public MarkTaskCommand(User user, int RandomID, bool IsChecked)
            : base("Mark")
        {
            mUser = user;
            mRandomID = RandomID;
            mIsChecked = IsChecked;
        }

        public override void Execute()
        {
            ToDoTrainingGit.Task taskItem = mUser.Tasks.FirstOrDefault<ToDoTrainingGit.Task>(task => task.ID == mRandomID);

            if (taskItem != null)
            {
                taskItem.Marked = mIsChecked;
            }
            else
            {
                Console.WriteLine("Could not find task to mark.");
            }
        }
    }
}
