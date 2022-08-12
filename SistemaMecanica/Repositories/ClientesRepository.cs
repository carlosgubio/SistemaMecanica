﻿using Dapper;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using SistemaMecanica.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Repositories
{
    public class ClientesRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        public bool SalvarCliente(SalvarClienteViewModel salvarPessoaViewModel)
        {
            int IdPessoaCriada = -1;
            try
            {
                var query = @"INSERT INTO Clientes 
                              (NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente) 
                              OUTPUT Inserted.Id
                              VALUES (@nomeCliente,@cpfCliente,@telefoneCliente,@enderecoCliente,@veiculoCliente,@placaVeiculoCliente,@corVeiculoCliente)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nomeCliente", salvarPessoaViewModel.NomeCliente);
                    command.Parameters.AddWithValue("@cpfCliente", salvarPessoaViewModel.CpfCliente);
                    command.Parameters.AddWithValue("@telefoneCliente", salvarPessoaViewModel.TelefoneCliente);
                    command.Parameters.AddWithValue("@enderecoCliente", salvarPessoaViewModel.EnderecoCliente);
                    command.Parameters.AddWithValue("@veiculoCliente", salvarPessoaViewModel.VeiculoCliente);
                    command.Parameters.AddWithValue("@placaVeiculoCliente", salvarPessoaViewModel.PlacaVeiculoCliente);
                    command.Parameters.AddWithValue("@corVeiculoCliente", salvarPessoaViewModel.CorVeiculoCliente);

                    command.Connection.Open();
                    IdPessoaCriada = (int)command.ExecuteScalar();
                }

                //SalvarProfissionais(profissionais, IdPessoaCriada);
                //SalvarServicos(servicos, IdPessoaCriada);
                //SalvarProduto(produtos, IdPessoaCriada);

                Console.WriteLine("Cliente cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public List<ClientesDto> BuscarPorNomeClientes(string nomeCliente)
        {
            List<ClientesDto> ClientesEncontrados;
            try
            {
                var query = @"SELECT Id, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculocliente FROM Clientes
                                      WHERE NomeCliente like CONCAT('%',@nomeCliente,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nomeCliente
                    };
                    ClientesEncontrados = connection.Query<ClientesDto>(query, parametros).ToList();
                }

                ClientesEncontrados.ForEach(e =>
                {
                    e.Profissionais = BuscarProfissional(e.Id);
                    e.Produtos = BuscarProdutos(e.Id);
                    e.Servicos = BuscarServicos(e.Id);
                    e.OrdemServico = BuscarOrdemServico(e.Id);
                });
                return ClientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
    }
}