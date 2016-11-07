using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ToDoTrainingGit
{
    class Check
    {
        public static bool XMLNodeValidCheck(XElement node)
        {
            if (node.Attribute("name") != null && node.Attribute("description")
                != null && node.Attribute("marked") != null && node.Attribute("tags") 
                != null && node.Attribute("sortorder") != null && node.Attribute("isviewable") != null)
            {
                return true;
            }
            return false;
        }

        public static bool LocalListCountGreaterZeroCheck(MainWindow MainWindow)
        {
            if (MainWindow.Tasks.Count > 0)
                return true;
            else return false;
        }

        public static bool ListCountGreaterZeroCheck(MainWindow MainWindow)
        {
            try
            {
                bool ListCountGreaterZeroResult = false;
                var mConnection = MainWindow.HubConnection;
                var mHub = MainWindow.Hub;

                while (mConnection.State != ConnectionState.Connected)
                {
                    mConnection.Start().Wait();
                }

                ListCountGreaterZeroResult = mHub.Invoke<bool>("ClientListCountGreaterThanZeroCheckRequest").Result;

                return ListCountGreaterZeroResult;
            }
            catch (Exception e)
            {
                string mMessageBoxText = e.Message;
                string mCaption = "Exception";
                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
                MessageBox.Show(mMessageBoxText, mCaption, btnMessageBox, icnMessageBox);
                return false;
            }
        }

        public static bool BoolCheck(string tempName)
        {
            if (String.IsNullOrWhiteSpace(tempName))
            {
                return false;
            }

            if (tempName.ToLower().Trim() != "true" && tempName.ToLower().Trim() != "false")
            {
                Console.WriteLine("Marked is not true or false.");
                return false;
            }

            return true;
        }
    }
}
