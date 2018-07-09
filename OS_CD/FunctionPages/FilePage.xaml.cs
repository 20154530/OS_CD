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
    /// FilePage.xaml 的交互逻辑
    /// </summary>
    public partial class FilePage : Page {
        FilePageViewModel viewModel = new FilePageViewModel();
        public FilePage() {
            DataContext = viewModel;
            viewModel.OnFileRemoved += ViewModel_OnFileRemove;
            viewModel.OnFileAdded += ViewModel_OnFileAdded;
            InitializeComponent();
        }

        private void ViewModel_OnFileAdded(object sender, EventArgs e) {

        }

        private void ViewModel_OnFileRemove(object sender, EventArgs e) {

        }


        private void OpenFileLabel_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void FileTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            Console.WriteLine("Value" + FileTree.SelectedValue.GetType());
            Console.WriteLine("Path" + FileTree.SelectedValuePath.GetType());
            Console.WriteLine("Item" + FileTree.SelectedItem.GetType());
        }
    }
}
