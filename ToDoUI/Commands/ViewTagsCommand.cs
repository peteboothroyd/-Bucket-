using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class ViewTagsCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private string mTagName;

        public ViewTagsCommand(MainWindow MainWindow, string TagName)
            : base("View Tags")
        {
            mMainWindow = MainWindow;
            mTagName = TagName;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            mMainWindow.ViewFilter = task =>
            {
                if (!String.IsNullOrWhiteSpace(task.Tags))
                {
                    if (task.Tags.Contains(mTagName))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }

            };

            foreach (Task task in todos)
            {
                task.IsViewable = false;
            }

            var tempTodos = todos.Where(mMainWindow.ViewFilter);
            foreach (Task task in tempTodos)
            {
                task.IsViewable = true;
            }
        }
    }
}
