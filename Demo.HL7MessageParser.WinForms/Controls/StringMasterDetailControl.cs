using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms.Controls
{
    public partial class RichTextBoxEx : RichTextBox
    {
        public RichTextBoxEx()
        {

        }
        public RichTextBoxEx(string selectedText, string textStr)
        {
            BorderStyle = BorderStyle.None;
            this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            this.ContentsResized += RichTextBoxEx_ContentsResized;
            SetDrugNameColor(selectedText, textStr);
        }

        private void RichTextBoxEx_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height;
        }

        private void SetDrugNameColor(string drugName, string message)
        {
            Text = message;

            var index = message.IndexOf(drugName);
            if (index == 0)
            {
                Select(0, drugName.Length);
            }
        }
    }
}
