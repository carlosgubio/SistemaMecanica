using Newtonsoft.Json;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class ServicosServices
    {
        public List<ServicosDto> BuscarTodosServicos()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os Servicos dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/servicos/BuscarTodosServicos").Result;
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
        public void EnviarServicos(Servicos servicos)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                servicos,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/servicos/BuscarTodosServicos", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<ServicosDto> BuscarPorDescricaoProduto()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/servicos/BuscarPorDescricaoProduto").Result;
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
        public void EnviarBuscaPorNomeProduto(Servicos servicos)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                servicos,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/servicos/BuscarPorDescricaoProduto", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AtualizarServico(int id, Servicos servicos)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var viewModel = new
            {
                Encontrar = id,
                Atualizar = servicos
            };
            try
            {
                var json = JsonConvert.SerializeObject(servicos);
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/servicos/atualizar?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;

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
        public void EnviarAtualizacao(Servicos servicos)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                servicos,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/servicos/Atualizar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Remover(string nome)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            try
            {
                //monta a request para a api;
                response = httpClient.DeleteAsync($"https://localhost:44363/servicos/remover?nome={nome}").Result;

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
        public void Salvar(Servicos servicos)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            var json = JsonConvert.SerializeObject(servicos);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44363/servicos/Cadastrar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
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
        public ServicosDto ConfirmarServicos(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                response = httpClient.GetAsync($"https://localhost:44363/servicos/Confirmar?id={id}").Result;
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
