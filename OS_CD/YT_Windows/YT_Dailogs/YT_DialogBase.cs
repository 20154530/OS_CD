using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OS_CD {
    /// <summary>
    /// 对话框基类
    /// </summary>
    public class YT_DialogBase : Window {

        #region ButtonCommands
        public CommandBase CancelCommand {
            get { return (CommandBase)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new PropertyMetadata(null));

        public CommandBase YesCommand {
            get { return (CommandBase)GetValue(YesCommandProperty); }
            set { SetValue(YesCommandProperty, value); }
        }
        public static readonly DependencyProperty YesCommandProperty =
            DependencyProperty.Register("YesCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new PropertyMetadata(null));

        public CommandBase NoCommand {
            get { return (CommandBase)GetValue(NoCommandProperty); }
            set { SetValue(NoCommandProperty, value); }
        }
        public static readonly DependencyProperty NoCommandProperty =
            DependencyProperty.Register("NoCommand", typeof(CommandBase),
                typeof(YT_DialogBase), new PropertyMetadata(null));
        #endregion

        #region CommandActions

        #endregion

        #region PrivateMethod
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            this.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        private void InitCommands() {
            CancelCommand = new CommandBase();
            CancelCommand.Commandaction += CancelCommand_Commandaction;
            YesCommand = new CommandBase();
            YesCommand.Commandaction += YesCommand_Commandaction;
            NoCommand = new CommandBase();
            NoCommand.Commandaction += NoCommand_Commandaction;
        }

        protected virtual void NoCommand_Commandaction(object para) {
            DialogResult = false;
            Close();
        }

        protected virtual void YesCommand_Commandaction(object para) {
            DialogResult = true;
            Close();
        }

        protected virtual void CancelCommand_Commandaction(object para) {
            Close();
        }
        #endregion

        #region PublicMethod
        public virtual void ShowDialog(Window Holder) {
            Owner = Holder;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowDialog();
        }
        #endregion

        #region Constructors
        public YT_DialogBase() {
            this.WindowStyle = WindowStyle.None;
            this.AllowsTransparency = true;
            InitCommands();
        }
        #endregion
    }
}