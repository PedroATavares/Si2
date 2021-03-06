use TestesSI2

if object_id('InserirAluguerSemCliente') is not null  DROP PROCEDURE InserirAluguerSemCliente
go
create procedure InserirAluguerSemCliente
@Nif int,
@Nome varchar(50),
@Morada varchar(100),
@CodCli int output,
@Duracao int,
@NumEmp int,
@DataI DateTime,
@DataF DateTime, 
@idAluguer int output

as
begin tran
set transaction isolation level REPEATABLE READ
begin try
	INSERT INTO Cliente values( @Nif, @Nome, @Morada)
	select @CodCli = IDENT_CURRENT('Cliente')

	exec InserirAluguerComCliente @DataI,@DataF,@Duracao,@NumEmp,@CodCli,@idAluguer output

	commit
end try

begin catch
	rollback
end catch
	return
go
