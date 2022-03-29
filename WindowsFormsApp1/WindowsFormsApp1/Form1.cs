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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlDataAdapter DAdepartment, DAdoctor;
        DataSet ds = new DataSet();
        SqlCommandBuilder cb;
        BindingSource bsDoctor, bsDepartment;

        public Form1()
        { //DESKTOP-6R40TAM
            InitializeComponent();
            connection = new SqlConnection("Data Source=DESKTOP-6R40TAM; Initial Catalog=Hospital; Integrated Security=true;");
            //table1= ConfigurationManager.AppSettings.Get("table1");
            
            DAdepartment = new SqlDataAdapter("SELECT * FROM Department", connection);
            DAdoctor = new SqlDataAdapter("SELECT * FROM Doctor", connection);
            cb = new SqlCommandBuilder(DAdoctor);

            DAdepartment.Fill(ds, "Department");
            DAdoctor.Fill(ds, "Doctor");

            DataRelation dr = new DataRelation("FK__Doctor__departme__538D5813",
            ds.Tables["Department"].Columns["dep_id"],
            ds.Tables["Doctor"].Columns["department_id"]);
            ds.Relations.Add(dr);
            
            bsDepartment = new BindingSource();
            bsDepartment.DataSource = ds;
            bsDepartment.DataMember = "Department";

            bsDoctor = new BindingSource();
            bsDoctor.DataSource = bsDepartment;
            bsDoctor.DataMember = "FK__Doctor__departme__538D5813";

            dgvDepartment.DataSource = bsDepartment;
            dgvDoctor.DataSource = bsDoctor;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DAdoctor.Update(ds, "Doctor");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //DataGridViewRow row = dgvDoctor.Rows[e.RowIndex];
            foreach (DataGridViewRow item in this.dgvDoctor.SelectedRows)
            {
                dgvDoctor.Rows.RemoveAt(item.Index);
            }
            DAdoctor.Update(ds, "Doctor");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DAdoctor.Update(ds, "Doctor");
        }

    }
}
