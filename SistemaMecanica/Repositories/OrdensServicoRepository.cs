using Dapper;
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
    public class OrdensServicoRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public bool SalvarOrdemServico(CadastrarOrdemServicoViewModel cadastrarOrdemServicoViewModel)
        {
            try
            {
                var query = @"INSERT INTO OrdensServico 
                              (IdProfissional, IdCliente, IdServico, IdPeca, TotalGeral) 
                              OUTPUT Inserted.Id
                              VALUES (@idProfissional,@idCliente,@idServico,@idPeca,@totalGeral)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idProfissional", cadastrarOrdemServicoViewModel.IdProfissional);
                    command.Parameters.AddWithValue("@idCliente", cadastrarOrdemServicoViewModel.IdCliente);
                    command.Parameters.AddWithValue("@idServico", cadastrarOrdemServicoViewModel.IdServico);
                    command.Parameters.AddWithValue("@idPeca", cadastrarOrdemServicoViewModel.IdPeca);
                    command.Parameters.AddWithValue("@totalGeral", cadastrarOrdemServicoViewModel.TotalGeral);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Ordem de Servico cadastrada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }

        public List<OrdensServicoDto> BuscarOrdemServico(string nomeVeiculoCliente)
        {
            List<OrdensServicoDto> OrdensServicoEncontrados;
            try
            {
                var query = @"SELECT * FROM OrdemServico
                                      WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nomeVeiculoCliente
                    };
                    OrdensServicoEncontrados = connection.Query<OrdensServicoDto>(query, parametros).ToList();
                    return OrdensServicoEncontrados;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(OrdensServico ordensServico, int id)
        {
            try
            {
                var query = @"UPDATE OrdensServico set IdProfissional = @idProfissional, IdCliente = @idCliente, IdServico = @idServico, IdPeca = @idpeca WHERE IdOrdemServico = @idOrdemServico";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idOrdemServico", id);
                    command.Parameters.AddWithValue("@idProfissional", ordensServico.IdProfissional);
                    command.Parameters.AddWithValue("@idCliente", ordensServico.IdCliente);
                    command.Parameters.AddWithValue("@idServico", ordensServico.IdServico);
                    command.Parameters.AddWithValue("@idpeca", ordensServico.IdPeca);
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
