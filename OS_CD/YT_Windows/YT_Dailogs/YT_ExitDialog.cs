using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OS_CD {
    /// <summary>
    /// 窗口关闭对话框
    /// 一般用于提醒保存操作或进行一些选择
    /// </summary>
    public class YT_ExitDialog : YT_DialogBase {

        public YT_ExitDialog() {
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }
        static YT_ExitDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ExitDialog), new FrameworkPropertyMetadata(typeof(YT_ExitDialog)));
        }
    }
}
