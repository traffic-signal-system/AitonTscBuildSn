using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AitonTscBuildSn
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool sendUdpNoReciveData(string ipstr, int port, byte[] hex)
        {
            int recv;
            Socket server = null;
            byte[] bytes = new byte[65535];
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(ipstr), port);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                // string str = "Hello Server!";
                //bytes = System.Text.Encoding.ASCII.GetBytes(str);
                server.SendTimeout = 4000;
                server.ReceiveTimeout = 4000;
                server.SendTo(hex, ip);
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)(sender);
                //server.Bind(sender);
                recv = server.ReceiveFrom(bytes, ref Remote);
                Console.WriteLine("Message received from {0}", Remote.ToString());
                //str = System.Text.Encoding.ASCII.GetString(bytes, 0, recv);
                Console.WriteLine("Message: " + bytes[0]);
                server.Close();
                server = null;
                if (bytes[0] == 134)
                    return false;
                else
                    return true;
            }
            catch (Exception exce)
            {
                Console.WriteLine(exce.ToString());
                server.Close();
                server = null;
                return false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             byte[] BUILD_SN = { 0x81, 0xE4, 0x00, 0x02 };
             bool b = sendUdpNoReciveData(txtIP.Text.Trim(), 5435, BUILD_SN);
            if(b)
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }
    }
}
