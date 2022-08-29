using Newtonsoft.Json;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class OrdensServicoServices
    {
        public void Salvar(OrdensServico ordensServico)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            var json = JsonConvert.SerializeObject(ordensServico);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44363/ordensServico/Cadastrar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<OrdensServicoDto> BuscarTodasOrdensServico()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os Servicos dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/ordensservico/buscarTodasOrdensServico").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<OrdensServicoDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<OrdensServicoDto>();
            }
        }
        public void EnviarOrdensServico(OrdensServico ordensServicos)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                ordensServicos,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/ordensservico/buscarTodasOrdensServico", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<OrdensServicoDto> BuscarPorIdOrdemServico()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/servicos/buscarPorIdOrdemServico").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<OrdensServicoDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<OrdensServicoDto>();
            }
        }
        public void EnviarBusca(OrdensServico ordensServicos)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                ordensServicos,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/ordensServicos/BuscarPorIdOrdemServico", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Atualizar(int id, OrdensServico ordensServico)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            var viewModel = new
            {
                Encontrar = id,
                Atualizar = ordensServico
            };
            try
            {
                var json = JsonConvert.SerializeObject(viewModel);
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/ordensServico/atualizar?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;

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
        public void EnviarAtualizacao(OrdensServico ordensServico)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                ordensServico,
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/ordensServico/enviarAtualizacao", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
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
                response = httpClient.DeleteAsync($"https://localhost:44363/ordensServico/remover?id={id}").Result;

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
        public OrdensServicoDto Confirmar(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                response = httpClient.GetAsync($"https://localhost:44363/ordensServico/confirmar?id={id}").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                var objetoDesserializado = JsonConvert.DeserializeObject<OrdensServicoDto>(resultado);
                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new OrdensServicoDto();
            }
        }

    }
}
