using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class MarkTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private Task mTask;

        public MarkTaskCommand(MainWindow mainwindow, Task task)
            : base("Mark")
        {
            mMainWindow = mainwindow;
            mTask = task;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientMarkTaskRequest", mTask.ID, mTask.Marked).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
