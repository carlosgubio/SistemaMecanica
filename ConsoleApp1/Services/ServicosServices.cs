using Client.Dtos.Servicos;
using Client.Models.Servicos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class ServicosServices
    {
        public string Salvar(Servicos servicos)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            var json = JsonConvert.SerializeObject(servicos);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44363/servicos/cadastrar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        public List<ServicosDto> BuscarTodos()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os Servicos dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/servicos/buscarTodos").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<ServicosDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ServicosDto>();
            }
        }

        public List<ServicosDto> BuscarPorNome(string nome)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync($"https://localhost:44363/servicos/consultaNome?nome={nome}").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<ServicosDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ServicosDto>();
            }
        }

        public string Atualizar(int id, ServicosDto servicos)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            var viewModel = new
            {
                Encontrar = id,
                Atualizar = servicos
            };
            try
            {
                var json = JsonConvert.SerializeObject(viewModel);
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/servicos/atualizar?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;

                resultado = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(resultado);
                }
                //converte os dados recebidos e retorna eles como objetos do C#;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }

        public string Remover(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            try
            {
                //monta a request para a api;
                response = httpClient.DeleteAsync($"https://localhost:44363/servicos/remover?id={id}").Result;

                resultado = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(resultado);
                }
                //converte os dados recebidos e retorna eles como objetos do C#;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
        public ServicosDto Confirmar(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                response = httpClient.GetAsync($"https://localhost:44363/servicos/confirmar?id={id}").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                var objetoDesserializado = JsonConvert.DeserializeObject<ServicosDto>(resultado);
                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new ServicosDto();
            }
        }
    }
}
