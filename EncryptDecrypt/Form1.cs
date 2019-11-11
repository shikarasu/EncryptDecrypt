using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace EncryptDecrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Encrypt(string a, byte[] k, byte[] i)
        {
            string result;

            RijndaelManaged rijM = new RijndaelManaged();
            using(MemoryStream mStream = new MemoryStream())
            {
                using(ICryptoTransform encryptor = rijM.CreateEncryptor(k, i))
                {
                    using(CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sWriter = new StreamWriter(cStream))
                        {
                            sWriter.Write(a);
                        }
                    }
                }
                result = System.Convert.ToBase64String(mStream.ToArray());
            }
            rijM.Clear();
            return result;
        }

        
        private string Decrypt(string a, byte[] K, byte[] I)
        {
            string result;
            RijndaelManaged rijM = new RijndaelManaged();
            using (MemoryStream msDecrypt = new MemoryStream(System.Convert.FromBase64String(a)))
            {
                using (ICryptoTransform decryptor = rijM.CreateDecryptor(K, I))
                {
                    using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            rijM.Clear();
            return result;
        }

        private void btnEncrypt_Click_1(object sender, EventArgs e)
        {
            byte[] rijK = Encoding.ASCII.GetBytes("abcdefg_abcdefg_abcdefg_abcdefg_");
            byte[] i = Encoding.ASCII.GetBytes("abcdefg_abcdefg_");

            txtEncryptResult.Text = Encrypt(txtEncryptText.Text, rijK, i);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDecrypt_Click_1(object sender, EventArgs e)
        {
            byte[] rijK = Encoding.ASCII.GetBytes("abcdefg_abcdefg_abcdefg_abcdefg_");
            byte[] i = Encoding.ASCII.GetBytes("abcdefg_abcdefg_");

            txtDecryptResult.Text = Decrypt(txtDecryptText.Text, rijK, i);
        }

        private void txtEncryptResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
