use Si2

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
	Codigo int IDENTITY(0,1) primary key,
	Nome varchar(50) NOT NULL,
)

create table Aluguer(
	Num int Identity(1,1) primary key,
	DataInicio Date NOT NULL,
	DataFim Date NOT NULL,
	Duracao int NOT NULL,
	NumEmp int foreign key references Empregado,
	CodCli int foreign key references Cliente,
)

create table AluguerEquipamentos(
	preco smallmoney, 
	NumAluguer int foreign key references Aluguer,
	CodEquip int foreign key references Equipamentos
	primary key (NumAluguer, CodEquip)
)

create table AluguerPromocao(
	NumAluguer int foreign key references Aluguer,
	IdProm int foreign key references Promocoes
	primary key (NumAluguer,IdProm)
)