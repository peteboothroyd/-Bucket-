using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoTrainingGit.Checks;
using ToDoTrainingGit.Commands;

namespace ToDoTrainingGit
{
    [HubName("ToDoHub")]
    public class ToDoHub : Hub
    {
        public static char[] sDelemiters = { ',', '.', '\n', '\t' };
        public static ConcurrentDictionary<string, User> UserDictionary { get; set; }
        //Arbitrary comment to sync with GitHub
        public int Login(string username, string password)
        {
            Console.WriteLine("Received request to: Login with username: " + username + " password: " + password);

            string connectionID = Context.ConnectionId;

            User mUser;
            UserDictionary.TryGetValue(username, out mUser);
            if (mUser == null)
            {
                return 1;
            }
            else
            {       
                if (mUser.checkPassword(password))
                {
                    lock (mUser.ConnectionIDs)
                    {
                        mUser.ConnectionIDs.Add(connectionID);
                    }

                    UserDictionary.TryAdd(username, mUser);

                    Clients.Caller.ReceiveUpdatedTasks(mUser.Tasks);

                    return 0;
                }
                else
                {
                    return 2;
                }                 
            }
        }

        public int CreateUser(string username, string password)
        {
            Console.WriteLine("Received request to: Create User with username: " + username);

            string connectionID = Context.ConnectionId;

            User mUser;
            UserDictionary.TryGetValue(username, out mUser);
            if(mUser != null)
            {
                return 1;
            }
            else
            {
                User newUser = new User(username, connectionID, password);

                lock (UserDictionary)
                {
                    UserDictionary.TryAdd(username, newUser);
                }
                return 0;
            }
        }

        public void ClientChangeTaskTagsRequest(int RandomID, string NewTags)
        {
            Console.WriteLine("Received request to: Change tags" + RandomID + "with tags: " + NewTags);
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            ChangeTaskTagsCommand mChangeTags = new ChangeTaskTagsCommand(mUser, RandomID, NewTags);
            mChangeTags.Execute();

            Clients.Clients(mUser.ConnectionIDs).UpdateTask(RandomID, NewTags, 4);
        }

        public void ClientClearTasksRequest()
        {
            Console.WriteLine("Received request to: Clear Tasks");
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            ClearTasksCommand mClearTasks = new ClearTasksCommand(mUser);
            mClearTasks.Execute();

            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientNewTaskRequest()
        {
            Console.WriteLine("\nReceived request to: New Task");
            User mUser = FindUser(Context.ConnectionId);

            NewTaskCommand mNewTask = new NewTaskCommand(mUser);
            mNewTask.Execute();
            int id = mNewTask.GetId();
            int sortorder = mNewTask.GetSortOrder();

            Clients.Clients(mUser.ConnectionIDs).UpdateTask(id, "||false||true|" + sortorder, 6);

        }

        public void ClientDeleteTaskRequest(int RandomID)
        {
            Console.WriteLine("Received request to: Delete Task" + RandomID);
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            DeleteTaskCommand mDeleteTask = new DeleteTaskCommand(mUser, RandomID);
            mDeleteTask.Execute();

            Clients.Clients(mUser.ConnectionIDs).UpdateTask(RandomID, "", 5);
        }

        public void ClientMarkTaskRequest(int RandomID, bool IsChecked)
        {
            Console.WriteLine("Received request to: Mark Task " + RandomID + "as:" + IsChecked.ToString());
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            MarkTaskCommand mMarkTask = new MarkTaskCommand(mUser, RandomID, IsChecked);
            mMarkTask.Execute();
            
            Clients.Clients(mUser.ConnectionIDs).UpdateTask(RandomID, IsChecked, 3);
        }

        public void ClientRenameTaskRequest(int RandomID, string NewName)
        {
            Console.WriteLine("Received request to: Rename Task " + RandomID + " to: " + NewName);
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            RenameTaskCommand mRenameTask = new RenameTaskCommand(mUser, RandomID, NewName);
            mRenameTask.Execute();
            
            Clients.Clients(mUser.ConnectionIDs).UpdateTask(RandomID, NewName, 1);
        }

        public void ClientUpdateTaskDescriptionRequest(int RandomID, string NewDescription)
        {
            Console.WriteLine("Received request to: Update Description " + RandomID + "to: " + NewDescription);
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            UpdateDescriptionTaskCommand mUpdateTaskDescription = new UpdateDescriptionTaskCommand(mUser, RandomID, NewDescription);
            mUpdateTaskDescription.Execute();
            
            Clients.Clients(mUser.ConnectionIDs).UpdateTask(RandomID, NewDescription, 2);
        }

        public void ClientMoveTaskRequest(int RandomID, bool Direction)
        {
            Console.WriteLine("Received request to: Move task " + RandomID + "Direction: " + Direction.ToString());
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            MoveTaskCommand mMoveTask = new MoveTaskCommand(mUser, RandomID, Direction);
            mMoveTask.Execute();

            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientCreateTaskRequest(string name, string description, string tags)
        {
            Console.WriteLine("\nReceived request to: Create task " + name);
            User mUser = FindUser(Context.ConnectionId);

            CreateTaskCommand mCreateTask = new CreateTaskCommand(mUser, name, description, tags);
            mCreateTask.Execute();

            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientPasteTaskRequest(string name, string description, bool marked, int sortorder, string tags, bool isviewable)
        {
            Console.WriteLine("\nReceived request to: Paste task " + name);
            User mUser = FindUser(Context.ConnectionId);

            PasteTaskCommand mPasteTask = new PasteTaskCommand(mUser, name, description, marked, sortorder, tags, isviewable);
            mPasteTask.Execute();

            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientImportTaskRequest(string name, string description, bool marked, int sortorder, string tags, bool isviewable)
        {
            Console.WriteLine("\nReceived request to: Import task " + name);

            User mUser = FindUser(Context.ConnectionId);

            ImportTaskCommand mImportTask = new ImportTaskCommand(mUser, name, description, marked, sortorder, tags, isviewable);
            mImportTask.Execute();
            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientViewAllTasksRequest()
        {
            Console.WriteLine("Received request to: View all task ");
            User mUser = FindUser(Context.ConnectionId);

            ViewAllTaskCommand mViewAllTasks = new ViewAllTaskCommand(mUser);
            mViewAllTasks.Execute();
            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientViewCompletedOrUnfinishedTasksRequest(bool CompletedOrUnfinished)
        {
            Console.WriteLine("Received request to: View completed/unfinished task " + CompletedOrUnfinished.ToString());
            User mUser = FindUser(Context.ConnectionId);

            ViewCompletedOrUnfinishedTasksCommand mViewCompletedOrUnfinishedTasks = new ViewCompletedOrUnfinishedTasksCommand(mUser, CompletedOrUnfinished);
            mViewCompletedOrUnfinishedTasks.Execute();
            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientViewTagsRequest(string Tags)
        {
            Console.WriteLine("Received request to: View tags " + Tags);
            User mUser = FindUser(Context.ConnectionId);

            ViewTagsCommand mViewTags = new ViewTagsCommand(mUser, Tags);
            mViewTags.Execute();
            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public void ClientRequestTaskList()
        {
            Console.WriteLine("Received request to: Refresh task list ");
            User mUser = FindUser(Context.ConnectionId);

            if (mUser.Tasks.Count == 0)
            {
                EmptyListBroadcast();
                return;
            }

            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        public bool ClientPresenceCheckRequest(string PossibleName)
        {
            Console.WriteLine("Received request to: Check presence of task ");
            User mUser = FindUser(Context.ConnectionId);

            PresenceCheck mPresenceCheck = new PresenceCheck(mUser, PossibleName);
            bool mPresenceCheckOutput = mPresenceCheck.Execute();
            Console.WriteLine("Result broadcast to caller." + mPresenceCheckOutput.ToString());
            return mPresenceCheckOutput;
        }

        public bool ClientListCountGreaterThanZeroCheckRequest()
        {
            Console.WriteLine("Received request to: Check collection count ");
            User mUser = FindUser(Context.ConnectionId);

            ListCountGreaterThanZeroCheck mListCountGreatherThanZeroCheck = new ListCountGreaterThanZeroCheck(mUser);
            bool mListCountGreatherThanZeroCheckOutput = mListCountGreatherThanZeroCheck.Execute();
            Console.WriteLine("Bool broadcast to caller " + mListCountGreatherThanZeroCheckOutput.ToString());
            return mListCountGreatherThanZeroCheckOutput;
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            Console.WriteLine("Client connected: " + Context.ConnectionId);
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);

            string connectionID = Context.ConnectionId;
            User mUser = UserDictionary?.FirstOrDefault<KeyValuePair<string, User>>(kvp => kvp.Value.ConnectionIDs.Contains(connectionID)).Value;

            if (mUser != null)
            {
                mUser.ConnectionIDs.Remove(connectionID);
                if (mUser.ConnectionIDs.Count == 0)
                {
                    UserDictionary.TryRemove(mUser.Username, out mUser);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public void EmptyListBroadcast()
        {
            User mUser = FindUser(Context.ConnectionId);
            Clients.Caller.SendErrorMessage("List empty.");
            Clients.Clients(mUser.ConnectionIDs).ReceiveUpdatedTasks(mUser.Tasks);
        }

        private User FindUser(string connectionId)
        {
            User foundUser = null;
            foundUser = UserDictionary.FirstOrDefault<KeyValuePair<string, User>>(user => user.Value.ConnectionIDs.Contains(connectionId)).Value;
            return foundUser;
        }

    }
}
