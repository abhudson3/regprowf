using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Net.Mail;
using System.Text;

namespace regprowf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1!= null&& emailInput != null) 
            {
                MessageBox.Show("Thanks for using Regipro! You should get an email within a few minutes of the class getting an open slot");
                string crnInput = textBox1.Text;
                string toEmail = emailInput.Text;
                WebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);
                regChecker(toEmail, driver);
                static void fileChecker(string toEmail, WebDriver driver)
                {
                    StreamReader inFile = new StreamReader("output.txt");
                    string line = inFile.ReadLine();
                    if (int.Parse(line) > 0)
                    {
                        SendMail(toEmail);
                        driver.Close();
                        Environment.Exit(0);
                    }
                    else
                    {
                        regChecker(toEmail, driver);
                    }
                }
                static void regChecker(string toEmail, WebDriver driver)
                {
                    var content = driver.FindElement(By.XPath("/html/body/div[3]/table[1]/tbody/tr[2]/td/table[1]/tbody/tr[2]/td[3]"));
                    //StreamWriter outFile = new StreamWriter("output.txt");
                    //outFile.WriteLine(content.Text);
                    //outFile.Close();
                    Console.WriteLine(content.Text);
                    using(StreamWriter writeFile = new StreamWriter("output.txt"))
                    {
                        writeFile.WriteLine(content.Text);
                    }
                    driver.Navigate().Refresh();

                    System.Threading.Thread.Sleep(60000);
                    fileChecker(toEmail, driver);
                }
                static void SendMail(string toEmail)
                {
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("regiprotest2@gmail.com");
                    mail.To.Add(toEmail);
                    mail.Subject = "Class drop notification";
                    mail.Body = "Class has an open slot";

                    SmtpClient SmtpServer = new SmtpClient();
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Credentials = new NetworkCredential("regiprotest2@gmail.com", "stsjpserbidjwwby");
                    SmtpServer.Timeout = 20000;
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }

            }
            else
            {
                MessageBox.Show("Make sure you input a crn and email");
            }
        }

        private void emailInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}