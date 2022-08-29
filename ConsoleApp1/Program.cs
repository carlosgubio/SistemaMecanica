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
            Console.WriteLine("Digite a Opção desejada:\n------------------------------>\n0-Sair\n------------------------------>\n1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n------------------------------>\n" +
                             "6-Atualizar Cliente\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n10-Atualizar Ordem de Serviço\n------------------------------>\n" +
                             "11-Remover Cliente\n12-Remover Profissional\n13-Remover Produto\n14-Remover Serviço\n15-Remover Ordem de Serviço\n------------------------------>\n" +
                             "16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço");
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
                } //ok
                if (opcoes == Opcoes.CadastrarProfissional)
                {
                    var profissional = new Profissionais();
                    Console.WriteLine("Informe o Nome do Profissional:");
                    profissional.NomeProfissional = Console.ReadLine();
                    Console.WriteLine("Informe o Cargo do Profissional:");
                    profissional.CargoProfissional = Console.ReadLine();
                    profissionaisServices.Salvar(profissional);
                } //ok
                if (opcoes == Opcoes.CadastrarProduto)
                {
                    var produto = new Produtos();
                    Console.WriteLine("Informe a Descrição do Produto:");
                    produto.DescricaoPeca = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    produto.ValorPeca = Convert.ToSingle(Console.ReadLine());
                    produtosServices.Salvar(produto);
                } //ok
                if (opcoes == Opcoes.CadastrarServico)
                {
                    var servico = new Servicos();
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    servico.DescricaoServico = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    servico.ValorServico = (float)Convert.ToDouble(Console.ReadLine());
                    servicosServices.Salvar(servico);
                } //ok
                if (opcoes == Opcoes.CadastrarOrdemServico)
                {
                    var ordensServico = new OrdensServico();
                    Console.WriteLine("Informe o Id do Cliente:");
                    ordensServico.IdCliente = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id do Profissional:");
                    ordensServico.IdProfissional = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id do Serviço:");
                    ordensServico.IdServico = Convert.ToInt32(Console.ReadLine());
                    
                    List<int> idsProdutos = new List<int>();

                    Console.WriteLine("Informe o Id da Peça:");
                    int idProduto = Convert.ToInt32(Console.ReadLine());
                    idsProdutos.Add(idProduto);
                    Console.WriteLine("Deseja inserir mais uma Peça? 0-Não 1-Sim ");
                    int opcao = Convert.ToInt32(Console.ReadLine());
                    while (opcao != 0) 
                    {
                        Console.WriteLine("Informe o Id da Peça:");
                        idProduto = Convert.ToInt32(Console.ReadLine());
                        idsProdutos.Add(idProduto);
                        Console.WriteLine("Deseja inserir mais uma Peça? 0-Não 1-Sim ");
                        opcao = Convert.ToInt32(Console.ReadLine());
                    }
                    ordensServico.IdItens = idsProdutos;
                    ordensServicoServices.Salvar(ordensServico);
                } // ok
                if (opcoes == Opcoes.AtualizarCliente)
                {
                    Clientes clientes = new Clientes();
                    Console.WriteLine("Informe a ID do Cliente que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var clienteRetorno = clientesServices.Confirmar(id);
                    if (clienteRetorno != null && clienteRetorno.IdCliente == id)
                    { 
                        Console.WriteLine("O Nome cadastrado é: " + clienteRetorno.NomeCliente + "\nDigite o novo Nome caso deseje alterar.");
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
                } //ok
                if (opcoes == Opcoes.AtualizarProfissional)
                {
                    Profissionais profissionais = new Profissionais();
                    Console.WriteLine("Informe a ID do Profissional que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var profissionalRetorno = profissionaisServices.Confirmar(id);

                    if (profissionalRetorno != null && profissionalRetorno.IdProfissional == id)
                    {
                        Console.WriteLine("O Nome cadastrado é: " + profissionalRetorno.NomeProfissional + "\nDigite o novo Nome caso deseje alterar.");
                        profissionalRetorno.NomeProfissional = Console.ReadLine();
                        Console.WriteLine("O Cargo cadastrado é: " + profissionalRetorno.CargoProfissional + "\nDigite o novo Cargo caso deseje alterar.");
                        profissionalRetorno.CargoProfissional = Console.ReadLine();
                    }
                    profissionaisServices.Atualizar(id, profissionalRetorno);
                } //ok
                if (opcoes == Opcoes.AtualizarProduto)
                {
                    Produtos produtos = new Produtos();
                    Console.WriteLine("Informe a ID da Peça que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var produtoRetorno = produtosServices.Confirmar(id);

                    if (produtoRetorno != null && produtoRetorno.IdProduto == id)
                    {
                        Console.WriteLine("A Descrição da Peça cadastrada é: " + produtoRetorno.DescricaoPeca + "\nDigite a nova Descriação da Peça caso deseje alterar.");
                        produtoRetorno.DescricaoPeca = Console.ReadLine();
                        Console.WriteLine("O Valor da Peça cadastrada é: " + produtoRetorno.ValorPeca + "\nDigite o novo Valor caso deseje alterar.");
                        produtoRetorno.ValorPeca = Convert.ToSingle(Console.ReadLine());
                    }
                    produtosServices.Atualizar(id, produtoRetorno);
                } //ok
                if (opcoes == Opcoes.AtualizarServico)
                {
                    Servicos servicos = new Servicos();
                    Console.WriteLine("Informe a ID do Serviço que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var servicoRetorno = servicosServices.Confirmar(id);

                    if (servicoRetorno != null && servicoRetorno.IdServico == id)
                    {
                        Console.WriteLine("A Descrição do Serviço cadastrada é: " + servicoRetorno.DescricaoServico + "\nDigite a nova Descrição do Serviço caso deseje alterar.");
                        servicoRetorno.DescricaoServico = Console.ReadLine();
                        Console.WriteLine("O Valor do Serviço cadastrado é: " + servicoRetorno.ValorServico + "\nDigite o novo Valor do Serviço caso deseje alterar.");
                        servicoRetorno.ValorServico = Convert.ToSingle(Console.ReadLine());
                        servicosServices.Atualizar(id, servicoRetorno);
                    }
                } //ok
                //if (opcoes == Opcoes.AtualizarOrdemServico)
                //{
                //    OrdensServico ordensServico = new OrdensServico();
                //    Console.WriteLine("Informe a ID da ordem ao qual deseja atualizar:");
                //    int id = Convert.ToInt32(Console.ReadLine());
                //    var ordemServicoRetorno = ordensServicoServices.Confirmar(id);

                //    if (ordemServicoRetorno != null && ordemServicoRetorno.IdOrdemServico == id)
                //    {
                //        Console.WriteLine("Informe o(s) dado(s) que deseja Atualizar:");
                //        Console.WriteLine("A ID Cliente cadastrada é: " + ordemServicoRetorno.IdCliente + "\nDigite a nova ID do Cliente caso deseje alterar.");
                //        ordemServicoRetorno.IdProfissional = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("A ID Profissional cadastrada é: " + ordemServicoRetorno.IdProfissional + "\nDigite a nova ID do Profissional caso deseje alterar.");
                //        ordemServicoRetorno.IdProfissional = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("A ID Serviço cadastrada é: " + ordemServicoRetorno.IdServico + "\nDigite a nova ID do Serviço caso deseje alterar.");
                //        ordemServicoRetorno.IdServico = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("A ID Servico cadastrada é: " + ordemServicoRetorno.IdProduto + "\nDigite a nova ID da Peça caso deseje alterar.");
                //        ordemServicoRetorno.IdProduto = Convert.ToInt32(Console.ReadLine());
                //        Console.WriteLine("O Valor da Peça cadastrada é: " + ordemServicoRetorno.TotalGeral + "\nDigite o novo Valor caso deseje alterar.");
                //        ordemServicoRetorno.TotalGeral = Convert.ToSingle(Console.ReadLine());
                //        ordensServicoServices.Atualizar(id, ordemServicoRetorno);
                //    }
                //}//Tem necessidade de alterar o Total Geral? Erro no final
                if (opcoes == Opcoes.RemoverCliente)
                {
                    Console.WriteLine("Digite a ID do Cliente para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var cliente = clientesServices.Confirmar(id);
                    if (cliente != null && cliente.IdCliente == id)
                    {
                        Console.WriteLine($"Id Cliente: " + cliente.IdCliente);
                        Console.WriteLine($"   Cliente: " + cliente.NomeCliente);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                           var resultado = clientesServices.Remover(id);
                            Console.WriteLine(resultado);
                        } 
                    }
                } //ok
                if (opcoes == Opcoes.RemoverProfissional)
                {
                    Console.WriteLine("Digite a ID do Profissional para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var profissional = profissionaisServices.Confirmar(id);
                    if (profissional != null && profissional.IdProfissional == id)
                    {
                        Console.WriteLine($"Id Profissional: " + profissional.IdProfissional);
                        Console.WriteLine($"   Profissional: " + profissional.NomeProfissional);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = profissionaisServices.Remover(id);
                            Console.WriteLine(resultado);
                        }
                    }
                } //ok
                if (opcoes == Opcoes.RemoverProduto)
                {
                    Console.WriteLine("Digite a ID do Produto para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var produto = produtosServices.Confirmar(id);
                    if (produto != null && produto.IdProduto == id)
                    {
                        Console.WriteLine($"Id Produto: " + produto.IdProduto);
                        Console.WriteLine($"   Produto: " + produto.DescricaoPeca);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = produtosServices.Remover(id);
                            Console.WriteLine(resultado);
                        }
                    }
                } //ok
                if (opcoes == Opcoes.RemoverServico)
                {
                    Console.WriteLine("Digite a ID do Serviço para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var servico = servicosServices.Confirmar(id);
                    if (servico != null && servico.IdServico == id)
                    {
                        Console.WriteLine($"       Id Serviço: " + servico.IdServico);
                        Console.WriteLine($"Descrição Serviço: " + servico.DescricaoServico);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = servicosServices.Remover(id);
                            Console.WriteLine(resultado);
                        }
                    }
                } //ok
                if (opcoes == Opcoes.RemoverOrdemServico)
                {
                    Console.WriteLine("Digite a ID da Ordem de Serviço para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var ordemServico = ordensServicoServices.Confirmar(id);
                    if (ordemServico != null && ordemServico.IdOrdemServico == id)
                    {
                        Console.WriteLine($"Id Ordem de Serviço: " + ordemServico.IdOrdemServico);
                        Console.WriteLine("Digite 1 se deseja mesmo deletar:");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = ordensServicoServices.Remover(id);
                            Console.WriteLine(resultado);
                        }
                    }
                }// nao testada
                if (opcoes == Opcoes.PesquisarCliente)
                {
                    Console.WriteLine("Informe o Nome do Cliente:");
                    string nome = Console.ReadLine();
                    List<ClientesDto> clientes = new List<ClientesDto>();
                    if (nome != null)
                    {
                        Console.WriteLine(nome);                        
                        clientes = clientesServices.BuscarPorNome(nome);
                        foreach (var item in clientes)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine($"    Nome: " + item.NomeCliente);
                            Console.WriteLine($"     CPF: " + item.CpfCliente);
                            Console.WriteLine($"Telefone: " + item.TelefoneCliente);
                            Console.WriteLine($"Endereço: " + item.EnderecoCliente);
                            Console.WriteLine($" Veículo: " + item.VeiculoCliente);
                            Console.WriteLine($"   Placa: " + item.PlacaVeiculoCliente);
                            Console.WriteLine($"     Cor: " + item.CorVeiculoCliente);
                            Console.WriteLine("=================================");
                        }
                    }
                } //ok
                if (opcoes == Opcoes.PesquisarProfissional) //ok
                {
                    Console.WriteLine("Informe o Nome do Profissional:");
                    string nome = Console.ReadLine();
                    List<ProfissionaisDto> profissionais = new List<ProfissionaisDto>();
                    if (nome != null)
                    {
                        Console.WriteLine(nome);
                        profissionais = profissionaisServices.BuscarPorNome(nome);
                        foreach (var item in profissionais)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine($"Nome: " + item.NomeProfissional);
                            Console.WriteLine($" CPF: " + item.CargoProfissional);
                            Console.WriteLine("=================================");
                        }
                    }
                } //ok
                if (opcoes == Opcoes.PesquisarProduto)
                {
                    Console.WriteLine("Informe a Descrição do Produto:");
                    string nome = Console.ReadLine();
                    List<ProdutosDto> produtos = new List<ProdutosDto>();
                    if (nome != null)
                    {
                        Console.WriteLine(nome);
                        produtos = produtosServices.BuscarPorNome(nome);
                        foreach (var item in produtos)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine($" Peça: " + item.DescricaoPeca);
                            Console.WriteLine($"Valor: " + item.ValorPeca);
                            Console.WriteLine("=================================");
                        }
                    }
                } //ok
                if (opcoes == Opcoes.PesquisarServico)
                {
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    string nome = Console.ReadLine();
                    List<ServicosDto> servicos = new List<ServicosDto>();
                    if (nome != null)
                    {
                        Console.WriteLine(nome);
                        servicos = servicosServices.BuscarPorNome(nome);
                        foreach (var item in servicos)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine($"Serviço: " + item.DescricaoServico);
                            Console.WriteLine($"  Valor: " + item.ValorServico);
                            Console.WriteLine("=================================");
                        }
                    }
                } //ok
                //if (opcoes == Opcoes.PesquisarOrdemServico)
                //{
                //    Console.WriteLine("Informe o ID da Ordem do Serviço:");
                //    int id = Convert.ToInt32(Console.ReadLine());
                //    OrdensServico ordensServico = new OrdensServico();
                //    if (ordensServico != null && ordensServico.IdOrdemServico == id)
                //    {
                //        Console.WriteLine(ordensServico.IdOrdemServico);
                //        ordensServicoServices.BuscarPorIdOrdemServico();
                //    }
                //    ordensServicoServices.BuscarPorIdOrdemServico();
                //}

                Console.WriteLine("======================================");
                Console.WriteLine("Digite a Opção desejada:\n------------------------------>\n0-Sair\n------------------------------>\n1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n------------------------------>\n" +
                             "6-Atualizar Cliente\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n10-Atualizar Ordem de Serviço\n------------------------------>\n" +
                             "11-Remover Cliente\n12-Remover Profissional\n13-Remover Produto\n14-Remover Serviço\n15-Remover Ordem de Serviço\n------------------------------>\n" +
                             "16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço");
                opcoes = (Opcoes)Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}


    