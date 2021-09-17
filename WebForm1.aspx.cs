using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace WebApplication3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void Fill()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            SqlDataAdapter da = new SqlDataAdapter("select * from items", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "items");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack==false)
            {
                Fill();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fname = FileUpload1.FileName;
            string fpath = "~/images/" + fname;
            FileUpload1.SaveAs(Server.MapPath(fpath));
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into items values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + fpath + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Fill();

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            Label l1 = (Label)row.FindControl("Label1");
            Label l2 = (Label)row.FindControl("Label2");
            Label l3 = (Label)row.FindControl("Label3");
            Label l4 = (Label)row.FindControl("Label4");
            TextBox1.Text = l1.Text;
            TextBox2.Text = l2.Text;
            TextBox3.Text = l3.Text;
            TextBox4.Text = l4.Text;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}