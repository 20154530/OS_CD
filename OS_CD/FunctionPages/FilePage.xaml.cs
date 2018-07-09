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
using OS_CD.Mangers.Serverces;

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
            AddFilePopup.PlacementTarget = AddFileBtn;
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
            if (sf is null)
            {
                MessageBoxServices.ShowSimpleStringDialog("请选择一个文件夹来添加文件", false, false);
                return;
            }
            if (sf.FileMode.Equals(TFileNode.Mode.Folder))
            {
                if (((PropertyChangeArgs)e).NewValue is null)
                {
                    FileSystem.Instance.CreateFile("新建文件", sf.ID, Systeminfo.Instence.UserNow.ID);
                    Systeminfo.Instence.UpdateFileTree();
                }
                else
                {
                    FileSystem.Instance.CreateFolder("新建文件夹", sf.ID, Systeminfo.Instence.UserNow.ID);
                    Systeminfo.Instence.UpdateFileTree();
                }
            }
        }

        private void ViewModel_OnFileRemove(object sender, EventArgs e) {
            //if()
            FileSystem.Instance.DestoryFileNode((FileTree.SelectedItem as TFileNode).ID);
            Systeminfo.Instence.UpdateFileTree();
        }

        private void OpenFileLabel_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e) {
            AddFilePopup.IsOpen = true;
        }

        private void YT_IconButton_Click(object sender, RoutedEventArgs e) {
            AddFilePopup.IsOpen = false;
        }

        private void FileTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            Console.WriteLine(e.NewValue.GetType());
        }
    }
}
