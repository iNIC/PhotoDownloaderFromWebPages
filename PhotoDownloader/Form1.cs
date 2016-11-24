using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;

namespace PhotoDownloader
{
    public partial class MainForm : Form
    {

     
        

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnToDo_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            string webData = wc.DownloadString(tbUrlSite.Text);
            fieldInput.ScrollBars = ScrollBars.Vertical;
            fieldInput.Text = webData;


            List<string> listImg = new List<string>();
          
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webData);
            foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//img"))
            {
                
                listImg.Add(img.GetAttributeValue("src", null));
                
            }

            dropDownImageList.DataSource = listImg.ToList();


            try
            {
                List<string> pcPath = new List<string>();
                for (int counter = 6; counter < (listImg.Select(x => x)).Count(); counter++)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(listImg.ElementAt(counter), "D://image" + counter + ".png");
                    pcPath.Add("D://image" + counter + ".png");
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show("Деякі файли можуть бути не збережені на Ваг комп'ютер. Під час роботи програми виникла помилка веб-сторінки!");
            }

           
           
    
            

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

         
            pictureBox.ImageLocation = dropDownImageList.Text;
        }
    }
}
