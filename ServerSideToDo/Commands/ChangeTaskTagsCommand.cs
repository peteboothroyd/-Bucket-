using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ChangeTaskTagsCommand : CommandBase
    {
        private User mUser;
        private int mID;
        private string mNewTags;

        public ChangeTaskTagsCommand(User user, int ID, string NewTags)
            : base("Change tags")
        {
            mUser = user;
            mID = ID;
            mNewTags = NewTags;
        }

        public override void Execute()
        {
            ToDoTrainingGit.Task taskItem = mUser.Tasks.FirstOrDefault<ToDoTrainingGit.Task>(task => task.ID == mID);
            
            if (taskItem != null)
            {
                taskItem.Tags = mNewTags;
                taskItem.TagsList.Clear();

                string[] mTags = mNewTags.Split(ToDoHub.sDelemiters);

                foreach (string tag in mTags)
                {
                    taskItem.TagsList.Add(tag);
                }
            }
            else
            {
                Console.WriteLine("Could not find task to change tags.");
            }
        }
    }
}
