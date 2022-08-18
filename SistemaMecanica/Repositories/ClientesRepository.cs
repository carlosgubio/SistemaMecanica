﻿using Dapper;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using SistemaMecanica.ViewModels;
using SistemaMecanica.ViewModelsAtualizar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Repositories
{
    public class ClientesRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        public bool SalvarCliente(CadastrarClienteViewModel salvarPessoaViewModel)
        {
            try
            {
                var query = @"INSERT INTO Clientes 
                              (NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente) 
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
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Cliente cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public List<ClientesDto> BuscarPorNomeCliente(string nomeCliente)
        {
            List<ClientesDto> ClientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculocliente FROM Clientes
                                      WHERE NomeCliente like CONCAT('%',@nomeCliente,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nomeCliente
                    };
                    ClientesEncontrados = connection.Query<ClientesDto>(query, parametros).ToList();
                }

                return ClientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<ClientesDto> BuscarTodos()
        {
            List<ClientesDto> clientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente FROM Clientes";

                using (var connection = new SqlConnection(_connection))
                {
                    clientesEncontrados = connection.Query<ClientesDto>(query).ToList();
                }
                return clientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(Clientes clientes, int id)
        {
            try
            {
                var query = @"UPDATE Clientes set NomeCliente = @nomeCliente, CpfCliente = @cpfCliente, TelefoneCliente = @telefoneCliente, EnderecoCliente = @enderecoCliente, VeiculoCliente = @veiculoCliente, PlacaVeiculoCliente = @placaVeiculoCliente, CorVeiculoCliente = @corVeiculoCliente WHERE IdCliente = @idCliente";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idCliente", id);
                    command.Parameters.AddWithValue("@nomeCliente", clientes.NomeCliente);
                    command.Parameters.AddWithValue("@cpfCliente", clientes.CpfCliente);
                    command.Parameters.AddWithValue("@telefoneCliente", clientes.TelefoneCliente);
                    command.Parameters.AddWithValue("@enderecoCliente", clientes.EnderecoCliente);
                    command.Parameters.AddWithValue("@veiculoCliente", clientes.VeiculoCliente);
                    command.Parameters.AddWithValue("@placaVeiculoCliente", clientes.PlacaVeiculoCliente);
                    command.Parameters.AddWithValue("@corVeiculoCliente", clientes.CorVeiculoCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
