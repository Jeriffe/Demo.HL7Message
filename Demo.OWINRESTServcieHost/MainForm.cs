using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.OWINRESTServcieHost
{
    public partial class MainForm : Form
    {
        private IDisposable myServer;
        private string baseAddress = "http://localhost:9000/";

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            myServer= WebApp.Start<Startup>(url: baseAddress);

            RefreshUI(false);

            // Start OWIN host 
            //using (WebApp.Start<Startup>(url: baseAddress))
            //{
            //    // Create HttpClient and make a request to api/values 
            //    HttpClient client = new HttpClient();

            //    var response = client.GetAsync(baseAddress + "api/values").Result;

            //    Console.WriteLine(response);
            //    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            //    Console.ReadLine();
            //}

        }

        private void RefreshUI(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = !enable;
            btnTest.Enabled = !enable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshUI(true);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            myServer.Dispose();

            RefreshUI(true);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var response = client.GetAsync(baseAddress + "api/values").Result;

            Console.WriteLine(response);
            MessageBox.Show(response.ToString());
            MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            Console.ReadLine();
        }
    }
}
