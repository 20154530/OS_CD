using System;
using System.Collections.Generic;
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

        public User noUser = new User(-1,"未登录","");

        #region PropertiesNoVisual
        private Timer systime_timer;

        #endregion


        #region Properties
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

        private int[] blockcell;
        public int[] Blockcell {
            get => blockcell;
            set {
                blockcell = value;
                OnPropertyChanged("Blockcell");
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
        #endregion

        #region Constructors
        public Systeminfo() {
            InitTiemr();
            InitDisk();
            UserNow = noUser;
            //初始化属性
        }
        #endregion
    }

}
