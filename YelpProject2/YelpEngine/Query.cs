using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;


namespace YelpEngine
{
    internal class Query
    {
        public Query()
        {

        }

        //public Query(string host, string username, string database, string password)
        //{
        //    this.buildConnectionString("localhost", "postgres", "yelpdb", "postgres");
        //}
         
        private string buildConnectionString(string host="localhost", string username= "postgres", string database= "yelpdb", string password="postgres")
        {
            return "Host = " + host + "; Username = " + username + "; Database = " + database + "; password = " + password + ";";
        }

        public bool executeQuery(string sqlstr, Action<NpgsqlDataReader> myfunc)
        {
            using (var connection = new NpgsqlConnection(buildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            myfunc(reader);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        return true;
                        //System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }

                    return false;
                }
            }
        }

    }
}
