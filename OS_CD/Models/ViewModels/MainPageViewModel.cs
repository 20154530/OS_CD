using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OS_CD.Mangers.Serverces;

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

        public event EventHandler OnNavigate;

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
            MessageBoxSeverces.ShowSimpleStringDialog("确认离开系统吗？", true, true, true, _ExitDialog_YesAction, _ExitDialog_NoAction);
        }

        private void _ExitDialog_NoAction() {

        }

        private void _ExitDialog_YesAction() {
            App.Current.MainWindow.Close();
        }

        private void OpenUserSelectMenuCommand_Commandaction(object para) {
            OnNavigate.Invoke(this,new PropertyChangeArgs(para, para));
            OpenUserSelectMenuAction.Invoke(this, EventArgs.Empty);
        }

        private void NavigateCommand_Commandaction(object para) {
            if (Systeminfo.Instence.LoginState.Equals(Visibility.Collapsed)) {
                MessageBoxSeverces.ShowSimpleStringDialog("请先登录！");
                return;
            }
            OnNavigate.Invoke(this, new PropertyChangeArgs(para, para));
        }

        private void Instence_LoginStateChanged(object sender, EventArgs e) {
            if (!(((PropertyChangeArgs)e).NewValue is null))
            {
                User user = (User)((PropertyChangeArgs)e).NewValue;
                UserPageVis = user.ID == 0 ? Visibility.Visible : Visibility.Collapsed;
                Username = user.Name;
                userid = user.ID.ToString();
            }
        }
        #endregion

        #region Constructors
        public MainPageViewModel() {
            InitAction();
            Systeminfo.Instence.LoginStateChanged += Instence_LoginStateChanged;
        }

        #endregion
    }
}
