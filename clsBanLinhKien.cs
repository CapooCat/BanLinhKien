using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace BanLinhKien
{
    class clsBanLinhKien
    {
        OleDbConnection con = new OleDbConnection();
        OleDbCommandBuilder build = new OleDbCommandBuilder();
        DataSet dsTable = new DataSet();

        public DataSet datasetTable
        {
            get { return dsTable; }
            set { dsTable = value; }
        }

        public clsBanLinhKien()
        {
            if(con.State==ConnectionState.Closed)
            {
                string str;
                str = "provider=microsoft.jet.oledb.4.0;data source=QLMT.mdb";
                con.ConnectionString = str;
                con.Open();
            }
        }
        public int KiemTra(string sql)
        {
            DataSet ds = new DataSet();
            DataTable tbl = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            da.SelectCommand = cmd;
            da.Fill(tbl);
            int check = tbl.Rows.Count;
            return check;
        }
        public DataTable LayDuLieu(string sql)
        {
            
            DataSet ds = new DataSet();
            DataTable tbl = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            da.SelectCommand = cmd;
            da.Fill(tbl);
            return tbl;
        }
        public int CapNhatDuLieu(string sql)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
    }
}
