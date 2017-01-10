use TestesSI2

if object_id('InserirAluguerComCliente') is not null drop proc InserirAluguerComCliente
go
create proc InserirAluguerComCliente
@DataI DateTime,
@DataF DateTime, 
@Duracao int,
@NumEmp int,
@CodCli int,
@id int output
as
insert into Aluguer values (@DataI,@DataF,@Duracao,@NumEmp,@CodCli)
set @id = SCOPE_IDENTITY()
return
go
