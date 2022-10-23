select * from Pessoa
CREATE TABLE Telefone 
(
	Id int IDENTITY(1,1) NOT NULL,
	DDD varchar(5) NOT NULL,
	Numero varchar(20) NOT NULL,
	IdPessoa int NOT NULL,
	CONSTRAINT Pk_Telefone PRIMARY KEY (Id),
	CONSTRAINT FK_Id_Pessoa FOREIGN KEY (IdPessoa)
	REFERENCES Pessoa(Id)
);
select * from Pessoa
select * from Telefone
Drop table Telefone
CREATE TABLE Telefone 
(
	Id int IDENTITY(1,1) NOT NULL,
	Ddd varchar(5) NOT NULL,
	NumTelefone varchar(20) NOT NULL,
	IdPessoa int NOT NULL,
	CONSTRAINT Pk_Telefone PRIMARY KEY (Id),
	CONSTRAINT FK_Id_Pessoa FOREIGN KEY (IdPessoa)
	REFERENCES Pessoa(Id)
);
