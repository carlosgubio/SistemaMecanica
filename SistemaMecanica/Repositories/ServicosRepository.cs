using Dapper;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
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

        private void SalvarServico(Servicos servicos, int IdCliente)
        {
            try
            {
                var query = @"INSERT INTO Servicos 
                              (DescricaoServico, ValorServico, IdPessoa)                               
                              VALUES (@descricaoServico,@valorServico,@idPessoa)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@descricaoPeca", servicos.DescricaoServico);
                    command.Parameters.AddWithValue("@valorPeca", servicos.ValorServico);
                    command.Parameters.AddWithValue("@idPessoa", IdCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Serviço Cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        private ServicosDto BuscarServicos(int idclientes)
        {
            try
            {
                var query = @"SELECT * FROM Servicos
                                      WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idclientes
                    };
                    return connection.QueryFirstOrDefault<ServicosDto>(query, parametros);
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
