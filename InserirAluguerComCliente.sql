if object_id('InserirAluguerComCliente') is not null drop proc InserirAluguerComCliente
go
create proc InserirAluguerComCliente
@CodCli int,
@Duracao int,
@NumEmp int,
@DataI DateTime = GETDATE,
@DataF DateTime =getDate, 
@id int output
as
insert into Aluguer values (@DataI,@DataF,@Duracao,@NumEmp,@CodCli)
set @id = SCOPE_IDENTITY()
return
go
