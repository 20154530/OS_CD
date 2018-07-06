using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OS_CD {
    class CellPanel : Panel {

        #region 


        public double CellWidth {
            get { return (double)GetValue(CellWidthProperty); }
            set { SetValue(CellWidthProperty, value); }
        }
        public static readonly DependencyProperty CellWidthProperty =
            DependencyProperty.Register("CellWidth", typeof(double), 
                typeof(CellPanel), new PropertyMetadata(0));


        #endregion

        protected override Size MeasureOverride(Size availableSize) {
            Size size = new Size();
            foreach (var child in Children)
            {
               
            }

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize) {
            Size size = new Size();


            return size;
        }
    }
}
