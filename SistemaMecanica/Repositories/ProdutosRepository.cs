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
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";


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
        public List<ProdutosDto> BuscarPorNome(string nome)
        {
            List<ProdutosDto> produtosEncontrados;
            try
            {
                var query = @"SELECT IdPeca, DescricaoPeca, ValorPeca FROM Produtos WHERE DescricaoPeca = @nome";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        nome
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
        public void Atualizar(Produtos produtos, int id)
        {
            try
            {
                var query = @"UPDATE Produtos set DescricaoPeca = @descricaoPeca, ValorPeca = @valorPeca WHERE IdPeca = @idPeca";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idPeca", id);
                    command.Parameters.AddWithValue("@descricaoPeca", produtos.DescricaoPeca);
                    command.Parameters.AddWithValue("@valorPeca", produtos.ValorPeca);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public ProdutosDto ConfirmarProduto(int idPeca)
        {
            var produto = new ProdutosDto();
            try
            {
                var query = "SELECT * FROM Produtos WHERE IdPeca = @idPeca";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idPeca
                    };
                    produto = connection.QueryFirstOrDefault<ProdutosDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                produto = null;
            }
            return produto;
        }

        public void DeletarProduto(int id)
        {
            try
            {
                var query = "Delete From Produtos where IdPeca = @id";
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
    }
}
