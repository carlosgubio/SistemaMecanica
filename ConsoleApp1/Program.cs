using Client.Services;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ClientesDto> clientes = new List<ClientesDto>();

            int opcao = Convert.ToInt32(Console.ReadLine());

            while (opcao != 0)
            {
                if (opcao == 1)
                {
                    var resultado = new ClientesServices();
                    clientes = resultado.BuscarTodosClientes();

                    //mostra os dados na tela
                    foreach (var item in clientes)
                    {
                        Console.WriteLine("=====================================");
                        Console.WriteLine("Id: " + item.IdCliente);
                        Console.WriteLine("Nome: " + item.NomeCliente);
                        Console.WriteLine("CPF: " + item.CpfCliente);
                        Console.WriteLine("Idade: " + item.TelefoneCliente);
                        Console.WriteLine("Idade: " + item.EnderecoCliente);
                        Console.WriteLine("Idade: " + item.VeiculoCliente);
                        Console.WriteLine("Idade: " + item.PlacaVeiculoCliente);
                        Console.WriteLine("Idade: " + item.CorVeiculoCliente);
                        Console.WriteLine("=====================================");
                    }
                }
                opcao = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
 