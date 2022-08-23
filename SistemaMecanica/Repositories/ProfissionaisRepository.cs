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
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public bool SalvarProfissional(CadastrarProfissionalViewModel salvarProfissionalViewModel)
        {
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
                var query = @"SELECT IdProfissional, NomeProfissional, CargoProfissional FROM Profissionais WHERE NomeProfissional like CONCAT('%',@nomeProfissional,'%')";

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
        public void Atualizar(Profissionais profissionais, int id)
        {
            try
            {
                var query = @"UPDATE Profissionais set NomeProfissional = @nomeProfissional, CargoProfissional = @cargoProfissional WHERE IdProfissional = @idProfissional";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idProfissional", id);
                    command.Parameters.AddWithValue("@nomeProfissional", profissionais.NomeProfissional);
                    command.Parameters.AddWithValue("@cargoProfissional", profissionais.CargoProfissional);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public ProfissionaisDto ConfirmarProfissional(int idProfissional)
        {
            var Profissional = new ProfissionaisDto();
            try
            {
                var query = "SELECT * FROM Profissionais WHERE IdProfissional = @idProfissional";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idProfissional
                    };
                    Profissional = connection.QueryFirstOrDefault<ProfissionaisDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Profissional = null;
            }
            return Profissional;
        }
    }
}
