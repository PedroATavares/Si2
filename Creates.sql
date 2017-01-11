use TestesSI2

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
	constraint PrecoAluguer_Data_Constraint check (ValidadeI<ValidadeF),
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
	Percentagem int,
	Id int foreign key references fbo.Promocoes primary key
)


create table TempoExtra(
	TempoExtra int, --Em minutos
	Id int foreign key references fbo.Promocoes primary key
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
insert into fbo.Cliente(NIF) values (0)


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
	constraint Aluguer_Removed_Constraint check (Removido in (1,0)),
	constraint Aluguer_Data_Constraint check (dataInicio<dataFim)

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




