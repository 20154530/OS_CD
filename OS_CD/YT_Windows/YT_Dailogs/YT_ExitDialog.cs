using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OS_CD {
    /// <summary>
    /// 窗口关闭对话框
    /// 一般用于提醒保存操作或进行一些选择
    /// </summary>
    public sealed class YT_ExitDialog : YT_DialogBase {

        #region Properties
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string),
                typeof(YT_ExitDialog), new PropertyMetadata(""));
        #endregion

        protected override void OnClosing(CancelEventArgs e) {
            this.Content = null;
            base.OnClosing(e);
        }

        public YT_ExitDialog() {
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        static YT_ExitDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ExitDialog), new FrameworkPropertyMetadata(typeof(YT_ExitDialog)));
        }
    }
}
