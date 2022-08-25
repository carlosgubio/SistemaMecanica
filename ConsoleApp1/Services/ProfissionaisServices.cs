using Newtonsoft.Json;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class ProfissionaisServices
    {
        public List<ProfissionaisDto> BuscarTodosProfissionais()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os Servicos dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/profissionais/buscartodos").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<ProfissionaisDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProfissionaisDto>();
            }
        }
        public void EnviarProfissionais(Profissionais profissionais)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                profissionais,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/profissionais/BuscarTodosProfissionais", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<ProfissionaisDto> BuscarPorNomeProfissional()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/profissionais/BuscarPorNomeProfissional").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<ProfissionaisDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProfissionaisDto>();
            }
        }
        public void EnviarBuscaPorNomeProfissional(Profissionais profissionais)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                profissionais,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/profissionais/BuscarPorNomeProfissional", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void Atualizar(int id, ProfissionaisDto profissionais)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            
            var viewModel = new
            {
                Encontrar = id,
                Atualizar = profissionais
            };

            try
            {
                var json = JsonConvert.SerializeObject(viewModel);
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/profissionais/atualizar?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;

                var resultado = response.Content.ReadAsStringAsync().Result;

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
        }
        public void EnviarAtualizacao(Profissionais profissionais)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                profissionais,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/profissionais/EnviarAtualizacao", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Salvar(Profissionais profissionais)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            var json = JsonConvert.SerializeObject(profissionais);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44363/profissionais/Cadastrar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public string Remover(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            try
            {
                //monta a request para a api;
                response = httpClient.DeleteAsync($"https://localhost:44363/profissionais/remover?id={id}").Result;

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
        public ProfissionaisDto ConfirmarProfissionais(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                response = httpClient.GetAsync($"https://localhost:44363/profissionais/Confirmar?id={id}").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                var objetoDesserializado = JsonConvert.DeserializeObject<ProfissionaisDto>(resultado);
                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new ProfissionaisDto();
            }
        }
    }
}
