using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Services
{
    public class SistemaMecanicaServices
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;

        //    try
        //    {
        //        response = HttpClient.GetAsync(" https://localhost:44363/clientes/BuscarPorNomeCliente ").Result;
        //        response.EnsureSuccessStatusCode();
        //        var resultado = response.Content.ReadAsStringAsync().Result;
                

        //        string responseBory = response.Content.ReadAsStringAsync().Result;
        //}
        //    catch (HttpRequestException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
    }
}
