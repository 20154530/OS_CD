using System;
using System.Windows;
using System.Windows.Controls;

namespace OS_CD.FunctionPages {
    /// <summary>
    /// DiskPage.xaml 的交互逻辑
    /// </summary>
    public partial class DiskPage : Page {
        private DiskPageViewModel DiskVM;

        public DiskPage() {
            DiskVM = new DiskPageViewModel();
            DataContext = DiskVM;
            InitializeComponent();
        }

        private void DiskBlocks_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int selected = (int)((ListView)sender).SelectedItem;
            try
            {
                Systeminfo.Instence.SelectedFile = FileSystem.Instance.FCBList[selected] as File;
            }
            catch (Exception s)
            {
                s.GetType();
            }
            if (selected != -1)
                Systeminfo.Instence.FileInfoVisibility = Visibility.Visible;
            else
                Systeminfo.Instence.FileInfoVisibility = Visibility.Collapsed;
        }
    }
}
