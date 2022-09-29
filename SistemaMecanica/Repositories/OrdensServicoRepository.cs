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
    public class OrdensServicoRepository
    {
        private readonly string _connection = @"Data Source=ITELABD02\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";
        //private readonly string _connection = @"Data Source=Gubio\SQLEXPRESS;Initial Catalog=SistemaMecanica;Integrated Security=True;";

        public bool Salvar(CadastrarOrdemServicoViewModel cadastrarOrdemServicoViewModel)
        {
            try
            {
                int idOrdemCriada = -1;
                var query = @"INSERT INTO OrdensServico (IdVeiculo) OUTPUT INSERTED.IdOrdemServico VALUES (@idVeiculo)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    //command.Parameters.AddWithValue("@idCliente", cadastrarOrdemServicoViewModel.IdCliente);
                    //command.Parameters.AddWithValue("@idProfissional", cadastrarOrdemServicoViewModel.IdProfissional);
                    //command.Parameters.AddWithValue("@idServico", cadastrarOrdemServicoViewModel.IdServico);
                    command.Parameters.AddWithValue("@idVeiculo", cadastrarOrdemServicoViewModel.IdVeiculo);
                    command.Connection.Open();
                    idOrdemCriada =  (int)command.ExecuteScalar();
                }

                VincularItensOs(cadastrarOrdemServicoViewModel.IdItens, idOrdemCriada);
                VincularExecucaosOs(cadastrarOrdemServicoViewModel.IdExecucoes, idOrdemCriada);
                VincularServicosExecutadosOs(cadastrarOrdemServicoViewModel.IdServicosExecutados, idOrdemCriada);
                

                var os = BuscarOrdemServicoPorId(idOrdemCriada);

                if(os != null) 
                {
                    os.TotalGeral = CalcularTotalOrdemServico(idOrdemCriada);
                    AtualizarTotalGeralOs(os);
                }

                Console.WriteLine("Ordem de Servico cadastrada com sucesso!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        public void AtualizarTotalGeralOs(OrdensServicoDto ordensServicoDto)
        {
            try
            {
                var query = @"UPDATE OrdensServico SET TotalGeral = @totalGeral WHERE IdOrdemServico = @idOrdemServico";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idOrdemServico", ordensServicoDto.IdOrdemServico);
                    command.Parameters.AddWithValue("@totalGeral", ordensServicoDto.TotalGeral);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public OrdensServicoDto BuscarPorIDOrdemServico(int id)
        {
            OrdensServicoDto ordensServicoDto;
            try
            {
                var query = @"SELECT IdOrdemServico, IdVeiculo TotalGeral FROM OrdensServico WHERE IdOrdemServico = @id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id
                    };
                    ordensServicoDto = connection.QueryFirst<OrdensServicoDto>(query, parametros);
                }

                if (ordensServicoDto != null)
                {
                    ordensServicoDto.Itens = BuscarProdutosDaOrdemId(id);
                }
                if (ordensServicoDto != null)
                {
                    ordensServicoDto.Execucoes = BuscarProfissionaisDaOrdemId(id);
                }

                return ordensServicoDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public OrdensServicoDto BuscarOrdemServicoPorId(int id)
        {
            OrdensServicoDto ordensServicoDto;
            try
            {
                var query = @"SELECT IdOrdemServico, IdVeiculo, TotalGeral FROM OrdensServico WHERE IdOrdemServico = @id";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        id
                    };
                    ordensServicoDto = connection.QueryFirst<OrdensServicoDto>(query, parametros);
                }

                if (ordensServicoDto != null)
                {
                    ordensServicoDto.Itens = BuscarProdutosDaOrdemId(id);
                }
                if (ordensServicoDto != null)
                {
                    ordensServicoDto.Execucoes = BuscarProfissionaisDaOrdemId(id);
                }
                if (ordensServicoDto != null)
                {
                    ordensServicoDto.ServicosExecutados = BuscarServicosDaOrdemId(id);
                }

                return ordensServicoDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public OrdemServicoDadosDto BuscarOrdemServicoPorVeiculo(string veiculo)
        {
            OrdemServicoDadosDto ordemServicoDadosDto;
            try
            {
                var query = @"SELECT c.NomeCliente AS NomeCliente, c.CpfCliente AS CpfCliente, c.TelefoneCliente AS TelefoneCliente, c.Enderecocliente AS Enderecocliente,
                            v.VeiculoCliente AS VeiculoCliente, v.PlacaVeiculoCliente AS PlacaVeiculoCliente, v.CorVeiculoCliente AS CorVeiculoCliente,
                            pf.NomeProfissional AS NomeProfissional, p.DescricaoPeca AS DescricaoPeca, p.ValorPeca AS ValorPeca, s.DescricaoServico AS DescricaoServico,
                            s.ValorServico AS ValorServico, os.TotalGeral AS TotalGeral FROM Clientes c 
                            INNER JOIN Veiculos v ON c.IdCliente = v.IdCliente
                            INNER JOIN OrdensServico os ON os.IdVeiculo = v.IdVeiculo
                            INNER JOIN Itens i ON i.IdOrdemServico = os.IdOrdemServico
                            INNER JOIN Produtos p ON p.IdProduto = i.IdProduto
                            INNER JOIN ServicosExecutados se ON os.IdOrdemServico = se.IdOrdemServico
                            INNER JOIN Servicos S ON S.IdServico = SE.IdServico
                            INNER JOIN Execucoes e ON e.IdOrdemServico = os.IdOrdemServico
                            INNER JOIN Profissionais pf ON pf.IdProfissional = e.IdProfissional
                            WHERE v.PlacaVeiculoCliente like CONCAT('%', @placa,'%')";

                   
                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        veiculo
                    };
                    ordemServicoDadosDto = connection.QueryFirst<OrdemServicoDadosDto>(query, parametros);
                }

                if (ordemServicoDadosDto != null)
                {
                    ordemServicoDadosDto.Itens = BuscarProdutosDaOrdem(veiculo);
                }
                if (ordemServicoDadosDto != null)
                {
                    ordemServicoDadosDto.Execucoes = BuscarProfissionaisDaOrdem(veiculo);
                }
                if (ordemServicoDadosDto != null)
                {
                    ordemServicoDadosDto.ServicosExecutados = BuscarServicosDaOrdem(veiculo);
                }

                return ordemServicoDadosDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public List<OrdensServicoDto> BuscarTodos()
        {
            List<OrdensServicoDto> ordensServicoEncontrados;
            try
            {
                var query = @"SELECT IdOrdemServico, IdVeiculo, TotalGeral FROM OrdensServico";

                using (var connection = new SqlConnection(_connection))
                {
                    ordensServicoEncontrados = connection.Query<OrdensServicoDto>(query).ToList();
                }
                return ordensServicoEncontrados;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        public void Atualizar(OrdensServico ordensServico, int id)
        {
            try
            {
                var query = @"UPDATE OrdensServico SET IdCliente = @idCliente, IdProfissional = @idProfissional, IdServico = @idServico, IdProduto = @idProduto, TotalGeral = @totalGeral
                            WHERE IdOrdemServico = @idOrdemServico";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@idCliente", id);
                    command.Parameters.AddWithValue("@idProfissional", ordensServico.IdProfissional);
                    command.Parameters.AddWithValue("@idServico", ordensServico.IdServico);
                    command.Parameters.AddWithValue("@idProduto", ordensServico.IdProduto);
                    command.Parameters.AddWithValue("@totalGeral", ordensServico.TotalGeral);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public OrdensServicoDto Confirmar(int idOrdemServico)
        {
            var ordemServico = new OrdensServicoDto();
            try
            {
                var query = "SELECT * FROM OrdensServico WHERE IdOrdemServico = @idOrdemServico";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idOrdemServico
                    };
                    ordemServico = connection.QueryFirstOrDefault<OrdensServicoDto>(query, parametros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                ordemServico = null;
            }
            return ordemServico;
        }
        public void VincularItensOs(List<int> idItens, int idOrdemServico)  
        {
            foreach (var item in idItens) 
            {
                var sql = @"INSERT INTO Itens (IdProduto, IdOrdemServico) VALUES (@idProduto, @idOrdemServico)";

                var parametros = new 
                {
                    idProduto = item,
                    idOrdemServico
                };
                try 
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex) 
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public void VincularExecucaosOs(List<int> idExecucoes, int idOrdemServico)
        {
            foreach (var item in idExecucoes)
            {
                var sql = @"INSERT INTO Execucoes (IdProfissional, IdOrdemServico) VALUES (@idProfissional, @idOrdemServico)";

                var parametros = new
                {
                    idProfissional = item,
                    idOrdemServico
                };
                try
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public void VincularServicosExecutadosOs(List<int> idServicosExecutados, int idOrdemServico)
        {
            foreach (var item in idServicosExecutados)
            {
                var sql = @"INSERT INTO ServicosExecutados (IdServico, IdOrdemServico) VALUES (@idServico, @idOrdemServico)";

                var parametros = new
                {
                    idServico = item,
                    idOrdemServico
                };
                try
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public float CalcularTotalOrdemServico (int idOrdemServico)
        {
            var query = @"SELECT (SELECT SUM(
	                    CASE 
		                    WHEN p.ValorPeca IS NULL THEN 0
		                    ELSE p.ValorPeca
	                    END) as TotalPecas			
		                    FROM Produtos p 
	                    INNER JOIN Itens i on p.IdProduto = i.IdProduto
	                    INNER JOIN OrdensServico os ON os.IdOrdemServico = i.IdOrdemServico
		                    WHERE os.IdOrdemServico = @idOrdemServico) 
	                    +
	                    (SELECT SUM(
	                    CASE 
		                    WHEN s.ValorServico IS NULL THEN 0
		                    ELSE s.ValorServico
	                    END) as TotalServico							
		                    FROM Servicos s
	                    INNER JOIN ServicosExecutados se on se.IdServico = s.IdServico
	                    INNER JOIN OrdensServico os on os.IdOrdemServico = se.IdOrdemServico
		                    WHERE os.IdOrdemServico = @idOrdemServico) as ValorTotalServico";
            var parametros = new
            {
                idOrdemServico
            };
            using (var connection = new SqlConnection(_connection))
            {
                var res = connection.QuerySingle<float>(query,parametros);
                return res;
            }           
        }
        public void InserirProfissionalOS(List<int> idItens, int idOrdemServico)
        {
            foreach (var item in idItens)
            {
                var sql = @"INSERT INTO Execucoes (IdProfissional, IdOrdemServico) VALUES (@idProfissional, @idOrdemServico)";
                var parametros = new
                {
                    idProfissional = item,
                    idOrdemServico
                };
                try
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public void InserirProdutoOS(List<int> idItens, int idOrdemServico)
        {
            foreach (var item in idItens)
            {
                var sql = @"INSERT INTO Itens (IdProduto, IdOrdemServico) VALUES (@idProduto, @idOrdemServico)";
                var parametros = new
                {
                    idProduto = item,
                    idOrdemServico
                };
                try
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public void InserirServicoOS(List<int> idServicosExecutados, int idOrdemServico)
        {
            foreach (var item in idServicosExecutados)
            {
                var sql = @"INSERT INTO ServicosExecutados (IdServico, IdOrdemServico) VALUES (@idServico, @idOrdemServico)";
                var parametros = new
                {
                    idServico = item,
                    idOrdemServico
                };
                try
                {
                    using (var connection = new SqlConnection(_connection))
                    {
                        connection.Execute(sql, parametros);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro ao salvar Item {item}, para a OS {idOrdemServico}. Erro: {ex.Message}");
                }
            }
        }
        public float FaturamentoBruto()
        {
            var query = "SELECT(SELECT SUM(TotalGeral) FROM OrdensServico)";

            
            using (var connection = new SqlConnection(_connection))
            {
                var res = connection.QuerySingle<float>(query);
                return res;
            }
        }
        private List<ProdutosDto> BuscarProdutosDaOrdem(string nome) 
        {
            try
            {
                var query = @"select i.IdProduto, DescricaoPeca, ValorPeca from Itens i 
                            inner join Produtos p on i.IdProduto = p.IdProduto
                            where i.IdOrdemServico = @idOrdensServico";

                var parametros = new 
                {
                    idOrdensServico = nome
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return  connection.Query<ProdutosDto>(query, parametros).ToList();
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private List<ProfissionaisDto> BuscarProfissionaisDaOrdem(string nome)
        {
            try
            {
                var query = @"SELECT i.IdProfissional, NomeProfissional, CargoProfissional FROM Execucoes i 
                            INNER JOIN Profissionais p on i.IdProfissional = p.IdProfissional
                            WHERE i.IdOrdemServico = @idOrdensServico";

                var parametros = new
                {
                    idOrdensServico = nome
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return connection.Query<ProfissionaisDto>(query, parametros).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private List<ServicosDto> BuscarServicosDaOrdem(string nome)
        {
            try
            {
                var query = @"SELECT i.IdServico, DescricaoServico, ValorServico FROM ServicosExecutados i 
                            INNER JOIN Servicos s on i.IdServico = s.IdServico
                            WHERE i.IdOrdemServico = @idOrdensServico";

                var parametros = new
                {
                    idOrdensServico = nome
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return connection.Query<ServicosDto>(query, parametros).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private List<ProdutosDto> BuscarProdutosDaOrdemId(int id)
        {
            try
            {
                var query = @"select i.IdProduto, DescricaoPeca, ValorPeca from Itens i 
                            inner join Produtos p on i.IdProduto = p.IdProduto
                            where i.IdOrdemServico = @idOrdensServico";

                var parametros = new
                {
                    idOrdensServico = id
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return connection.Query<ProdutosDto>(query, parametros).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private List<ProfissionaisDto> BuscarProfissionaisDaOrdemId(int id)
        {
            try
            {
                var query = @"SELECT i.IdProfissional, NomeProfissional, CargoProfissional FROM Execucoes i 
                            INNER JOIN Profissionais p on i.IdProfissional = p.IdProfissional
                            WHERE i.IdOrdemServico = @idOrdensServico";

                var parametros = new
                {
                    idOrdensServico = id
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return connection.Query<ProfissionaisDto>(query, parametros).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
        private List<ServicosDto> BuscarServicosDaOrdemId(int id)
        {
            try
            {
                var query = @"SELECT i.IdServico, DescricaoServico, ValorServico FROM ServicosExecutados i 
                            INNER JOIN Servicos s on i.IdServico = s.IdServico
                            WHERE i.IdOrdemServico = @idOrdensServico";

                var parametros = new
                {
                    idOrdensServico = id
                };

                using (var connection = new SqlConnection(_connection))
                {
                    return connection.Query<ServicosDto>(query, parametros).ToList();
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
