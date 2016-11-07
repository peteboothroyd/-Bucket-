using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands 
{
    class UpdateDescriptionTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private Task mTask;

        public UpdateDescriptionTaskCommand(MainWindow mainwindow, Task task)
            : base("Update Description")
        {
            mMainWindow = mainwindow;
            mTask = task;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientUpdateTaskDescriptionRequest", mTask.ID, mTask.Description).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
