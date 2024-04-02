using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FLiNG_Trainer.core.sqlitep;

public class SQLiteDataAccess
{
    private string _connectionString;
    private SQLiteConnection _connection;

    public SQLiteDataAccess()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db", "data.db");
        _connectionString = $"Data Source={dbPath};Version=3;";
    }

    public void OpenConnection()
    {
        if (_connection == null)
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (_connection != null && _connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    public void ExecuteNonQuery(string query, SQLiteParameter[] parameters = null)
    {
        using (var command = new SQLiteCommand(query, _connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            command.ExecuteNonQuery();
        }
    }

    public object ExecuteScalar(string query, SQLiteParameter[] parameters = null)
    {
        using (var command = new SQLiteCommand(query, _connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command.ExecuteScalar();
        }
    }

    public SQLiteDataReader ExecuteReader(string query, SQLiteParameter[] parameters = null)
    {
        using (var command = new SQLiteCommand(query, _connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command.ExecuteReader();
        }
    }

    public DataTable GetPagedData(string query, int pageIndex, int pageSize, SQLiteParameter[] parameters = null)
    {
        if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index must be non-negative.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be positive.");

        DataTable dataTable = new DataTable();
        using (var command = new SQLiteCommand(query, _connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            int offset = pageIndex * pageSize;

            string pagedQuery = query + " LIMIT @PageSize OFFSET @Offset";
            command.CommandText = pagedQuery;
            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.AddWithValue("@Offset", offset);

            using (var adapter = new SQLiteDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
        }

        return dataTable;
    }
}
