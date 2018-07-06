using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OS_CD
{
    internal class ConfigTable {
        public static ConfigTable Instance = new ConfigTable();

        #region Properties
        public Dictionary<int, string> ColorTable { get; set; }

        public Dictionary<int, string> SysInfoTable { get;set; }
        #endregion



        public ConfigTable() {
            InitColorTable();
        }


        public void InitColorTable() {
            ColorTable = new Dictionary<int, string>
            {
                { 0, "#A1CE5E" },
                { 1, "#DC6E65" }
            };

        }
    }
}
