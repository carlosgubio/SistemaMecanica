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
    public class ProdutosRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

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


    }
}
