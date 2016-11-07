using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewTagsCommand : CommandBase
    {
        private User mUser;
        private string mTagName;

        public ViewTagsCommand(User user, string TagName)
            : base("View Tags")
        {
            mUser = user;
            mTagName = TagName;
        }

        public override void Execute()
        {
            foreach (Task task in mUser.Tasks)
            {
                task.IsViewable = false;
            }

            var tempTodos = mUser.Tasks.Where(task =>
            {
                if (!String.IsNullOrWhiteSpace(task.Tags))
                {
                    string[] mTags = mTagName.Split(ToDoHub.sDelemiters);

                    foreach (string filtertag in mTags)
                    {
                        foreach(string tasktag in task.TagsList)
                        {
                            if (tasktag.Contains(filtertag))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }

            });

            foreach(Task task in tempTodos)
            {
                task.IsViewable = true;
            }
        }
    }
}
