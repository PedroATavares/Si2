use TestesSI2

if object_id('dbo.InsertCliente') is not null drop proc dbo.InsertCliente

go
create proc dbo.InsertCliente
@NIF int,
@nome varchar(50),
@morada varchar(100),
@id int output
as
insert into cliente (NIF,nome,morada) values (@NIF,@nome,@morada)
set @id = SCOPE_IDENTITY()
return
go

if object_id('dbo.deleteCliente') is not null drop proc dbo.deleteCliente

go
create proc dbo.DeleteCliente
@id int
as
delete from Cliente where Codigo=@id
go

if object_id('dbo.updateCliente') is not null drop proc dbo.updateCliente
go
create proc dbo.UpdateCliente
@id int,
@NIF int = null,
@nome varchar(50) = null,
@morada varchar(100) =null
as

if @Nif is not null
begin
	update Cliente
	set nif=@nif
	where Codigo=@id
end

if @nome is not null
begin
	update Cliente
	set nome=@nome
	where Codigo=@id
end

if @morada is not null
begin
	update Cliente
	set morada=@morada
	where Codigo=@id
end
go