using FarzinSecurityLibrary;
using KeyACOMLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ICAN.FollowingPasswordUtility;
//using LkhJkqtTH7fCtLpZX6;
//using GJ1InQL1Ddwy04idP6;
//using qju0khufNomcLhTLjH;


namespace EncryptDecryptFarzin
{
    public partial class Form1 : Form
    {
      //  private KeyAInterface _KeyALib = (KeyAInterface) new KeyAInterfaceClass();
        //enc.EncryptDataString(ref
        private CFarzinEncryption  cFarzinEncryption;
        private FollowingPassword enc = new FollowingPassword();
        private CFarzinEncryption test;
        public string ConnectionString { get; set; }
        public Form1()
        {
            InitializeComponent();
            // G63vsTqX4pRNTntxEy.Vcma6LkzbGGOh();
            this.test = new CFarzinEncryption("appFarzinSoftKey ver 4.0.0", new int[0]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strErrorMsg = "";
            
            cFarzinEncryption.EncryptFile(textBox2.Text);
            //MessageBox.Show(this.cFarzinEncryption.ResultMessage.ToString());
       

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            string strErrorMSG = "";
            try
            {
                if (this.IsValidSerialNumberForUsedSoftWare(out strErrorMSG))
                    return;
                int num = (int)MessageBox.Show(strErrorMSG, "مديريت هوشمند خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                int num1 = (int)this.openFileDialog1.ShowDialog();
                this.textBox1.Text = this.openFileDialog1.FileName;
                if (this.textBox1.Text == "" && !File.Exists(this.textBox1.Text ))
                {
                    int num2 = (int)MessageBox.Show("لطفاً مسير ورودي به را به درستي انتخاب نمائيد", "مديريت هوشمند خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!this.ProcessConnectionString(this.textBox1.Text))
                {
                    int num3 = (int)MessageBox.Show("وجود اشكال در بدست آوردن رشته اتصال به بانك اطلاعاتي", "مديريت هوشمند خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (!this.IsValidLicense(this.textBox1.Text))
                {
                    int num4 = (int)MessageBox.Show("به دليل نداشتن ليسانس عمليات مربوطه متوقف گرديد", "مديريت هوشمند خطا", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }
        }
        public bool ProcessConnectionString(string FarzinLicense)
        {
            XmlDocument xmlDocument = new XmlDocument();
            MemoryStream memoryStream = new MemoryStream(this.cFarzinEncryption.EncryptDecryptLicense(FarzinLicense, false));
            StreamReader streamReader = new StreamReader((Stream)memoryStream, Encoding.UTF8);
            xmlDocument.LoadXml(streamReader.ReadToEnd());
            memoryStream.Close();
            streamReader.Close();
            this.ConnectionString = xmlDocument.SelectSingleNode("//ConnectionString").InnerText;
            return this.ConnectionString != "";
        }
        private bool DecprytFile(string filepath, string strErrorMsg)
        {
            strErrorMsg = "";
            this.cFarzinEncryption = new CFarzinEncryption("appFarzinSoftKey ver 4.0.0", new int[0]);

            try
            {
                this.cFarzinEncryption.DecryptFile("E:\\Farzin\\Upload\\EntityUploadedFiles\\public_import\\1394\\4\\2\\img[14-4-1-993]_8754.tif", ref strErrorMsg);
                if (this.cFarzinEncryption.HasError)
                {
                    strErrorMsg = "انجام عمليات تبديل با مشكل روبرو شد !!!";
                    return false;
                }
            }
            catch (Exception exception)
            {
                strErrorMsg = exception.Message;
                return false;
            }
            return true;
        }
        private string ByteArrayToHexString(byte[] Data, bool bInclud0x)
        {
            string str = "";
            for (int index = 0; index < Data.Length; ++index)
                str = index != 0 ? (Data[index].ToString("X").Length != 1 || !bInclud0x ? (!bInclud0x ? (Data[index].ToString("X").Length != 1 ? str + Data[index].ToString("X") : str + "0" + Data[index].ToString("X")) : str + " ,0x" + Data[index].ToString("X")) : str + " ,0x0" + Data[index].ToString("X")) : (Data[index].ToString("X").Length != 1 || !bInclud0x ? (!bInclud0x ? (Data[index].ToString("X").Length != 1 ? Data[index].ToString("X") : "0" + Data[index].ToString("X")) : "0x" + Data[index].ToString("X")) : "0x0" + Data[index].ToString("X"));
            return str;
        }
        public bool IsValidSerialNumberForUsedSoftWare(out string strErrorMSG)
        {
            strErrorMSG = "";
            string[] source = new string[] { "DCEFF894B4C0" };
            DateTime time = System.Convert.ToDateTime("2016/04/02 00:00:00");
            if (DateTime.Now > time)
            {
                strErrorMSG = "بازه زماني استفاده از اين نرم افزار به پايان رسيده است لطفاً با شركت فني و مهندسي آي كن تماس بگيريد";
                return false;
            }
            //string[] st =new string[]{"E:\\Farzin\\Upload\\EntityUploadedFiles\\public_import\\1394\\4\\2\\img[14-4-1-993]_87542.tif"} ;
            //this.DecprytFile("E:\\Farzin\\Upload\\EntityUploadedFiles\\public_import\\1394\\4\\2\\img[14-4-1-993]_8754.tif", st[0]);

            try
            {
                this.cFarzinEncryption = new CFarzinEncryption("appFarzinSoftKey ver 5.0.0", new int[0]);
            }
            catch (Exception)
            {
                strErrorMSG = "قفل شناسائي نشد !!!";
                return false;
            }
            Array serial = null;
            /*if (this._KeyALib.GetModuleCount() != 1)
            {
                strErrorMSG = "قفل شناسائي نشد !!!";
                return false;
            }
            this._KeyALib.OpenDevice(0);
            this._KeyALib.GetSerialNumber(out serial);*/
            string strSerialNumber = this.ByteArrayToHexString((byte[])serial, false);
            //MessageBox.Show(strSerialNumber);
            if (!source.Any<string>(s => (s.ToLower() == strSerialNumber.ToLower())))
            {
                strErrorMSG = "قفل شناسائي نشد !!!";
                return false;
            }
            return true;
        }

        public bool IsValidLicense(string FarzinLicense)
        {
            XmlDocument document = new XmlDocument();
            MemoryStream stream = new MemoryStream(this.cFarzinEncryption.EncryptDecryptLicense(FarzinLicense, false));
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            document.LoadXml(reader.ReadToEnd());
            stream.Close();
            reader.Close();
            bool flag = false;
            XmlNode node = document.SelectSingleNode("//Module[@Id='5']");
            flag = node.Attributes["Active"].Value.ToLower() == "true";
            object obj2 = node.Attributes["ExpireDate"].Value;
            if (obj2 != null)
            {
                DateTime time = System.Convert.ToDateTime(obj2);
                DateTime.Compare(DateTime.Now, time);
            }
            //Response.Write(empDoc.InnerXml);
            //MessageBox.Show();
            return flag;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            textBox2.Text = openFileDialog2.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
            textBox3.Text = saveFileDialog2.FileName;
        }
        private bool EncryptFile(string filepath, out string strErrorMsg)
        {
            strErrorMsg = "";
            if (!this.IsValidSerialNumberForUsedSoftWare(out strErrorMsg))
                return false;
            try
            {
                this.cFarzinEncryption = new CFarzinEncryption("appFarzinSoftKey ver 5.0.0", new int[0]);
            }
            catch (Exception ex)
            {
                strErrorMsg = "اشكال در شناسائي قفل !!!";
                return false;
            }
            if (!File.Exists(filepath))
            {
                strErrorMsg = "فايل يافت نشد !!!";
                return false;
            }
            if (this.cFarzinEncryption != null)
            {
                try
                {
                    this.cFarzinEncryption.EncryptFile(filepath);
                    if (this.cFarzinEncryption.HasError)
                    {
                        strErrorMsg = "انجام عمليات تبديل با مشكل روبرو شد !!!";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    strErrorMsg = ex.Message;
                    return false;
                }
            }
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int pass = enc.DecryptPassword("uY1q3+KQqaS3eOsxsYvVjA==");
            textBox4.Text = test1("15234").ToString();
            //uY1q3+KQqaS3eOsxsYvVjA==
           // uY1q3+KQqaS3eOsxsYvVjA==
            //MessageBox.Show();
            
        }
        public string test1(string test2)
        {
           // int num;
            //string t = this.test.EncryptDataString(ref test2);
            //test2=test2.Substring(0,Math.Min(test2.Length,26));

            return test.EncryptDataString(ref test2);

        }

    }
}
