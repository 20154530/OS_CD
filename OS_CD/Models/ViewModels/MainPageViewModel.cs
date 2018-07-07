using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OS_CD {
    internal class MainPageViewModel : ViewModelBase {
        #region Properties
        private Visibility userPageVis;
        public Visibility UserPageVis {
            get => userPageVis;
            set {
                userPageVis = value;
                OnPropertyChanged("UserPageVis");
            }
        }

        private string userid;
        public string Userid {
            get => userid;
            set {
                userid = value;
                OnPropertyChanged("Userid");
            }
        }

        private string username;
        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public event EventHandler OpenUserSelectMenuAction;

        public Frame Mainframe { get; set; }

        public CommandBase NavigateCommand { get; set; }

        public CommandBase OpenUserSelectMenuCommand { get; set; }

        public CommandBase CloseCommand { get; set; }

        #endregion

        #region PrivateMethods
        private void InitAction() {
            NavigateCommand = new CommandBase();
            NavigateCommand.Commandaction += NavigateCommand_Commandaction;
            OpenUserSelectMenuCommand = new CommandBase();
            OpenUserSelectMenuCommand.Commandaction += OpenUserSelectMenuCommand_Commandaction;
            CloseCommand = new CommandBase();
            CloseCommand.Commandaction += CloseCommand_Commandaction;
        }

        private void CloseCommand_Commandaction(object para) {
            YT_ExitDialog _ExitDialog = new YT_ExitDialog
            {
                ContentWidth = 280,
                ContentHeight = 120,
                Content = App.Current.FindResource("DefaultExitContent")
            };
            _ExitDialog.YesAction += _ExitDialog_YesAction;
            _ExitDialog.NoAction += _ExitDialog_NoAction;
            _ExitDialog.ShowDialog(App.Current.MainWindow);
        }

        private void _ExitDialog_NoAction() {

        }

        private void _ExitDialog_YesAction() {
            App.Current.MainWindow.Close();
        }

        private void OpenUserSelectMenuCommand_Commandaction(object para) {
            Mainframe.Navigate(new Uri("FunctionPages/LoginPage.xaml", UriKind.Relative));
            OpenUserSelectMenuAction.Invoke(this, EventArgs.Empty);
        }

        private void NavigateCommand_Commandaction(object para) {
            Mainframe.Navigate(new Uri("FunctionPages/" + para.ToString(), UriKind.Relative));
        }

        private void Instence_LoginStateChanged(object sender, EventArgs e) {
            if (!(((PropertyChangeArgs)e).NewValue is null))
                UserPageVis = ((PropertyChangeArgs)e).NewValue.Equals("Administor") ?
                    Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region Constructors
        public MainPageViewModel() {
            InitAction();
            Systeminfo.Instence.LoginStateChanged += Instence_LoginStateChanged;
            Userid = "12";
            Username = "Administor";
        }

        #endregion
    }
}
