using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    internal class LoginPageViewModel : ViewModelBase {


        #region Properties
        private string userAccount;
        public string UserAccount {
            get => userAccount;
            set { userAccount = value; }
        }

        private string password;
        public string Password {
            get => password;
            set { password = value; }
        }

        private string sysState;
        public string SysState {
            get => sysState;
            set {
                sysState = value;
                OnPropertyChanged("SysState");
            }
        }
        #endregion

        #region Events
        public event EventHandler SubmitAction;
        #endregion

        #region Commands
        public CommandBase Submit { get; set; }

        private void Submit_Commandaction(object para) {
            SubmitAction.Invoke(this, EventArgs.Empty);
            //判断用户名密码

            //Yes
            sysState = "已登录";
            Systeminfo.Instence.LoginState = UserAccount;
            //No
            sysState = "未登录";
            Systeminfo.Instence.LoginState = null;

        }
        #endregion

        private void InitCommand() {
            Submit = new CommandBase();
            Submit.Commandaction += Submit_Commandaction;
        }


        public LoginPageViewModel() {
            InitCommand();
            sysState = "未登录";
        }
    }
}
