using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Web.Configuration;

public partial class clsDatabase : System.Web.UI.Page
{
    private OracleConnection objConn;
    private OracleCommand objCmd;
    private OracleTransaction Trans;
    private String strConnString;

    public clsDatabase()
    {
        strConnString = WebConfigurationManager.ConnectionStrings["ConnectionString_Root"].ConnectionString;
    }

    public OracleDataReader QueryDataReader(String strSQL)
    {
        OracleDataReader dtReader;
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();

        objCmd = new OracleCommand(strSQL, objConn);
        dtReader = objCmd.ExecuteReader();
        return dtReader; //*** Return DataReader ***//
    }

    public DataSet QueryDataSet(String strSQL)
    {
        DataSet ds = new DataSet();
        OracleDataAdapter dtAdapter = new OracleDataAdapter();
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();

        objCmd = new OracleCommand();
        objCmd.Connection = objConn;
        objCmd.CommandText = strSQL;
        objCmd.CommandType = CommandType.Text;

        dtAdapter.SelectCommand = objCmd;
        dtAdapter.Fill(ds);
        return ds;   //*** Return DataSet ***//
    }

    public DataTable QueryDataTable(String strSQL)
    {
        OracleDataAdapter dtAdapter;
        DataTable dt = new DataTable();
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();

        dtAdapter = new OracleDataAdapter(strSQL, objConn);
        dtAdapter.Fill(dt);
        return dt; //*** Return DataTable ***//
    }

    public Boolean QueryExecuteNonQuery(String strSQL)
    {
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();

        try
        {
            objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;

            objCmd.ExecuteNonQuery();
            return true; //*** Return True ***//
        }
        catch (Exception)
        {
            return false; //*** Return False ***//
        }
    }


    public Object QueryExecuteScalar(String strSQL)
    {
        Object obj;
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();

        try
        {
            objCmd = new OracleCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.Text;
            objCmd.CommandText = strSQL;
            

            obj = objCmd.ExecuteScalar();  //*** Return Scalar ***//
            return obj;
        }
        catch (Exception)
        {
            return null; //*** Return Nothing ***//
        }
    }

    public void TransStart()
    {
        objConn = new OracleConnection();
        objConn.ConnectionString = strConnString;
        objConn.Open();
        Trans = objConn.BeginTransaction(IsolationLevel.ReadCommitted);
    }


    public void TransExecute(String strSQL)
    {
        objCmd = new OracleCommand();
        objCmd.Connection = objConn;
        objCmd.Transaction = Trans;
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = strSQL;
        objCmd.ExecuteNonQuery();
    }




    public void TransRollBack()
    {
        Trans.Rollback();
    }

    public void TransCommit()
    {
        Trans.Commit();
    }

    public void Close()
    {
        objConn.Close();
        objConn = null;
    }



    public String UniversityName()
    {
        string name = "มหาวิทยาลัยราชภัฏสกลนคร";
        return name;
    }


}