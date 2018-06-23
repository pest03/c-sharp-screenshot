using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Net;
using System.IO;

namespace ImageUploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void CaptureMyScreen(string name, string path)

        {

            try

            {
               

                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);


                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);


                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);


                captureBitmap.Save(path + name, ImageFormat.Jpeg);

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            this.WindowState = FormWindowState.Minimized;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string name = RandomString(32) + ".jpg";
            CaptureMyScreen(name, path);
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("ftp username", "ftp password");
                client.UploadFile("ftp://yourftoip" + name, WebRequestMethods.Ftp.UploadFile, path + name); // you gotta specify the path on your ftp server.
                Clipboard.Clear();
                Clipboard.SetText("http://yourftpip/images/" + name); // for example folder images you gotta specify it coresponding with your account 2 lines above.
                File.Delete(path + name);
                MessageBox.Show("Link copied to clipboard");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
