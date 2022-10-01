using Dapper;
using SistemaMecanica.Dtos;
using SistemaMecanica.Models;
using SistemaMecanica.ViewModels;
using SistemaMecanica.ViewModelsAtualizar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaMecanica.Repositories
{
    public class ClientesRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";


        public int Salvar(CadastrarClienteViewModel cadastrarClienteViewModel)
        {
            int IdCliente = -1;
            try
            {
                var query = @"INSERT INTO Clientes (NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente) 
                              OUTPUT Inserted.IdCliente
                              VALUES (@nomeCliente,@cpfCliente,@telefoneCliente,@enderecoCliente)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nomeCliente", cadastrarClienteViewModel.Cliente.NomeCliente);
                    command.Parameters.AddWithValue("@cpfCliente", cadastrarClienteViewModel.Cliente.CpfCliente);
                    command.Parameters.AddWithValue("@telefoneCliente", cadastrarClienteViewModel.Cliente.TelefoneCliente);
                    command.Parameters.AddWithValue("@enderecoCliente", cadastrarClienteViewModel.Cliente.EnderecoCliente);
                    command.Connection.Open();
                    IdCliente = (int)command.ExecuteScalar();
                }
                Console.WriteLine("Cliente cadastrado com sucesso!");
                return IdCliente;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return IdCliente;
            }
        }
        public List<ClientesDto> BuscarPorNome(string nome)
        {
            List<ClientesDto> ClientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente FROM Clientes
                                      WHERE NomeCliente like CONCAT('%', @nome, '%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
                    };
                    ClientesEncontrados = connection.Query<ClientesDto>(query, parametros).ToList();
                }

                return ClientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<ClientesDto> BuscarTodos()
        {
            List<ClientesDto> clientesEncontrados;
            try
            {
                var query = @"SELECT IdCliente, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente FROM Clientes";

                using (var connection = new SqlConnection(_connection))
                {
                    clientesEncontrados = connection.Query<ClientesDto>(query).ToList();
                }
                return clientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(Clientes clientes, int id)
        {
            try
            {
                var query = @"UPDATE Clientes SET NomeCliente = @nomeCliente, CpfCliente = @cpfCliente, TelefoneCliente = @telefoneCliente, EnderecoCliente = @enderecoCliente
                             WHERE IdCliente = @idCliente";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idCliente", id);
                    command.Parameters.AddWithValue("@nomeCliente", clientes.NomeCliente);
                    command.Parameters.AddWithValue("@cpfCliente", clientes.CpfCliente);
                    command.Parameters.AddWithValue("@telefoneCliente", clientes.TelefoneCliente);
                    command.Parameters.AddWithValue("@enderecoCliente", clientes.EnderecoCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public ClientesDto Confirmar(int idCliente)
        {
            var cliente = new ClientesDto();
            try
            {
                var query = "SELECT * FROM Clientes WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idCliente
                    };
                    cliente = connection.QueryFirstOrDefault<ClientesDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                cliente = null;
            }
            return cliente;
        }
        public void Deletar(int id) 
        {
            try
            {
                var query = "Delete From Clientes where IdCliente = @id";
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
        //bool VerificaLogin()
        //{
        //    bool result = false;
        //    //string StringDeConexao = @”Data Source = localhost; Initial Catalog = tempdb; User Id = sa; Password = minhasenha;”;
        //    //using (SqlConnection cn = new SqlConnection())
        //    {
        //        //cn.ConnectionString = StringDeConexao;
        //        List<ClientesDto> clientesEncontrados;
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(“select * from login where usuario = ‘” +txtUsuario.Text + “‘ and senha = ‘” +txtSenha.Text + “‘;”, cn);
        //            cn.Open();
        //            SqlDataReader dados = cmd.ExecuteReader();
        //            result = dados.HasRows;

        //        }
        //        catch (SqlException e)
        //        {
        //            throw new Exception(e.Message);
        //        }
        //        finally
        //        {
        //            cn.Close();
        //        }
        //    }
        //    return result;
        //}
    }
}
