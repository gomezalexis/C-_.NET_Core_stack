using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using LostInTheWoods.Models;

namespace LostInTheWoods.Factories
{
    public class TrailFactory{
        static string server = "localhost";
        static string db = "lostinthewoods";//Change depending on Schema name
        static string port = "3306"; 
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }
        //Get all trails
        public List<Trail> GetAllTrails(){
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = "SELECT * FROM trails";
                    dbConnection.Open();
                    return dbConnection.Query<Trail>(query).ToList();
                }
            }
        }
        //Find one Trail by ID
        public Trail FindById(int id){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT * FROM trails WHERE id = @id";
                return dbConnection.Query<Trail>(query, new {id = id}).FirstOrDefault();
            }
        }

        //Add a new trail
        public void AddNewTrail(Trail trail)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails (trailName, description, trailLength, elevationChange, created_at) " +
                "VALUES (@trailName, @description, @trailLength, @elevationChange, NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, trail);
            }
        }
    }
}