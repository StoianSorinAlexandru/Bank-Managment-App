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
    public partial class FormMain : Form
    {

        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuerry;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        string CNP;

        DataSet DS = new DataSet();

        String selFromName;
        String selFromSurName;
        String selFromCNP;
        String selFromEmail;
        String selFromTel;
        String selFromDep;
        String selFromSucc;

        public FormMain(string CNP)
        {
            this.CNP = CNP;

            InitializeComponent();
            StartupForm();

        }

        String GetSqlCommand2()
        {
            String aux = "SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, client.data_nasterii, client.sursa_venit, client.numar_conturi FROM client, persoana WHERE";

            ///SELECTAREA DUPA CNP
            if (textBox19.Text != null && textBox19.Text.Length > 0)
            {

                Console.Write("merge!\n");
                aux = aux + " persoana.CNP = " + '"' + textBox19.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.CNP IN (SELECT CNP from persoana) AND ";
            }

            ///SELECTAREA DUPA NUME
            if (textBox18.Text != null && textBox18.Text.Length > 0)
            {
                aux = aux + " persoana.nume = " + '"' + textBox18.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.nume IN (SELECT nume from persoana) AND ";
            }


            ///SELECTAREA DUPA PRENUME
            if (textBox12.Text != null && textBox12.Text.Length > 0)
            {
                aux = aux + " persoana.prenume = " + '"' + textBox12.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.prenume IN (SELECT prenume from persoana) AND ";
            }


            ///SELECTAREA DUPA EMAIL
            if (textBox20.Text != null && textBox20.Text.Length > 0)
            {
                aux = aux + " persoana.email = " + '"' + textBox20.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.email IN (SELECT email from persoana) AND ";
            }

            ///SELECTAREA DUPA TELEFON
            if (textBox21.Text != null && textBox21.Text.Length > 0)
            {
                aux = aux + " persoana.numar_telefon = " + '"' + textBox21.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.numar_telefon IN (SELECT numar_telefon from persoana) AND ";
            }

            ///SELECTAREA DUPA SURSA DE VENIT
            if (textBox22.Text != null && textBox22.Text.Length > 0)
            {
                aux = aux + " client.sursa_venit = " + '"' + textBox22.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " client.sursa_venit IN (SELECT sursa_venit from client) AND ";
            }

            ///SELECTAREA DUPA DATA NASTERII
            if (textBox23.Text != null && textBox23.Text.Length > 0)
            {
                aux = aux + " client.data_nasterii = " + '"' + textBox23.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " client.data_nasterii IN (SELECT data_nasterii from client) AND ";
            }

            ///SELECTAREA DUPA NUMAR CONTURI
            if (textBox24.Text != null && textBox24.Text.Length > 0)
            {
                aux = aux + " client.numar_conturi = " + '"' + textBox24.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " client.numar_conturi IN (SELECT numar_conturi from client) AND ";
            }
            aux = aux + " client.CNP = persoana.CNP";
            return aux;
        }
        String GetSqlCommand3()
        {
            String aux = "SELECT notificari.detalii, persoana.CNP, persoana.nume, persoana.prenume FROM notificari, persoana WHERE";

            ///SELECTAREA DUPA CNP
            if (textBox26.Text != null && textBox26.Text.Length > 0)
            {

                Console.Write("merge!\n");
                aux = aux + " persoana.CNP = " + '"' + textBox26.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.CNP IN (SELECT CNP from persoana) AND ";
            }

            ///SELECTAREA DUPA NUME
            if (textBox27.Text != null && textBox27.Text.Length > 0)
            {
                aux = aux + " persoana.nume = " + '"' + textBox27.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.nume IN (SELECT nume from persoana) AND ";
            }


            ///SELECTAREA DUPA PRENUME
            if (textBox28.Text != null && textBox28.Text.Length > 0)
            {
                aux = aux + " persoana.prenume = " + '"' + textBox28.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.prenume IN (SELECT prenume from persoana) AND ";
            }


            ///SELECTAREA DUPA NUMAR CONTURI
            if (textBox25.Text != null && textBox25.Text.Length > 0)
            {
                aux = aux + " notificari.detalii = " + '"' + textBox25.Text + '"' + " AND ";

            }
            else
            {
                aux = aux + " notificari.detalii IN (SELECT detalii from notificari) AND ";
            }
            aux = aux + " notificari.idPersoana = persoana.idPersoana";
            return aux;
        }
        String GetSqlCommand()
        {
            String aux = "SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, angajat.sucursala, angajat.departament, angajat.salariu, angajat.norma FROM angajat, persoana WHERE";

            ///SELECTAREA DUPA CNP
            if (selFromCNP != null && selFromCNP.Length > 0)
            {

                Console.Write("merge!\n");
                aux = aux + " persoana.CNP = " + '"' + selFromCNP + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.CNP IN (SELECT CNP from persoana) AND ";
            }

            ///SELECTAREA DUPA NUME
            if (selFromName != null && selFromName.Length > 0)
            {
                aux = aux + " persoana.nume = " + '"' + selFromName + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.nume IN (SELECT nume from persoana) AND ";
            }


            ///SELECTAREA DUPA PRENUME
            if (selFromSurName != null && selFromCNP.Length > 0)
            {
                aux = aux + " persoana.prenume = " + '"' + selFromSurName + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.prenume IN (SELECT prenume from persoana) AND ";
            }


            ///SELECTAREA DUPA EMAIL
            if (selFromEmail != null && selFromEmail.Length > 0)
            {
                aux = aux + " persoana.email = " + '"' + selFromEmail + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.email IN (SELECT email from persoana) AND ";
            }

            ///SELECTAREA DUPA TELEFON
            if (selFromTel != null && selFromTel.Length > 0)
            {
                aux = aux + " persoana.numar_telefon = " + '"' + selFromTel + '"' + " AND ";

            }
            else
            {
                aux = aux + " persoana.numar_telefon IN (SELECT numar_telefon from persoana) AND ";
            }

            ///SELECTAREA DUPA DEPARTAMENT
            if (selFromDep != null && selFromDep.Length > 0)
            {
                aux = aux + " angajat.departament = " + '"' + selFromDep + '"' + " AND ";

            }
            else
            {
                aux = aux + " angajat.departament IN (SELECT departament from angajat) AND ";
            }

            ///SELECTAREA DUPA SUCURSALA
            if (selFromSucc != null && selFromSucc.Length > 0)
            {
                aux = aux + " angajat.sucursala = " + '"' + selFromSucc + '"' + " AND ";

            }
            else
            {
                aux = aux + " angajat.sucursala IN (SELECT sucursala from angajat) AND ";
            }

            aux = aux + " angajat.CNP = persoana.CNP";
            return aux;
        }

        private void StartupForm()
        {
            textBox9.Text = "# INTRODUCETI O OPEATIE";
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT notificari.detalii, persoana.CNP, persoana.nume, persoana.prenume FROM notificari, persoana WHERE notificari.idPersoana = persoana.idPersoana; ", sqc);
            //MySqlCommand cmd = new MySqlCommand("SELECT detalii FROM notificari; ", sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(read);


            read.Close();
            sqc.Close();


            dataGridView2.DataSource = dt;

        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ///DATE PERSONALE
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {

                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT CNP, nume, prenume, numar_telefon, email, adresa FROM persoana WHERE persoana.CNP = " + '"' + this.CNP + '"' + " ", sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                while (read.Read())
                {
                    textBox15.Text = (read[0].ToString());
                    textBox17.Text = (read[1].ToString());
                    textBox16.Text = (read[2].ToString());
                    textBox13.Text = (read[3].ToString());
                    textBox14.Text = (read[4].ToString());
                    textBox8.Text = (read[5].ToString());
                }
                read.Close();
                sqc.Close();


            }
            ///NOTIFICARI
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT notificari.detalii, persoana.CNP, persoana.nume, persoana.prenume FROM notificari, persoana WHERE notificari.idPersoana = persoana.idPersoana; ", sqc);
                //MySqlCommand cmd = new MySqlCommand("SELECT detalii FROM notificari; ", sqc);

                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(read);


                read.Close();
                sqc.Close();

                dataGridView2.DataSource = dt;

            }
            ///CAUTARE IN SISTEM
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
            {

            }
            ///OPERATII PE BAZA DE DATE
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                //MySqlConnection sqc = new MySqlConnection();
                //sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                //sqc.Open();

                //MySqlCommand cmd = new MySqlCommand(textBox9.Text, sqc);
                //MySqlDataReader read = cmd.ExecuteReader();

                //string posOperation = textBox9.Text.Substring(0, 6);
                //posOperation = posOperation.ToUpper();
                //bool result = posOperation.Equals("SELECT");
                //MessageBox.Show("WORK IN PROGRESS");
                //if (result)
                //{
                //    MySqlDataReader read2 = cmd.ExecuteReader();

                //    DataTable dt = new DataTable();

                //    while (read2.Read())
                //    {
                //        int i = 0;
                //        while (read2[i] != null)
                //        {
                //            dt.Rows.Add(read2[i]);
                //            ++i;
                //        }
                //    }

                //    read2.Close();
                //    sqc.Close();

                //    dataGridView3.DataSource = dt;

                //}



            }
            ///MODIFICARE DATE UTILIZATORI
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage5"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, client.data_nasterii, client.sursa_venit, client.numar_conturi FROM client, persoana WHERE client.CNP = persoana.CNP;", sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(read);


                read.Close();
                sqc.Close();

                dataGridView4.DataSource = dt;
            }
            ///ANGAJATI
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage6"])
            {
                MySqlConnection sqc = new MySqlConnection();
                sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

                sqc.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT persoana.CNP, persoana.nume, persoana.prenume, persoana.numar_telefon, persoana.email, angajat.sucursala, angajat.departament, angajat.salariu, angajat.norma FROM angajat, persoana WHERE angajat.CNP = persoana.CNP;", sqc);
                MySqlDataReader read = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(read);
               

                read.Close();
                sqc.Close();

                dataGridView1.DataSource = dt;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

            selFromSurName = textBox2.Text;
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            selFromName = textBox1.Text;

        }

        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            selFromDep = textBox5.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            selFromEmail = textBox4.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string myQuerry = GetSqlCommand(); 
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand(myQuerry, sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(read);


            read.Close();
            sqc.Close();

            dataGridView1.DataSource = dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormAdminAngajat frm = new FormAdminAngajat(textBox3.Text, this.CNP);
            frm.Show();
            this.Hide();
        }
        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            selFromCNP = textBox3.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

            selFromTel = textBox7.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            selFromSucc = textBox6.Text;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("update persoana set nume = '{0}', prenume = '{1}', adresa = '{4}', numar_telefon = '{3}', email = '{2}' where CNP = " + '"' + this.CNP + '"'
                , textBox17.Text,
                textBox16.Text,
                textBox14.Text,
                textBox13.Text,
                textBox8.Text), sqc);


            cmd.ExecuteNonQuery();

            MessageBox.Show("Actualizare reusita!");
            sqc.Close();
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";
            try
            {
                sqc.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MySqlCommand cmd = new MySqlCommand(textBox9.Text, sqc);
            try
            {
                DataTable dt = new DataTable();

                String aux = textBox9.Text.Substring(0, 6);
                aux.ToUpper();
                if (aux.Equals("SELECT"))
                {
                    MySqlDataReader read = cmd.ExecuteReader();
                    dt.Load(read);
                    read.Close();
                }
                else
                    cmd.ExecuteNonQuery();

                sqc.Close();

                dataGridView3.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //String selFromName;
            //String selFromSurName;
            //String selFromCNP;
            //String selFromEmail;
            //String selFromTel;
            //String selFromDep;
            //String selFromSucc;
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox10.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox11.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Apasati pe sageata din fata tuplei!");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //String selFromName;
            //String selFromSurName;
            //String selFromCNP;
            //String selFromEmail;
            //String selFromTel;
            //String selFromDep;
            //String selFromSucc;
            try
            {
                textBox25.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox26.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox27.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                textBox28.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Apasati pe sageata din fata tuplei!");
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String selFromName;
            String selFromSurName;
            String selFromCNP;
            String selFromEmail;
            String selFromTel;
            String selFromDep;
            String selFromSucc;
            try
            {
                textBox18.Text = dataGridView4.SelectedRows[0].Cells[1].Value.ToString();
                textBox12.Text = dataGridView4.SelectedRows[0].Cells[2].Value.ToString();
                textBox19.Text = dataGridView4.SelectedRows[0].Cells[0].Value.ToString();
                textBox20.Text = dataGridView4.SelectedRows[0].Cells[4].Value.ToString();
                textBox21.Text = dataGridView4.SelectedRows[0].Cells[3].Value.ToString();
                textBox22.Text = dataGridView4.SelectedRows[0].Cells[6].Value.ToString();
                textBox23.Text = dataGridView4.SelectedRows[0].Cells[5].Value.ToString();
                textBox24.Text = dataGridView4.SelectedRows[0].Cells[7].Value.ToString();
                textBox23.Text = textBox23.Text.Substring(0, 10);
                textBox23.Text = Reverse(textBox23.Text);
                StringBuilder sb = new StringBuilder(textBox1.Text);
                char[] ch = textBox23.Text.ToCharArray();
                char aux;
                aux = ch[3];
                ch[3] = ch[0]; // index starts at 0!
                ch[0] = aux;

                aux = ch[2];
                ch[2] = ch[1]; // index starts at 0!
                ch[1] = aux;

                aux = ch[6];
                ch[6] = ch[5]; // index starts at 0!
                ch[5] = aux;

                aux = ch[8];
                ch[8] = ch[9]; // index starts at 0!
                ch[9] = aux;

                textBox23.Text = new string(ch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Doriti sa iesiti din aplicatie?", "Mesaj", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
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

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Sunteti sigur ca vreti sa va delogati?", "Mesaj", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Form1 fr = new Form1();
                fr.Show();
                this.Hide();
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string myQuerry = GetSqlCommand2();

            MessageBox.Show(myQuerry);
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand(myQuerry, sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(read);


            read.Close();
            sqc.Close();

            dataGridView4.DataSource = dt;
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormAdminUtil frm = new FormAdminUtil(textBox19.Text, this.CNP);
            frm.Show();
            this.Hide();
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string myQuerry = GetSqlCommand3();
            MySqlConnection sqc = new MySqlConnection();
            sqc.ConnectionString = "server=localhost;user=root;database=bancarproject;port=3306;password=12345678";

            sqc.Open();

            MySqlCommand cmd = new MySqlCommand(myQuerry, sqc);
            MySqlDataReader read = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(read);


            read.Close();
            sqc.Close();

            dataGridView2.DataSource = dt;
        }
    }
}
