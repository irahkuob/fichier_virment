using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace virm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public String search(String path,String cpt_vrm,String nom) {
            String nom_result ="";
            StreamReader sr = new StreamReader(path);
            bool trouv = true; String line;
            while (((line=sr.ReadLine())!= null)&&(trouv))
             {
                // MessageBox.Show(line.Substring(8,10)+"||"+ cpt_vrm);
                if (cpt_vrm == line.Substring(8, 10))
                {
                    nom_result = line.Substring(21, line.Length - 22);
                    trouv = false;
                    //MessageBox.Show(nom);
                }
                else { nom_result=nom;  }
            
            }
            //sr.Dispose();
            sr.Close();
            return nom_result;
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (textBox2.Text != "")
            {
                Class1 c = new Class1();
                string lignebdd, lignevrm, comptebdd, comptevrm;

                string p = @"C:\bdd.txt";


                string path = textBox2.Text;
                FileInfo fil = new FileInfo(path);
                string name = fil.Name;
                string dir = Path.GetDirectoryName(path) + @"\NEW_" + name;

                StreamReader sr2 = new StreamReader(textBox2.Text);
                StreamWriter sw = new StreamWriter(dir);
                lignevrm = sr2.ReadLine();
                string nombdd = "";
                string lignere = "";
                sw.WriteLine(lignevrm);
                bool trouv = true;
               // try
                //{
                    while ((lignevrm = sr2.ReadLine()) != null)
                    {
                        comptevrm = lignevrm.Substring(9, 10);

                    lignere = lignevrm.Substring(0, 34) + c.mot_spc(search(p, comptevrm, (lignevrm.Substring(34,26)).Trim()));
                   //MessageBox.Show(comptevrm);
                        sw.WriteLine(lignere);

                    }
                    sw.Close();
                    sr2.Dispose();
                    sr2.Close();
                    MessageBox.Show("opération executer avec succée!!");
                
//                }
  //              catch {  MessageBox.Show("الملف غير صالح"); }


                //lignebdd = sr1.ReadLine();



            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfvrm = new OpenFileDialog();
            opfvrm.Title = "SELECT A  virment TXT FILE";
            opfvrm.Filter = "TXT files|*.txt";
            if (opfvrm.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = opfvrm.FileName.ToString();
            }
        }
    }
}
