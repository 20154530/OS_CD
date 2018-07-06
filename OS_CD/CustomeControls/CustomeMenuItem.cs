using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OS_CD {
    class CustomeMenuItem : MenuItem {

        #region IconOnly
        public Visibility ContentTextVisiblity {
            get { return (Visibility)GetValue(ContentTextVisiblityProperty); }
            set { SetValue(ContentTextVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ContentTextVisiblityProperty =
            DependencyProperty.Register("ContentTextVisiblity", typeof(Visibility), typeof(CustomeMenuItem),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region TextOnly
        public Visibility IconVisiblity {
            get { return (Visibility)GetValue(IconVisiblityProperty); }
            set { SetValue(IconVisiblityProperty, value); }
        }
        public static readonly DependencyProperty IconVisiblityProperty =
            DependencyProperty.Register("IconVisiblity", typeof(Visibility),
                typeof(CustomeMenuItem), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region HeadIcon
        public string HeadIcon {
            get { return (string)GetValue(HeadIconProperty); }
            set { SetValue(HeadIconProperty, value); }
        }
        public static readonly DependencyProperty HeadIconProperty =
            DependencyProperty.Register("HeadIcon", typeof(string), 
                typeof(CustomeMenuItem), new PropertyMetadata(""));
        #endregion

        #region LabelPadding
        public Thickness LabelPadding {
            get { return (Thickness)GetValue(LabelPaddingProperty); }
            set { SetValue(LabelPaddingProperty, value); }
        }
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness),
                typeof(CustomeMenuItem), new PropertyMetadata(new Thickness(0)));
        #endregion

        #region Textloc
        public bool Textloc {
            get { return (bool)GetValue(TextlocProperty); }
            set { SetValue(TextlocProperty, value); }
        }
        public static readonly DependencyProperty TextlocProperty =
            DependencyProperty.Register("Textloc", typeof(bool),
                typeof(CustomeMenuItem), new PropertyMetadata(false));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(CustomeMenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region ContentText
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(CustomeMenuItem),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextFontWeight
        public double ContentTextFontSize {
            get { return (double)GetValue(ContentTextFontSizeProperty); }
            set { SetValue(ContentTextFontSizeProperty, value); }
        }
        public static readonly DependencyProperty ContentTextFontSizeProperty =
            DependencyProperty.Register("ContentTextFontSize", typeof(double), typeof(CustomeMenuItem),
                new PropertyMetadata(0.0));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(CustomeMenuItem),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(CustomeMenuItem),
                new PropertyMetadata(Visibility.Visible));
        #endregion

        static CustomeMenuItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomeMenuItem), new FrameworkPropertyMetadata(typeof(CustomeMenuItem)));
        }
    }
}
