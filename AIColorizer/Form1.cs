using System;
using System.Collections.Generic;
using System.ComponentModel;
using DeepAI;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace AIColorizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool iscolorized = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            richTextBox1.BorderStyle = BorderStyle.None;

        }
        bool fileSelected = false;
        string pathfull = "";
        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = openFileDialog.FileName;
                pathfull = filepath;
            }
            fileSelected = true;
            string extractedfilename = null;

            // using the method
            extractedfilename = Path.GetFileName(pathfull);
            button2.ForeColor = Color.Lime;
            button1.ForeColor = Color.Lime;
            button1.Text="Colorize: " + extractedfilename;
            //selectedfile.Text = extractedfilename;
            //label2.Text = pathfull;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fileSelected==true)
            {
                
            }
        }
        string downloadpath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (fileSelected == true)
            {
                DeepAI_API api = new DeepAI_API(apiKey: "f324a1e7-c056-4613-9e27-9cfb0ccad096");

                StandardApiResponse resp = api.callStandardApi("colorizer", new
                {
                    image = File.OpenRead(pathfull),
                });
                Console.Write(api.objectAsJsonString(resp));
                string result = api.objectAsJsonString(resp);
                string cleanresult;
                string fullcleanresult;
                cleanresult = result.Remove(0, 55);
                fullcleanresult = cleanresult.Remove(cleanresult.Length - 4);
                button3.ForeColor = Color.Lime;
                label1.ForeColor = Color.Lime;
                iscolorized = true;
                string extractedfilename = Path.GetFileName(pathfull);
                string downloadpath = @"C:\Temp\" + extractedfilename;
                
                using (WebClient client = new WebClient())
                {
                    
                    client.DownloadFile(new Uri(fullcleanresult), downloadpath);
                }
                System.Drawing.Image img = System.Drawing.Image.FromFile(downloadpath);
                int dividedwidth = img.Width / 2;
                int dividedheight = img.Height / 2;
                pictureBox1.Size = new Size(dividedwidth, dividedheight);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                pictureBox1.BackgroundImage = new Bitmap(downloadpath);
                label2.Text = downloadpath;
                //MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
            }
            else
            {
                MessageBox.Show("No file selected!", "AIColorizer");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (iscolorized==true)
            {
                //string currentpath = System.Environment.CurrentDirectory;
                string välikäsi = "";
                string currentpath = Directory.GetCurrentDirectory();
                string photopath = label2.Text;
                string getfilename = Path.GetFileName(photopath);
                string fileextension = Path.GetExtension(photopath);
                välikäsi = getfilename.Remove(getfilename.Length - 4);
                string lopussa = välikäsi+"colorized";
                string lopullinen = lopussa + fileextension;
                string fix = @"\\" + lopullinen;
                File.Copy(photopath, currentpath + fix);
            }
            else
            {
                MessageBox.Show("Please colorize an image first!", "AIColorizer");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://elmeri.xyz/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/elmerij");
        }
    }
}
