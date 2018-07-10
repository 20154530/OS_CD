using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OS_CD.Models;
using static OS_CD.Models.TFileNode;

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
            return System.Convert.ToInt32(value) == -1 ? "空闲" : "占用";
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
            TFileNode tf = value as TFileNode;
            if (!(tf is null))
                if (parameter is null)
                {
                    if (tf.FileMode.Equals(Mode.File))
                        return "\uE8A5";
                    else
                        return "\uED41";
                }
                else
                {
                    if (tf.FileMode.Equals(Mode.File))
                        return "\uE8A5";
                    else
                        return "\uED43";
                }
            else
                return "";

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
            TFileNode tf = value as TFileNode;
            if (!(tf is null))
            {
                if (tf.FileMode.Equals(Mode.File))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
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

    /// <summary>
    /// 获得文件父目录的名称
    /// </summary>
    public class GetPDock : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (FileSystem.Instance.GetFileNodeById(System.Convert.ToInt32(value)) as Folder).name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 获得文件占用盘块信息
    /// </summary>
    public class GetFBlockList : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            File f = FileSystem.Instance.GetFileNodeById(System.Convert.ToInt32(value)) as File;
            if (f is null)
            {
                return "";
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("");
                foreach (int i in f.blockIdList)
                    builder.AppendFormat(" {0} ", i);
                return builder.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 获得文件内容的名称
    /// </summary>
    public class GetFContent : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            File f = FileSystem.Instance.GetFileNodeById(System.Convert.ToInt32(value)) as File;
            if (f is null)
            {
                return "";
            }
            else
                return f.fileBody.GetContent();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// ID _ Name
    /// </summary>
    public class GetFName : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            FileNode f = FileSystem.Instance.GetFileNodeById(System.Convert.ToInt32(value));
            if (f is null)
            {
                return "";
            }
            else
                return f.name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 获得当前用户权限
    /// </summary>
    public class GetPowerList : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            File f = (FileSystem.Instance.GetFileNodeById(System.Convert.ToInt32(value)) as File);

            if (f != null)
            {
                if (f.PowerList.ContainsKey(Systeminfo.Instence.UserNow.ID))
                    return f.PowerList[Systeminfo.Instence.UserNow.ID];
                else
                    return "此用户没有权限";
            }
            else
                return "未知";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 隐藏非文件节点
    /// </summary>
    public class HideNotFile : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (((TFileNode)value).Contains.Count == 0)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 隐藏没有权限
    /// </summary>
    public class HideNoPower : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var tf = value as TFileNode;
            if (!(tf is null))
            {
                if (tf.FileMode.Equals(TFileNode.Mode.Folder))
                    return Visibility.Collapsed;
                int id = tf.ID;
                if (FileSystem.Instance.GetFileNodeById(id).CheckWriterPower(Systeminfo.Instence.UserNow.ID))
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class CheckPower : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var tf = value as TFileNode;
            if (!(tf is null))
            {
                int id = tf.ID;
                if (parameter is null)
                    return FileSystem.Instance.GetFileNodeById(id).CheckReadPower(Systeminfo.Instence.UserNow.ID);
                else
                    return FileSystem.Instance.GetFileNodeById(id).CheckWriterPower(Systeminfo.Instence.UserNow.ID);
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}