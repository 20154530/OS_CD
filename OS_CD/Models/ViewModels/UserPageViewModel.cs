using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_CD.Mangers.Serverces;

namespace OS_CD {
    internal class UserPageViewModel : ViewModelBase {

        #region Commands
        public CommandBase AddUser { get; set; }

        public CommandBase RemoveUser { get; set; }
        #endregion

        #region Events
        public event EventHandler RemoveUsers;
        #endregion

        #region CommandActions

        #endregion


        #region PrivateMotheds
        private void InitCommands() {
            AddUser = new CommandBase();
            AddUser.Commandaction += AddUser_Commandaction;
            RemoveUser = new CommandBase();
            RemoveUser.Commandaction += RemoveUser_Commandaction;
        }

        private void RemoveUser_Commandaction(object para) {
            RemoveUsers?.Invoke(this,EventArgs.Empty);
        }

        private void AddUser_Commandaction(object para) {
            MessageBoxServices.ShowFormDialog("FormContent", 2, true, true, true, AddUserAction);
        }

        #endregion

        #region CallBacks
        private void AddUserAction(object obj) {
            YT_FormDialog YFD = (YT_FormDialog)obj;
            FileSystem.Instance.CreateUser(YFD.FormItems[0], YFD.FormItems[1]);
            
        }
        #endregion

        #region Constructor
        public UserPageViewModel() {
            InitCommands();
        }
        #endregion

    }
}
