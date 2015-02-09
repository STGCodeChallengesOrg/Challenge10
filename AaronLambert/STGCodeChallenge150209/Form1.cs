using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STGCodeChallenge150209
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DoTheWork();
        }

        private void DoTheWork()
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string Keyword = txtKeyword.Text.ToUpper().Trim();
            string Text = txtText.Text.Trim();

            if (string.IsNullOrWhiteSpace(Keyword) || Keyword.Length > 7 || string.IsNullOrWhiteSpace(Text) || Text.Length > 255)
            {
                MessageBox.Show("Invalid input was detected.");
                return;
            }

            // Remove duplicate letters from the Keyword
            Keyword = string.Concat(Keyword.ToCharArray().Distinct());
            int keylen = Keyword.Length;
            
            // Build the transposition cipher
            // Create the grid of letters
            char[] keygrid = new char[26];
            int indx = keylen;
            Array.Copy(Keyword.ToCharArray(), keygrid, keylen);
            foreach (char c in alphabet)
            {
                if (!keygrid.Contains(c))
                {
                    keygrid[indx] = c;
                    indx++;
                }
            }

            // Create dictionary for transposition
            Dictionary<char, char> dict = new Dictionary<char, char>();
            string keysort = string.Concat(Keyword.OrderBy(c => c));
            indx = 0;
            for (int i = 0; i < keylen; i++)
            {
                int j = Keyword.IndexOf(keysort[i]);
                while (j < 26)
                {
                    dict[keygrid[j]] = alphabet[indx];
                    j += keylen;
                    indx++;
                }
            }

            StringBuilder sb = new StringBuilder();
            string s;
            foreach (char c in Text)
            {
                char uc = char.ToUpper(c);
                if (dict.ContainsKey(uc))
                {
                    s = dict[uc].ToString();
                    sb.Append(char.IsLower(c) ? s.ToLower() : s);
                }
                else
                {
                    sb.Append(' ');
                }
            }

            MessageBox.Show(sb.ToString());
        }
    }
}
