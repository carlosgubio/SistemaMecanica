CREATE TABLE Clientes
(
	Id int IDENTITY(1,1) NOT NULL,
	Nome varchar(255) NOT NULL,
	Cpf varchar(11) NOT NULL,
	Telefone varchar (20) NOT NULL,
	Endereco varchar(300) NOT NULL,
	Email varchar(100) NOT NULL,
	Veiculo varchar(50) NOT NULL,
	PlacaVeiculo varchar(10) NOT NULL,
	CorVeiculo varchar(20) NOT NULL,
	CONSTRAINT Pk_Clientes PRIMARY KEY (Id)
)
select * from Clientes

INSERT INTO Clientes (Nome, Cpf, Telefone, Endereco, Email, Veiculo, PlacaVeiculo,  CorVeiculo) VALUES( 'Leila da Silva', '95836521411', '47 999585884', 'Rua Indaial, 254, centro, Itajaí-SC', 'leila@leila.com.br', 'VW Gol', 'GOL 2000', 'Branca')
INSERT INTO Clientes (Nome, Cpf, Telefone, Endereco, Email, Veiculo, PlacaVeiculo,  CorVeiculo) VALUES( 'Marcio Moreira', '97458236921', '47 998745858', 'Rua Brusque, 1000, centro, Itajaí-SC', 'marcio@marcio.com.br', 'GM Monza', 'MON 1990', 'Cinza')
INSERT INTO Clientes (Nome, Cpf, Telefone, Endereco, Email, Veiculo, PlacaVeiculo,  CorVeiculo) VALUES( 'Joaquim Fernandes', '96325874125', '47 999622154', 'Rua São José, 14, São Vicente, Itajaí-SC', 'joaquim@joaquim.com.br', 'Fiat Palio', 'Pal 2010', 'Vermelha')

UPDATE Clientes SET PlacaVeiculo='MON 1990'where Nome='Marcio Moreira';

create table Produtos
(
	IdProduto int IDENTITY(1,1) NOT NULL,
	DescricaoProduto varchar(255) NOT NULL,
	ValorProduto decimal(6,2) NOT NULL,
)
select * from Produtos

insert into Produtos (DescricaoProduto, ValorProduto) values('Amortecedor', 300.00)
insert into Produtos (DescricaoProduto, ValorProduto) values('Carburador', 1800.00)
insert into Produtos (DescricaoProduto, ValorProduto) values('Kit de Velas', 500.00)

create table Profissionais
(
	IdProfissional int IDENTITY(1,1) NOT NULL,
	NomeProfissional varchar(255) NOT NULL,
	CargoProfissional varchar(255) NOT NULL,
)
select * from Profissionais

insert into Profissionais (NomeProfissional, CargoProfissional) values('Marcos', 'Mecânico')
insert into Profissionais (NomeProfissional, CargoProfissional) values('Flávio', 'Mecânico')
insert into Profissionais (NomeProfissional, CargoProfissional) values('Tiago', 'Auxiliar de Mecânico')

create table Servicos
(
	IdServico int IDENTITY(1,1) NOT NULL,
	DescricaoServico varchar(255) NOT NULL,
	ValorServico decimal(6,2) NOT NULL,
)

select * from Servicos

insert into Servicos (DescricaoServico, ValorServico) values('Troca de Amortecedor', 80.00)
insert into Servicos (DescricaoServico, ValorServico) values('Troca de Carburador', 160.00)
insert into Servicos (DescricaoServico, ValorServico) values('Troca de Velas', 40.00)

select * from OrdensServico



insert into OrdensServico (Nome, IdProfissional, DataEntrada, IdServico, IdProduto, TotalGeral) values (1, 1, '07/08/2022', 1, 1, 380.00)
insert into OrdensServico (IdCliente, IdProfissional, DataEntrada, IdServico, IdProduto, TotalGeral) values (2, 2, '07/08/2022', 2, 2, 1960.00)
insert into OrdensServico (IdCliente, IdProfissional, DataEntrada, IdServico, IdProduto, TotalGeral) values (3, 3, '07/08/2022', 3, 3, 540.00)

select * from Clientes
select * from Produtos
select * from Profissionais
select * from Servicos
select * from OrdensServico
