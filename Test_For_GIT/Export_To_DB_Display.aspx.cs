using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Test_For_GIT
{
    public partial class Export_To_DB_Display : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(Object sender, EventArgs e)
        {

            if (FileUpload1.PostedFile != null)
            {
                try
                {
                    string path = string.Concat(Server.MapPath("~/" + FileUpload1.FileName));
                    //FileUpload1.SaveAs(path);
                    // Connection String to Excel Workbook  
                    string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                    using (OleDbConnection con = new OleDbConnection(excelCS))
                    {
                        OleDbCommand cmd = new OleDbCommand("select * from [Sheet1$]", con);
                        con.Open();
                        // Create DbDataReader to Data Worksheet  
                        DbDataReader dr = cmd.ExecuteReader();
                        // SQL Server Connection String  
                        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                        // Bulk Copy to SQL Server   
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
                        bulkInsert.DestinationTableName = "GOT";
                        bulkInsert.WriteToServer(dr);
                        lblMessage.Text = "Your file uploaded successfully";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {

                    lblMessage.Text = Convert.ToString(ex);
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

        protected void gvBind()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("Select * from GOT", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBF.DataSource = ds;
                gvBF.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvBF.DataSource = ds;
                gvBF.DataBind();
                int columncount = gvBF.Rows[0].Cells.Count;
                gvBF.Rows[0].Cells.Clear();
                gvBF.Rows[0].Cells.Add(new TableCell());
                gvBF.Rows[0].Cells[0].ColumnSpan = columncount;
                gvBF.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        protected void btnShowgv_Click(Object sender, EventArgs e)
        {
            gvBind();

        }

        protected void gvBF_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            gvBF.PageIndex = e.NewPageIndex;
            btnShowgv_Click(sender, e);

        }

    }
}