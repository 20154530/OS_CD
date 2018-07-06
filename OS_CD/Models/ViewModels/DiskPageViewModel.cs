using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    internal class DiskPageViewModel : ViewModelBase {

        /// <summary>
        /// 磁盘块 位图
        /// </summary>
        private int[] blockcell;
        public int[] Blockcell {
            get => blockcell;
            set {
                blockcell = value;
                OnPropertyChanged("Blockcell");
            }
        }

        

        public DiskPageViewModel() {
            blockcell = new int[512];
            for (int i = 0; i < 512; i++)
                blockcell[i] = 0;

            blockcell[123] = 1;
            blockcell[124] = 1;
            blockcell[125] = 1;
        }
    }

}
