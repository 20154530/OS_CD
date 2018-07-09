﻿using System;
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
        public delegate void CommandAction(object obj = null);

        #region Properties
        public double ContentWidth {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }
        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));

        public double ContentHeight {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }
        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(double),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility YseButtonVisibility {
            get { return (Visibility)GetValue(YseButtonVisibilityProperty); }
            set { SetValue(YseButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty YseButtonVisibilityProperty =
            DependencyProperty.Register("YseButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility NoButtonVisibility {
            get { return (Visibility)GetValue(NoButtonVisibilityProperty); }
            set { SetValue(NoButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty NoButtonVisibilityProperty =
            DependencyProperty.Register("NoButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));

        public Visibility CancelButtonVisibility {
            get { return (Visibility)GetValue(CancelButtonVisibilityProperty); }
            set { SetValue(CancelButtonVisibilityProperty, value); }
        }
        public static readonly DependencyProperty CancelButtonVisibilityProperty =
            DependencyProperty.Register("CancelButtonVisibility", typeof(Visibility),
                typeof(YT_DialogBase), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

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

        private CommandAction yesAction;
        public event CommandAction YesAction {
            add { yesAction = value; }
            remove { yesAction -= value; }
        }

        private CommandAction noAction;
        public event CommandAction NoAction {
            add { noAction = value; }
            remove { noAction -= value; }

        }
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
            noAction?.Invoke(this);
            Close();
        }

        protected virtual void YesCommand_Commandaction(object para) {
            DialogResult = true;
            yesAction?.Invoke(this);
            Close();
        }

        protected virtual void CancelCommand_Commandaction(object para) {
            Close();
        }
        #endregion

        #region PublicMethod
        public virtual void ShowDialog(Window Holder) {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Holder;
            ShowDialog();
        }
        #endregion

        #region Constructors
        public YT_DialogBase() {
            InitCommands();
        }

        #endregion
    }
}