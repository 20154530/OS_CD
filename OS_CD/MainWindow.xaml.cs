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

namespace OS_CD
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window
    {
        MainPageViewModel viewModel;

        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            viewModel = new MainPageViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }
        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            viewModel.Mainframe = MainFrame;
        }
    }
}
