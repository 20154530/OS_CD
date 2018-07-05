using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public MainPageViewModel() {
            Userid = "12";
            Username = "Administor";
        }
    }
}
