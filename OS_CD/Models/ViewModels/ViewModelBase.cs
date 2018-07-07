using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OS_CD {
    internal class ViewModelBase : INotifyPropertyChanged {
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
