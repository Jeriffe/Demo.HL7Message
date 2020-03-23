using Demo.HL7MessageParser.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser
{
    public static class StaticExtensions
    {
        public static void SafeInvoke(this Control controlofUI, Action callback, bool forceSynchronous)
        {
            if (controlofUI == null)
            {
                throw new ArgumentNullException("uiElement");
            }

            if (controlofUI.InvokeRequired)
            {
                if (forceSynchronous)
                {
                    controlofUI.Invoke((Action)delegate { SafeInvoke(controlofUI, callback, forceSynchronous); });
                }
                else
                {
                    controlofUI.BeginInvoke((Action)delegate { SafeInvoke(controlofUI, callback, forceSynchronous); });
                }
            }
            else
            {
                if (controlofUI.IsDisposed)
                {
                    throw new ObjectDisposedException("Control is already disposed.");
                }
                else
                {
                    callback();
                }
            }
        }
    }
}
