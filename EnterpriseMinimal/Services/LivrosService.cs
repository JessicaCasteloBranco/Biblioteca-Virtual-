using EnterpriseMinimal.Data.Models;
using Newtonsoft.Json;
using RestSharp;


namespace EnterpriseMinimal.Services
{
    public class LivrosService
    {
        private static string sUrlBase = (System.Diagnostics.Debugger.IsAttached ? "https://localhost:7232/" : "http://172.19.5.5:44361/");//

        public async Task<List<User>>ListarLivros(string sToken){

            string sUrl = "/Usuarios/Listar";
            RestClient client = new RestClient(sUrlBase);
            RestRequest request = new RestRequest(sUrl, Method.Get);

            request.AddHeader("Authorization", "Bearer " + sToken);

            var result = await client.ExecuteGetAsync(request); //QueryAsync<TB_CADCF>(request);
            var lista = JsonConvert.DeserializeObject<List<User>>(result.Content);

            return lista;
        }



        public async Task<bool> SalvarLivro(User Dados)//
        {
            string sUrl = "/InsertUser";
            RestClient client = new RestClient(sUrlBase);
            RestRequest request = new RestRequest(sUrl, Method.Put);
          
            //var parametroData = JsonConvert.SerializeObject(Dasdos);
            request.AddJsonBody(Dados);

            var result = await client.ExecuteAsync(request);

            if (result.IsSuccessful)
                return true;

            return false;
        }


        public async Task<RestResponse> IncluirLivro( User  Dados)//, string Token
        {
            string sUrl = "/InsertUser";
            RestClient client = new RestClient(sUrlBase);
            RestRequest request = new RestRequest(sUrl, Method.Post);

            request.AddJsonBody(Dados);

            RestResponse result = await client.ExecuteAsync(request);
            IResult valor = Results.Ok(result);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return new RestResponse { StatusCode = System.Net.HttpStatusCode.OK, Content = result.Content };
            else
                return new RestResponse { StatusCode = System.Net.HttpStatusCode.BadRequest, Content = result.Content };
        }



        public async Task ExcluirLivro( int id)//, string Token
        {
            string sUrl = $"/DeletarUsuarios/{id}";
            RestClient client = new RestClient(sUrlBase);
            RestRequest request = new RestRequest(sUrl, Method.Delete);
            RestResponse result = await client.ExecuteAsync(request);


        }

        public async Task<User> ConsultarLivro() //, string Token
        {
            string sUrl = $"/Administracao/Empresas/Consultar";
            RestClient client = new RestClient(sUrlBase);
            RestRequest request = new RestRequest(sUrl, Method.Get);
            request.AddHeader("Authorization", "Bearer " );


            var result = await client.ExecuteAsync(request);

            var ListaRetorno = JsonConvert.DeserializeObject<User>((result.StatusCode == System.Net.HttpStatusCode.OK ? result.Content : ""));

            return ListaRetorno;
        }








    }
}
