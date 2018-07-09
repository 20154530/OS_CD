using System;
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

        public Mode FileMode { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public int PID { get; set; }

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
        }

        private TFileNode FromFileNode(FileNode f) {
            return new TFileNode(f);
        }

        public TFileNode(FileNode f) {
            NameChanged = new CommandBase();
            Contains = new List<TFileNode>();
            Rename = false;
            Name = f.name;
            ID = f.ID;
            PID = f.fatherFileNodeId;
            FileMode = f is Folder ? Mode.Folder : Mode.File;
        }

    }
}
