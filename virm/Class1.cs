using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace virm
{
    class Class1
    {
   
            public bool is_valid_line(string s)
            {
                Boolean a;
                if (s.StartsWith("*") && (s.Length >= 62) && (s.EndsWith("1")))
                {
                    a = true;
                }
                else { a = false; }
                return a;
            }
            public string cle_cpt(String cpt)
            {///return string value!!!
                string cle = "";
                char[] tt = cpt.ToCharArray();
                if (is_nbr(cpt))
                {
                    int s = 0; int j = 4;
                    for (int i = 9; i >= 0; i--)
                    {
                        s += (int.Parse(tt[i].ToString())) * j;
                        j++;
                    }
                    cle = s.ToString().Substring(s.ToString().Length - 2);
                }
                return cle;
            }
            public string rib_cpt(string cpt)
            {
                string rib = "";
                if (is_nbr(cpt))
                {
                    Int64 ribb = (97 - (((long.Parse(cpt)) * 100) % 97));
                    if (ribb < 10)
                    { rib = "0" + ribb; }
                    else
                    {
                        if (ribb == 0)
                        { rib = "97"; }
                        else rib = ribb.ToString();
                    }
                }

                return rib;
            }
            public bool is_nbr(string s)
            {
                var cheq = s.All(char.IsDigit);
                return cheq;
            }

        public  bool IsAllLetters(string s)
        {
            foreach (char c in s)
            { 
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }
        public bool is_mot(string s)
            {
           // s.Replace(" ",string.Empty);
          var cheq =Regex.IsMatch(s,"^[A-Za-z '/]+$");
           // var cheq = Regex.IsMatch(s,@"^[\p{L}]+$");
            // var cheq = s.All(char.IsLetter);
            return cheq;
            }
            public string con_ten(String s)
            {
                string ccp = "00799999"; int y; string h; int l = s.Length;
                if (l <= 10) { y = 10 - l; } else y = 0;
                for (int j = 0; j < y; j++)
                {
                    ccp += "0";
                }
                return ccp + s;

            }
            public  string mnt_ten(string s)
            {

                int y; string h; int l = s.Length;
                if (l <= 13)
                {
                    y = 13 - l;
                    for (int j = 0; j < y; j++)
                    {
                        s = "0" + s;
                    }
                }
                return s;

            }
            public  string nbr_seven(string s)
            {

                int y; string h; int l = s.Length;
                if (l <= 7)
                {
                    y = 7 - l;
                    for (int j = 0; j < y; j++)
                    {
                        s = "0" + s;
                    }
                }
                return s;

            }
            public  string mot_spc(string m)
            {
                int l = m.Length;
                int y = 27 - l;
                if (y > 0)
                {
                    for (int i = 0; i < y; i++)
                    {
                        m += " ";
                    }
                }
                return m + "1";
            }
            public  string year_spc(string m)
            {

                int y = 14;

                for (int i = 0; i < y; i++)
                {
                    m += " ";
                }

                return m + "0";
            }
            public  decimal get_som(DataGridView dg)
            {
                decimal som = 0;
                for (int i = 0; i < dg.Rows.Count - 1; i++)
                {
                    som = som + decimal.Parse(dg.Rows[i].Cells[4].Value.ToString());

                }
                return som;
            }
            /* fsts test = new fsts();
                         string s=test.cle_cpt("15998162");
                        long a, b, c, s,d;
                        long k;
                        string cpt = "003010200006835";
                         //char[] tt = cpt.ToCharArray();
                        string rib = "";

                        b = long.Parse(cpt);
                        a = 97-(((long.Parse(cpt)) * 100)%97);
                       // s = a / 97;
                        //c = a % 97;
                        k = 97 - a;
                        if (k == 0)
                        {
                            rib = "97";
                        }
                        if (k < 10)
                        { rib = "0" + k; }
                        else rib = k.ToString();

                        MessageBox.Show(rib);*/
        }
    }


