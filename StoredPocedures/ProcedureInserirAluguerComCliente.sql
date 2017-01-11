if object_id('InserirAluguerComCliente') is not null drop proc InserirAluguerComCliente
go
create procedure InserirAluguerComCliente
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


declare @id int
exec  InserirAluguerComCliente '2017-01-01', '2017-01-02', 60, 1, 1, @id out
select @id