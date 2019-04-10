using System;
using System.Windows.Forms;

namespace TextView
{
    public partial class Area : Form
    {
        public Area(string info, string text)
        {
            InitializeComponent();

            if (String.IsNullOrEmpty(text))
                Info.Text = $"Ввод нового текста \"{info}\"";
            else
                Info.Text = $"Изменение текста \"{info}\"";

            Data.Text = text;
        }

        private void Area_FormClosing(object sender, FormClosingEventArgs e)
        {
            Execute.area = Data.Text;
        }
    }
}