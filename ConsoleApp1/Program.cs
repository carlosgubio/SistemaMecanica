using Client.Dtos.Clientes;
using Client.Dtos.Produtos;
using Client.Dtos.Profissionais;
using Client.Dtos.Servicos;
using Client.Dtos.Veiculos;
using Client.Models.Clientes;
using Client.Models.OrdensServico;
using Client.Models.Produtos;
using Client.Models.Profissionais;
using Client.Models.Servicos;
using Client.Services;
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
        AtualizarOrdemServicoProfissional,
        AtualizarOrdemServicoProduto,
        RemoverCliente,
        RemoverProfissional,
        RemoverProduto,
        RemoverServico,
        PesquisarCliente,
        PesquisarProfissional,
        PesquisarProduto,
        PesquisarServico,
        PesquisarOrdemServico,
        CadastrarVeiculo,
        AtualizarVeiculo,
        RemoverVeiculo,
        PesquisarVeiculo,
        AtualizarOrdemServicoServico,
        PesquisarTodos
    }
    enum BuscarTodos
    {
        Sair,
        BuscarClientes,
        BuscarProfissionais,
        BuscarProdutos,
        BuscarServiços,
        BuscarFaturamentoBruto,
        BuscarVeiculos
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
            VeiculosServices veiculosServices = new VeiculosServices();

            Opcoes opcoes;
            Console.WriteLine("======================================");
            Console.WriteLine("DIGITE A OPÇÃO DESEJADA:\n------------------------------>\n0-Sair\n------------------------------>\n" +
                            "1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n------------------------------>\n" +
                            "6-Atualizar Cliente\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n------------------------------>\n10-Inserir Profissional na Ordem de Serviço\n11-Inserir Produto na Ordem de Serviço\n------------------------------>\n" +
                            "12-Remover Cliente\n13-Remover Profissional\n14-Remover Produto\n15-Remover Serviço\n------------------------------>\n" +
                            "16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço\n21-Cadastrar Veículo\n22-Atualizar Veículo\n23-Remover Veículo\n24-Remover Veículo\n25-Inserir Produto NaOrdem de Serviço" +
                            "\n------------------------------>\n26-Buscas Gerais");

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
                    var resultado = clientesServices.Salvar(cliente);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.CadastrarProfissional)
                {
                    var profissional = new Profissionais();
                    Console.WriteLine("Informe o Nome do Profissional:");
                    profissional.NomeProfissional = Console.ReadLine();
                    Console.WriteLine("Informe o Cargo do Profissional:");
                    profissional.CargoProfissional = Console.ReadLine();
                    var resultado = profissionaisServices.Salvar(profissional);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.CadastrarProduto)
                {
                    var produto = new Produtos();
                    Console.WriteLine("Informe a Descrição do Produto:");
                    produto.DescricaoPeca = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    produto.ValorPeca = Convert.ToSingle(Console.ReadLine());
                    var resultado = produtosServices.Salvar(produto);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.CadastrarServico)
                {
                    var servico = new Servicos();
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    servico.DescricaoServico = Console.ReadLine();
                    Console.WriteLine("Informe o Valor:");
                    servico.ValorServico = (float)Convert.ToDouble(Console.ReadLine());
                    var resultado = servicosServices.Salvar(servico);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.CadastrarOrdemServico)
                {
                    var ordensServico = new OrdensServico();
                    Console.WriteLine("Informe o Id do Cliente:");
                    ordensServico.IdCliente = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Id do Veiculo:");
                    ordensServico.IdVeiculo = Convert.ToInt32(Console.ReadLine());
                                   
                    List<int> idsProdutos = new List<int>();
                    Console.WriteLine("Informe o Id da Peça:");
                    int idProduto = Convert.ToInt32(Console.ReadLine());
                    idsProdutos.Add(idProduto);
                    Console.WriteLine("Deseja inserir mais uma Peça? 0-Não 1-Sim ");
                    int opcaoProduto = Convert.ToInt32(Console.ReadLine());
                    while (opcaoProduto != 0)
                    {
                        Console.WriteLine("Informe o Id da Peça:");
                        idProduto = Convert.ToInt32(Console.ReadLine());
                        idsProdutos.Add(idProduto);
                        Console.WriteLine("Deseja inserir mais uma Peça? 0-Não 1-Sim ");
                        opcaoProduto = Convert.ToInt32(Console.ReadLine());
                    }
                    ordensServico.IdItens = idsProdutos;

                    List<int> idsProfissionais = new List<int>();
                    Console.WriteLine("Informe o Id do Profissional:");
                    int idProfissional = Convert.ToInt32(Console.ReadLine());
                    idsProfissionais.Add(idProfissional);
                    Console.WriteLine("Deseja inserir mais um Profissional? 0-Não 1-Sim ");
                    int opcaoProfissional = Convert.ToInt32(Console.ReadLine());
                    while (opcaoProfissional != 0)
                    {
                        Console.WriteLine("Informe o Id do Profissional:");
                        idProfissional = Convert.ToInt32(Console.ReadLine());
                        idsProfissionais.Add(idProfissional);
                        Console.WriteLine("Deseja inserir mais um Profissional? 0-Não 1-Sim ");
                        opcaoProfissional = Convert.ToInt32(Console.ReadLine());
                    }
                    ordensServico.IdExecucoes = idsProfissionais;

                    List<int> idsServicos = new List<int>();
                    Console.WriteLine("Informe o Id do Serviço:");
                    int idServico = Convert.ToInt32(Console.ReadLine());
                    idsServicos.Add(idServico);
                    Console.WriteLine("Deseja inserir mais um Serviço? 0-Não 1-Sim ");
                    int opcaoServico = Convert.ToInt32(Console.ReadLine());
                    while (opcaoServico != 0)
                    {
                        Console.WriteLine("Informe o Id do Serviço:");
                        idServico = Convert.ToInt32(Console.ReadLine());
                        idsServicos.Add(idServico);
                        Console.WriteLine("Deseja inserir mais um Serviço? 0-Não 1-Sim ");
                        opcaoServico = Convert.ToInt32(Console.ReadLine());
                    }
                    ordensServico.IdServicosExecutados = idsServicos;

                    var resultado = ordensServicoServices.Salvar(ordensServico);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
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
                        var resultado = clientesServices.Atualizar(id, clienteRetorno);
                        Console.WriteLine("***************************************");
                        Console.WriteLine(resultado);
                        Console.WriteLine("***************************************\n");
                    }
                }
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
                    var resultado = profissionaisServices.Atualizar(id, profissionalRetorno);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
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
                    var resultado = produtosServices.Atualizar(id, produtoRetorno);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
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
                        var resultado = servicosServices.Atualizar(id, servicoRetorno);
                        Console.WriteLine("***************************************");
                        Console.WriteLine(resultado);
                        Console.WriteLine("***************************************\n");
                    }
                }
                if (opcoes == Opcoes.AtualizarOrdemServicoProfissional)
                {
                    Console.WriteLine("Informe a ID da Ordem de Serviço ao qual deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var ordemServicoRetorno = ordensServicoServices.Confirmar(id);

                    List<int> idsProfissionais = new List<int>();

                    Console.WriteLine("Informe a ID do Profissional que deseja inserir na Ordem de Serviço:");
                    int idProfissional = Convert.ToInt32(Console.ReadLine());
                    idsProfissionais.Add(idProfissional);
                    var resultado = ordensServicoServices.AdicionarProfissional(id, idsProfissionais);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.AtualizarOrdemServicoProduto)
                {
                    Console.WriteLine("Informe a ID da Ordem de Serviço ao qual deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var ordemServicoRetorno = ordensServicoServices.Confirmar(id);

                    List<int> idsProdutos = new List<int>();

                    Console.WriteLine("Informe a ID do Produto que deseja inserir na Ordem de Serviço:");
                    int idProduto = Convert.ToInt32(Console.ReadLine());
                    idsProdutos.Add(idProduto);
                    var resultado = ordensServicoServices.AdicionarProduto(id, idsProdutos);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.RemoverCliente)
                {
                    Console.WriteLine("Digite a ID do Cliente para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var cliente = clientesServices.Confirmar(id);
                    if (cliente != null && cliente.IdCliente == id)
                    {
                        Console.WriteLine("***************************************");
                        Console.WriteLine($"Id Cliente: " + cliente.IdCliente);
                        Console.WriteLine($"   Cliente: " + cliente.NomeCliente);
                        Console.WriteLine("***************************************");
                        Console.WriteLine("=====> Digite 1 se deseja mesmo deletar.<=====");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = clientesServices.Remover(id);
                            Console.WriteLine("***************************************");
                            Console.WriteLine(resultado);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.RemoverProfissional)
                {
                    Console.WriteLine("Digite a ID do Profissional para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var profissional = profissionaisServices.Confirmar(id);
                    if (profissional != null && profissional.IdProfissional == id)
                    {
                        Console.WriteLine("***************************************");
                        Console.WriteLine($"Id Profissional: " + profissional.IdProfissional);
                        Console.WriteLine($"   Profissional: " + profissional.NomeProfissional);
                        Console.WriteLine("***************************************");
                        Console.WriteLine("=====> Digite 1 se deseja mesmo deletar.<=====");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = profissionaisServices.Remover(id);
                            Console.WriteLine("***************************************");
                            Console.WriteLine(resultado);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.RemoverProduto)
                {
                    Console.WriteLine("Digite a ID do Produto para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var produto = produtosServices.Confirmar(id);
                    if (produto != null && produto.IdProduto == id)
                    {
                        Console.WriteLine("***************************************");
                        Console.WriteLine($"Id Produto: " + produto.IdProduto);
                        Console.WriteLine($"   Produto: " + produto.DescricaoPeca);
                        Console.WriteLine("***************************************");
                        Console.WriteLine("=====> Digite 1 se deseja mesmo deletar.<=====");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = produtosServices.Remover(id);
                            Console.WriteLine("***************************************");
                            Console.WriteLine(resultado);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.RemoverServico)
                {
                    Console.WriteLine("Digite a ID do Serviço para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var servico = servicosServices.Confirmar(id);
                    if (servico != null && servico.IdServico == id)
                    {
                        Console.WriteLine("***************************************");
                        Console.WriteLine($"       Id Serviço: " + servico.IdServico);
                        Console.WriteLine($"Descrição Serviço: " + servico.DescricaoServico);
                        Console.WriteLine("***************************************");
                        Console.WriteLine("=====> Digite 1 se deseja mesmo deletar.<=====");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = servicosServices.Remover(id);
                            Console.WriteLine("***************************************");
                            Console.WriteLine(resultado);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarCliente)
                {
                    Console.WriteLine("Informe o Nome do Cliente:");
                    string nome = Console.ReadLine();
                    List<ClientesDto> clientes = new List<ClientesDto>();
                    if (nome != null)
                    {
                        clientes = clientesServices.BuscarPorNome(nome);
                        foreach (var item in clientes)
                        {
                            Console.WriteLine("***************************************");
                            Console.WriteLine($"    Nome: " + item.NomeCliente);
                            Console.WriteLine($"     CPF: " + item.CpfCliente);
                            Console.WriteLine($"Telefone: " + item.TelefoneCliente);
                            Console.WriteLine($"Endereço: " + item.EnderecoCliente);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarProfissional)
                {
                    Console.WriteLine("Informe o Nome do Profissional:");
                    string nome = Console.ReadLine();
                    List<ProfissionaisDto> profissionais = new List<ProfissionaisDto>();
                    if (nome != null)
                    {
                        profissionais = profissionaisServices.BuscarPorNome(nome);
                        foreach (var item in profissionais)
                        {
                            Console.WriteLine("***************************************");
                            Console.WriteLine($" Nome: " + item.NomeProfissional);
                            Console.WriteLine($"Cargo: " + item.CargoProfissional);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarProduto)
                {
                    Console.WriteLine("Informe a Descrição do Produto:");
                    string nome = Console.ReadLine();
                    List<ProdutosDto> produtos = new List<ProdutosDto>();
                    if (nome != null)
                    {
                        produtos = produtosServices.BuscarPorNome(nome);
                        foreach (var item in produtos)
                        {
                            Console.WriteLine("***************************************");
                            Console.WriteLine($" Peça: " + item.DescricaoPeca);
                            Console.WriteLine($"Valor: " + item.ValorPeca);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarServico)
                {
                    Console.WriteLine("Informe a Descrição do Serviço:");
                    string nome = Console.ReadLine();
                    List<Client.Dtos.Servicos.ServicosDto> servicos = new List<ServicosDto>();
                    if (nome != null)
                    {
                        servicos = servicosServices.BuscarPorNome(nome);
                        foreach (var item in servicos)
                        {
                            Console.WriteLine("***************************************");
                            Console.WriteLine($"Serviço: " + item.DescricaoServico);
                            Console.WriteLine($"  Valor: " + item.ValorServico);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarOrdemServico) //Erro
                {
                    Console.WriteLine("Informe o ID da Ordem do Serviço:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    if (id != 0)
                    {
                        var ordensServico = ordensServicoServices.BuscarPorIdOrdemServico(id);

                        Console.WriteLine("***************************************");
                        Console.WriteLine($"        id Cliente: " + ordensServico.IdCliente);
                        foreach (var item in ordensServico.Execucoes)
                        {
                            Console.WriteLine("Id Profissional: " + item.IdProfissional);
                        }
                        foreach (var item in ordensServico.ServicosExecutados)
                        {
                            Console.WriteLine("Id Profissional: " + item.IdServico);
                        }
                        foreach (var item in ordensServico.Itens)
                        {
                            Console.WriteLine($"     Id Produto: " + item.IdProduto);
                        }
                        Console.WriteLine($"         totalGeral: R$ " + ordensServico.TotalGeral);
                        Console.WriteLine("***************************************\n");
                    }
                }
                if (opcoes == Opcoes.CadastrarVeiculo)
                {
                    var veiculo = new VeiculosDto();
                    Console.WriteLine("Informe o Id do Cliente:");
                    veiculo.IdCliente = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Informe o Veículo:");
                    veiculo.VeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Placa do Veículo:");
                    veiculo.PlacaVeiculoCliente = Console.ReadLine();
                    Console.WriteLine("Informe a Cor do Veículo:");
                    veiculo.CorVeiculoCliente = Console.ReadLine();
                    var resultado = veiculosServices.Salvar(veiculo);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.AtualizarVeiculo)
                {
                    VeiculosDto veiculos = new VeiculosDto();
                    Console.WriteLine("Informe a ID do Veiculo que deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var veiculoRetorno = veiculosServices.Confirmar(id);
                    if (veiculoRetorno != null && veiculoRetorno.IdVeiculo == id)
                    {
                        Console.WriteLine("O Veículo cadastrado é: " + veiculoRetorno.VeiculoCliente + "\nDigite o novo Veículo caso deseje alterar.");
                        veiculoRetorno.VeiculoCliente = Console.ReadLine();
                        Console.WriteLine("A placa cadastrada é: " + veiculoRetorno.PlacaVeiculoCliente + "\nDigite a nova Placa caso deseje alterar.");
                        veiculoRetorno.PlacaVeiculoCliente = Console.ReadLine();
                        Console.WriteLine("A cor cadastrada é: " + veiculoRetorno.CorVeiculoCliente + "\nDigite a nova Cor caso deseje alterar.");
                        veiculoRetorno.CorVeiculoCliente = Console.ReadLine();
                        var resultado = veiculosServices.Atualizar(id, veiculoRetorno);
                        Console.WriteLine("***************************************");
                        Console.WriteLine(resultado);
                        Console.WriteLine("***************************************\n");
                    }
                }
                if (opcoes == Opcoes.RemoverVeiculo)
                {
                    Console.WriteLine("Digite a ID do Veículo para remover:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var veiculo = veiculosServices.Confirmar(id);
                    if (veiculo != null && veiculo.IdCliente == id)
                    {
                        Console.WriteLine("***************************************");
                        Console.WriteLine($"Id Veiculo: " + veiculo.IdVeiculo);
                        Console.WriteLine($"Id Cliente: " + veiculo.IdCliente);
                        Console.WriteLine("***************************************");
                        Console.WriteLine("=====> Digite 1 se deseja mesmo deletar.<=====");
                        int confirma = Convert.ToInt32(Console.ReadLine());
                        if (confirma == 1)
                        {
                            var resultado = veiculosServices.Remover(id);
                            Console.WriteLine("***************************************");
                            Console.WriteLine(resultado);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.PesquisarVeiculo)
                {
                    Console.WriteLine("Informe o Veiculo do Cliente:");
                    string veiculo = Console.ReadLine();
                    List<VeiculosDto> veiculos = new List<VeiculosDto>();
                    if (veiculo != null)
                    {
                        veiculos = veiculosServices.BuscarPorNome(veiculo);
                        foreach (var item in veiculos)
                        {
                            Console.WriteLine("***************************************");
                            Console.WriteLine($" Veículo: " + item.VeiculoCliente);
                            Console.WriteLine($"   Placa: " + item.PlacaVeiculoCliente);
                            Console.WriteLine($"     Cor: " + item.CorVeiculoCliente);
                            Console.WriteLine("***************************************\n");
                        }
                    }
                }
                if (opcoes == Opcoes.AtualizarOrdemServicoServico)
                {
                    Console.WriteLine("Informe a ID da Ordem de Serviço ao qual deseja atualizar:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var ordemServicoRetorno = ordensServicoServices.Confirmar(id);

                    List<int> idsServicos = new List<int>();

                    Console.WriteLine("Informe a ID do Serviço que deseja inserir na Ordem de Serviço:");
                    int idServico = Convert.ToInt32(Console.ReadLine());
                    idsServicos.Add(idServico);
                    var resultado = ordensServicoServices.AdicionarServico(id, idsServicos);
                    Console.WriteLine("***************************************");
                    Console.WriteLine(resultado);
                    Console.WriteLine("***************************************\n");
                }
                if (opcoes == Opcoes.PesquisarTodos)
                {
                    BuscarTodos buscar;
                    do
                    {
                        Console.WriteLine("ESCOLHA A BUSCA DESEJADA:\n------------------------->\n0-Sair\n------------------------->\n" +
                                            "1-Buscar Todos os Clientes\n2-Buscar Todos os Profissionais\n3-Buscar Todos os Produtos\n4-Buscar Todos os Serviços\n5-Buscar Faturamento Bruto Total\n6-Buscar Todos os Veículos");
                        buscar = (BuscarTodos)Convert.ToInt32(Console.ReadLine());

                        if (buscar == BuscarTodos.BuscarClientes)
                        {
                            var cliente = clientesServices.BuscarTodos();
                            foreach (var item in cliente)
                            {
                                Console.WriteLine("******************************");
                                Console.WriteLine($"      ID: " + item.IdCliente);
                                Console.WriteLine($"    Nome: " + item.NomeCliente);
                                Console.WriteLine($"     CPF: " + item.CpfCliente);
                                Console.WriteLine($"Telefone: " + item.TelefoneCliente);
                                Console.WriteLine($"Endereço: " + item.EnderecoCliente);
                                Console.WriteLine("******************************\n");
                            }
                        }
                        if (buscar == BuscarTodos.BuscarProfissionais)
                        {
                            var profissional = profissionaisServices.BuscarTodos();
                            foreach (var item in profissional)
                            {
                                Console.WriteLine("******************************");
                                Console.WriteLine($"   ID: " + item.IdProfissional);
                                Console.WriteLine($" Nome: " + item.NomeProfissional);
                                Console.WriteLine($"Cargo: " + item.CargoProfissional);
                                Console.WriteLine("******************************\n");
                            }
                        }
                        if (buscar == BuscarTodos.BuscarProdutos)
                        {
                            var produto = produtosServices.BuscarTodos();
                            foreach (var item in produto)
                            {
                                Console.WriteLine("******************************");
                                Console.WriteLine($"       ID: " + item.IdProduto);
                                Console.WriteLine($"Descrição: " + item.DescricaoPeca);
                                Console.WriteLine($"    Valor: " + item.ValorPeca);
                                Console.WriteLine("******************************\n");
                            }
                        }
                        if (buscar == BuscarTodos.BuscarServiços)
                        {
                            var servicos = servicosServices.BuscarTodos();
                            foreach (var item in servicos)
                            {
                                Console.WriteLine("******************************");
                                Console.WriteLine($"      ID: " + item.IdServico);
                                Console.WriteLine($"    Nome: " + item.DescricaoServico);
                                Console.WriteLine($"     CPF: " + item.ValorServico);
                                Console.WriteLine("******************************\n");
                            }
                        }
                        if (buscar == BuscarTodos.BuscarFaturamentoBruto)
                        {
                            var ordensServico = ordensServicoServices.Faturamento();

                            Console.WriteLine("******************************");
                            Console.WriteLine($"Faturamento Bruto: R$ " + ordensServico);
                            Console.WriteLine("******************************\n");
                        }
                        if (buscar == BuscarTodos.BuscarVeiculos)
                        {
                            var veiculos = veiculosServices.BuscarTodos();
                            foreach (var item in veiculos)
                            {
                                Console.WriteLine("******************************");
                                Console.WriteLine($" Veículo: " + item.VeiculoCliente);
                                Console.WriteLine($"   Placa: " + item.PlacaVeiculoCliente);
                                Console.WriteLine($"     Cor: " + item.CorVeiculoCliente);
                                Console.WriteLine("******************************\n");
                            }
                        }

                    }
                    while (buscar != BuscarTodos.Sair);
                }
                
                Console.WriteLine("======================================");
                Console.WriteLine("DIGITE A OPÇÃO DESEJADA:\n------------------------------>\n0-Sair\n------------------------------>\n" +
                           "1-Cadastrar Cliente\n2-Cadastrar Profissional\n3-Cadastrar Produto\n4-Cadastrar Serviço\n5-Cadastrar Ordem de Serviço\n------------------------------>\n" +
                           "6-Atualizar Cliente\n7-Atualizar Profissional\n8-Atualizar Produto\n9-Atualizar Serviço\n------------------------------>\n10-Inserir Profissional na Ordem de Serviço\n11-Inserir Produto na Ordem de Serviço\n------------------------------>\n" +
                           "12-Remover Cliente\n13-Remover Profissional\n14-Remover Produto\n15-Remover Serviço\n------------------------------>\n" +
                           "16-Pesquisar Cliente\n17-Pesquisar Profissional\n18-Pesquisar Produto\n19-Pesquisar Serviço\n20-Pesquisar Ordem de Serviço\n21-Cadastrar Veículo\n22-Atualizar Veículo\n23-Remover Veículo\n24-Remover Veículo\n25-Inserir Produto NaOrdem de Serviço" +
                           "\n------------------------------>\n26-Buscas Gerais");
                opcoes = (Opcoes)Convert.ToInt32(Console.ReadLine());
                
            }
        }
    }
}


    