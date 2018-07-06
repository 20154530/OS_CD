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

        public static Systeminfo Instence = new Systeminfo();

        #region Properties
        private Timer systime_timer;

        private string sysTime_HMS;
        public string SysTime_HMS {
            get => sysTime_HMS;
            set {
                sysTime_HMS = value;
                OnPropertyChanged("SysTime_HMS");
            }
        }

        private string sys_Op_state;
        public string Sys_Op_state {
            get => sys_Op_state;
            set {
                sys_Op_state = value;
                OnPropertyChanged("Sys_Op_state");
            }
        }
        #endregion

        #region TimerMethod
        private void Systime_timer_Elapsed(object sender, ElapsedEventArgs e) {
            SysTime_HMS = "Time : " + DateTime.Now.ToLongTimeString();
        }
        #endregion

        #region Constructors
        public Systeminfo() {
            systime_timer = new Timer();
            systime_timer.Interval = 1000;
            systime_timer.Elapsed += Systime_timer_Elapsed;
            systime_timer.Enabled = true;
        }
        #endregion
    }

    /// <summary>
    /// 系统状态信息表
    /// </summary>
    internal class SystemOpStateTable {
    

    }

}
