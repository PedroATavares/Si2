
if object_id('InsertPromocoes') is not null  DROP PROCEDURE InsertPromocoes
go
create procedure InsertPromocoes
@DataInicio Date,
@DataFim Date,
@Descricao varchar(200), 
@id int output
as
	INSERT INTO Promocoes(DataInicio,DataFim,Descricao) values( @DataInicio, @DataFim, @Descricao)
	select @id= SCOPE_IDENTITY()
	return
go

if object_id('DeletePromocoes') is not null  DROP PROCEDURE DeletePromocoes
go
create procedure DeletePromocoes
@id int
as
	DELETE FROM Promocoes 
	where id = @id
go

if object_id('UpdatePromocoes') is not null  DROP PROCEDURE UpdatePromocoes
go
create procedure UpdatePromocoes
@id int,
@DataInicio Date = null,
@DataFim Date = null,
@Descricao varchar(200) = null 
as

IF @dataInicio is not null
begin
	update Promocoes
	set DataInicio = @DataInicio
	where id = @id
end
IF @DataFim is not null
begin
	update Promocoes
	set DataFim = @DataFim
	where id = @id
end
IF @Descricao is not null
begin
	update Promocoes
	set Descricao = @Descricao
	where id = @id
end
go