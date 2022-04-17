using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace ProiectBRC
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string nume, string prenume, string CNP)
        {
            InitializeComponent();

            label28.Hide();
            
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Select CNP,departament from angajat where CNP=@cnp", sqc);
            cmd.Parameters.AddWithValue("@cnp", CNP);
            MySqlDataReader read = cmd.ExecuteReader();

            label24.Text = nume + " " + prenume;

            while (read.Read())
            {
                label26.Text = read[1].ToString();
            }

            sqc.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Doriti sa iesiti din cont?", "Mesaj", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Form1 frm = new Form1();
                    frm.Show();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM client", sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Columns.Add("Id Client");
                dt.Columns.Add("CNP");
                dt.Columns.Add("Data nasterii");
                dt.Columns.Add("Sursa venit");
                dt.Columns.Add("Tranzactii online");
                dt.Columns.Add("Numar conturi");

                while (read.Read())
                {
                    dt.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5]);
                }
                read.Close();
                sqc.Close();

                dataGridView2.DataSource = dt;
            }
            else
            {
                label28.Hide();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox22.Clear();
                textBox18.Clear();
                textBox19.Clear();
                textBox20.Clear();
                textBox25.Clear();
                textBox26.Clear();
                textBox27.Clear();
                textBox28.Clear();
                textBox29.Clear();
                textBox30.Clear();

                listBox1.Items.Clear();

                checkBox1.Checked = false;
            }
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tranzactie", sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("Id Tranzactie");
                dt.Columns.Add("IBAN sursa");
                dt.Columns.Add("IBAN destinatar");
                dt.Columns.Add("Valoarea");
                dt.Columns.Add("Status Tranzactie");

                while(read.Read())
                {
                    dt.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[4].ToString(), read[5].ToString());
                }
                read.Close();

                dataGridView1.DataSource = dt;

                sqc.Close();
            }

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage7"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM card where stare_card=@stare", sqc);
                cmd.Parameters.AddWithValue("@stare", "PENDING");
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Columns.Add("Id Card");
                dt.Columns.Add("Stare Card");
                dt.Columns.Add("Id cont");

                while (read.Read())
                {
                    dt.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString());
                }
                read.Close();

                dataGridView5.DataSource = dt;

                sqc.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(textBox4.Text.Length > 6)
            {
                string prelucrat = textBox4.Text.ToString();
                string an = "";
                string luna = "";
                string zi = "";

                an = textBox4.Text.Substring(1,2);
                luna = textBox4.Text.Substring(3, 2);
                zi = textBox4.Text.Substring(5, 2);

                if(Convert.ToInt32(an) < 5)
                {
                    an = "20" + an;
                    textBox8.Text = zi + "-" + luna + "-" + an;
                }
                else if(Convert.ToInt32(an) > 5 && Convert.ToInt32(an) < 18)
                {
                    textBox4.Clear();
                    textBox8.Clear();
                }
                else
                {
                    an = "19" + an;
                    textBox8.Text = zi + "-" + luna + "-" + an;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nume = textBox2.Text.ToString();
            string prenume = textBox3.Text.ToString();
            string CNP = textBox4.Text.ToString();
            string adresa = textBox5.Text.ToString();
            string telefon = textBox6.Text.ToString();
            string email = textBox7.Text.ToString();
            string sursavenit = textBox9.Text.ToString();

            int allowTranz = 0;
            if (checkBox1.Checked)
                allowTranz = 1;

            string prelucrat = textBox8.Text.ToString();
            string an = "";
            string luna = "";
            string zi = "";
            zi = textBox8.Text.Substring(0, 2);
            luna = textBox8.Text.Substring(3, 2);
            an = textBox8.Text.Substring(6, 4);
            string datanasterii = an + "-" + luna + "-" + zi;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO persoana(CNP,nume,prenume,adresa,numar_telefon,email) " +
                "values (@CNP,@nume,@prenume,@adresa,@numar_telefon,@email)", sqc);
            cmd.Parameters.Add("@CNP", MySqlDbType.VarChar);
            cmd.Parameters["@CNP"].Value = CNP;
            cmd.Parameters.AddWithValue("@nume", nume);
            cmd.Parameters.AddWithValue("@prenume", prenume);
            cmd.Parameters.AddWithValue("@adresa", adresa);
            cmd.Parameters.AddWithValue("@numar_telefon", telefon);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();

            MySqlCommand cmd2 = new MySqlCommand("INSERT INTO client(CNP,data_nasterii,sursa_venit,tranzactii_online,numar_conturi) " +
                "values (@CNP,@data_nasterii,@sursa_venit,@tranzactii_online,@numar_conturi)", sqc);

            cmd2.Parameters.AddWithValue("@CNP", CNP);
            cmd2.Parameters.AddWithValue("@data_nasterii", datanasterii);
            cmd2.Parameters.AddWithValue("@sursa_venit", sursavenit);
            cmd2.Parameters.AddWithValue("@tranzactii_online", allowTranz);
            cmd2.Parameters.AddWithValue("@numar_conturi", 1);
            cmd2.ExecuteNonQuery();

            sqc.Close();

            Random rnd = new Random();
            label28.Show();
            textBox22.Text = rnd.Next(1000, 9999).ToString();
        }

        int idCont = -1;
        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox14.Text;
            string prename = textBox13.Text;
            string CNP = textBox12.Text;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Select idClient,numar_conturi from client where CNP = @cnp", sqc);
            cmd.Parameters.AddWithValue("@cnp", CNP);
            MySqlDataReader read = cmd.ExecuteReader();

            int idlacont = -1;
            int nrConturi = -1;

            while(read.Read())
            {
                idlacont = Convert.ToInt32(read[0].ToString());
                idCont = idlacont;
                nrConturi = Convert.ToInt32(read[1].ToString());
            }
            read.Close();
            textBox18.Text = nrConturi.ToString();

            MySqlCommand cmd1 = new MySqlCommand("Select IBAN, numerar from cont where idCont = @id", sqc);
            cmd1.Parameters.AddWithValue("@id", idlacont);
            MySqlDataReader read1 = cmd1.ExecuteReader();

            while(read1.Read())
            {
                textBox19.Text = read1[0].ToString();
                textBox20.Text = read1[1].ToString();
            }
            read1.Close();
            
            DataTable dt = new DataTable();
            int index = 1;
            MySqlCommand cmd2 = new MySqlCommand("Select IBAN,tip_cont,stare_card_cont,numerar from cont where idClient=@id", sqc);
            cmd2.Parameters.AddWithValue("@id", idlacont);
            MySqlDataReader read2 = cmd2.ExecuteReader();

            dt.Columns.Add("Nr. Crt");
            dt.Columns.Add("IBAN");
            dt.Columns.Add("Tip cont");
            dt.Columns.Add("Stare card");
            dt.Columns.Add("Numerar");


            while (read2.Read())
            {
                dt.Rows.Add(index, read2[0].ToString(), read2[1].ToString(), read2[2].ToString(), read2[3].ToString());
                index++;
            }
            read2.Close();

            dataGridView4.DataSource = dt;

            sqc.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string CNP = textBox21.Text;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Select idClient from client where CNP = @cnp", sqc);
            cmd.Parameters.AddWithValue("@cnp", CNP);
            MySqlDataReader read = cmd.ExecuteReader();
            
            int idlacont = -1;
            while(read.Read())
            {
                idlacont = Convert.ToInt32(read[0].ToString());
            }
            read.Close();
            
            MySqlCommand cmd1 = new MySqlCommand("Select IBAN from cont where idClient=@id", sqc);
            cmd1.Parameters.AddWithValue("@id", idlacont);
            MySqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                listBox1.Items.Add(read1[0].ToString());
            }
            read.Close();
            sqc.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iban = listBox1.SelectedItem.ToString();

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Select numerar,tip_cont from cont where IBAN = @iban", sqc);
            cmd.Parameters.AddWithValue("@iban", iban);
            MySqlDataReader read = cmd.ExecuteReader();

            while(read.Read())
            {
                textBox25.Text = read[0].ToString();
                label35.Text = read[1].ToString();
            }

            sqc.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Ti am schimbat astea ca dadea exception daca nu ii double - Radu
            string iban = listBox1.SelectedItem.ToString();
            /*int newSum =  Convert.ToInt32(textBox26.Text);
            int oldSum = Convert.ToInt32(textBox25.Text);*/

            double newSum =  Convert.ToDouble(textBox26.Text);
            double oldSum = Convert.ToDouble(textBox25.Text);

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Update cont set numerar=@newsum where IBAN=@iban", sqc);
            cmd.Parameters.AddWithValue("@iban", iban);
            cmd.Parameters.AddWithValue("@newsum", oldSum + newSum);
            cmd.ExecuteNonQuery();

            textBox25.Text = (Convert.ToDouble(textBox25.Text) + newSum).ToString();

            sqc.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Asta crapa cand vreau sa imi fac un cont nou
            string CNP = textBox12.Text;
            string IBAN = textBox19.Text;
            double oldSum = Convert.ToDouble(textBox20.Text);
            double newSum = Convert.ToDouble(textBox27.Text);
            int nrConturi = 0;
            
            if (oldSum > newSum)
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                double tbinst = oldSum - newSum;

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("Update cont set numerar=@newsum where IBAN=@iban", sqc);
                cmd.Parameters.AddWithValue("@iban", IBAN);
                cmd.Parameters.AddWithValue("@newsum", tbinst);
                cmd.ExecuteNonQuery();

                MySqlCommand cmd1 = new MySqlCommand("Select numar_conturi from client where CNP=@cnp", sqc);
                cmd1.Parameters.AddWithValue("@cnp", CNP);
                MySqlDataReader read = cmd1.ExecuteReader();

                while (read.Read())
                {
                    nrConturi = Convert.ToInt32(read[0].ToString());
                }
                read.Close();

                MySqlCommand cmd2 = new MySqlCommand("Update client set numar_conturi=@newCont where CNP=@cnp", sqc);
                cmd2.Parameters.AddWithValue("@newCont", nrConturi + 1);
                cmd2.Parameters.AddWithValue("@cnp", CNP);
                cmd2.ExecuteNonQuery();

                if (listBox2.SelectedItem.ToString() == "economii")
                {
                    MySqlCommand cmd3 = new MySqlCommand("Select COUNT(*) from cont where idClient=@id and tip_cont=@tip", sqc);
                    cmd3.Parameters.AddWithValue("@id", idCont);
                    cmd3.Parameters.AddWithValue("@tip", "economii");
                    MySqlDataReader read1 = cmd3.ExecuteReader();

                    int nrCont = -1;
                    while (read1.Read())
                    {
                        nrCont = Convert.ToInt32(read1[0].ToString());
                    }
                    read1.Close();
                    string IBANnou = "RO49BRCXY" + idCont.ToString() + nrCont.ToString();
                    
                    MySqlCommand cmd4 = new MySqlCommand("Insert into cont(IBAN,idClient,tip_cont,stare_card_cont,numerar)" +
                        "values(@iban,@idclient,@tip_cont,@stare,@numerar)", sqc);
                    cmd4.Parameters.AddWithValue("@iban", IBANnou);
                    cmd4.Parameters.AddWithValue("@idclient", idCont);
                    cmd4.Parameters.AddWithValue("@tip_cont", "economii");
                    cmd4.Parameters.AddWithValue("@stare", "incopatibil");
                    cmd4.Parameters.AddWithValue("@numerar", newSum);
                    cmd4.ExecuteNonQuery();
                    label42.Text = IBANnou;
                    textBox20.Text = tbinst.ToString();
                }
                else if (listBox2.SelectedItem.ToString() == "facturi")
                {
                    MySqlCommand cmd3 = new MySqlCommand("Select COUNT(*) from cont where idClient=@id and tip_cont=@tip", sqc);
                    cmd3.Parameters.AddWithValue("@id", idCont);
                    cmd3.Parameters.AddWithValue("@tip", "facturi");
                    MySqlDataReader read1 = cmd3.ExecuteReader();

                    int nrCont = -1;
                    while (read1.Read())
                    {
                        nrCont = Convert.ToInt32(read1[0].ToString());
                    }
                    read1.Close();
                    string IBANnou = "RO49BRCXZ" + idCont + nrCont;

                    MySqlCommand cmd4 = new MySqlCommand("Insert into cont(IBAN,idClient,tip_cont,stare_card_cont,numerar)" +
                        "values(@iban,@idclient,@tip_cont,@stare,@numerar)", sqc);
                    cmd4.Parameters.AddWithValue("@iban", IBANnou);
                    cmd4.Parameters.AddWithValue("@idclient", idCont);
                    cmd4.Parameters.AddWithValue("@tip_cont", "facturi");
                    cmd4.Parameters.AddWithValue("@stare", "incopatibil");
                    cmd4.Parameters.AddWithValue("@numerar", newSum);
                    cmd4.ExecuteNonQuery();
                    label42.Text = IBANnou;
                    textBox20.Text = tbinst.ToString();
                }

                sqc.Close();
            }
            else
            {
                MessageBox.Show("Nu aveti suficienti bani in contul curent!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string CNP = textBox16.Text;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("Select idClient from client where CNP = @cnp", sqc);
            cmd.Parameters.AddWithValue("@cnp", CNP);
            MySqlDataReader read = cmd.ExecuteReader();

            int idlacont = -1;

            while (read.Read())
            {
                idlacont = Convert.ToInt32(read[0].ToString());
            }
            read.Close();

            MySqlCommand cmd1 = new MySqlCommand("Select IBAN, numerar from cont where idCont = @id", sqc);
            cmd1.Parameters.AddWithValue("@id", idlacont);
            MySqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                textBox29.Text = read1[0].ToString();
                textBox28.Text = read1[1].ToString();
            }
            read1.Close();

            DataTable dt = new DataTable();
            int index = 1;
            MySqlCommand cmd2 = new MySqlCommand("Select tip_depozit,dobanda,numerar from depozit where idClient=@id", sqc);
            cmd2.Parameters.AddWithValue("@id", idlacont);
            MySqlDataReader read2 = cmd2.ExecuteReader();

            dt.Columns.Add("Nr. Crt");
            dt.Columns.Add("tip_depozit");
            dt.Columns.Add("Dobanda");
            dt.Columns.Add("Numerar");
            
            while(read2.Read())
            {
                dt.Rows.Add(index, read2[0].ToString(), read2[1].ToString(), read2[2].ToString());
                index++;
            }
            read2.Close();

            dataGridView3.DataSource = dt;

            sqc.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double dobanda = 0;
            int tipdepozit = -1;
            string CNP = textBox16.Text;
            string IBAN = textBox29.Text;
            int newSum = Convert.ToInt32(textBox30.Text);
            int oldSum = Convert.ToInt32(textBox28.Text);

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqc;

            sqc.Open();
            
            if(listBox3.SelectedItem.ToString() == "1")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 1);
                tipdepozit = 1;
            }
            else if(listBox3.SelectedItem.ToString() == "3")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 3);
                tipdepozit = 3;
            }
            else if(listBox3.SelectedItem.ToString() == "6")
            {
                cmd.CommandText = "Select dobanda from tip_depozit where type_depozit=@tip";
                cmd.Parameters.AddWithValue("@tip", 6);
                tipdepozit = 6;
            }
            MySqlDataReader read = cmd.ExecuteReader();

            while(read.Read())
            {
                dobanda = Convert.ToDouble(read[0].ToString());
            }
            read.Close();

            MySqlCommand cmd1 = new MySqlCommand("Select idClient from client where CNP = @cnp", sqc);
            cmd1.Parameters.AddWithValue("@cnp", CNP);
            MySqlDataReader read1 = cmd1.ExecuteReader();

            int idlacont = -1;

            while (read1.Read())
            {
                idlacont = Convert.ToInt32(read1[0].ToString());
            }
            read1.Close();

            MySqlCommand cmd2 = new MySqlCommand("Insert into depozit(idClient,tip_depozit,dobanda,numerar) " +
                "values(@id,@tip,@dobanda,@numerar) ", sqc);
            cmd2.Parameters.AddWithValue("@id", idlacont);
            cmd2.Parameters.AddWithValue("@tip", tipdepozit);
            cmd2.Parameters.AddWithValue("@dobanda", dobanda*10);
            cmd2.Parameters.AddWithValue("@numerar", Convert.ToInt32(newSum + newSum * dobanda));
            cmd2.ExecuteNonQuery();

            MySqlCommand cmd3 = new MySqlCommand("Update cont set numerar=@newsum where IBAN=@iban", sqc);
            cmd3.Parameters.AddWithValue("@newsum", oldSum - newSum);
            cmd3.Parameters.AddWithValue("@iban", IBAN);
            cmd3.ExecuteNonQuery();

            sqc.Close();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox31.Text = dataGridView4.CurrentCell.Value.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string IBANcotcurent = textBox19.Text;
            string IBAN = textBox31.Text;
            int sum = 0;
            int ok = 1;
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("Select tip_cont,numerar from cont where IBAN=@iban",sqc);
            cmd.Parameters.AddWithValue("@iban", IBAN);
            MySqlDataReader read = cmd.ExecuteReader();

            while(read.Read())
            {
                if(read[0].ToString() == "curent")
                {
                    MessageBox.Show("Nu se poate sterge contul curent!");
                    ok = 0;
                    break;
                }
                else
                {
                    sum = Convert.ToInt32(read[1].ToString());
                }
            }
            read.Close();

            if (ok == 1) 
            {
                MySqlCommand cmd2 = new MySqlCommand("Update cont set numerar=numerar+@newsum where IBAN=@iban", sqc);
                cmd2.Parameters.AddWithValue("@iban", IBANcotcurent);
                cmd2.Parameters.AddWithValue("@newsum", Convert.ToInt32(sum - sum*0.02));
                cmd2.ExecuteNonQuery();
                
                MySqlCommand cmd1 = new MySqlCommand("Delete from cont where IBAN=@iban", sqc);
                cmd1.Parameters.AddWithValue("@iban", IBAN);
                cmd1.ExecuteNonQuery();

                textBox31.Clear();
            }
            sqc.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView3.CurrentCell.RowIndex;
            int col = dataGridView3.CurrentCell.ColumnIndex;

            textBox32.Text = dataGridView3.CurrentCell.Value.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double aux = Convert.ToDouble(textBox32.Text.ToString());
            int sum = Convert.ToInt32(aux);

            string IBANcotcurent = textBox29.Text;

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("Update cont set numerar=numerar+@newsum where IBAN=@iban", sqc);
            cmd.Parameters.AddWithValue("@iban", IBANcotcurent);
            cmd.Parameters.AddWithValue("@newsum", Convert.ToInt32(sum - sum * 0.01));
            cmd.ExecuteNonQuery();

            MySqlCommand cmd1 = new MySqlCommand("Delete from depozit where numerar=@sum", sqc);
            cmd1.Parameters.AddWithValue("@sum", aux);
            cmd1.ExecuteNonQuery();

            sqc.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox33.Text = dataGridView1.CurrentCell.Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("Update tranzactie set status_tran=@stat where idTranzactie=@id", sqc);
            cmd.Parameters.AddWithValue("@stat", "ACCEPTED");
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox33.Text));
            cmd.ExecuteNonQuery();

            MySqlCommand cmd1 = new MySqlCommand("Select valoare,IBAN_sursa,IBAN_destinatar from tranzactie where idTranzactie=@id", sqc);
            cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(textBox33.Text));
            MySqlDataReader read1 = cmd1.ExecuteReader();

            double suma = 0;
            string IBANS = "";
            string IBAND = "";
            while(read1.Read())
            {
                suma = Convert.ToDouble(read1[0].ToString());
                IBANS = read1[1].ToString();
                IBAND = read1[2].ToString();
            }
            read1.Close();

            MySqlCommand cmd2 = new MySqlCommand("Update cont set numerar=numerar-@sum where IBAN=@IBANS", sqc);
            cmd2.Parameters.AddWithValue("@sum", suma);
            cmd2.Parameters.AddWithValue("@IBANS", IBANS);
            cmd2.ExecuteNonQuery();

            MySqlCommand cmd3 = new MySqlCommand("Update cont set numerar=numerar+@sum where IBAN=@IBAND", sqc);
            cmd3.Parameters.AddWithValue("@sum", suma);
            cmd3.Parameters.AddWithValue("@IBAND", IBAND);
            cmd3.ExecuteNonQuery();

            sqc.Close();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox34.Text = dataGridView5.CurrentCell.Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("Update card set stare_card=@stat where idCard=@id", sqc);
            cmd.Parameters.AddWithValue("@stat", "ACCEPTED");
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox34.Text));
            cmd.ExecuteNonQuery();

            sqc.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tranzactie where IBAN_sursa=@iban", sqc);
            cmd.Parameters.AddWithValue("@iban", textBox10.Text);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Id Tranzactie");
            dt.Columns.Add("IBAN sursa");
            dt.Columns.Add("IBAN destinatar");
            dt.Columns.Add("Valoarea");
            dt.Columns.Add("Status Tranzactie");

            while (read.Read())
            {
                dt.Rows.Add(read[0].ToString(), read[1].ToString(), read[2].ToString(), read[4].ToString(), read[5].ToString());
            }
            read.Close();

            dataGridView1.DataSource = dt;

            sqc.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
