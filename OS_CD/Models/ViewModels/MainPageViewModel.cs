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

        private Frame mainframe;
        public Frame Mainframe { get; set; }


        public MainPageViewModel() {
            Userid = "12";
            Username = "Administor";
        }
    }
}
