using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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

        #region PropertiesNoVisual
        private Timer systime_timer;

        public event EventHandler LoginStateChanged;
        private User userNow ;
        public User UserNow {
            get => userNow;
            set {
                LoginStateChanged.Invoke(this, new PropertyChangeArgs(userNow, value));
                userNow = value;
                OnPropertyChanged("UserNow");
            }
        }
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
        #endregion

        #region Constructors
        public Systeminfo() {
            InitTiemr();
            userNow = new User(-1,"未登录");
            //初始化属性
        }
        #endregion
    }

}
