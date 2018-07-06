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

        public Frame Mainframe { get; set; }

        public CommandBase NavigateCommand { get; set; }
        #endregion

        #region PrivateMethods
        private void InitAction() {
            NavigateCommand = new CommandBase();
            NavigateCommand.Commandaction += NavigateCommand_Commandaction;
        }

        private void NavigateCommand_Commandaction(object para) {
            Mainframe.Navigate(new Uri("FunctionPages/"+para.ToString(), UriKind.Relative));
            Systeminfo.Instence.Sys_Op_state = para.ToString().Trim().Replace(".xaml","");
        }
        #endregion

        #region Constructors
        public MainPageViewModel() {
            InitAction();
            Userid = "12";
            Username = "Administor";
        }
        #endregion
    }
}
