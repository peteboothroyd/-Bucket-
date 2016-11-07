using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ToDoTrainingGit.Commands
{
    class ClearTaskCommand : CommandBase
    {
        private MainWindow mMainWindow;

        public ClearTaskCommand(MainWindow mainwindow)
            : base("Clear")
        {
            mMainWindow = mainwindow;
        }

        public override void Execute()
        {
            if (!Check.ListCountGreaterZeroCheck(mMainWindow))
                return;

            string messageBoxText = "Are you sure you want to clear all tasks?";
            string caption = "Clear Tasks Warning";
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            
            MessageBoxResult rsltMessageBox = MessageBox.Show(messageBoxText,caption,btnMessageBox,icnMessageBox, MessageBoxResult.No);
            switch(rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    return;
            }

            try
            {
                mMainWindow.Hub.Invoke("ClientClearTasksRequest").Wait();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            } 
        }
    }
}
