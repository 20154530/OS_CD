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
                { 2, "#701FFF" },
                { 3, "#73BEFF" },
                { 4, "#478733" },
                { 5, "#1C8DA5" },
                { 6, "#C6A322" },
                { 7, "#ACC74A" },
                { 8, "#323584" },
                { 9, "#CE2AD6" },
                { 10, "#FFBB32" },
                { 11, "#156B01" },
                { 12, "#F1FF30" },
                { 13, "#F10230" },
                { 14, "#817A01" },
                { 15, "#370167" },
                { 16, "#01675B" },
                { 17, "#664129" },
                { 18, "#003F54" },
                { 19, "#2D9E00" },
                { 20, "#2D9E8B" },
                { 21, "#CC6600" },
                { 22, "#6D706C" },
                { 23, "#4070FF" },
                { 24, "#40FF7D" },
                { 25, "#000066" },
                { 26, "#666400" },
                { 27, "#3F0066" },
                { 28, "#003B66" },
                { 29, "#FF931F" },
            };

        }
    }
}
