using OS_CD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    internal class FilePageViewModel : ViewModelBase {
        
        private List<TFileNode> fileDictionary { get; set; }
        public List<TFileNode> FileDictionary {
            get => fileDictionary;
            set {
                fileDictionary = value;
                OnPropertyChanged("FileDictionary");
            }
        }


        public FilePageViewModel() {
            FileDictionary = new List<TFileNode>();
            FileDictionary = GetDictioniary();
        }

        private List<TFileNode> GetDictioniary() {
            List<TFileNode> dic = new List<TFileNode>();
            TFileNode folder = new TFileNode(0,0,"文件夹0");
            TFileNode folder1 = new TFileNode(4,0, "文件夹1");
            TFileNode file1 = new TFileNode(1,0, "文件1");
            TFileNode file2 = new TFileNode(2,0, "文件2");
            TFileNode file3 = new TFileNode(3,0, "文件3");
            TFileNode file5 = new TFileNode(5,0, "文件4");
            TFileNode file6 = new TFileNode(6,4, "文件5");
            TFileNode file7 = new TFileNode(7,4, "文件6");
            TFileNode file8 = new TFileNode(8,4, "文件7");
            TFileNode file9 = new TFileNode(8,4, "文件7");
            TFileNode file10 = new TFileNode(8,4, "文件7");
            TFileNode file11 = new TFileNode(8,4, "文件7");
            TFileNode file12 = new TFileNode(8,4, "文件7");
            TFileNode file13 = new TFileNode(8,4, "文件7");

            folder.AddFileNode(file1);
            folder.AddFileNode(file2);
            folder.AddFileNode(file3);
            folder.AddFileNode(folder1);
            folder.AddFileNode(file5);

            folder1.AddFileNode(file6);
            folder1.AddFileNode(file7);
            folder1.AddFileNode(file8);
            folder1.AddFileNode(file9);
            folder1.AddFileNode(file10);
            folder1.AddFileNode(file11);
            folder1.AddFileNode(file12);
            folder1.AddFileNode(file13);

            dic.Add(folder);
            return dic;
        }
    }
}
