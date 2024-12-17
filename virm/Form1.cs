using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace virm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StyleDatagridview();

           /* string[] row = new string[] { "1", "Product 1", "1000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "00799999999999999999", "2000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dataGridView1.Rows.Add(row);*/
        }
        void StyleDatagridview()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            label1.Text = ""; label3.Text = ""; label4.Text = ""; validatebar.Text = "";
            label7.Text = ""; label8.Text = ""; label9.Text = "";
            string fline; string line;
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "SELECT A TXT FILE";
            opf.Filter = "TXT files|*.txt";
            string[] rline = new string[7];
            // DataTable dt = new DataTable();
            List<int> nb_faux = new List<int>();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                decimal mon1 = 0; bool fstline = true; int nbr_line; decimal s_line = 0;
                string nom;
                try
                {
                    fline = opf.FileName.ToString();
                    StreamReader sr = new StreamReader(fline);
                    Class1 vr = new Class1();
                    line = sr.ReadLine();
                    nbr_line = int.Parse(line.Substring(34, 7));//nbr tt lignes
                    if (vr.is_nbr(line.Substring(21, 13)))
                    {
                       // label8.Text = line.Substring(21, 13);
                        mon1 = (decimal.Parse(line.Substring(21, 13))) / 100;//somme 1er ligne
                    }
                    else fstline = false;
                    string compt;
                    int s = dataGridView1.Rows.Count-1 ;
                    int f = 0;
                    int corr = 0;
                    decimal mnt;
                    string smnt; bool check = true;
                    while ((line = sr.ReadLine()) != null)
                    {
                        check = true;
                        if (vr.is_valid_line(line))
                        {
                            //corr += 1;
                            rline[0] = s.ToString();
                            compt = line.Substring(1, 18);
                            if (!vr.is_nbr(compt)) { check = false; }//check compt is nbr or not
                            rline[1] = compt;
                            //rline[3] = vr.cle_cpt(compt.Substring(compt.Length-10));
                            rline[2] = line.Substring(19, 2); if (!vr.is_nbr(line.Substring(19, 2))) { check = false; }//check compt is nbr or not
                            nom = line.Substring(34, 26).ToString().Trim();
                            nom.Replace(" ",string.Empty);
                            if (!vr.is_mot(nom))
                            { check = false;
                              //  MessageBox.Show(vr.is_mot(nom).ToString());
                            }//check nom is word or not
                            rline[3] = nom;
                            smnt = line.Substring(21, 13);
                            //if (!vr.is_nbr(line.Substring(21, 13))) { check = false; }//check mnt is nbr or not
                            if (vr.is_nbr(smnt))
                            {
                                mnt = (decimal.Parse(smnt)) / 100;
                                s_line += mnt;
                                rline[4] = mnt.ToString();
                            }
                            else { rline[4] = smnt.ToString(); check = false; }
                            // rline[6] = vr.rib_cpt(compt.Substring(3,15));
                            //MessageBox.Show(compt.Substring(3, 15));
                            if (vr.rib_cpt(compt.Substring(3, 15)).ToString() != rline[2])
                            {
                                if (vr.cle_cpt(compt.Substring(3, 15)).ToString() != rline[2])
                                {
                                    check = false;
                                    //MessageBox.Show("خطأ في RIP/B" + "\n رقم السطر" + s);
                                }
                            }
                            //
                            //  dt.Rows.Add(rline);
                            dataGridView1.Rows.Add(rline);
                           // MessageBox.Show(s.ToString());
                        }
                        else
                        {
                            check = false;
                            MessageBox.Show("format incorrect"+s);
                            f += 1;
                        }
                        if (check == false)
                        {
                            nb_faux.Add(s);
                            f += 1;
                        }
                        s += 1;
                    }
                    if (s_line != mon1) { /*MessageBox.Show(s_line.ToString());*/ fstline = false; }
                   // dataGridView1.DataSource = dt;
                    label7.Text = s_line.ToString();
                   label8.Text = mon1.ToString();
                    label9.Text = (s_line - mon1).ToString();
                    if ((s-f) != nbr_line) { fstline = false; }
                    if (fstline) { validatebar.Text = "Fichier Validée"; validatebar.ForeColor = Color.Green; } else { validatebar.Text = "Fichier Non Validée";validatebar.ForeColor = Color.Red; }
                
                    label1.Text = s.ToString();
                    corr = s - f;
                    label3.Text = corr.ToString();
                    label4.Text = f.ToString();
                    //MessageBox.Show("the valid lines =" + s + "the invalide lines=" + f);
                    sr.Close();
                }
                catch { MessageBox.Show("format incorrect!!"); }
            }
            // MessageBox.Show(nbr_faux.ToString());
            foreach (int ch in nb_faux)
            {
                dataGridView1.Rows[int.Parse(ch.ToString())].DefaultCellStyle.BackColor = Color.Firebrick;
                //MessageBox.Show(ch.ToString());
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            label1.Text = ""; label3.Text = ""; label4.Text = "";validatebar.Text = "";
            label7.Text = ""; label8.Text = ""; label9.Text = "";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
           
            Class1 vr = new Class1();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string line ;
                using (StreamWriter myStream = new StreamWriter(saveFileDialog1.OpenFile()))
                {//if (validatebar.Text =="Fichier Validée") { 
                    line = "*00000000000030002453" + vr.mnt_ten((vr.get_som(dataGridView1) * 100).ToString()) + vr.nbr_seven((dataGridView1.Rows.Count - 1).ToString()) + DateTime.Now.Month.ToString("00") + vr.year_spc(DateTime.Now.Year.ToString("0000"));
                    myStream.WriteLine(line);
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {//if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Firebrick) { break; }
                       // MessageBox.Show((decimal.Round((decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())))).ToString());
                        line = "*" + dataGridView1.Rows[i].Cells[1].Value.ToString() + dataGridView1.Rows[i].Cells[2].Value.ToString() +
                        vr.mnt_ten((decimal.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) * 100).ToString()) +
                        vr.mot_spc(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        myStream.WriteLine(line);

                    }
                    MessageBox.Show("Succée");//}
                    myStream.Close();
                }
            }
        }

        private void comparerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
            
        }

        private void àProposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("هذا البرنامج من تطوير §علي بوخاري");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
