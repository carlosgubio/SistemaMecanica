﻿using Dapper;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using SistemaMecanica.ViewModelsCadastrar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Repositories
{
    public class VeiculosRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";


        public bool Salvar(CadastrarVeiculoViewModel cadastrarVeiculoViewModel)
        {
            try
            {
                var query = @"INSERT INTO Veiculos 
                            (VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente, IdCliente) 
                            VALUES (@veiculoCliente,@placaVeiculoCliente,@corVeiculoCliente,@idcliente)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@veiculoCliente", cadastrarVeiculoViewModel.VeiculoCliente);
                    command.Parameters.AddWithValue("@placaVeiculoCliente", cadastrarVeiculoViewModel.PlacaVeiculoCliente);
                    command.Parameters.AddWithValue("@corVeiculoCliente", cadastrarVeiculoViewModel.CorVeiculoCliente);
                    command.Parameters.AddWithValue("@idcliente", cadastrarVeiculoViewModel.IdCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Veículo cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public List<VeiculosDto> BuscarPorNome(string nome)
        {
            List<VeiculosDto> veiculosEncontrados;
            try
            {
                var query = @"SELECT VeiculoCliente, PlacaVeiculoCliente, CorVeiculocliente, IdCliente FROM Veiculos
                                    WHERE VeiculoCliente = @veiculoCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
                    };
                veiculosEncontrados = connection.Query<VeiculosDto>(query, parametros).ToList();
                }

                return veiculosEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<VeiculosDto> BuscarTodos()
        {
            List<VeiculosDto> veiculosEncontrados;
            try
            {
                var query = @"SELECT VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente, IdCliente FROM Veiculos";

                using (var connection = new SqlConnection(_connection))
                {
                veiculosEncontrados = connection.Query<VeiculosDto>(query).ToList();
                }
                return veiculosEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(Veiculos veiculos, int id)
        {
            try
            {
                var query = @"UPDATE Veiculos SET VeiculoCliente = @veiculoCliente, PlacaVeiculoCliente = @placaVeiculoCliente, CorVeiculoCliente = @corVeiculoCliente,  IdCliente = @idCliente WHERE IdVeiculo = @idVeiculo";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idVeiculo", id);
                    command.Parameters.AddWithValue("@veiculoCliente", veiculos.VeiculoCliente);
                    command.Parameters.AddWithValue("@placaVeiculoCliente", veiculos.PlacaVeiculoCliente);
                    command.Parameters.AddWithValue("@corVeiculoCliente", veiculos.CorVeiculoCliente);
                    command.Parameters.AddWithValue("@idCliente", veiculos.IdCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public VeiculosDto Confirmar(int idVeiculo)
        {
            var veiculo = new VeiculosDto();
            try
            {
                var query = "SELECT * FROM Veiculos WHERE IdVeiculo = @idVeiculo";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idVeiculo
                    };
                veiculo = connection.QueryFirstOrDefault<VeiculosDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                veiculo = null;
            }
            return veiculo;
        }
        public void Deletar(int id)
        {
            try
            {
                var query = "Delete From Veiculos where IdVeiculo = @id";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@id", id);
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
