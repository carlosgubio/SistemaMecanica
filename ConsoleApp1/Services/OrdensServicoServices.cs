using Client.Dtos.OrdensServico;
using Client.Models.OrdensServico;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class OrdensServicoServices
    {
        public string Salvar(OrdensServico ordensServico)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            var json = JsonConvert.SerializeObject(ordensServico);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44363/ordensServico/Cadastrar", new StringContent(json, Encoding.UTF8, "application/json")).Result;
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

        public OrdensServicoDto BuscarPorIdOrdemServico(int id)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync($"https://localhost:44363/ordensServico/Consultar?id={id}").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<OrdensServicoDto>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new OrdensServicoDto();
            }
        }
        //public OrdensServicoDto BuscarOrdemServicoPorVeiculo(string veiculo)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    HttpResponseMessage response;

        //    //Busca todos os clientes dentro da api;
        //    try
        //    {
        //        //monta a request para a api;
        //        response = httpClient.GetAsync($"https://localhost:44363/ordensServico/ConsultarVeiculo?veiculo={veiculo}").Result;
        //        response.EnsureSuccessStatusCode();

        //        var resultado = response.Content.ReadAsStringAsync().Result;

        //        //converte os dados recebidos e retorna eles como objetos do C#;
        //        var objetoDesserializado = JsonConvert.DeserializeObject<OrdensServicoDto>(resultado);

        //        return objetoDesserializado;
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new OrdensServicoDto();
        //    }
        //}
        public double Faturamento()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync($"https://localhost:44363/ordensServico/Faturamento").Result;
                response.EnsureSuccessStatusCode();

                resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Convert.ToDouble(resultado);
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

        public string AdicionarProfissional(int id, List<int> profissionais)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            var json = JsonConvert.SerializeObject(profissionais);

            try
            {
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/ordensServico/adicionarProfissional?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                resultado = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
        public string AdicionarProduto(int id, List<int> produtos)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;
            var resultado = string.Empty;
            var json = JsonConvert.SerializeObject(produtos);

            try
            {
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44363/ordensServico/adicionarProduto?id={id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
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
    }
}
