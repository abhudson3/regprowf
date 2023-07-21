using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace regprowf
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string crnInput = textBox1.Text;

            string mbUsername = textBox2.Text;
            
            string mbPassword = textBox3.Text;

            

            WebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);

            regChecker(mbUsername, mbPassword, crnInput, driver);

            static void fileChecker(string mbUsername, string mbPassword, string crnInput, WebDriver driver)
            {

                StreamReader inFile = new StreamReader("output.txt");
                string line = inFile.ReadLine();
                if (int.Parse(line) > 0)
                {
                    driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
                    System.Threading.Thread.Sleep(3000);
                    driver.FindElement(By.Id("UserID")).SendKeys(mbUsername);
                    System.Threading.Thread.Sleep(500);
                    driver.FindElement(By.Id("PIN")).SendKeys(mbPassword);
                    driver.FindElement(By.XPath("/html/body/div[3]/form/p/input")).Click();
                    System.Threading.Thread.Sleep(500);
                    driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
                    driver.FindElement(By.XPath("/html/body/div[3]/form/input")).Click();
                    driver.FindElement(By.XPath("/html/body/div[3]/form/table[3]/tbody/tr[2]/td[1]/input[2]")).SendKeys(crnInput);//crn
                    System.Threading.Thread.Sleep(500);
                    driver.FindElement(By.XPath("/html/body/div[3]/form/input[19]")).Click();
                    System.Threading.Thread.Sleep(5000);
                    driver.Close();
                    System.Console.WriteLine("Thanks for using RegiPro! You should have been registered!");
                    Environment.Exit(0);
                }
                else
                {
                    regChecker(mbUsername, mbPassword, crnInput, driver);
                }
            }


            static void regChecker(string mbUsername, string mbPassword, string crnInput, WebDriver driver)
            {
                var content = driver.FindElement(By.XPath("/html/body/div[3]/table[1]/tbody/tr[2]/td/table[1]/tbody/tr[2]/td[3]"));
                StreamWriter outFile = new StreamWriter("output.txt");
                outFile.WriteLine(content.Text);
                outFile.Close();
                driver.Navigate().Refresh();

                System.Threading.Thread.Sleep(60000);
                fileChecker(mbUsername, mbPassword, crnInput, driver);
            }
        }
    }
}
