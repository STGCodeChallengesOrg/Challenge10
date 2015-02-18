using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challenge10 {
    public partial class frmMain : Form {

        private const string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public frmMain() {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e) {
            lblResultEncrypt.Text = EncryptText(txtKeyword.Text, txtEncrypt.Text);
            lblResultDecrypt.Text = DecryptText(txtKeyword.Text, txtDecrypt.Text);
        }

        private string EncryptText(string key, string value) {
            
            string cipher = setupCipher(key);

            return applyCyper(value, cipher, ABC);
        }

        private string DecryptText(string key, string value) {
            string cipher = setupCipher(key);

            return applyCyper(value, ABC, cipher);
        }

        private string setupCipher(string key) {

            string theKey = setupKey(key);

            int len = theKey.Length;

            char[] keySorted = theKey.ToCharArray().OrderBy(x => x).ToArray();

            string cipherString = theKey;

            for (int i = 65; i < (65 + 26); i++) {
                if (!theKey.Contains(((char)i).ToString())) {
                    cipherString += ((char)i).ToString();
                }
            }

            List<string> ciphers = new List<string>();

            for (int i = 0; i < 26; i += len) {
                try {
                    ciphers.Add(cipherString.Substring(i, len));
                } catch {
                    ciphers.Add(cipherString.Substring(i));
                }
            }

            string rtnString = string.Empty;
            int pos = 0;

            for (int p = 0; p < theKey.Length; p++) {
                pos = theKey.IndexOf(keySorted[p]);

                for (int c = 0; c < ciphers.Count; c++) {

                    try {
                        rtnString += ciphers[c].Substring(pos, 1);
                    } catch {

                    }
                }
            }

            return rtnString;
        }

        private static string applyCyper(string value, string cipher, string alpha) {

            string rtn = string.Empty;
            int pos = 0;

            foreach (char c in value) {

                try {
                    pos = alpha.IndexOf(c);

                    rtn += (pos >= 0) ? cipher.Substring(pos, 1) : c.ToString();

                } catch {
                    rtn += c.ToString();
                }

            }

            return rtn;
        }


        private string setupKey(string key) {

            List<string> cs = new List<string>();

            foreach (char c in key) {
                if (!cs.Contains(c.ToString())) {
                    cs.Add(c.ToString());
                }
            }

            return string.Join(string.Empty, cs);
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e) {
            txtKeyword.Text = txtKeyword.Text.ToUpper();
            txtKeyword.SelectionStart = txtKeyword.Text.Length;
        }

        private void txtEncrypt_TextChanged(object sender, EventArgs e) {
            txtEncrypt.Text = txtEncrypt.Text.ToUpper();
            txtEncrypt.SelectionStart = txtEncrypt.Text.Length;
        }

        private void txtDecrypt_TextChanged(object sender, EventArgs e) {
            txtDecrypt.Text = txtDecrypt.Text.ToUpper();
            txtDecrypt.SelectionStart = txtDecrypt.Text.Length;
        }
    }
}
