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
    public class ProfissionaisRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        private void SalvarProfissional(Profissionais profissionais, int IdCliente)
        {
            try
            {
                var query = @"INSERT INTO Profissionais 
                              (NomeProfissional, CargoProfissional, IdPessoa)                               
                              VALUES (@nomeProfissional,@cargoProfissional,@idPessoa)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nomeProfissional", profissionais.NomeProfissional);
                    command.Parameters.AddWithValue("@cargoProfissional", profissionais.CargoProfissional);
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
        private ProfissionaisDto BuscarProfissionais(int idclientes)
        {
            try
            {
                var query = @"SELECT * FROM Profissionais
                                      WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idclientes
                    };
                    return connection.QueryFirstOrDefault<ProfissionaisDto>(query, parametros);
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
