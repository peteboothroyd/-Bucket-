using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoTrainingGit;

namespace ToDoListWPF
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        private MainWindow mMainWindow;
        public ConnectWindow(MainWindow mainWindow)
        {
            mMainWindow = mainWindow;
            this.Owner = mainWindow;
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Text;
            int errorCode = mMainWindow.Hub.Invoke<int>("Login", username, password).Result;
            switch (errorCode)
            {
                case 0:
                    mMainWindow.LoggedIn = true;
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("Username does not exist!");
                    break;
                case 2:
                    MessageBox.Show("Password does not match");
                    break;
            }
        }

        private void CreateUser_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Text;
            int errorCode = mMainWindow.Hub.Invoke<int>("CreateUser", username, password).Result;
            switch (errorCode)
            {
                case 0:
                    mMainWindow.LoggedIn = true;
                    this.Close();
                    break;
                case 1:
                    MessageBox.Show("Username already exists!");
                    break;
            }
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = "";
            }
        }

        private void UsernameInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                if (String.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = "Username";
                }
            }
        }

        private void PasswordInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (TextBox)sender;
                if (String.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = "Password";
                }
            }
        }

        private void Button_KeyUp(object sender, KeyEventArgs e)
        {
            if (!UsernameInput.Text.Equals("Username") || !PasswordInput.Text.Equals("Password"))
            {
                loginButton.IsEnabled = true;
                createUserButton.IsEnabled = true;
            }
            else
            {
                loginButton.IsEnabled = false;
                createUserButton.IsEnabled = false;
            }
        }
    }
}
