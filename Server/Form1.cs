using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using LibraryWithLRU_NRU;

namespace Server {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var serviceAddress = "127.0.0.1:10000";
            var serviceName = "MyService";

            var host = new ServiceHost(typeof(LRU_NRU), new Uri($"net.tcp://{serviceAddress}/{serviceName}"));
            var serverBinding = new NetTcpBinding();
            host.AddServiceEndpoint(typeof(IAlgorithm), serverBinding, "");
            host.Open();
            textBox1.Text = "Host has started";

        }
    }
}
