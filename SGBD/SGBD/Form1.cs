using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGBD
{
    public partial class Form1 : Form
    {
        SqlConnection cs =
            new SqlConnection(
                @"Data Source=DESKTOP-IS7SFF6\SQLEXPRESS; Initial Catalog=Camine_Tema_3; Integrated Security=True");

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        BindingSource bs = new BindingSource();

        public Form1()
        {

            InitializeComponent();
        }


        private void buttonConnect_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM CAMINE", cs);
            ds.Clear();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            bs.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Camere WHERE CamineId=@id", cs);
            da.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value =
                Convert.ToInt32(dataGridView1.CurrentCell.Value);
            ds2.Clear();
            da.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0];
            bs.DataSource = ds2.Tables[0];
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            da.UpdateCommand =
                new SqlCommand("Update Camere set NrCamera=@nrCamera, EtajCamera=@etajCamera, NrStudenti=@nrStudenti where CamereId=@id", cs);
            da.UpdateCommand.Parameters.Add("@nrCamera", SqlDbType.Int).Value = Convert.ToInt32(textBoxNrCamera.Text);
            da.UpdateCommand.Parameters.Add("@etajCamera", SqlDbType.Int).Value =
                Convert.ToInt32(textBoxEtajCamera.Text);
            da.UpdateCommand.Parameters.Add("@nrStudenti", SqlDbType.Int).Value = Convert.ToInt32(textBoxStudenti.Text);
            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int).Value =
                Convert.ToInt32(dataGridView2.CurrentCell.Value);
            cs.Open();
            var x = da.UpdateCommand.ExecuteNonQuery();
            cs.Close();
            if (x >= 1)
            {
                MessageBox.Show("The record has been updated");
            }

            ds2.Clear();
            da.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0];
            bs.DataSource = ds2.Tables[0];

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Are you sure?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                da.DeleteCommand = new SqlCommand("Delete from Camere where CamereId=@id", cs);
                da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value =
                    Convert.ToInt32(dataGridView2.CurrentCell.Value);
                cs.Open();
                da.DeleteCommand.ExecuteNonQuery();
                cs.Close();
                ds2.Clear();
                da.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                bs.DataSource = ds2.Tables[0];
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            da.InsertCommand = new SqlCommand("INSERT INTO Camere VALUES (@cameraId, @camineId, @nrCamera, @etajCamera, @nrStudenti)", cs);
            da.InsertCommand.Parameters.Add("@cameraId", SqlDbType.Int).Value = Convert.ToInt32(textBoxId.Text);
            da.InsertCommand.Parameters.Add("@camineId", SqlDbType.Int).Value =
                Convert.ToInt32(dataGridView1.CurrentCell.Value);
            da.InsertCommand.Parameters.Add("@nrCamera", SqlDbType.Int).Value = Convert.ToInt32(textBoxNrCamera.Text);
            da.InsertCommand.Parameters.Add("@etajCamera", SqlDbType.Int).Value = Convert.ToInt32(textBoxEtajCamera.Text);
            da.InsertCommand.Parameters.Add("@nrStudenti", SqlDbType.Int).Value = Convert.ToInt32(textBoxStudenti.Text);
            cs.Open();
            da.InsertCommand.ExecuteNonQuery();
            cs.Close();
            ds2.Clear();
            da.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0];
            bs.DataSource = ds2.Tables[0];
        }
    }
}