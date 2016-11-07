using System;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTrainingGit
{
    public class User
    {
        private string mPassword;
        public List<ToDoTrainingGit.Task> Tasks { get; set; }
        public int ToDoIDCounter { get; set; }
        public string Username { get; set; }
        public List<string> ConnectionIDs { get; set; }

        public bool checkPassword(string password)
        {
            return mPassword.Equals(password);
        }

        public User(string username, string password)
        {
            Username = username;
            mPassword = password;
            ConnectionIDs = new List<string>();
            Tasks = new List<Task>();
        }

        public User(string username, string connectionId, string password)
        {
            Username = username;
            mPassword = password;
            ConnectionIDs = new List<string>();
            ConnectionIDs.Add(connectionId);
            Tasks = new List<Task>();
        }
    }
}
