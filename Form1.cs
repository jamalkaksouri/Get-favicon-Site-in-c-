using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Focus();
                return;
            }
            var url = new Uri("http://" + textBox1.Text);
            WebRequest request = (HttpWebRequest)WebRequest.Create("http://" + url.Host + "/favicon.ico");

            MemoryStream memStream;

            using (var response = request.GetResponse().GetResponseStream())
            {
                memStream = new MemoryStream();
                var buffer = new byte[1024];
                var byteCount = 0;

                do
                {
                    if (response != null) byteCount = response.Read(buffer, 0, buffer.Length);
                    memStream.Write(buffer, 0, byteCount);
                } while (byteCount > 0);
            }

            var bm = new Bitmap(Image.FromStream(memStream));

            {
                var ic = Icon.FromHandle(bm.GetHicon());
                Icon = ic;
                pictureBox1.Image = ic.ToBitmap();
            }
        }
    }
}
