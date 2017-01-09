
if object_id('InsertPromocaoDesconto') is not null  DROP PROCEDURE InsertPromocaoDesconto
go
create procedure InsertPromocaoDesconto
@DataInicio Date,
@DataFim Date,
@Descricao varchar(200),
@Preco smallmoney, 
@id int output
as
	begin tran
	begin try 
		INSERT INTO Promocoes(DataInicio,DataFim,Descricao) values( @DataInicio, @DataFim, @Descricao)
		select @id= SCOPE_IDENTITY()
		Insert into Descontos values(@Preco,@id)
		commit
	end try
	begin catch
		select @id=0
		rollback
	end catch

	return

go

if object_id('InsertPromocaoTempo') is not null  DROP PROCEDURE InsertPromocaoTempo
go
create procedure InsertPromocaoTempo
@DataInicio Date,
@DataFim Date,
@Descricao varchar(200),
@Tempo int, 
@id int output
as
	begin tran
	begin try 
		INSERT INTO Promocoes(DataInicio,DataFim,Descricao) values( @DataInicio, @DataFim, @Descricao)
		select @id= SCOPE_IDENTITY()
		Insert into TempoExtra values(@Tempo,@id)
		commit
	end try
	begin catch
		select @id=0
		rollback
	end catch

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