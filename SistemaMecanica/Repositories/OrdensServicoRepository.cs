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
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

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
        public List<OrdensServicoDto> BuscarPorIDOrdemServico(int id)
        {
            List<OrdensServicoDto> OrdemServicoEncontrados;
            try
            {
                var query = @"SELECT IdCliente, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculocliente FROM Clientes
                                      WHERE NomeCliente LIKE CONCAT('%',@nomeCliente,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id
                    };
                    OrdemServicoEncontrados = connection.Query<OrdensServicoDto>(query, parametros).ToList();
                }

                return OrdemServicoEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<OrdensServicoDto> BuscarTodos()
        {
            List<OrdensServicoDto> ordensServicoEncontrados;
            try
            {
                var query = @"SELECT IdOrdemServico, IdCliente, IDProfissional, IdServico, IdPeca, TotalGeral FROM OrdensServico";

                using (var connection = new SqlConnection(_connection))
                {
                    ordensServicoEncontrados = connection.Query<OrdensServicoDto>(query).ToList();
                }
                return ordensServicoEncontrados;
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
                var query = @"UPDATE OrdensServico set IdCliente = @idCliente, IdProfissional = @idProfissional, IdServico = @idServico, IdPeca = @idPeca, TotalGeral = @totalGeral WHERE IdOrdemServico = @idOrdemServico";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idCliente", id);
                    command.Parameters.AddWithValue("@idProfissional", ordensServico.IdProfissional);
                    command.Parameters.AddWithValue("@idServico", ordensServico.IdServico);
                    command.Parameters.AddWithValue("@idPeca", ordensServico.IdPeca);
                    command.Parameters.AddWithValue("@totalGeral", ordensServico.TotalGeral);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public OrdensServicoDto ConfirmarOrdemServico(int idOrdemServico)
        {
            var ordemServico = new OrdensServicoDto();
            try
            {
                var query = "SELECT * FROM OrdensServico WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idOrdemServico
                    };
                    ordemServico = connection.QueryFirstOrDefault<OrdensServicoDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                ordemServico = null;
            }
            return ordemServico;
        }
        public void DeletarOrdemServico(int id)
        {
            try
            {
                var query = "Delete From OrdensServico where IdOrdemServico = @id";
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
