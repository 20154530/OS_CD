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

namespace OS_CD {
    /// <summary>
    /// MenuTable.xaml 的交互逻辑
    /// </summary>
    public partial class MenuTable : UserControl {
        #region
        public double Minwidth {
            get { return (double)GetValue(MinwidthProperty); }
            set { SetValue(MinwidthProperty, value); }
        }
        public static readonly DependencyProperty MinwidthProperty =
            DependencyProperty.Register("Minwidth", typeof(double),
                typeof(MenuTable), new PropertyMetadata(320.0));
        #endregion

        public MenuTable() {
            InitializeComponent();
        }
    }
}
