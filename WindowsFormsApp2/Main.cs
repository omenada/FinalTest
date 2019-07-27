using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using system.microsoft.office;


namespace WindowsFormsApp2
{
    public partial class Main : Form
    {
        myInfo userInfo;

        string ConnectionString = "Server=INSTRUCTORIT;Database=TournamentManager;User Id=ProfileUser;Password=ProfileUser2019";

        public Main( myInfo infoFromLogin)
        {
            InitializeComponent();
            userInfo = infoFromLogin; 
            lblCreateBy.Text = userInfo.loginMessage;
            

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                string sql = "insert into Teams (TeamName,CoachName,DirectorName,AddressLine1,AddressLine2,PostCode,City,ContactNumber,EmailAddress,CreatedBy) values(@TeamName,@CoachName,@DirectorName,@AddressLine1,@AddressLine2,@PostCode,@City,@ContactNumber,@EmailAddress,@CreatedBy)";
                cnn.Open();
                
                if (txtTeamName.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the TeamName! ");
                }
                if (txtCoachName.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Coach Name ");
                }
                if (txtDirectorName.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Director Name  ");
                }
                if (txtAddress1.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Address2 ");
                }
                if (txtAddress2.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Address1 ");
                }
                if (txtpotalCode.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Postal Code ");
                }
                if (txtCity.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the City ");
                }
                if (txtContactNum.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Contact Number ");
                }
                if (txtEmail.Text == string.Empty)
                {
                    MessageBox.Show("Please insert the Email ");
                }
                

                else
                {

                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.AddWithValue("@TeamName", txtTeamName.Text);
                        cmd.Parameters.AddWithValue("@CoachName", txtCoachName.Text);
                        cmd.Parameters.AddWithValue("@DirectorName", txtDirectorName.Text);
                        cmd.Parameters.AddWithValue("@AddressLine1", txtAddress1.Text);
                        cmd.Parameters.AddWithValue("@AddressLine2", txtAddress2.Text);
                        cmd.Parameters.AddWithValue("@PostCode", txtpotalCode.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtContactNum.Text);
                        cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@CreatedBy", lblCreateBy.Text);
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("The Information was successfuly saved !! ");

                        txtTeamName.Clear();
                        txtCoachName.Clear();
                        txtDirectorName.Clear();
                        txtAddress1.Clear();
                        txtAddress2.Clear();
                        txtpotalCode.Clear();
                        txtCity.Clear();
                        txtContactNum.Clear();
                        txtEmail.Clear();


                    }
                }
            }
        }

        private void btnload_Click(object sender, EventArgs e)
        {

            using (SqlConnection cnn = new SqlConnection(ConnectionString))

                try
                {

                    cnn.Open();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from Teams order by TeamName Asc";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    cnn.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally

                {
                    if (cnn.State == ConnectionState.Open)
                    {
                        cnn.Close();
                    }
                }

            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            
                DataSet team = new DataSet();
                team.ReadXml(System.IO.Path.GetFullPath(@"../../Data/team.xml"));
        }

        }
        
        //private void dataGridView1_AllowUserToDeleteRowsChanged(object sender, EventArgs e)
        //{
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        CheckBox chkbox = (CheckBox)row.FindControl("CheckBox1");
        //        if (chkbox.Checked == true)
        //        {
        //            // Your Code
        //        }
        //    }
        //}
    }
}
