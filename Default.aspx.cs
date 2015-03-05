using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    clsDatabase ClsDb = new clsDatabase();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        foreach (GridViewRow row in GridView1.Rows)
        {
            string sql_ins = "INSERT INTO KRUNGSRI_BILL_PAYMENT(TYPE_PAY,STD_NAME,TOTAL,ADMIS_ID,CIT_ID,DATE_PAY) VALUES ('KTB','" + row.Cells[1].Text.ToString() + "','" + row.Cells[5].Text.ToString().Trim() + "','" + row.Cells[2].Text.ToString().Trim() + "','" + row.Cells[3].Text.ToString() + "','"+ Calendar1.SelectedDate + "')";
            Response.Write(sql_ins);
            Response.Write("</br>");
            ClsDb.QueryExecuteNonQuery(sql_ins);         
        }
         //   ClsDb.Close();
        //Label1.Text = "Records inserted successfully";
        string sql_del;
        sql_del = "DELETE FROM KRUNGSRI_BILL_PAYMENT WHERE ADMIS_ID = '&nbsp;' ";
        ClsDb.QueryExecuteNonQuery(sql_del);
        ClsDb.Close();
    }
}