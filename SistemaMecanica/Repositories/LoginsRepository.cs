using Dapper;
using SistemaMecanica.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Repositories
{
    public class LoginsRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public LoginsDto ConfirmarCliente(int idCliente)
        {
            var login = new LoginsDto();
            try
            {
                var query = "SELECT * FROM Logins WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idCliente
                    };
                    login = connection.QueryFirstOrDefault<LoginsDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                login = null;
            }
            return login;
        }
        public LoginsDto ConfirmarProfissional(int idProfissional)
        {
            var login = new LoginsDto();
            try
            {
                var query = "SELECT * FROM Logins WHERE IdProfissionais = @idProfissionais";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idProfissional
                    };
                    login = connection.QueryFirstOrDefault<LoginsDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                login = null;
            }
            return login;
        }

        public List<LoginsDto> BuscarLogin(string nome)
        {
            List<LoginsDto> LoginEncontrado;
            try
            {
                var query = @"SELECT IdCliente, IdProfissional, Usuario, Senha FROM Logins";
                                     

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
                    };
                    LoginEncontrado = connection.Query<LoginsDto>(query, parametros).ToList();
                }

                return LoginEncontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
    }
}
