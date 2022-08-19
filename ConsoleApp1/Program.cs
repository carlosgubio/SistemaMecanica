using Client.Services;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleApp1
{
    enum Opcoes
    {
        Sair,
        CadastrarCliente,
        CadastrarProfissional,
        CadastrarProduto,
        CadastrarServico,
        CadastrarOrdemServico,
        AtualizarCliente,
        AtualizarProfissional,
        AtualizarProduto,
        AtualizarServico,
        AtualizarOrdemServico,
        RemoverCliente,
        RemoverProfissional,
        RemoverProduto,
        RemoverServico,
        RemoverOrdemServico,
        PesquisarCliente,
        PesquisarProfissional,
        PesquisarProduto,
        PesquisarServico,
        PesquisarOrdemServico
    }

    class Program
    {
        static void Main(string[] args)
        {
            ClientesServices clientesServices = new ClientesServices();
            //List<ClientesDto> clientes = new List<ClientesDto>();

            //int opcao = Convert.ToInt32(Console.ReadLine());

            //while (opcao != 0)
            //{
            //    if (opcao == 1)
            //    {
            //        var resultado = new ClientesServices();
            //        clientes = resultado.BuscarTodosClientes();

            //        //mostra os dados na tela
            //        foreach (var item in clientes)
            //        {
            //            Console.WriteLine("=====================================");
            //            Console.WriteLine("Id: " + item.IdCliente);
            //            Console.WriteLine("Nome: " + item.NomeCliente);
            //            Console.WriteLine("CPF: " + item.CpfCliente);
            //            Console.WriteLine("Idade: " + item.TelefoneCliente);
            //            Console.WriteLine("Idade: " + item.EnderecoCliente);
            //            Console.WriteLine("Idade: " + item.VeiculoCliente);
            //            Console.WriteLine("Idade: " + item.PlacaVeiculoCliente);
            //            Console.WriteLine("Idade: " + item.PlacaVeiculoCliente);
            //            Console.WriteLine("=====================================");
            //        }
            //    }
            //    opcao = Convert.ToInt32(Console.ReadLine());
            //}
            Opcoes opcoes;
            Console.WriteLine("======================================");
            Console.WriteLine("Digite a Opção desejada:\n0-Sair\n1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n6-Atualizar Cliente" +
                              "\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n10-Atualizar Ordem de Serviço\n11-Remover Cliente\n12-Remover Profissional\n13-Remover Produto\n14-Remover Serviço" +
                              "\n15-Remover Ordem de Serviço\n16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço");
            opcoes = (Opcoes)Convert.ToInt32(Console.ReadLine());

            while (opcoes != Opcoes.Sair)
            {
                if (opcoes == Opcoes.CadastrarCliente)
                {
                    var cliente = new Clientes();
                    Console.WriteLine("Informe o nome do Cliente:");
                    cliente.NomeCliente = Console.ReadLine();
                    Console.WriteLine("Informe o CPF:");
                    cliente.CpfCliente = Console.ReadLine();
                    Console.WriteLine("Informe o Telefone com DDD:");
                    cliente.TelefoneCliente = Console.ReadLine();
                    Console.WriteLine("Informe o Endereco completo:");
                    cliente.EnderecoCliente = Console.ReadLine();
                    Console.WriteLine("Informe O Veículo:");
                    cliente.VeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Placa do Veículo:");
                    cliente.PlacaVeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Cor do Veículo:");
                    cliente.CorVeiculoCliente = Console.ReadLine();
                    clientesServices.Salvar(cliente);
                }

            }
        }
    }
}
 