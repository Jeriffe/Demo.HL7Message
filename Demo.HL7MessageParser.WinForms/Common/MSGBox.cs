using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public static class MSGBox
    {
        public static void ShowBox(string msg, string caption, MSGLevel level = MSGLevel.Information)
        {
            switch (level)
            {
                case MSGLevel.Warning:
                    ShowWarning(msg, caption);
                    break;
                case MSGLevel.Error:
                    ShowError(msg, caption);
                    break;
                case MSGLevel.Information:
                default:
                    ShowInfo(msg, caption);
                    break;
            }
        }

        public static void ShowWarning(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowError(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public enum MSGLevel
    {
        Information,
        Warning,
        Error
    }
}




