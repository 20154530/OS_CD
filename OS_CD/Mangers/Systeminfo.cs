using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using OS_CD.Models;

namespace OS_CD {
    /// <summary>
    /// 系统信息类,记录全局系统活动
    /// </summary>
    internal class Systeminfo : ViewModelBase {

        private static Systeminfo _Instence;
        public static Systeminfo Instence {
            get {
                if (_Instence is null)
                    _Instence = new Systeminfo();
                return _Instence;
            }
        }

        public User noUser = new User(-1, "未登录", "");

        #region PropertiesNoVisual
        private Timer systime_timer;

        #endregion


        #region Properties
        //General
        private string sysTime_HMS;
        public string SysTime_HMS {
            get => sysTime_HMS;
            set {
                sysTime_HMS = value;
                OnPropertyChanged("SysTime_HMS");
            }
        }

        private string filePath;
        public string FilePath {
            get => filePath;
            set {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        //LoginPage
        public event EventHandler LoginStateChanged;
        private User userNow;
        public User UserNow {
            get => userNow;
            set {
                LoginStateChanged?.Invoke(this, new PropertyChangeArgs(userNow, value));
                userNow = value;
                OnPropertyChanged("UserNow");
            }
        }

        private Visibility loginState = Visibility.Collapsed;
        public Visibility LoginState {
            get => loginState;
            set {
                loginState = value;
                OnPropertyChanged("LoginState");
            }
        }

        #endregion

        #region TimerMethod
        private void Systime_timer_Elapsed(object sender, ElapsedEventArgs e) {
            SysTime_HMS = "Time : " + DateTime.Now.ToLongTimeString();
        }
        #endregion

        #region PrivateMethod
        private void InitTiemr() {
            systime_timer = new Timer();
            systime_timer.Interval = 1000;
            systime_timer.Elapsed += Systime_timer_Elapsed;
            systime_timer.Enabled = true;
        }

        #region Disk
        //DiskPage
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

        public string Usage { get; set; }

        private int blockused = 0;
        public int BlockUsed {
            get => blockused;
            set { blockused = value; }
        }

        private int blockremain = 0;
        public int BlockRemain {
            get => blockremain;
            set { blockremain = value; }
        }

        private void UpdataBlockInfo() {
            foreach (var pair in FileSystem.Instance.FCBList) {
                if(pair.Value is File)
                {
                    File tf = (File)pair.Value;
                    foreach (int i in tf.blockIdList) {
                        Blockcell[i] = tf.ID;
                        BlockUsed++;
                    }
                }
            }
            Usage = String.Format("{0:F1} %", BlockUsed / 51.2);
        }

        private void InitDisk() {
            Blockcell = new int[512];
            BlockRemain = 512;
            for (int i = 0; i < 512; i++)
                blockcell[i] = -1;

            UpdataBlockInfo();
        }

        private void LoadDisk() {

        }
        #endregion

        #region User
        //UserPage
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users {
            get => users;
            set {
                users = value;
            }
        }

        private void InitUser() {
            UserNow = noUser;
            Users = new ObservableCollection<User>();
            LoadUsers();
            FileSystem.Instance.UCBListChanged += Instance_UCBListChanged;
        }

        private void Instance_UCBListChanged(object sender, EventArgs e) {
            LoadUsers();
        }

        private void LoadUsers() {
            Users.Clear();
            if (FileSystem.Instance.UCBList.Count > 0)
                foreach (var pair in FileSystem.Instance.UCBList)
                    Users.Add(pair.Value);
        }
        #endregion

        #region File
        private Dictionary<int, int> idtonode;
        public Dictionary<int, int> IDtoNode {
            get => idtonode;
            set {
                idtonode = value;
            }
        }

        private List<TFileNode> fileDictionary { get; set; }
        public List<TFileNode> FileDictionary {
            get => fileDictionary;
            set {
                fileDictionary = value;
                OnPropertyChanged("FileDictionary");
            }
        }

        private TFileNode filenow;
        public TFileNode Filenow {
            get => filenow;
            set {
                filenow = value;
                OnPropertyChanged("Filenow");
            }
        }

        private void Systeminfo_FileTreeChanged(object sender, EventArgs e) {
           // fileDictionary = GetFileTreeByRoot();
        }

        public void UpdateFileTree() {
            fileDictionary = GetDictioniary();
        }

        private List<TFileNode> GetDictioniary() {
            List<TFileNode> dic = new List<TFileNode>();
            foreach (var pair in FileSystem.Instance.FCBList)
            {
                dic.Add(new TFileNode(pair.Value));
            }
            return getTrees(0, dic);
        }

        private List<TFileNode> getTrees(int parentid, List<TFileNode> nodes) {
            List<TFileNode> mainNodes = nodes.Where(x => x.PID == parentid).ToList<TFileNode>();
            List<TFileNode> otherNodes = nodes.Where(x => x.PID != parentid).ToList<TFileNode>();
            foreach (TFileNode dpt in mainNodes)
            {
                dpt.Contains = getTrees(dpt.ID, otherNodes);
            }
            return mainNodes;
        }

        private void InitFile() {
            fileDictionary = GetDictioniary();
            //for (int i= 0;i< fileDictionary.Count;i++) 
            //    IDtoNode.Add(fileDictionary[i].ID, i);
            Filenow = new TFileNode(-1, -1, "无文件");
        }
        #endregion


        #endregion


        #region EventActions


        #endregion

        #region Constructors
        public Systeminfo() {
            InitTiemr();
            InitDisk();
            InitUser();
            InitFile();
            //初始化属性
        }
        #endregion
    }

}
