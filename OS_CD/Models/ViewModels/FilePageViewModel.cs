using OS_CD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    internal class FilePageViewModel : ViewModelBase {

        #region Commands
        public CommandBase AddFile { get; set; }

        public CommandBase RemoveFile { get; set; }

        public CommandBase CloseFile { get; set; }

        public CommandBase OpenFile { get; set; }

        public CommandBase Rename { get; set; }
        #endregion

        #region Events
        public event EventHandler OnFileRemoved;

        public event EventHandler OnFileAdded;

        public event EventHandler OnFileRename;
        #endregion


        #region Actions
        private void OpenFile_Commandaction(object para) {
            
        }

        private void CloseFile_Commandaction(object para) {
            
        }

        private void AddFile_Commandaction(object para) {
            OnFileAdded?.Invoke(this,EventArgs.Empty);

        }

        private void RemoveFile_Commandaction(object para) {
            OnFileRemoved?.Invoke(this, EventArgs.Empty);

        }

        private void Rename_Commandaction(object para) {
            OnFileRename?.Invoke(this, EventArgs.Empty);
            
        }
        #endregion


        #region Method
        private void InitCommands() {
            AddFile = new CommandBase();
            AddFile.Commandaction += AddFile_Commandaction;
            RemoveFile = new CommandBase();
            RemoveFile.Commandaction += RemoveFile_Commandaction;
            CloseFile = new CommandBase();
            CloseFile.Commandaction += CloseFile_Commandaction;
            OpenFile = new CommandBase();
            OpenFile.Commandaction += OpenFile_Commandaction;
            Rename = new CommandBase();
            Rename.Commandaction += Rename_Commandaction;
        }

        #endregion

        public FilePageViewModel() {
            InitCommands();
        }
    }
}

#region
//TFileNode folder = new TFileNode(0,0,"文件夹0");
//TFileNode folder1 = new TFileNode(4,0, "文件夹1");
//TFileNode file1 = new TFileNode(1,0, "文件1");
//TFileNode file2 = new TFileNode(2,0, "文件2");
//TFileNode file3 = new TFileNode(3,0, "文件3");
//TFileNode file5 = new TFileNode(5,0, "文件4");
//TFileNode file6 = new TFileNode(6,4, "文件5");
//TFileNode file7 = new TFileNode(7,4, "文件6");
//TFileNode file8 = new TFileNode(8,4, "文件7");
//TFileNode file9 = new TFileNode(8,4, "文件7");
//TFileNode file10 = new TFileNode(8,4, "文件7");
//TFileNode file11 = new TFileNode(8,4, "文件7");
//TFileNode file12 = new TFileNode(8,4, "文件7");
//TFileNode file13 = new TFileNode(8,4, "文件7");

//folder.AddFileNode(file1);
//folder.AddFileNode(file2);
//folder.AddFileNode(file3);
//folder.AddFileNode(folder1);
//folder.AddFileNode(file5);

//folder1.AddFileNode(file6);
//folder1.AddFileNode(file7);
//folder1.AddFileNode(file8);
//folder1.AddFileNode(file9);
//folder1.AddFileNode(file10);
//folder1.AddFileNode(file11);
//folder1.AddFileNode(file12);
//folder1.AddFileNode(file13);

//dic.Add(folder);
#endregion