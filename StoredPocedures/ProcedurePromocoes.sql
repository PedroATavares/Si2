use TestesSI2

if object_id('InsertPromocaoDesconto') is not null  DROP PROCEDURE InsertPromocaoDesconto
go
create procedure InsertPromocaoDesconto
@DataInicio Date,
@DataFim Date,
@Descricao varchar(200),
@Percentagem int, 
@id int output
as
	begin tran
	begin try 
		INSERT INTO Promocoes(DataInicio,DataFim,Descricao) values( @DataInicio, @DataFim, @Descricao)
		select @id= SCOPE_IDENTITY()
		Insert into Descontos values(@Percentagem,@id)
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
	begin tran
	begin try 
		
		DELETE FROM  Descontos
		where id = @id
		
		DELETE FROM  TempoExtra
		where id = @id
		
		DELETE FROM Promocoes 
		where id = @id

		commit
	end try
	begin catch
		rollback
	end catch

go

if object_id('UpdatePromocoesDescontos') is not null  DROP PROCEDURE UpdatePromocoesDescontos
go
create procedure UpdatePromocoesDescontos
@id int,
@DataInicio Date = null,
@DataFim Date = null,
@Descricao varchar(200) = null,
@Percentagem smallmoney =null
as

begin tran
begin try

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

IF @Percentagem is not null
begin
	update Descontos
	set Percentagem = @Percentagem
	where id = @id
end

commit

end try
begin catch
rollback
end catch
go

if object_id('UpdatePromocoesTempo') is not null  DROP PROCEDURE UpdatePromocoesTempo
go
create procedure UpdatePromocoesTempo
@id int,
@DataInicio Date = null,
@DataFim Date = null,
@Descricao varchar(200) = null,
@Tempo int =null
as

begin tran
begin try

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

IF @Tempo is not null
begin
	update TempoExtra
	set TempoExtra = @Tempo
	where id = @id
end

commit

end try
begin catch
rollback
end catch
go