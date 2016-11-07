using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class RenameTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;
        private Task mRenamedTask;

        public RenameTaskCommand(MainWindow mainwindow, Task renamedtask)
            : base("Rename")
        {
            mMainWindow = mainwindow;
            mRenamedTask = renamedtask;
        }

        public override void Execute()
        {
            try
            {
                mMainWindow.Hub.Invoke("ClientRenameTaskRequest", mRenamedTask.ID, mRenamedTask.Name).Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }           
        }
    }
}
