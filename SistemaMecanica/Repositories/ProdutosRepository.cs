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
    public class ProdutosRepository
    {
        private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        public bool SalvarProduto(CadastrarProdutoViewModel salvarProdutoViewModel)
        {
            try
            {
                var query = @"INSERT INTO Produtos (DescricaoPeca, ValorPeca) VALUES (@descricaoPeca,@valorPeca)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@descricaoPeca", salvarProdutoViewModel.DescricaoPeca);
                    command.Parameters.AddWithValue("@valorPeca", salvarProdutoViewModel.ValorPeca);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Peça cadastrada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;

            }
        }
        public List<ProdutosDto> BuscarPorNomeProduto(string descricaoProduto)
        {
            List<ProdutosDto> produtosEncontrados;
            try
            {
                var query = @"SELECT IdPeca, DescricaoProduto, ValorProduto FROM Produtos WHERE DescricaoProduto like CONCAT('%',@descricaoProduto,'%')";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        descricaoProduto
                    };
                    produtosEncontrados = connection.Query<ProdutosDto>(query, parametros).ToList();
                }  
                    return produtosEncontrados;                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
    }
}
