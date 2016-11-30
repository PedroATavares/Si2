go
create schema fbo
go
create table Tipo(
	Nome varchar(50) primary key,
	Descricao varchar (100) NOT NULL
)

Create table fbo.Equipamentos(
	Codigo int IDENTITY(1,1) primary key,
	Descricao varchar(200) NOT NULL,
	Tipo varchar(50) foreign key references Tipo,
	Removido int default 0, 
	constraint Equipamento_Removed_Constraint check (Removido in (1,0))
)
go

create table PrecoAluguer(
	ValidadeI Date,
	ValidadeF Date NOT NULL,
	Duracao int, --Em minutos
	Valor SmallMoney,--Em euros 
	EquipId int foreign key references fbo.Equipamentos,
	primary key (ValidadeI, Duracao,EquipId)
)

create table fbo.Promocoes(
	Id int IDENTITY(1,1) primary key,
	DataInicio Date NOT NULL,
	DataFim Date NOT NULL,
	Descricao varchar(200) NOT NULL,
	Removido int default 0, 
	constraint Promocoes_Removed_Constraint check (Removido in (1,0))
)
go

create table Descontos(
	Preco smallmoney, -- Em euros
	Id int foreign key references fbo.Promocoes 
)


create table TempoExtra(
	TempoExtra int, --Em minutos
	Id int foreign key references fbo.Promocoes
)

create table fbo.Cliente(
	Codigo int IDENTITY(0,1) primary key,
	NIF int NULL unique,
	Nome varchar(50) NULL,
	Morada varchar(100) NULL,
	Removido int default 0, 
	constraint Cliente_Removed_Constraint check (Removido in (1,0))
)
go

create table fbo.Empregado(
	Codigo int IDENTITY(1,1) primary key,
	Nome varchar(50) NOT NULL,
	Removido int default 0, 
	constraint Empregado_Removed_Constraint check (Removido in (1,0))
)
go

create table fbo.Aluguer(
	Num int Identity(1,1) primary key,
	DataInicio DateTime NOT NULL,
	DataFim DateTime NOT NULL,
	Duracao int NOT NULL,
	NumEmp int foreign key references fbo.Empregado,
	CodCli int foreign key references fbo.Cliente,
	Removido int default 0, 
	constraint Auluguer_Removed_Constraint check (Removido in (1,0)),
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
	IdProm int foreign key references fbo.Promocoes
	primary key (NumAluguer,IdProm)
)

/*
insert into Cliente (nif,nome,Morada) values (1,'1','1')
insert into Cliente (nif,nome,Morada) values (2,'2','2')
insert into Cliente (nif,nome,Morada) values (3,'3','3')

insert into empregado (nome) values ('1')
insert into empregado (nome) values ('2')

insert into aluguer (CodCli,DataFim,DataInicio,Duracao,NumEmp) values (0,'2010-10-12','2010-10-11',10,1)

insert into tipo values ('canoa', 'canoas bonitas')

insert into equipamentos (descrip,tipo) values ('canoa amarela','canoa')
insert into equipamentos (descrip,tipo) values ('canoa vermelha','canoa')
insert into equipamentos (descrip,tipo) values ('canoa azul','canoa')

insert into AluguerEquipamentos values (10,1,1)
insert into AluguerEquipamentos values (10,1,2)

insert into promocoes (descrip,datainicio,datafim) values ('1','2010-10-10','2012-10-10')
insert into TempoExtra values (10,1)

insert into promocoes (descrip,datainicio,datafim) values ('2','2010-10-10','2012-10-10')
insert into Descontos values (10,2)

insert into promocoes (descrip,datainicio,datafim) values ('3','2010-10-10','2012-10-10')


delete from Cliente where Codigo=1
delete from empregado where Codigo=2

delete from equipamentos where tipo = 'canoa'


select * from fbo.empregado
select * from fbo.cliente
select * from fbo.equipamentos
select * from fbo.promocoes


select Codigo from deleted inner join (
select * from fbo.AluguerEquipamentos inner join Aluguer
	on AluguerEquipamentos.NumAluguer=Aluguer.Num) as R1
	on deleted.Codigo= r1.CodEquip
	where DataInicio<getDate()
	group by Codigo
	
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
*/