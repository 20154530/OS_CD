using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace OS_CD {
    public class YT_TextBox : TextBox {

        #region Properties
        public bool EnableFlag {
            get { return (bool)GetValue(EnableFlagProperty); }
            set { SetValue(EnableFlagProperty, value); }
        }
        public static readonly DependencyProperty EnableFlagProperty =
            DependencyProperty.Register("EnableFlag", typeof(bool),
                typeof(YT_TextBox), new PropertyMetadata(false));

        public bool OriNoable {
            get { return (bool)GetValue(OriNoableProperty); }
            set { SetValue(OriNoableProperty, value); }
        }
        public static readonly DependencyProperty OriNoableProperty =
            DependencyProperty.Register("OriNoable", typeof(bool),
                typeof(YT_TextBox), new PropertyMetadata(false));

        public ICommand DisableCommand {
            get { return (ICommand)GetValue(DisableCommandProperty); }
            set { SetValue(DisableCommandProperty, value); }
        }
        public static readonly DependencyProperty DisableCommandProperty =
            DependencyProperty.Register("DisableCommand", typeof(ICommand), 
                typeof(YT_TextBox), new PropertyMetadata(null));

        public object CommandParameter {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object),
                typeof(YT_TextBox), new PropertyMetadata(null));

        public IInputElement CommandTarget {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement),
                typeof(YT_TextBox), new PropertyMetadata(null));
        #endregion

        #region overrides
        protected override void OnPreviewKeyUp(KeyEventArgs e) {
            base.OnPreviewKeyUp(e);
            if (e.Key.Equals(Key.Enter))
            {
                IsEnabled = false;
            }
        }

        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e) {
            //if (OriNoable) {
            //    IsEnabled = !IsEnabled;
            //}
            base.OnPreviewMouseDoubleClick(e);
        }

        public override void EndInit() {
            base.EndInit();
            if (OriNoable)
                IsEnabled = false;
        }

        private void YT_TextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
            if (!(bool)e.NewValue)
            {
                
                BindingExpression binding = GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
                RoutedCommand rcmd = DisableCommand as RoutedCommand;
                if (rcmd != null)
                {
                    if (rcmd.CanExecute(CommandParameter, CommandTarget))
                        rcmd.Execute(CommandParameter, CommandTarget);
                }
                else
                {
                    if (DisableCommand != null)
                        if (DisableCommand.CanExecute(CommandParameter))
                            DisableCommand.Execute(CommandParameter);
                }
            }
        }
        #endregion

        public YT_TextBox() {
            IsEnabledChanged += YT_TextBox_IsEnabledChanged;
        }

        static YT_TextBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TextBox), new FrameworkPropertyMetadata(typeof(YT_TextBox)));
        }

    }
}
