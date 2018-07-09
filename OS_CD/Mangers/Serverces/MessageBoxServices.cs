using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static OS_CD.YT_DialogBase;

namespace OS_CD.Mangers.Serverces {
    internal class MessageBoxServices {

        public static void ShowSimpleStringDialog(string content, bool yes = true, bool no = true, bool cancel = true, CommandAction ya = null, CommandAction na = null) {
            Grid cont = App.Current.FindResource("WarningDialog") as Grid;
            YT_GeneralDialog _ExitDialog = new YT_GeneralDialog();
            _ExitDialog.YseButtonVisibility = yes ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.NoButtonVisibility = no ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.CancelButtonVisibility = cancel ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.ContentWidth = 280;
            _ExitDialog.ContentHeight = 120;
            _ExitDialog.Content = cont;
            cont.DataContext = _ExitDialog;
            _ExitDialog.ContentText = content;

            _ExitDialog.YesAction += ya;
            _ExitDialog.NoAction += na;

            _ExitDialog.ShowDialog(App.Current.MainWindow);
        }

        public static void ShowFormDialog(string content,int formitems, bool yes = true, bool no = true, bool cancel = true, CommandAction ya = null,CommandAction na = null) {
            Grid cont = App.Current.FindResource(content) as Grid;
            YT_FormDialog _ExitDialog = new YT_FormDialog();
            _ExitDialog.YseButtonVisibility = yes ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.NoButtonVisibility = no ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.CancelButtonVisibility = cancel ? Visibility.Visible : Visibility.Collapsed;
            _ExitDialog.ContentWidth = 320;
            _ExitDialog.ContentHeight = 140;
            _ExitDialog.Content = cont;
            _ExitDialog.FormItemCounts = formitems;
            cont.DataContext = _ExitDialog;

            _ExitDialog.YesAction += ya;
            _ExitDialog.NoAction += na;

            _ExitDialog.ShowDialog(App.Current.MainWindow);
        }
    }
}
