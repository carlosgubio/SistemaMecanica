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
    public class ServicosRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public bool SalvarServico(CadastrarServicoViewModel salvarServiçoViewModel)
        {
            try
            {
                var query = @"INSERT INTO Servicos (DescricaoServico, ValorServico) VALUES (@descricaoServico, @valorServico)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idServico", salvarServiçoViewModel.IdServico);
                    command.Parameters.AddWithValue("@descricaoServico", salvarServiçoViewModel.DescricaoServico);
                    command.Parameters.AddWithValue("@valorServico", salvarServiçoViewModel.ValorServico);
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
        public List<ProdutosDto> BuscarServicos(string descricaoServico)
        {
            List<ProdutosDto> servicosEncontrados;
            try
            {
                var query = @"SELECT IdServico, DescricaoServico, ValorServico FROM Servicos WHERE DescricaoServico like CONCAT('%',@descricaoServico,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        descricaoServico
                    };
                    servicosEncontrados = connection.Query<ProdutosDto>(query, parametros).ToList();
                    return servicosEncontrados;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
    }
}
