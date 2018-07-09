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
using OS_CD.Models;

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
            viewModel.OnFileRename += ViewModel_OnFileRename;
            InitializeComponent();
        }

        public override void EndInit() {
            base.EndInit();
            FileTree.MouseDoubleClick += FileTree_MouseDoubleClick;
        }

        private void ViewModel_OnFileRename(object sender, EventArgs e) {
            TFileNode sf = FileTree.SelectedItem as TFileNode;
            sf.Rename = true;
            sf.NameChanged.Commandaction += NameChanged_Commandaction;
        }

        private void NameChanged_Commandaction(object para) {
            TFileNode tf = para as TFileNode;
            FileSystem.Instance.GetFileNodeById(tf.ID).name = tf.Name;
        }

        private void FileTree_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            
        }

        private void ViewModel_OnFileAdded(object sender, EventArgs e) {
            TFileNode sf = FileTree.SelectedItem as TFileNode;
            if (sf.FileMode.Equals(TFileNode.Mode.Folder)) {
                Folder fl = FileSystem.Instance.GetFileNodeById(sf.ID) as Folder;
                fl.subFileNodeIdList.Add(FileSystem.Instance.CreateFile("", sf.ID,));
            }
        }

        private void ViewModel_OnFileRemove(object sender, EventArgs e) {

        }


        private void OpenFileLabel_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void FileTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            
        }
    }
}
