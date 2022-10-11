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


        public bool Salvar(CadastrarProdutoViewModel salvarProdutoViewModel)
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
                var query = @"SELECT IdProduto, DescricaoPeca, ValorPeca FROM Produtos WHERE DescricaoPeca = @nome";

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
        public List<ProdutosDto> BuscarTodos()
        {
            List<ProdutosDto> produtosEncontrados;
            try
            {
                var query = @"SELECT IdProduto, DescricaoPeca, ValorPeca FROM Produtos";

                using (var connection = new SqlConnection(_connection))
                {
                    produtosEncontrados = connection.Query<ProdutosDto>(query).ToList();
                }
                return produtosEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(Produtos produtos)
        {
            try
            {
                var query = @"UPDATE Produtos SET DescricaoPeca = @descricaoPeca, ValorPeca = @valorPeca WHERE IdProduto = @idProduto";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idProduto", produtos.IdProduto);
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
        public ProdutosDto Confirmar(int idProduto)
        {
            var produto = new ProdutosDto();
            try
            {
                var query = "SELECT * FROM Produtos WHERE IdProduto = @idProduto";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idProduto
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
        public void Deletar(int id)
        {
            try
            {
                var query = "DELETE FROM Produtos WHERE idProduto = @id";
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
