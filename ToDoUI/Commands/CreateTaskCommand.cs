using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoTrainingGit.Commands
{
    class CreateTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public CreateTaskCommand(MainWindow MainWindow)
            : base("Create")
        {
            mMainWindow = MainWindow;
        }

        public override void Execute()
        {
            var todos = mMainWindow.Tasks;
            todos.Add(new Task());
        }
    }
}
