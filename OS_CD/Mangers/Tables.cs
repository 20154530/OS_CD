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
                { 1, "#DC6E65" },
                { 2, "#DC6E65" },
                { 3, "#DC6E65" },
                { 4, "#DC6E65" },
                { 5, "#DC6E65" },
                { 6, "#DC6E65" },
                { 7, "#DC6E65" },
                { 8, "#DC6E65" },
                { 9, "#DC6E65" },
                { 10, "#DC6E65" },
                { 11, "#DC6E65" },
                { 12, "#DC6E65" },
                { 13, "#DC6E65" },
                { 14, "#DC6E65" },
                { 15, "#DC6E65" },
                { 16, "#DC6E65" },
                { 17, "#DC6E65" },
                { 18, "#DC6E65" },
                { 19, "#DC6E65" },
                { 20, "#DC6E65" },
                { 21, "#DC6E65" },
                { 22, "#DC6E65" },
                { 23, "#DC6E65" },
                { 24, "#DC6E65" },
                { 25, "#DC6E65" },
                { 26, "#DC6E65" },
                { 27, "#DC6E65" },
                { 28, "#DC6E65" },
                { 29, "#DC6E65" },
            };

        }
    }
}
