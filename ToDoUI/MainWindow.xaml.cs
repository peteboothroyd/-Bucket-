using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoTrainingGit.Commands;
//using ToDoTrainingGit;

namespace ToDoTrainingGit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public CollectionViewSource ViewableTasks { get; set; }

        string FOLDER_LOCATION, NAME_ATTRIBUTION, DESCRIPTION_ATTRIBUTION, MARKED_ATTRIBUTE, TAGS_ATTRIBUTE;

        public Func<Task, bool> ViewFilter
        {
            get;
            set;
        }

        public MainWindow()
        {
            var todosCollection = new ObservableCollection<Task>();
            this.Tasks = todosCollection;

            var todosViewableCollection = new CollectionViewSource();
            this.ViewableTasks = todosViewableCollection;
            todosViewableCollection.Source = todosCollection;
            todosViewableCollection.SortDescriptions.Add(new SortDescription("SortOrder", ListSortDirection.Ascending));
            todosViewableCollection.Filter += (s, e) =>
            {
                Task task = e.Item as Task;
                e.Accepted = task.IsViewable;
            };

            InitializeComponent();

            FOLDER_LOCATION = (String)Application.Current.Resources["FOLDER_LOCATION"];
            NAME_ATTRIBUTION = (String)Application.Current.Resources["NAME_ATTRIBUTION"];
            DESCRIPTION_ATTRIBUTION = (String)Application.Current.Resources["DESCRIPTION_ATTRIBUTION"];
            MARKED_ATTRIBUTE = (String)Application.Current.Resources["MARKED_ATTRIBUTE"];
            TAGS_ATTRIBUTE = (String)Application.Current.Resources["TAGS_ATTRIBUTE"];

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

        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Check.ListCountGreaterZero(this);
        }

        public void OpenCommand_Executed(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog mOpenFileDialog = new OpenFileDialog();
                mOpenFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
                //string folderLocation = FOLDER_LOCATION;
                //mOpenFileDialog.InitialDirectory = @folderLocation;
                if (mOpenFileDialog.ShowDialog() == true)
                {
                    string path = mOpenFileDialog.FileName;
                    OpenFileCommand OpenFileCommand = new OpenFileCommand(this, path);
                    OpenFileCommand.Execute();
                    this.ViewableTasks.View.Refresh();
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Check.ListCountGreaterZero(this);

        }

        public void SaveCommand_Executed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog mSaveFileDialog = new SaveFileDialog();
            mSaveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            //mSaveFileDialog.InitialDirectory = @ToDoApplication.FOLDER_LOCATION;
            if (mSaveFileDialog.ShowDialog() == true)
            {
                string path = mSaveFileDialog.FileName;
                SaveFileCommand SaveFileCommand = new SaveFileCommand(this, path);
                SaveFileCommand.Execute();
                this.ViewableTasks.View.Refresh();
            }
        }

        private void ClearListCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Check.ListCountGreaterZero(this);
        }

        private void ClearListCommand_Executed(object sender, RoutedEventArgs e)
        {
            Tasks.Clear();
            this.ViewableTasks.View.Refresh();
        }

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
            this.ViewableTasks.View.Refresh();
        }

        private void RefreshCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RefreshCommand_Executed(object sender, RoutedEventArgs e)
        {
            this.ViewableTasks.View.Refresh();
        }

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
            this.ViewableTasks.View.Refresh();
        }

        private void MoveUpCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (TaskListBox.SelectedValue == null)
                e.CanExecute = false;
            else if (TaskListBox.SelectedIndex == 0)
                e.CanExecute = false;
            else e.CanExecute = true;
        }

        private void MoveUpCommand_Executed(object sender, RoutedEventArgs e)
        {
            MoveTaskCommand UpClick = new MoveTaskCommand(this, true);
            UpClick.Execute();
            this.ViewableTasks.View.Refresh();
        }

        private void NewTaskCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewTaskCommand_Executed(object sender, RoutedEventArgs e)
        {
            CreateTaskCommand CreateClick = new CreateTaskCommand(this);
            CreateClick.Execute();
            ViewableTasks.View.Refresh();
        }

        private void ViewFilteredCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string userInput = TagFilterInput.Text;
            if (String.IsNullOrWhiteSpace(userInput))
                e.CanExecute = false;
            e.CanExecute = true;
        }

        private void ViewFilteredCommand_Executed(object sender, RoutedEventArgs e)
        {
            string userInput = TagFilterInput.Text;
            ViewTagsCommand FilterTagsClick = new ViewTagsCommand(this, userInput);
            FilterTagsClick.Execute();
            this.ViewableTasks.View.Refresh();
        }

        private void ViewAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewAllCommand_Executed(object sender, RoutedEventArgs e)
        {
            ViewAllTaskCommand ViewAllTasks = new ViewAllTaskCommand(this);
            ViewAllTasks.Execute();
            this.ViewableTasks.View.Refresh();
        }

        private void ViewCompletedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewCompletedCommand_Executed(object sender, RoutedEventArgs e)
        {
            ViewCompletedTaskCommand ViewCompletedTasks = new ViewCompletedTaskCommand(this);
            ViewCompletedTasks.Execute();
            this.ViewableTasks.View.Refresh();
        }

        private void ViewUnfinishedCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ViewUnfinishedCommand_Executed(object sender, RoutedEventArgs e)
        {
            ViewUnfinishedTaskCommand ViewUnfinishedTasks = new ViewUnfinishedTaskCommand(this);
            ViewUnfinishedTasks.Execute();
            this.ViewableTasks.View.Refresh();
        }
    }
}

