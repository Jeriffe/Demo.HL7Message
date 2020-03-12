using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.DTOs;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Demo.HL7MessageParser.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
               Application.Run(new MainForm());
            //   Application.Run(new Form2());
            //// Application.Run(new HL7MessageParserFormTest());

            //var result = new List<string>().CheckIfSelectedItemExists();
        }

        private static int FindIDX(int index)
        {
            if (index == 1)
            {
                return 0;
            }

            return -1;
        }

        static int Max_Retry_Count = 5;
        private static string GetPatientEnquiry(string caseno)
        {
            int retryCount = 0;

            while (true)
            {
                try
                {
                    if (retryCount == 4)
                    {
                        return "sss";
                    }
                }
                catch
                {
                    retryCount++;

                    if (retryCount == Max_Retry_Count)
                    {
                        throw;
                    }
                }
            }
        }



    }



    public static class EX
    {
        public static bool CheckIfSelectedItemExists(this List<string> list)
        {
            return true;
            //return list.SelectedItems != null 
            //    && lvItemList.SelectedItems.Count > 0 
            //    && !String.IsNullOrEmpty(lvItemList.SelectedItems[0].Text);
        }
    }
}
