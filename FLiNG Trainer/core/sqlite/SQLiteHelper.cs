using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLiNG_Trainer.core.sqlite;

public class SQLiteHelper
{
    public static Dictionary<string, SQLiteHelper> DataBaceList = new Dictionary<string, SQLiteHelper>();

    public SQLiteHelper(string filename = null)
    {
        DataSource = filename;
    }

    public string DataSource { get; set; }
    
    public SQLiteConnection GetSQLiteConnection()
    {
        string connStr = string.Format("Data Source={0}", DataSource);
        var con = new SQLiteConnection(connStr);
        return con;
    }

    private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, Dictionary<String, String> data)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Parameters.Clear();
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 30;
        if (data != null && data.Count >= 1)
        {
            foreach (KeyValuePair<String, String> val in data)
            {
                cmd.Parameters.AddWithValue(val.Key, val.Value);
            }
        }
    }

    public DataSet ExecuteDataset(string cmdText, Dictionary<string, string> data = null)
    {
        var ds = new DataSet();
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var command = new SQLiteCommand();
            PrepareCommand(command, connection, cmdText, data);
            var da = new SQLiteDataAdapter(command);
            da.Fill(ds);
        }
        return ds;
    }

    public DataTable ExecuteDataTable(string cmdText, Dictionary<string, string> data = null)
    {
        var dt = new DataTable();
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var command = new SQLiteCommand();
            PrepareCommand(command, connection, cmdText, data);
            SQLiteDataReader reader = command.ExecuteReader();
            dt.Load(reader);
        }
        return dt;
    }

    public DataRow ExecuteDataRow(string cmdText, Dictionary<string, string> data = null)
    {
        DataSet ds = ExecuteDataset(cmdText, data);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            return ds.Tables[0].Rows[0];
        return null;
    }

    public int ExecuteNonQuery(string cmdText, Dictionary<string, string> data = null)
    {
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var command = new SQLiteCommand();
            PrepareCommand(command, connection, cmdText, data);
            return command.ExecuteNonQuery();
        }
    }

    public SQLiteDataReader ExecuteReader(string cmdText, Dictionary<string, string> data = null)
    {
        var command = new SQLiteCommand();
        SQLiteConnection connection = GetSQLiteConnection();
        try
        {
            PrepareCommand(command, connection, cmdText, data);
            SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        catch
        {
            connection.Close();
            command.Dispose();
            throw;
        }
    }

    public object ExecuteScalar(string cmdText, Dictionary<string, string> data = null)
    {
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var cmd = new SQLiteCommand();
            PrepareCommand(cmd, connection, cmdText, data);
            return cmd.ExecuteScalar();
        }
    }

    public DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, Dictionary<string, string> data = null)
    {
        if (recordCount < 0)
            recordCount = int.Parse(ExecuteScalar(countText, data).ToString());
        var ds = new DataSet();
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var command = new SQLiteCommand();
            PrepareCommand(command, connection, cmdText, data);
            var da = new SQLiteDataAdapter(command);
            da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
        }
        return ds;
    }

    public DataSet ExecutePagerWithSearch(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, string searchColumn, string searchValue, Dictionary<string, string> data = null)
    {
        if (recordCount < 0)
            recordCount = int.Parse(ExecuteScalar(countText, data).ToString());

        string searchQuery = string.Empty;
        if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchValue))
        {
            searchQuery = $"WHERE {searchColumn} LIKE @searchValue";
            data ??= new Dictionary<string, string>();
            data["@searchValue"] = $"%{searchValue}%"; 
        }

        string finalQuery = $"{cmdText} {searchQuery} LIMIT @pageSize OFFSET @offset";

        var ds = new DataSet();
        using (SQLiteConnection connection = GetSQLiteConnection())
        {
            var command = new SQLiteCommand();
            PrepareCommand(command, connection, finalQuery, data);

            command.Parameters.AddWithValue("@pageSize", pageSize);
            command.Parameters.AddWithValue("@offset", (pageIndex - 1) * pageSize);

            var da = new SQLiteDataAdapter(command);
            da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
        }
        return ds;
    }


    public void ResetDataBass()
    {
        using (SQLiteConnection conn = GetSQLiteConnection())
        {
            var cmd = new SQLiteCommand();

            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = "vacuum";
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            cmd.ExecuteNonQuery();
        }
    }
}

