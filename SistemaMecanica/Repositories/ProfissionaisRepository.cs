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
    public class ProfissionaisRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public bool SalvarProfissional(CadastrarProfissionalViewModel salvarProfissionalViewModel)
        {
            //int IdProfissionalCriada = -1;
            try
            {
                var query = @"INSERT INTO Profissionais (NomeProfissional, CargoProfissional) VALUES (@nomeProfissional,@cargoProfissional)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nomeProfissional", salvarProfissionalViewModel.NomeProfissional);
                    command.Parameters.AddWithValue("@cargoProfissional", salvarProfissionalViewModel.CargoProfissional);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    //IdProfissionalCriada = (int)command.ExecuteScalar();
                }
                Console.WriteLine("Profissional Cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public List<ProfissionaisDto> BuscarProfissionais(string nomeProfissional)
        {
            List<ProfissionaisDto> profissionaisEncontrados;
            try
            {
                var query = @"SELECT NomeProfissional, CargoProfissional FROM Profissionais WHERE NomeProfissional like CONCAT('%',@nomeProfissional,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nomeProfissional
                    };
                    profissionaisEncontrados = connection.Query<ProfissionaisDto>(query, parametros).ToList();

                    return profissionaisEncontrados;
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
