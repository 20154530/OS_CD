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
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page {
        LoginPageViewModel viewModel = new LoginPageViewModel();


        public LoginPage() {
            DataContext = viewModel;
            viewModel.SubmitAction += ViewModel_SubmitAction;
            viewModel.LoginAction += ViewModel_LoginAction;
            viewModel.LogoutAction += ViewModel_LogoutAction;
            InitializeComponent();
        }

        private void ViewModel_LogoutAction(object sender, EventArgs e) {
            Password.IsEnabled = true;
            Account.IsEnabled = true;
        }

        private void ViewModel_LoginAction(object sender, EventArgs e) {
            Password.IsEnabled = false;
            Account.IsEnabled = false;
        }

        private void ViewModel_SubmitAction(object sender, EventArgs e) {
            ((LoginPageViewModel)sender).Password = Password.Password;
            ((LoginPageViewModel)sender).UserAccount = Account.Text;
        }
    }
}
