using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;



namespace Student_HM_ADO.net
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            string str = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(str);

        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into student values(@nm,@ct,@pr)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@nm", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@ct",txtCity.Text);
                cmd.Parameters.AddWithValue("@pr", Convert.ToDecimal(txtPercentage.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update student set StudentName=@nm , City=@ct ,Percentage=@pr where Rollno=@rl";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@nm", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@ct", txtCity.Text);
                cmd.Parameters.AddWithValue("@pr", Convert.ToDecimal(txtPercentage.Text));
                cmd.Parameters.AddWithValue("@rl", Convert.ToInt32(txtRollno.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from student where Rollno=@rl";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rl", Convert.ToInt32(txtRollno.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from student where Rollno=@rl";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rl", Convert.ToInt32(txtRollno.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtStudentName.Text = dr["StudentName"].ToString();
                        txtCity.Text =dr["City"].ToString();
                        txtPercentage.Text = dr["Percentage"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowallstudent_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from student";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
