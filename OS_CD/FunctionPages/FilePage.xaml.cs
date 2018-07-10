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
            viewModel.OnFileClose += ViewModel_OnFileClose;
            viewModel.OnFileOpen += ViewModel_OnFileOpen;
            viewModel.OnSetRights += ViewModel_OnSetRights;
            InitializeComponent();
        }

        private void ViewModel_OnSetRights(object sender, EventArgs e) {
            
         
        }

        public override void EndInit() {
            base.EndInit();
            AddFilePopup.PlacementTarget = AddFileBtn;
        }

        private void ViewModel_OnFileOpen(object sender, EventArgs e) {
            TFileNode tfn = FileTree.SelectedItem as TFileNode;
            int id = tfn.ID;
            if (tfn is null)
            {
                MessageBoxServices.ShowSimpleStringDialog("请选文件择以打开!", false, false);
                return;
            }
            else if (Systeminfo.Instence.FileBodys.Count > 5)
            {
                MessageBoxServices.ShowSimpleStringDialog("打开文件超过数量!", false, false);
                return;
            }
            else if (FileSystem.Instance.GetUserById(Systeminfo.Instence.UserNow.ID).OpenFileRecordList.ContainsKey(id))
            {
                MessageBoxServices.ShowSimpleStringDialog("文件已经打开!", false, false);
                return;
            }
            else if (tfn.FileMode.Equals(TFileNode.Mode.Folder))
            {
                MessageBoxServices.ShowSimpleStringDialog("选择文件而不是文件夹", false, false);
                return;
            }
            else if (!(FileSystem.Instance.GetFileNodeById(id).CheckReadPower(Systeminfo.Instence.UserNow.ID))) {
                MessageBoxServices.ShowSimpleStringDialog("当前用户没有读取权限", false, false);
                return;
            }
            else
            {
                FileSystem.Instance.OpenFileNode(id, Systeminfo.Instence.UserNow.ID);
                Systeminfo.Instence.UpdataOpenFileList();
                Systeminfo.Instence.FileBodys.Add(id, (FileSystem.Instance.FCBList[id] as File).fileBody.GetContent());
                Properties.Settings.Default.SelectedFile = id;
                OpenFileLabel.SelectedIndex = Systeminfo.Instence.FileBodys.Count;
                viewModel.FileBodyText = Systeminfo.Instence.FileBodys[id];
            }
        }

        private void ViewModel_OnFileClose(object sender, EventArgs e) {
            int id = Convert.ToInt32(((PropertyChangeArgs)e).NewValue);
            if (!(FileSystem.Instance.GetFileNodeById(id).CheckWriterPower(Systeminfo.Instence.UserNow.ID)))
            {
                MessageBoxServices.ShowSimpleStringDialog("当前用户没有写入权限", false, false);
            }
            else
            {
                if (Systeminfo.Instence.FileBodys.Count == 1)
                    FileSystem.Instance.GetUserById(Systeminfo.Instence.UserNow.ID).openFileRecordList[id].buff.SetContent(viewModel.FileBodyText);
                else
                    FileSystem.Instance.GetUserById(Systeminfo.Instence.UserNow.ID).openFileRecordList[id].buff.SetContent(Systeminfo.Instence.FileBodys[id]);
            }
            FileSystem.Instance.CloseFile(id, Systeminfo.Instence.UserNow.ID);
            Systeminfo.Instence.UpdataOpenFileList();
            Systeminfo.Instence.FileBodys.Remove(id);
            Properties.Settings.Default.SelectedFile = 0;
            viewModel.FileBodyText = "";
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

        private void ViewModel_OnFileAdded(object sender, EventArgs e) {
            TFileNode sf = FileTree.SelectedItem as TFileNode;
            if (sf is null)
            {
                MessageBoxServices.ShowSimpleStringDialog("请选择一个文件夹来添加文件!", false, false);
                return;
            }
            else if (sf.FileMode.Equals(TFileNode.Mode.File)) {
                MessageBoxServices.ShowSimpleStringDialog("不能在文件下继续创建结构!", false, false);
                return;
            }
            if (sf.FileMode.Equals(TFileNode.Mode.Folder))
            {
                if (((PropertyChangeArgs)e).NewValue is null)
                {
                    if ((FileSystem.Instance.GetFileNodeById(sf.ID) as Folder).IsNameExist("新建文件")) {
                        MessageBoxServices.ShowSimpleStringDialog("存在同名文件!", false, false);
                        return;
                    }
                    FileSystem.Instance.CreateFile("新建文件", sf.ID, Systeminfo.Instence.UserNow.ID);
                    Systeminfo.Instence.UpdateFileTree();
                }
                else
                {
                    if ((FileSystem.Instance.GetFileNodeById(sf.ID) as Folder).IsNameExist("新建文件夹"))
                    {
                        MessageBoxServices.ShowSimpleStringDialog("存在同名文件夹!", false, false);
                        return;
                    }
                    FileSystem.Instance.CreateFolder("新建文件夹", sf.ID, Systeminfo.Instence.UserNow.ID);
                    Systeminfo.Instence.UpdateFileTree();
                }
            }
        }

        private void ViewModel_OnFileRemove(object sender, EventArgs e) {
            int id = (FileTree.SelectedItem as TFileNode).ID;
            if (id == 0)
            {
                MessageBoxServices.ShowSimpleStringDialog("不能删除根目录!!!", false, false);
                return;
            }
            MessageBoxServices.ShowSimpleStringDialog("确认删除!!!", true, true, true, FileRemoveSeccess);
        }

        private void FileRemoveSeccess(object obj) {
            int id = (FileTree.SelectedItem as TFileNode).ID;
            FileSystem.Instance.DestoryFileNode(id);
            Systeminfo.Instence.UpdateFileTree();
        }

        private void OpenFileLabel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Systeminfo.Instence.OpenFilenow = OpenFileLabel.SelectedItem as TFileNode;
            if (Systeminfo.Instence.OpenFilenow != null)
            {
                int id = Systeminfo.Instence.OpenFilenow.ID;
                if (Properties.Settings.Default.SelectedFile != 0)
                    Systeminfo.Instence.FileBodys[Properties.Settings.Default.SelectedFile] = viewModel.FileBodyText;
                if (Systeminfo.Instence.FileBodys.ContainsKey(id))
                    viewModel.FileBodyText = Systeminfo.Instence.FileBodys[id];
                Properties.Settings.Default.SelectedFile = id;
            }
            else
            {
                viewModel.FileBodyText = "";
            }
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e) {
            AddFilePopup.IsOpen = true;
        }

        private void YT_IconButton_Click(object sender, RoutedEventArgs e) {
            AddFilePopup.IsOpen = false;
        }

        private void FileTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            Systeminfo.Instence.Filenow = e.NewValue as TFileNode;
        }

    }
}
