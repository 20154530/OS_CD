using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace OS_CD {
    /// <summary>
    /// 命令基类，用于与生成相关类成员有耦合的命令
    /// </summary>
    public class CommandBase : ICommand {
        public event EventHandler CanExecuteChanged;
        public delegate void CommandAction(object para);
        private CommandAction commandaction;
        public event CommandAction Commandaction {
            add { commandaction = value; }
            remove { commandaction -= value; }
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            commandaction(parameter);
        }
    }
}
