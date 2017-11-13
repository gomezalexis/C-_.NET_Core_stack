using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using DojoLeague.Models;

namespace DojoLeague.Factories
{
    public class NinjaFactory
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

        //Get all ninjas
        public List<NinjaDojo> GetAllNinjasWithHomeDojos(){
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = "SELECT dojos.dojoName, ninjas.name, ninjas.id, dojos.id AS dojoId FROM ninjas " +
                    "JOIN dojos ON dojos.id = ninjas.dojo_id";
                    dbConnection.Open();
                    return dbConnection.Query<NinjaDojo>(query).ToList();
                }
            }
        }

        public void AddNewNinja(Ninja ninja)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO ninjas (name, level, description, dojo_id, created_at) " +
                "VALUES (@name, @level, @description, @dojo_id, NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, ninja);
            }
        }

        public void BanishNinja(int ninjaId){
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"UPDATE ninjas SET dojo_id = 3 WHERE id = {ninjaId}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }            
        }

        public void RecruitNinja(int ninjaId, int dojoId){
            using (IDbConnection dbConnection = Connection)
            {
                string query = $"UPDATE ninjas SET dojo_id = {dojoId} WHERE id = {ninjaId}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }  
        }

        public Ninja FindById(int id){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT * FROM ninjas WHERE id = @id";
                return dbConnection.Query<Ninja>(query, new {id = id}).FirstOrDefault();
            }
        }

        public List<Ninja> GetAllFromDojo(int dojo_id){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = $"SELECT * FROM ninjas WHERE dojo_id = {dojo_id}";
                return dbConnection.Query<Ninja>(query).ToList();
            }
        }

        public List<NinjaDojo> GetRogues(){
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = "SELECT dojos.dojoName, ninjas.name, ninjas.id, dojos.id AS dojoId FROM ninjas " +
                    "JOIN dojos ON dojos.id = ninjas.dojo_id WHERE dojoName = 'Rogue'";
                    dbConnection.Open();
                    return dbConnection.Query<NinjaDojo>(query).ToList();
                }
            }
        }
    }
}