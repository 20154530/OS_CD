using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OS_CD.Models;
using System.Windows.Media;

namespace OS_CD.Mangers.ValueConverters {
    /// <summary>
    /// 盘块颜色查表转换
    /// </summary>
    public class ColorSelector : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ConfigTable.Instance.ColorTable[System.Convert.ToInt32(value) % 30];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 判断磁盘块是否为空
    /// </summary>
    public class JudgeNone : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToInt32(value) == 0 ? "空闲" : "占用";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 使无显示的控件原来布局空间消失
    /// </summary>
    public class HideNoVisual : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (((Visibility)value).Equals(Visibility.Collapsed))
                return new GridLength(0, GridUnitType.Star);
            else
                return new GridLength(1, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 文件树图标选择
    /// </summary>
    public class FileTreeIconSelector : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
            {
                if (((TFileNode)value).Contains.Count == 0)
                    return "\uE8A5";
                else
                    return "\uED41";
            }
            else
            {
                if (((TFileNode)value).Contains.Count == 0)
                    return "\uE8A5";
                else
                    return "\uED43";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 文件树鼠标提示选择
    /// </summary>
    public class FileTreeToolTipSelector : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (((TFileNode)value).Contains.Count == 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 反可见性
    /// </summary>
    public class ConVisual : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((Visibility)value).Equals(Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 双向 Double To GridLength
    /// </summary>
    public class DoubleToGrid : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new GridLength(System.Convert.ToDouble(value), GridUnitType.Pixel);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            double x = ((GridLength)value).Value;
            if (x > 360)
                return 360;
            else
                return x;
        }
    }

    /// <summary>
    /// Visibility To True/Flase
    /// </summary>
    public class VisToTF : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return ((Visibility)value).Equals(Visibility.Visible) ? true : false;
            else
                return ((Visibility)value).Equals(Visibility.Visible) ? false : true;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 使特定值不显示
    /// </summary>
    public class HideFitValue : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value.ToString().Equals(parameter.ToString()))
                return "";
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}