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
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }
        bool isOpen = false;

        private void button1_Click(object sender, EventArgs e) {
            var serviceAddress = "127.0.0.1:10000";
            var serviceName = "MyService";


            // ServiceHost - Реализует узел, используемый моделью программирования модели Windows Communication Foundation (WCF).
            // Используется для настройки и предоставления службы для использования клиентскими приложениями
            var host = new ServiceHost(typeof(LRU_NRU), new Uri($"net.tcp://{serviceAddress}/{serviceName}"));
            if (isOpen) {
                return;
            }
            // NetTcpBinding - Задает безопасную, надежную и оптимизированную привязку, пригодную для обмена данными между компьютерами. 
            var serverBinding = new NetTcpBinding();
            // AddServiceEndpoint - При использовании метода узел службы выполняет проверку по имени конфигурации в описании контракта.
            host.AddServiceEndpoint(typeof(IAlgorithm), serverBinding, "");

           

            host.Open();
            textBox1.Text = "Host has started";
            isOpen = true;
        }
    }
}
