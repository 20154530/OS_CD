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

namespace OS_CD.FunctionPages
{
    /// <summary>
    /// DiskPage.xaml 的交互逻辑
    /// </summary>
    public partial class DiskPage : Page
    {
        private DiskPageViewModel DiskVM { get; set; }

        public DiskPage()
        {
            DiskVM = new DiskPageViewModel();
            DataContext = DiskVM;
            InitializeComponent();
        }
    }
}
