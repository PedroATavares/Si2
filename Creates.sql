go
create schema fbo
go
create table Tipo(
	Nome varchar(50) primary key,
	Descricao varchar (100) NOT NULL
)

Create table fbo.Equipamentos(
	Codigo int IDENTITY(1,1) primary key,
	Descrip varchar(200) NOT NULL,
	Tipo varchar(50) foreign key references Tipo,
	Removed int default 0, 
	constraint Equipamento_Removed_Constraint check (Removed in (1,0))
)
go
create view dbo.Equipamentos as
select Codigo,Descrip,Tipo
from fbo.Equipamentos
where Removed=0
go

create table PrecoAluguer(
	ValidadeI Date,
	ValidadeF Date NOT NULL,
	Duracao int, --Em minutos
	Valor SmallMoney,--Em euros 
	EquipId int foreign key references fbo.Equipamentos,
	primary key (ValidadeI, Duracao,EquipId)
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

create table fbo.Cliente(
	Codigo int IDENTITY(0,1) primary key,
	NIF int NULL,
	nome varchar(50) NULL,
	Morada varchar(100) NULL,
	Removed int default 0, 
	constraint Cliente_Removed_Constraint check (Removed in (1,0))
)
go
create view dbo.Cliente as
select codigo,nif,nome,morada
from fbo.cliente
where Removed=0
go

create table fbo.Empregado(
	Codigo int IDENTITY(1,1) primary key,
	Nome varchar(50) NOT NULL,
	Removed int default 0, 
	constraint Empregado_Removed_Constraint check (Removed in (1,0))
)
go
create view dbo.Empregado as
select codigo,nome
from fbo.Empregado
where Removed=0
go


create table fbo.Aluguer(
	Num int Identity(1,1) primary key,
	DataInicio DateTime NOT NULL,
	DataFim DateTime NOT NULL,
	Duracao int NOT NULL,
	NumEmp int foreign key references fbo.Empregado,
	CodCli int foreign key references fbo.Cliente,
	Removed int default 0, 
	constraint Auluguer_Removed_Constraint check (Removed in (1,0)),
	constraint Auluguer_Data_Constraint check (dataInicio<dataFim)

)
go
create table AluguerEquipamentos(
	preco smallmoney, 
	NumAluguer int foreign key references fbo.Aluguer,
	CodEquip int foreign key references fbo.Equipamentos
	primary key (NumAluguer, CodEquip)
)

create table AluguerPromocao(
	NumAluguer int foreign key references fbo.Aluguer,
	IdProm int foreign key references Promocoes
	primary key (NumAluguer,IdProm)
)

go
create trigger fbo.DeleteCliente
on fbo.cliente
Instead of delete
as

	select * from fbo.Aluguer inner join deleted
	on fbo.Aluguer.CodCli=deleted.Codigo
	where DataInicio<getDate()

if @@ROWCOUNT = 0
begin
	DELETE a
FROM 
    fbo.Cliente as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo
end

else
begin
	UPDATE fbo.Cliente 
    SET Removed = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)
end
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

create trigger fbo.DeleteEmpregado
on fbo.Empregado
Instead of delete
as

	select * from fbo.Aluguer inner join deleted
	on fbo.Aluguer.NumEmp=deleted.Codigo
	where DataInicio<getDate()

if @@ROWCOUNT = 0
begin
	DELETE a
FROM 
    fbo.Empregado as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo
end

else
begin
	UPDATE fbo.Empregado 
    SET Removed = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)
end
go
create trigger fbo.DeleteEquipamento
on fbo.Equipamentos
Instead of delete
as
select * from deleted inner join (
select * from AluguerEquipamentos inner join Aluguer
	on AluguerEquipamentos.NumAluguer=Aluguer.Num) as R1
	on deleted.Codigo= r1.CodEquip
	where DataInicio<getDate()

if @@ROWCOUNT = 0
begin
	DELETE a
FROM 
    fbo.Equipamentos as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo

DELETE a
FROM 
    AluguerEquipamentos as a
    JOIN deleted as b
        ON a.CodEquip = b.Codigo

end

else
begin
	UPDATE fbo.Equipamentos 
    SET Removed = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)
end
go

/*
insert into Cliente (nif,nome,Morada) values (1,'1','1')
insert into Cliente (nif,nome,Morada) values (2,'2','2')
insert into Cliente (nif,nome,Morada) values (3,'3','3')

insert into empregado (nome) values ('1')

insert into aluguer (CodCli,DataFim,DataInicio,Duracao,NumEmp) values (0,'2010-10-12','2010-10-11',10,1)

delete from Cliente where Codigo=0

select * from fbo.empregado
*/