using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OS_CD {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            App.Current.SessionEnding += Current_SessionEnding;
            MainWindow window = new MainWindow();
            window.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {
            OS_CD.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e) {
            OS_CD.Properties.Settings.Default.Save();
        }
    }
}
