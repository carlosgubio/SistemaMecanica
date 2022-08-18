using Newtonsoft.Json;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class ClientesServices
    {
        public List<ClientesDto> BuscarTodosClientes()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44363/clientes/BuscarTodosClientes").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<ClientesDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ClientesDto>();
            }
        }
        public void EnviarClientes(Clientes clientes)
        {
            //recebe os dados para enviar para a API cria a viewModel que será enviada;
            var viewModel = new
            {
                clientes,                
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //converte o objeto em um JSON 
            var json = JsonConvert.SerializeObject(viewModel);

            try
            {
                //envia os dados para a API, convertendo em uma cadeia de string
                response = httpClient.PostAsync("https://localhost:44363/clientes/BuscarTodosClientes", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();
                //faz a request, envia os dados e recebe a resposta da API.
                var resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public List<ClientesDto> AtualizarClientes(id)
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;

        try
        {
            response = httpClient.PutAsync("https://localhost:44363/clientes/AtualizarClientes").Result;
            response.EnsureSuccessStatusCode();

            var resultado = response.Content.ReadAsStringAsync().Result;

            var objetoDesserializado = JsonConvert.DeserializeObject<List<ClientesDto>>(resultado);

            return objetoDesserializado;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            return new List<ClientesDto>();
        }
}
