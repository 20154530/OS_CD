using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
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
                userNow = value;
                OnPropertyChanged("UserNow");
                LoginStateChanged?.Invoke(this, new PropertyChangeArgs(userNow, value));
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
            
            BlockRemain = FileSystem.Instance.Disc.freeBlockList.Count;
            BlockUsed = 512 - BlockRemain;
            Usage = String.Format("{0:F2} %", BlockUsed / 5.12);

            foreach (var pairs in FileSystem.Instance.Disc.usageBlockList) {
                Blockcell[pairs.Key] = pairs.Value;
            }

        }

        private void InitDisk() {
            Blockcell = new int[512];
            BlockRemain = 512;
            for (int i = 0; i < 512; i++)
                blockcell[i] = -1;

            UpdataBlockInfo();
        }

        public void LoadDisk() {
            for (int i = 0; i < 512; i++)
                blockcell[i] = -1;
            UpdataBlockInfo();
        }
        #endregion

        #region User
        //UserPage
        private List<User> users;
        public List<User> Users {
            get => users;
            set {
                users = value;
                OnPropertyChanged("UserList");
            }
        }

        private void InitUser() {
            UserNow = noUser;
            Users = new List<User>();
            LoadUsers();
            FileSystem.Instance.UCBListChanged += Instance_UCBListChanged;
        }

        private void Instance_UCBListChanged(object sender, EventArgs e) {
            LoadUsers();
        }

        private void LoadUsers() {
            Users.Clear();
            if (FileSystem.Instance.UCBList.Count > 0)
            {
                foreach (var pair in FileSystem.Instance.UCBList)
                {
                    Users.Add(pair.Value);
                }
            }
        }
        #endregion

        #region File
        private Dictionary<int, string> fileBodys;
        public Dictionary<int, string> FileBodys {
            get => fileBodys;
            set {
                fileBodys = value;
            }
        }


        private List<TFileNode> openedfile;
        public List<TFileNode> OpenedFile {
            get => openedfile;
            set {
                openedfile = value;
                OnPropertyChanged("OpenedFile");
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


        private TFileNode openfilenow;
        public TFileNode OpenFilenow {
            get => openfilenow;
            set {
                openfilenow = value;
                OnPropertyChanged("Filenow");
            }
        }

        public void UpdateFileTree() {
            FileDictionary = GetDictioniary();
        }

        public void UpdataOpenFileList() {
            List<TFileNode> dic = new List<TFileNode>();
            var oplist = FileSystem.Instance.GetUserById(UserNow.ID).openFileRecordList;
            if(oplist != null)
            foreach (var pairs in oplist) {
                dic.Add(new TFileNode(FileSystem.Instance.GetFileNodeById(pairs.Key)));
            }
            OpenedFile = dic;
        }

        private List<TFileNode> GetDictioniary() {
            List<TFileNode> dic = new List<TFileNode>();
            foreach (var pair in FileSystem.Instance.FCBList)
            {
                dic.Add(new TFileNode(pair.Value));
            }
            return getTrees(-1, dic);
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

        private List<TFileNode> GetOpenFileList() {
            List<TFileNode> op = new List<TFileNode>();
            var OpenList = FileSystem.Instance.GetUserById(UserNow.ID).openFileRecordList;
            if(OpenList != null)
            foreach (var pairs in OpenList)
            {
                op.Add(new TFileNode(FileSystem.Instance.GetFileNodeById(pairs.Key)));
            }
            return op;
        }

        private void InitFile() {
            LoginStateChanged += Systeminfo_LoginStateChanged;
            FileBodys = new Dictionary<int, string>();
        }

        private void Systeminfo_LoginStateChanged(object sender, EventArgs e) {
            if ((((PropertyChangeArgs)e).NewValue as User).ID != -1)
            {
                fileDictionary = GetDictioniary();
                OpenedFile = GetOpenFileList();
                Filenow = new TFileNode(-1, -1, "无文件");
            }
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
