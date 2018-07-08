using System;
using OS_CD.Mangers.Serverces;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    internal class LoginPageViewModel : ViewModelBase {


        #region Properties
        public enum LoginState {
            OK,
            PasswordError,
            NoUser
        }

        public LoginState LoginS { get; set; }

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
        #endregion

        #region Events
        public event EventHandler SubmitAction;

        public event EventHandler LoginAction;

        public event EventHandler LogoutAction;
        #endregion

        #region Commands
        public CommandBase Submit { get; set; }

        public CommandBase Logout { get; set; }

        private void Submit_Commandaction(object para) {
            SubmitAction.Invoke(this, EventArgs.Empty);
            //判断用户名密码
            LoginS = LoginState.NoUser;
            int id = 100;

            foreach (var pair in FileSystem.Instance.UCBList)
            {
                if (pair.Value.Name.Equals(UserAccount))
                {
                    if (pair.Value.Password.Equals(Password))
                    {
                        id = pair.Key;
                        LoginS = LoginState.OK;
                    }
                    else
                        LoginS = LoginState.PasswordError;
                }
            }

            switch (LoginS) {
                case LoginState.PasswordError:
                    MessageBoxSeverces.ShowSimpleStringDialog("密码错误！",false,false);
                    break;
                case LoginState.OK:
                    MessageBoxSeverces.ShowSimpleStringDialog("登录成功！", false, false);
                    Systeminfo.Instence.UserNow = FileSystem.Instance.GetUserById(id);
                    FileSystem.Instance.SwitchCurrentUser(id);
                    Systeminfo.Instence.LoginState = System.Windows.Visibility.Visible;
                    LoginAction?.Invoke(this,null);
                    break;
                case LoginState.NoUser:
                    MessageBoxSeverces.ShowSimpleStringDialog("用户不存在！", false, false);
                    break;
            }

        }

        private void Logout_Commandaction(object para) {
            LogoutAction.Invoke(this, EventArgs.Empty);

            Systeminfo.Instence.LoginState = System.Windows.Visibility.Collapsed;
            Systeminfo.Instence.UserNow = Systeminfo.Instence.noUser;
        }
        #endregion

        private void InitCommand() {
            Submit = new CommandBase();
            Submit.Commandaction += Submit_Commandaction;
            Logout = new CommandBase();
            Logout.Commandaction += Logout_Commandaction;
        }

        public LoginPageViewModel() {
            InitCommand();
        }
    }
}
