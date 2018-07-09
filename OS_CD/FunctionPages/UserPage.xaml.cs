using OS_CD.Mangers.Serverces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OS_CD.FunctionPages {
    /// <summary>
    /// UserPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserPage : Page {
        private UserPageViewModel viewModel = new UserPageViewModel();
        public UserPage() {
            DataContext = viewModel;
            viewModel.RemoveUsers += ViewModel_RemoveUsers;
            InitializeComponent();
        }

        private void ViewModel_RemoveUsers(object sender, EventArgs e) {
            User selected = UserList.SelectedItem as User;
            if (selected is null)
                MessageBoxServices.ShowSimpleStringDialog("未选择用户！", false, false);
            else
                MessageBoxServices.ShowSimpleStringDialog("确认删除所选用户？", true, true, true, DeleteUser);
        }

        private void DeleteUser(object obj) {
            FileSystem.Instance.DestoryUser((UserList.SelectedItem as User).ID);
        }
    }
}
