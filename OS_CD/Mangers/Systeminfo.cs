using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

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

        //UserPage
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users {
            get => users;
            set {
                users = value;
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

        private void InitDisk() {
            Blockcell = new int[512];
            for (int i = 0; i < 512; i++)
                blockcell[i] = -1;
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
                foreach (var pair in FileSystem.Instance.UCBList) {
                    Users.Add(pair.Value);
                }
        }

        private void LoadDisk() {

        }
        #endregion


        #region EventActions
    
        
        #endregion

        #region Constructors
        public Systeminfo() {
            InitTiemr();
            InitDisk();
            InitUser();

            //初始化属性
        }
        #endregion
    }

}
