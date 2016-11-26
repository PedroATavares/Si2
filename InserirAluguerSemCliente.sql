if object_id('InserirAluguerSemCliente') is not null  DROP PROCEDURE InserirAluguerSemCliente
go
create procedure InserirAluguerSemCliente
@Nif int,
@Nome varchar(50),
@Morada varchar(100),
@idCliente int output,
@Duracao int,
@NumEmp int,
@DataI DateTime = GETDATE,
@DataF DateTime =getDate, 
@idAluguer int output

as
	INSERT INTO Cliente values( @Nif, @Nome, @Morada)
	select @idCliente= SCOPE_IDENTITY()

	exec InserirAluguerComCliente @idCliente,@Duracao,@DataI,@DataF
	select @idAluguer= SCOPE_IDENTITY()
	
	return
go
