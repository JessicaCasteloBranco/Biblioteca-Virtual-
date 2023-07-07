using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinimalApiDapper
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            //aqui é onde vou fazer meu mapeamento (endpoint) na API
            app.MapGet(pattern: "/Users", GetUsers);
            app.MapGet(pattern: "/Useres/{id}", GetUser);
            app.MapPost(pattern: "/InsertUser", InsertUser);
            app.MapPut(pattern: "/UpdateUser", UpdateUser);
            app.MapDelete("/DeletarUsuarios/{id}", DeleteUser);
        }

        private static async Task<IResult> GetUsers()
        {
            string ConexaoIntranet = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinimalApiUserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var dbAutomacoes = new SqlConnection(ConexaoIntranet);
            dbAutomacoes.Open();


            try
            {

                var Elements = await dbAutomacoes.QueryAsync<IUserData>("SELECT * FROM tb_teste1");
                dbAutomacoes.Close();
                return Results.Ok(Elements);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);

            }


        }

        private static async Task <IResult> GetUser(int id, IUserData data)
        {

            try
            {
                var results = await data.GetUser(id);
                if (results == null) return Results.NotFound();   
                return Results.Ok(results); 
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);

            }
        }

       private static async Task<IResult> InsertUser(UserModel user, IUserData data)
        {
            try
            {
                string ConexaoIntranet = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinimalApiUserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                var dbAutomacoes = new SqlConnection(ConexaoIntranet);
                dbAutomacoes.Open();

                var Elements = await dbAutomacoes.ExecuteAsync($"INSERT INTO tb_teste1 (id,titulo,autor) VALUES ('{user.id}','{user.titulo}', '{user.autor}');");
                dbAutomacoes.Close();
                return Results.Ok(Elements);

              
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);

            }
        }

        private static async Task<IResult> UpdateUser (UserModel user, IUserData data)
        {

            try
            {
                await data.UpdateUser(user);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                string ConexaoIntranet = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinimalApiUserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                var dbAutomacoes = new SqlConnection(ConexaoIntranet);
                dbAutomacoes.Open();

               // var Elements = await dbAutomacoes.ExecuteAsync($"DELETE FROM tb_teste1 WHERE id  = {id}");
                dbAutomacoes.Close();
                return Results.Problem(ex.Message);

            }

        }

        private static async Task<IResult> DeleteUser(int id, IUserData data )
        {

            try
            {
                 string ConexaoIntranet = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinimalApiUserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                var dbAutomacoes = new SqlConnection(ConexaoIntranet);
                dbAutomacoes.Open();

                var Elements = await dbAutomacoes.ExecuteAsync($"DELETE FROM tb_teste1 WHERE id  = {id}");
                dbAutomacoes.Close();
                await data.DeleteUser(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);

            }


        }

    }
}
