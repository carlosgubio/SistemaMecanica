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
    public class ServicosRepository
    {
        //private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";


        public bool Salvar(CadastrarServicoViewModel cadastrarServiçoViewModel)
        {
            try
            {
                var query = @"INSERT INTO Servicos (DescricaoServico, ValorServico) VALUES (@descricaoServico, @valorServico)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    //command.Parameters.AddWithValue("@idServico", cadastrarServiçoViewModel.IdServico);
                    command.Parameters.AddWithValue("@descricaoServico", cadastrarServiçoViewModel.DescricaoServico);
                    command.Parameters.AddWithValue("@valorServico", cadastrarServiçoViewModel.ValorServico);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Serviço Cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public List<ServicosDto> BuscarPorNome(string nome)
        {
            List<ServicosDto> servicosEncontrados;
            try
            {
                var query = @"SELECT IdServico, DescricaoServico, ValorServico FROM Servicos WHERE DescricaoServico = @nome";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
                    };
                    servicosEncontrados = connection.Query<ServicosDto>(query, parametros).ToList();
                    return servicosEncontrados;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<ServicosDto> BuscarTodos()
        {
            List<ServicosDto> servicosEncontrados;
            try
            {
                var query = @"SELECT IdServico, DescricaoServico, ValorServico FROM Servicos";

                using (var connection = new SqlConnection(_connection))
                {
                    servicosEncontrados = connection.Query<ServicosDto>(query).ToList();
                }
                return servicosEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(Servicos servicos)
        {
            try
            {
                var query = @"UPDATE Servicos SET DescricaoServico = @descricaoServico, ValorServico = @valorServico WHERE IdServico = @idServico";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idServico", servicos.IdServico);
                    command.Parameters.AddWithValue("@descricaoServico", servicos.DescricaoServico);
                    command.Parameters.AddWithValue("@valorServico", servicos.ValorServico);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public ServicosDto Confirmar(int idServico)
        {
            var servico = new ServicosDto();
            try
            {
                var query = "SELECT * FROM Servicos WHERE IdServico = @idServico";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idServico
                    };
                    servico = connection.QueryFirstOrDefault<ServicosDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                servico = null;
            }
            return servico;
        }
        public void Deletar(int id)
        {
            try
            {
                var query = "DELETE FROM Servicos WHERE IdServico = @id";
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
