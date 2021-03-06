﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD.Models {

    /// <summary>
    /// 用于显示树状图的转换模型
    /// </summary>
    internal class TFileNode :ViewModelBase{
        public enum Mode {
            File,
            Folder
        }
        public CommandBase NameChanged { get; set; }

        public CommandBase CloseFile { get; set; }

        public Mode FileMode { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public int PID { get; set; }


        public event EventHandler IsExpandChanged;
        private bool isexpand = false;
        public bool IsExpand {
            get => isexpand;
            set {
                isexpand = value;
                OnPropertyChanged("IsExpand");
                IsExpandChanged?.Invoke(this,EventArgs.Empty);
            }
        }

        private bool rename = false;
        public bool Rename {
            get => rename;
            set { rename = value;
                OnPropertyChanged("Rename");
            }
        }

        public List<TFileNode> Contains { get; set; }

        public void AddFileNode(TFileNode f) {
            Contains.Add(f);
        }

        public TFileNode(int id, int pid = 0, string name = "",Mode mode = Mode.File,bool rn = false) {
            Contains = new List<TFileNode>();
            PID = pid;
            Name = name;
            FileMode = mode;
            NameChanged = new CommandBase();
            CloseFile = new CommandBase();
        }

        private TFileNode FromFileNode(FileNode f) {
            return new TFileNode(f);
        }

        public TFileNode(FileNode f) {
            NameChanged = new CommandBase();
            CloseFile = new CommandBase();
            Contains = new List<TFileNode>();
            Rename = false;
            Name = f.name;
            ID = f.ID;
            PID = f.fatherFileNodeId;
            FileMode = f is Folder ? Mode.Folder : Mode.File;
        }

        public override string ToString() {
            return String.Format(" < ID :{0}   PID : {1}  FileMode : {2}  >", ID, PID, FileMode.ToString());
        }
    }
}
