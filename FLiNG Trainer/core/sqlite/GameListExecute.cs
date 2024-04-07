using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FLiNG_Trainer.core.sqlite;

public class GameListExecute
{
    private readonly string database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db", "data.db");
    private readonly string baseQuery = "SELECT * FROM game_list ";
    private SQLiteHelper sqliteHelper;

    public GameListExecute()
    {
        sqliteHelper = new SQLiteHelper(database);
    }

    public DataTable ExecuteGameListPage(int pageIndex, string name = null, SQLiteParameter[] parameters = null)
    {
        string endQuery = string.Empty;

        if (!string.IsNullOrEmpty(name))
        {
            endQuery = " WHERE en_name = @name OR cn_name = @name";
        }

        DataTable dataTable = new DataTable();
        try
        {
            SQLiteConnection connection = sqliteHelper.GetSQLiteConnection();
            SQLiteCommand command = new SQLiteCommand(baseQuery + endQuery, connection);

            if (!string.IsNullOrEmpty(name))
            {
                command.Parameters.AddWithValue("@name", name);
            }

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                }
            }

            int offset = pageIndex * 20;

            string pageQuery = baseQuery + endQuery + " LIMIT 20 OFFSET @Offset";
            command.CommandText = pageQuery;
            command.Parameters.AddWithValue("@Offset", offset);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return dataTable;
    }

    public DataRow ExecuteDataByGameId(int gameId)
    {
        try
        {
            string endQuery = $" WHERE game_cover_id = {gameId}";
            DataRow row = sqliteHelper.ExecuteDataRow(baseQuery + endQuery);
            return row;
        }catch (Exception ex)
        {
            throw ex;
        }
    }
}
