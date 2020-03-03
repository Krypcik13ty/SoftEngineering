using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace SoftEngineering
{

    public partial class WebForm1 : System.Web.UI.Page
    {
       private string contu = "datasource=127.0.0.1; port=3306; username=root; password=; database=scheduledb; CharSet=utf8";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["contu"].ConnectionString);
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string usern, peseln, passcode;
                usern = TextBox1.Text;
                peseln = TextBox2.Text;
                passcode = TextBox3.Text;
                string q = "update [Password] set [passcode]='" + passcode + "' where Login='" + usern + "' and PESEL='" + peseln + "'";
                cmd = new SqlCommand(q, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Label1.Visible = true;
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}