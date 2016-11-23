create table Tipo(
	Nome varchar(50) primary key,
	Descricao varchar (100) NOT NULL
)

Create table Equipamentos(
	Codigo int IDENTITY(1,1) primary key,
	Descrip varchar(200) NOT NULL,
	Tipo varchar(50) foreign key references Tipo
)

create table PrecoAluguer(
	DataI Date NOT NULL,
	DataF Date NOT NULL,
	Duration int, --Em minutos
	Valor SmallMoney,--Em euros 
	EquipId int foreign key references Equipamentos,
	primary key (DataI,EquipId)
)

create table Promocoes(
	Id int IDENTITY(1,1) primary key,
	DataInicio Date NOT NULL,
	DataFim Date NOT NULL,
	Descrip varchar(200) NOT NULL
)

create table Descontos(
	Preco smallmoney, -- Em euros
	Id int foreign key references Promocoes 
)


create table TempoExtra(
	TempoExtra int, --Em minutos
	Id int foreign key references Promocoes
)

create table Cliente(
	Codigo int IDENTITY(0,1) primary key,
	NIF int NULL,
	nome varchar(50) NULL,
	Morada varchar(100) NULL
)

create table Empregado(
	Codigo int IDENTITY(1,1) primary key,
	Nome varchar(50) NOT NULL,
)

go
create schema fbo
go
create table fbo.Aluguer(
	Num int Identity(1,1) primary key,
	DataInicio DateTime NOT NULL,
	DataFim DateTime NOT NULL,
	Duracao int NOT NULL,
	NumEmp int foreign key references Empregado,
	CodCli int foreign key references Cliente,
	Removed int default 0, 
	constraint Auluguer_Removed_Constraint check (Removed in (1,0)),
	constraint Auluguer_Data_Constraint check (dataInicio<dataFim)

)
go
create trigger fbo.DeleteAluguer
on fbo.Aluguer
Instead of delete
as
DELETE a
FROM 
    fbo.Aluguer as a
    JOIN deleted as b
        ON a.Num = b.Num
        AND b.DataInicio > getdate()

UPDATE fbo.Aluguer 
    SET Removed = 1
	WHERE Num IN (SELECT t.Num 
                  FROM deleted t)
go
create view dbo.Aluguer as
select Num, DataInicio, DataFim, Duracao, NumEmp, CodCli
from fbo.Aluguer
where Removed=0

go
create trigger fbo.UpdateAluguer
on fbo.Aluguer
Instead of update
as
UPDATE fbo.Aluguer 
   SET fbo.Aluguer.CodCli = inserted.CodCli,
       fbo.Aluguer.DataFim=inserted.DataFim,
	   fbo.Aluguer.DataInicio=inserted.DataInicio,
	   fbo.Aluguer.Duracao=inserted.Duracao,
	   fbo.Aluguer.NumEmp=inserted.NumEmp 
   FROM fbo.Aluguer  INNER JOIN  inserted
   ON fbo.Aluguer.Num = inserted.Num
   inner join
   deleted
   on fbo.Aluguer.DataInicio> GETDATE()
go

create table AluguerEquipamentos(
	preco smallmoney, 
	NumAluguer int foreign key references fbo.Aluguer,
	CodEquip int foreign key references Equipamentos
	primary key (NumAluguer, CodEquip)
)

create table AluguerPromocao(
	NumAluguer int foreign key references fbo.Aluguer,
	IdProm int foreign key references Promocoes
	primary key (NumAluguer,IdProm)
)
