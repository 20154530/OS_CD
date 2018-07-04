using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OS_CD {
    public class YT_TitleBar : ContentControl {
        #region AttachedWindow
        private Window attachedWindow;
        public Window AttachedWindow {
            get => attachedWindow;
            set { attachedWindow = value; }
        }
        #endregion

        #region CloseCommand
        public CommandBase CloseCommand {
            get { return (CommandBase)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(CommandBase), typeof(YT_TitleBar), new PropertyMetadata(null));
        #endregion

        #region Methods Overrides
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            AttachedWindow.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public override void EndInit() {
            DependencyObject RootElement = this;
            while (!(RootElement is Window)) { RootElement = VisualTreeHelper.GetParent(RootElement); }
            AttachedWindow = RootElement as Window;

            base.EndInit();
        }
        #endregion

        #region EventActions
        private void CloseCommand_Commandaction(object para) {
            AttachedWindow.Close();
        }

        #endregion

        #region Constructors
        public YT_TitleBar() {
            CloseCommand = new CommandBase();
            CloseCommand.Commandaction += CloseCommand_Commandaction;
        }

        static YT_TitleBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TitleBar), new FrameworkPropertyMetadata(typeof(YT_TitleBar)));
        }
        #endregion
    }
}
