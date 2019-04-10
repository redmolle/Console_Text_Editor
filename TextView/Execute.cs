using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextView
{
    public static class Execute
    {
        public static string area { get; set; }
        private static Area AreaWindow { get; set; }

        public static void ShowAreaWindow(string info, string text = null)
        {
            area = text ?? string.Empty;

            AreaWindow = new Area(info, area);
            AreaWindow.ShowDialog();

        }

        public static void Init()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private static void Main()
        {
        }
    }
}
