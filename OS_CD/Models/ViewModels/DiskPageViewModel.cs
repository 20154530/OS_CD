using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OS_CD {
    internal class DiskPageViewModel : ViewModelBase {

        /// <summary>
        /// 磁盘块 位示图
        /// </summary>
        private int[] blockcell;
        public int[] Blockcell {
            get => blockcell;
            set {
                blockcell = value;
                OnPropertyChanged("Blockcell");
            }
        }

        /// <summary>
        /// 选中的文件
        /// </summary>
        private File selectedFile;
        public File SelectedFile {
            get => selectedFile;
            set {
                if (value != null)
                    fileInfoVisibility = Visibility.Visible;
                else
                    fileInfoVisibility = Visibility.Collapsed;
                selectedFile = value;
                OnPropertyChanged("SelectedFile");
            }
        }

        /// <summary>
        /// 文件详细信息标识
        /// </summary>
        private Visibility fileInfoVisibility = Visibility.Collapsed;
        public Visibility FileInfoVisibility {
            get => fileInfoVisibility;
            set {
                fileInfoVisibility = value;
                OnPropertyChanged("FileInfoVisibility");
            }
        }

        public DiskPageViewModel() {

        }

        #region   COLORTEST
        //blockcell[12] = 1;
        //blockcell[14] = 2;
        //blockcell[16] = 3;
        //blockcell[18] = 4;
        //blockcell[20] = 5;
        //blockcell[22] = 6;
        //blockcell[24] = 7;
        //blockcell[26] = 8;
        //blockcell[28] = 9;
        //blockcell[30] = 10;
        //blockcell[32] = 11;
        //blockcell[34] = 12;
        //blockcell[36] = 13;
        //blockcell[38] = 14;
        //blockcell[40] = 15;
        //blockcell[42] = 16;
        //blockcell[44] = 17;
        //blockcell[46] = 18;
        //blockcell[48] = 19;
        //blockcell[50] = 20;
        //blockcell[52] = 21;
        //blockcell[54] = 22;
        //blockcell[56] = 23;
        //blockcell[58] = 24;
        //blockcell[60] = 25;
        //blockcell[62] = 26;
        //blockcell[64] = 27;
        //blockcell[66] = 28;
        //blockcell[68] = 29;
        #endregion
    }
}
