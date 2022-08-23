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
            ProfissionaisServices profissionaisServices = new ProfissionaisServices();
            ProdutosServices produtosServices = new ProdutosServices();
            ServicosServices servicosServices = new ServicosServices();
            OrdensServicoServices ordensServicoServices = new OrdensServicoServices();
            
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
                    Console.WriteLine("Informe o Veículo:");
                    cliente.VeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Placa do Veículo:");
                    cliente.PlacaVeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Cor do Veículo:");
                    cliente.CorVeiculoCliente = Console.ReadLine();
                    clientesServices.Salvar(cliente);
                }
                if (opcoes == Opcoes.CadastrarProfissional)
                {
                    var profissional = new Profissionais();
                    Console.WriteLine("Informe o nome do profissional:");
                    profissional.NomeProfissional = Console.ReadLine();
                    Console.WriteLine("Informe o Cargo do profissional:");
                    profissional.CargoProfissional = Console.ReadLine();
                    profissionaisServices.Salvar(profissional);
                }
                if (opcoes == Opcoes.CadastrarProduto)
                {
                    var produto = new Produtos();
                    Console.WriteLine("Informe a Descrição do Produto:");
                    produto.DescricaoPeca = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    produto.ValorPeca = Convert.ToSingle(Console.ReadLine());
                    produtosServices.Salvar(produto);
                }
                if (opcoes == Opcoes.CadastrarServico)
                {
                    var servico = new Servicos();
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    servico.DescricaoServico = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    servico.ValorServico = (float)Convert.ToDouble(Console.ReadLine());
                    servicosServices.Salvar(servico);
                }
                if (opcoes == Opcoes.CadastrarOrdemServico)
                {
                    var ordensServico = new OrdensServico();
                    Console.WriteLine("Informe o Id do Profissional:");
                    ordensServico.IdProfissional = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id do Cliente:");
                    ordensServico.IdCliente = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id do Serviço:");
                    ordensServico.IdServico = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id da Peça:");
                    ordensServico.IdPeca = Convert.ToInt32(Console.ReadLine());

                    ordensServicoServices.Salvar(ordensServico);
                } // não está salvando. Precisa inserir o ValorTotal?
                if (opcoes == Opcoes.AtualizarCliente)
                {
                    Clientes clientes = new Clientes();
                    Console.WriteLine("Informe a ID do Cliente que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var clienteRetorno = clientesServices.ConfirmarClientes(id);

                    if (clienteRetorno != null && clienteRetorno.IdCliente == id)
                    { 
                        Console.WriteLine("O nome cadastrado é: " + clienteRetorno.NomeCliente + "\nDigite o novo Nome caso deseje alterar.");
                        clienteRetorno.NomeCliente = Console.ReadLine();
                        Console.WriteLine("O CPF cadastrado é: " + clienteRetorno.CpfCliente + "\nDigite o novo CPF caso deseje alterar.");
                        clienteRetorno.CpfCliente = Console.ReadLine();
                        Console.WriteLine("O Telefone cadastrado é: " + clienteRetorno.TelefoneCliente + "\nDigite o novo Telefone caso deseje alterar.");
                        clienteRetorno.TelefoneCliente = Console.ReadLine();
                        Console.WriteLine("O Endereço cadastrado é: " + clienteRetorno.EnderecoCliente + "\nDigite o novo Endereço caso deseje alterar.");
                        clienteRetorno.EnderecoCliente = Console.ReadLine();
                        Console.WriteLine("O Veículo cadastrado é: " + clienteRetorno.VeiculoCliente + "\nDigite o novo Veículo caso deseje alterar.");
                        clienteRetorno.VeiculoCliente = Console.ReadLine();
                        Console.WriteLine("A placa cadastrada é: " + clienteRetorno.PlacaVeiculoCliente + "\nDigite a nova Placa caso deseje alterar.");
                        clienteRetorno.PlacaVeiculoCliente = Console.ReadLine();
                        Console.WriteLine("A cor cadastrada é: " + clienteRetorno.CorVeiculoCliente + "\nDigite a nova Cor caso deseje alterar.");
                        clienteRetorno.CorVeiculoCliente = Console.ReadLine();
                        clientesServices.Atualizar(id, clienteRetorno);
                    }
                }
                if (opcoes == Opcoes.AtualizarProfissional)
                {
                    Profissionais profissionais = new Profissionais();
                    Console.WriteLine("Informe a ID do Profissional que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var profissionalRetorno = profissionaisServices.ConfirmarProfissionais(id);

                    if (profissionalRetorno != null && profissionalRetorno.IdProfissional == id)
                    {
                        Console.WriteLine("O nome cadastrado é: " + profissionalRetorno.NomeProfissional + "\nDigite o novo Nome caso deseje alterar.");
                        profissionalRetorno.NomeProfissional = Console.ReadLine();
                        Console.WriteLine("O Cargo cadastrado é: " + profissionalRetorno.CargoProfissional + "\nDigite o novo Cargo caso deseje alterar.");
                        profissionalRetorno.CargoProfissional = Console.ReadLine();
                    }
                    profissionaisServices.Atualizar(id, profissionalRetorno);
                }
                if (opcoes == Opcoes.AtualizarProduto)
                {
                    Produtos produtos = new Produtos();
                    Console.WriteLine("Informe a ID da Peça que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var produtoRetorno = produtosServices.ConfirmarProdutos(id);

                    if (produtoRetorno != null && produtoRetorno.IdPeca == id)
                    {
                        Console.WriteLine("A descrição da Peça cadastrada é: " + produtoRetorno.DescricaoPeca + "\nDigite a nova Descriação da Peça caso deseje alterar.");
                        produtoRetorno.DescricaoPeca = Console.ReadLine();
                        Console.WriteLine("O Valor da Peça cadastrada é: " + produtoRetorno.ValorPeca + "\nDigite o novo Valor caso deseje alterar.");
                        produtoRetorno.ValorPeca = Convert.ToSingle(Console.ReadLine());
                    }
                    produtosServices.Atualizar(id, produtoRetorno);
                }
                if (opcoes == Opcoes.AtualizarServico)
                {
                    Servicos servicos = new Servicos();
                    Console.WriteLine("Informe a Descrição da Peça que deseja atualizar:");
                    string nome = Console.ReadLine();

                    Console.WriteLine("Informe o(s) dado(s) que deseja Atualizar:");

                    if (servicos != null && servicos.DescricaoServico == nome)
                    {
                        Console.WriteLine("A descrição da Peça cadastrada é: " + servicos.DescricaoServico + "\nDigite a nova Descrição da Peça caso deseje alterar.");
                        servicos.DescricaoServico = Console.ReadLine();
                        Console.WriteLine("O Valor da Peça cadastrada é: " + servicos.ValorServico + "\nDigite o novo Valor caso deseje alterar.");
                        servicos.ValorServico = Convert.ToSingle(Console.ReadLine());
                    }
                    servicosServices.Atualizar(nome, servicos);
                }
                //if (opcoes == Opcoes.AtualizarOrdemServico)
                //{
                //    OrdensServico ordensServico = new OrdensServico();
                //    Console.WriteLine("Informe a ID da ordem ao qual deseja atualizar:");
                //    int id = Convert.ToInt32(Console.ReadLine());

                //    Console.WriteLine("Informe o(s) dado(s) que deseja Atualizar:");

                //    if (ordensServico != null && ordensServico.IdOrdemServico == id)
                //    {
                //        Console.WriteLine("A ID Profissional cadastrado é: " + ordensServico.IdProfissional + "\nDigite a nova ID do Profissional caso deseje alterar.");
                //        ordensServico.IdProfissional = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("A ID Serviço cadastrado é: " + ordensServico.IdServico + "\nDigite a nova ID do Serviço caso deseje alterar.");
                //        ordensServico.IdServico = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("A ID Descrição da Peça cadastrada é: " + ordensServico.IdPeca + "\nDigite a nova ID da Peça caso deseje alterar.");
                //        ordensServico.IdPeca = Convert.ToInt32(Console.ReadLine());
                //        //Console.WriteLine("O Valor da Peça cadastrada é: " + ordensServico.TotalGeral + "\nDigite o novo Valor caso deseje alterar.");
                //        //ordensServico.TotalGeral = Convert.ToSingle(Console.ReadLine()); 
                //    }
                //    ordensServicoServices.Atualizar(id, ordensServico);
                //}//Tem necessidade de alterar o Total Geral? Tem Erro no final tb
                if (opcoes == Opcoes.RemoverCliente)
                {
                    Console.WriteLine("Digite o nome do Cliente para remover:");
                    string nome = Console.ReadLine();
                   clientesServices.Remover(nome);
                }
                if (opcoes == Opcoes.RemoverProfissional)
                {
                    Console.WriteLine("Digite o nome do Profissional para remover:");
                    string nome = Console.ReadLine();
                    profissionaisServices.Remover(nome);

                }
                if (opcoes == Opcoes.RemoverProduto)
                {
                    Console.WriteLine("Digite o nome do Produto para remover:");
                    string nome = Console.ReadLine();
                    produtosServices.Remover(nome);

                }
                if (opcoes == Opcoes.RemoverServico)
                {
                    Console.WriteLine("Digite o nome do Serviço para remover:");
                    string nome = Console.ReadLine();
                    servicosServices.Remover(nome);

                }
                if (opcoes == Opcoes.RemoverOrdemServico)
                {
                    Console.WriteLine("Digite o numero do ID da Ordem de Serviço para remover:");
                    string id = Console.ReadLine();
                    ordensServicoServices.Remover(id);

                }
                if (opcoes == Opcoes.PesquisarCliente)
                {
                    Console.WriteLine("Informe o nome do Cliente:");
                    string cliente = Console.ReadLine();
                    Clientes clientes = new Clientes();
                    if (clientes != null && clientes.NomeCliente == cliente)
                    {
                        Console.WriteLine(clientes.NomeCliente);
                        clientesServices.BuscarPorNomeCliente();
                    }
                    clientesServices.BuscarPorNomeCliente();

                }
                if (opcoes == Opcoes.PesquisarProfissional)
                {
                    Console.WriteLine("Informe o nome do Profissional:");
                    string profissional = Console.ReadLine();
                    Profissionais profissionais = new Profissionais();
                    if (profissionais != null && profissionais.NomeProfissional == profissional)
                    {
                        Console.WriteLine(profissionais.NomeProfissional);
                        profissionaisServices.BuscarPorNomeProfissional();
                    }
                    profissionaisServices.BuscarPorNomeProfissional();

                }
                if (opcoes == Opcoes.PesquisarProduto)
                {
                    Console.WriteLine("Informe a Descrição do Produto:");
                    string produto = Console.ReadLine();
                    Produtos produtos = new Produtos();
                    if (produtos != null && produtos.DescricaoPeca == produto)
                    {
                        Console.WriteLine(produtos.DescricaoPeca);
                        produtosServices.BuscarPorNomeProduto();
                    }
                    produtosServices.BuscarPorNomeProduto();

                }
                if (opcoes == Opcoes.PesquisarServico)
                {
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    string produto = Console.ReadLine();
                    Servicos servicos = new Servicos();
                    if (servicos != null && servicos.DescricaoServico == produto)
                    {
                        Console.WriteLine(servicos.DescricaoServico);
                        servicosServices.BuscarPorDescricaoProduto();
                    }
                    produtosServices.BuscarPorNomeProduto();

                }
                if (opcoes == Opcoes.PesquisarOrdemServico)
                {
                    Console.WriteLine("Informe o ID da Ordem do Serviço:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    OrdensServico ordensServico = new OrdensServico();
                    if (ordensServico != null && ordensServico.IdOrdemServico == id)
                    {
                        Console.WriteLine(ordensServico.IdOrdemServico);
                        ordensServicoServices.BuscarPorIdOrdemServico();
                    }
                    ordensServicoServices.BuscarPorIdOrdemServico();
                }

                Console.WriteLine("======================================");
                Console.WriteLine("Digite a Opção desejada:\n0-Sair\n1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n6-Atualizar Cliente" +
                                  "\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n10-Atualizar Ordem de Serviço\n11-Remover Cliente\n12-Remover Profissional\n13-Remover Produto\n14-Remover Serviço" +
                                  "\n15-Remover Ordem de Serviço\n16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço");
                opcoes = (Opcoes)Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}


    