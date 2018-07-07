using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_CD {
    public class PropertyChangeArgs : EventArgs {
        private object oldValue;
        public object OldValue {
            get => oldValue;
        }

        private object newValue;
        public object NewValue {
            get => newValue;
        }
        public PropertyChangeArgs(object oV,object nV) {
            oldValue = oV;
            newValue = nV;
        }
    }
}
