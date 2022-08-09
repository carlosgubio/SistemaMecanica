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
    public class ClientesRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        public bool SalvarCliente(Clientes clientes, Profissionais profissionais, Servicos servicos, Produtos produtos)
        {
            int IdPessoaCriada = -1;
            try
            {
                var query = @"INSERT INTO Clientes 
                              (NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculoCliente) 
                              OUTPUT Inserted.Id
                              VALUES (@nomeCliente,@cpfCliente,@telefoneCliente,@enderecoCliente,@veiculoCliente,@placaVeiculoCliente,@corVeiculoCliente)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nomeCliente", clientes.NomeCliente);
                    command.Parameters.AddWithValue("@cpfCliente", clientes.CpfCliente);
                    command.Parameters.AddWithValue("@telefoneCliente", clientes.TelefoneCliente);
                    command.Parameters.AddWithValue("@enderecoCliente", clientes.EnderecoCliente);
                    command.Parameters.AddWithValue("@veiculoCliente", clientes.VeiculoCliente);
                    command.Parameters.AddWithValue("@placaVeiculoCliente", clientes.PlacaVeiculoCliente);
                    command.Parameters.AddWithValue("@corVeiculoCliente", clientes.CorVeiculoCliente);

                    command.Connection.Open();
                    IdPessoaCriada = (int)command.ExecuteScalar();
                }

                SalvarProfissionais(profissionais, IdPessoaCriada);
                SalvarServicos(servicos, IdPessoaCriada);
                SalvarProduto(produtos, IdPessoaCriada);

                Console.WriteLine("Cliente cadastrado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        private void SalvarProfissionais(Profissionais profissionais, int IdCliente)
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
        private void SalvarServicos(Servicos servicos, int IdCliente)
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
        private void SalvarProduto(Produtos produtos, int IdCliente)
        {
            try
            {
                var query = @"INSERT INTO Produtos 
                              (DescricaoPeca, ValorPeca, IdPessoa)                               
                              VALUES (@descricaoPeca,@valorPeca,@idPessoa)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@descricaoPeca", produtos.DescricaoPeca);
                    command.Parameters.AddWithValue("@valorPeca", produtos.ValorPeca);
                    command.Parameters.AddWithValue("@idPessoa", IdCliente);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Peça cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public List<ClientesDto> BuscarPorNomeClientes(string nomeCliente)
        {
            List<ClientesDto> ClientesEncontrados;
            try
            {
                var query = @"SELECT Id, NomeCliente, CpfCliente, TelefoneCliente, EnderecoCliente, VeiculoCliente, PlacaVeiculoCliente, CorVeiculocliente FROM Clientes
                                      WHERE NomeCliente like CONCAT('%',@nomeCliente,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nomeCliente
                    };
                    ClientesEncontrados = connection.Query<ClientesDto>(query, parametros).ToList();
                }

                ClientesEncontrados.ForEach(e =>
                {
                    e.Profissionais = BuscarProfissional(e.Id);
                    e.Produtos = BuscarProdutos(e.Id);
                    e.Servicos = BuscarServicos(e.Id);
                    e.OrdemServico = BuscarOrdemServico(e.Id);
                });
                return ClientesEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private ProfissionaisDto BuscarProfissional(int idclientes)
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
        private ProdutosDto BuscarProdutos(int idclientes)
        {
            try
            {
                var query = @"SELECT * FROM Produtos
                                      WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idclientes
                    };
                    return connection.QueryFirstOrDefault<ProdutosDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
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
        private OrdemServicoDto BuscarOrdemServico(int idclientes)
        {

            try
            {
                var query = @"SELECT * FROM OrdemServico
                                      WHERE IdCliente = @idCliente";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idclientes
                    };
                    return connection.QueryFirstOrDefault<OrdemServicoDto>(query, parametros);
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
