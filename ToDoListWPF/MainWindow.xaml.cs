using Microsoft.AspNet.SignalR.Client;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ToDoListWPF;
using ToDoTrainingGit.Commands;

namespace ToDoTrainingGit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task temporaryClipboard;
        private bool loggedIn;
        public bool LoggedIn { get { return loggedIn; } set { loggedIn = value; } }
        public ObservableCollection<Task> Tasks { get; set; }
        public CollectionViewSource ViewableTasks { get; set; }
        public IHubProxy Hub { get; set; }
        public HubConnection HubConnection { get; set; }

        string NAME_ATTRIBUTION, DESCRIPTION_ATTRIBUTION, MARKED_ATTRIBUTE, TAGS_ATTRIBUTE;

        public MainWindow()
        {
            string url = @"http://localhost:8080/";
            StartConnection startConnection = new StartConnection(url, "ToDoHub", this);
            startConnection.Execute();

            Tasks = new ObservableCollection<Task>();

            ViewableTasks = new CollectionViewSource();
            ViewableTasks.Source = Tasks;
            ViewableTasks.SortDescriptions.Add(new SortDescription("SortOrder", ListSortDirection.Ascending));
            ViewableTasks.Filter += (s, e) =>
                {
                    Task task = e.Item as Task;
                    e.Accepted = task.IsViewable;
                };

            InitializeComponent();  

            NAME_ATTRIBUTION = (String)Application.Current.Resources["NAME_ATTRIBUTION"];
            DESCRIPTION_ATTRIBUTION = (String)Application.Current.Resources["DESCRIPTION_ATTRIBUTION"];
            MARKED_ATTRIBUTE = (String)Application.Current.Resources["MARKED_ATTRIBUTE"];
            TAGS_ATTRIBUTE = (String)Application.Current.Resources["TAGS_ATTRIBUTE"];
            #region Command Bindings
            CommandBinding Close = new CommandBinding(ApplicationCommands.Close, ExitCommand_Executed, ExitCommand_CanExecute);
            CommandBinding Open = new CommandBinding(ApplicationCommands.Open, OpenCommand_Executed, OpenCommand_CanExecute);
            CommandBinding Save = new CommandBinding(ApplicationCommands.Save, SaveCommand_Executed, SaveCommand_CanExecute);
            CommandBinding Delete = new CommandBinding(ApplicationCommands.Delete, DeleteTaskCommand_Executed, DeleteTaskCommand_CanExecute);
            CommandBinding MoveDown = new CommandBinding(ComponentCommands.MoveDown, MoveDownCommand_Executed, MoveDownCommand_CanExecute);
            CommandBinding MoveUp = new CommandBinding(ComponentCommands.MoveUp, MoveUpCommand_Executed, MoveUpCommand_CanExecute);
            CommandBinding New = new CommandBinding(ApplicationCommands.New, NewTaskCommand_Executed, NewTaskCommand_CanExecute);
            CommandBinding Refresh = new CommandBinding(NavigationCommands.Refresh, RefreshCommand_Executed, RefreshCommand_CanExecute);
            CommandBinding Clear_List = new CommandBinding(Command.ClearList, ClearListCommand_Executed, ClearListCommand_CanExecute);
            CommandBinding View_Filtered = new CommandBinding(Command.ViewFiltered, ViewFilteredCommand_Executed, ViewFilteredCommand_CanExecute);
            CommandBinding View_Completed = new CommandBinding(Command.ViewCompleted, ViewCompletedCommand_Executed, ViewCompletedCommand_CanExecute);
            CommandBinding View_Unfinished = new CommandBinding(Command.ViewUnfinished, ViewUnfinishedCommand_Executed, ViewUnfinishedCommand_CanExecute);
            CommandBinding View_All = new CommandBinding(Command.ViewAll, ViewAllCommand_Executed, ViewAllCommand_CanExecute);
            CommandBinding Cut = new CommandBinding(ApplicationCommands.Cut, CutCommand_Executed, CutCommand_CanExecute);
            CommandBinding Copy = new CommandBinding(ApplicationCommands.Copy, CopyCommand_Executed, CopyCommand_CanExecute);
            CommandBinding Paste = new CommandBinding(ApplicationCommands.Paste, PasteCommand_Executed, PasteCommand_CanExecute);
            CommandBinding Connect = new CommandBinding(Command.Connect, ConnectCommand_Executed, ConnectCommand_CanExecute);
            
            this.CommandBindings.Add(Close);
            this.CommandBindings.Add(Open);
            this.CommandBindings.Add(Save);
            this.CommandBindings.Add(Delete);
            this.CommandBindings.Add(MoveDown);
            this.CommandBindings.Add(MoveUp);
            this.CommandBindings.Add(New);
            this.CommandBindings.Add(Refresh);
            this.CommandBindings.Add(Clear_List);
            this.CommandBindings.Add(View_Filtered);
            this.CommandBindings.Add(View_Completed);
            this.CommandBindings.Add(View_Unfinished);
            this.CommandBindings.Add(View_All);
            this.CommandBindings.Add(Cut);
            this.CommandBindings.Add(Copy);
            this.CommandBindings.Add(Paste);
            this.CommandBindings.Add(Connect);
            #endregion
        }
        #region Exit
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { 
            e.CanExecute = true;
        }
        public void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
                return;
            else
                Application.Current.Shutdown();
        }
        #endregion

        #region OpenCommand
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            {
                e.CanExecute = !Check.LocalListCountGreaterZeroCheck(this);
            }
        public void OpenCommand_Executed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog mOpenFileDialog = new OpenFileDialog();
            mOpenFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            if (mOpenFileDialog.ShowDialog() == true)
            {
                string path = mOpenFileDialog.FileName;

                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        OpenFileCommand OpenFileCommand = new OpenFileCommand(this, path);
                        OpenFileCommand.Execute();
                        Hub.Invoke("ClientRequestTaskList");
                    }
                    catch (AggregateException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                });
                //MessageBox.Show("Open Completed!", "Open", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
        }
        #endregion

        #region Save Command
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Check.LocalListCountGreaterZeroCheck(this);
        }
        public void SaveCommand_Executed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog mSaveFileDialog = new SaveFileDialog();
            mSaveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            if (mSaveFileDialog.ShowDialog() == true)
            {
                string path = mSaveFileDialog.FileName;
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        SaveFileCommand SaveFileCommand = new SaveFileCommand(this, path);
                        SaveFileCommand.Execute();
                    }
                    catch (AggregateException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }).Wait();
                MessageBox.Show("Save Completed!", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Clear Command
        private void ClearListCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Check.LocalListCountGreaterZeroCheck(this);
        }
        private void ClearListCommand_Executed(object sender, RoutedEventArgs e)
        {
            ClearTaskCommand ClearTasks = new ClearTaskCommand(this);
            ClearTasks.Execute();
        }
        #endregion

        #region Delete Command
        private void DeleteTaskCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void DeleteTaskCommand_Executed(object sender, RoutedEventArgs e)
        {
            DeleteTaskCommand DeleteClick = new DeleteTaskCommand(this);
            DeleteClick.Execute();

        }
        #endregion

        #region Refresh Command
        private void RefreshCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void RefreshCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    RefreshTasksCommand RefreshTasks = new RefreshTasksCommand(this);
                    RefreshTasks.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion
        
        #region Move Down Command
        private void MoveDownCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else if (TaskListBox.SelectedIndex == Tasks.Count - 1)
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void MoveDownCommand_Executed(object sender, RoutedEventArgs e)
        {
            MoveTaskCommand DownClick = new MoveTaskCommand(this, false);
            DownClick.Execute();
        }
        #endregion
        
        #region Move Up Command
        private void MoveUpCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else if(TaskListBox.SelectedIndex == 0)
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void MoveUpCommand_Executed(object sender, RoutedEventArgs e)
        {
            MoveTaskCommand UpClick = new MoveTaskCommand(this, true);
            UpClick.Execute();
        }
        #endregion
        
        #region New Task Command
        private void NewTaskCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NewTaskCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    NewTaskCommand CreateClick = new NewTaskCommand(this);
                    CreateClick.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion
        
        #region View Filtered Command
        private void ViewFilteredCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string userInput = TagFilterInput.Text;
            if (String.IsNullOrWhiteSpace(userInput))
                e.CanExecute = false;
            else if (!Check.LocalListCountGreaterZeroCheck(this))
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void ViewFilteredCommand_Executed(object sender, RoutedEventArgs e)
        {
            string userInput = TagFilterInput.Text;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ViewTagsCommand FilterTagsClick = new ViewTagsCommand(this, userInput);
                    FilterTagsClick.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion
        
        #region View All Command
        private void ViewAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ViewAllCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ViewAllTaskCommand ViewAllTasks = new ViewAllTaskCommand(this);
                    ViewAllTasks.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion
        
        #region View Completed Command
        private void ViewCompletedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ViewCompletedCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ViewCompletedOrUnfinishedTaskCommand ViewCompletedTasks = new ViewCompletedOrUnfinishedTaskCommand(this, true);
                    ViewCompletedTasks.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion
        
        #region View Unfinished Command
        private void ViewUnfinishedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ViewUnfinishedCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    ViewCompletedOrUnfinishedTaskCommand ViewUnfinishedTasks = new ViewCompletedOrUnfinishedTaskCommand(this, false);
                    ViewUnfinishedTasks.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }
        #endregion

        #region Cut Command
        private void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void CutCommand_Executed(object sender, RoutedEventArgs e)
        {
            temporaryClipboard = (Task)TaskListBox.SelectedValue;
            DeleteTaskCommand DeleteClick = new DeleteTaskCommand(this);
            DeleteClick.Execute();

            this.ViewableTasks.View.Refresh();
        }
        #endregion

        #region Copy Command
        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else e.CanExecute = true;
        }
        private void CopyCommand_Executed(object sender, RoutedEventArgs e)
        {
            temporaryClipboard = (Task)TaskListBox.SelectedValue;
        }
        #endregion

        #region Paste Command

        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            if (temporaryClipboard != null)
                e.CanExecute = true;
            else e.CanExecute = false;
        }
        private void PasteCommand_Executed(object sender, RoutedEventArgs e)
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    PasteTaskCommand PasteTask = new PasteTaskCommand(this, temporaryClipboard);
                    PasteTask.Execute();
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            });
        }

        #endregion

        #region Connect

        private void ConnectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (loggedIn)
            {
                e.CanExecute = false;
            }
            else e.CanExecute = true;
        }

        private void ConnectCommand_Executed(object sender, RoutedEventArgs e)
        {
            ConnectWindow logInWindow = new ConnectWindow(this);
            logInWindow.Show();
        }


        #endregion

        #region Update Tasks
        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            DependencyObject senderCast = (DependencyObject)sender;
            ListBoxItem selectedListBoxItem = FindParent<ListBoxItem>(senderCast);
            Task mTask = selectedListBoxItem.DataContext as Task;
            if(mTask != null)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (selectedListBoxItem != null)
                        {
                            RenameTaskCommand ChangeTags = new RenameTaskCommand(this, mTask);
                            ChangeTags.Execute();
                        }
                    }
                    catch (AggregateException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                });
            } 
        }
        private void Description_LostFocus(object sender, RoutedEventArgs e)
        {
            DependencyObject senderCast = (DependencyObject)sender;
            ListBoxItem selectedListBoxItem = FindParent<ListBoxItem>(senderCast);
            Task mTask = selectedListBoxItem.DataContext as Task;
            if(mTask != null)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (selectedListBoxItem != null)
                        {
                            UpdateDescriptionTaskCommand ChangeTags = new UpdateDescriptionTaskCommand(this, mTask);
                            ChangeTags.Execute();
                        }
                    }
                    catch (AggregateException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }).Wait();
            }
        }
        private void Tags_LostFocus(object sender, RoutedEventArgs e)
        {
            DependencyObject senderCast = (DependencyObject)sender;
            ListBoxItem selectedListBoxItem = FindParent<ListBoxItem>(senderCast);
            
            Task mTask = selectedListBoxItem.DataContext as Task;
            if(mTask !=null)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        if (selectedListBoxItem != null)
                        {
                            ChangeTaskTagsCommand ChangeTags = new ChangeTaskTagsCommand(this, mTask);
                            ChangeTags.Execute();
                        }
                    }
                    catch (AggregateException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                });
            }    
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject senderCast = (DependencyObject)sender;
            ListBoxItem selectedListBoxItem = FindParent<ListBoxItem>(senderCast);
            Task mTask = (Task)selectedListBoxItem.DataContext;
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                try
                {
                    if (selectedListBoxItem != null)
                    {
                        MarkTaskCommand ChangeTags = new MarkTaskCommand(this, mTask);
                        ChangeTags.Execute();
                    }
                }
                catch (AggregateException error)
                {
                    MessageBox.Show(error.Message);
                }
            }).Wait();
        }
        #endregion

        private static ListBoxItem FindParent <ListBoxItem>(DependencyObject child) where ListBoxItem:DependencyObject
        {
            DependencyObject mParentObject = VisualTreeHelper.GetParent(child);
            if (mParentObject == null) 
                return null;

            ListBoxItem parent = mParentObject as ListBoxItem;
            if (parent != null)
                return parent;
            else
                return FindParent<ListBoxItem>(mParentObject);
        }
        
        public void UpdateViewSource()
        {
            ViewableTasks.Source = Tasks;
        }
    }
}

