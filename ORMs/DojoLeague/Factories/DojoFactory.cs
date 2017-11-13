using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using DojoLeague.Models;

namespace DojoLeague.Factories
{
    public class DojoFactory
    {
        static string server = "localhost";
        static string db = "dojoleague";//Change depending on Schema name
        static string port = "3306"; 
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }

        //Get all Dojos
        public List<Dojo> GetAllDojos(){
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = "SELECT * FROM dojos";
                    dbConnection.Open();
                    return dbConnection.Query<Dojo>(query).ToList();
                }
            }
        }

        public Dojo FindById(int id){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT * FROM dojos WHERE id = @id";
                return dbConnection.Query<Dojo>(query, new {id = id}).FirstOrDefault();
            }
        }

        public void AddNewDojo(Dojo dojo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO dojos (dojoName, dojoLocation, description, created_at) " +
                "VALUES (@dojoName, @dojoLocation, @description, NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, dojo);
            }
        }
    }
}