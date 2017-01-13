use TestesSI2

if object_id('BuscarPercentagem') is not null drop proc BuscarPercentagem
go
create procedure BuscarPercentagem
@id1 int,
@id2 int
as select Percentagem from Descontos inner join Promocoes on Descontos.Id = Promocoes.Id
 where Promocoes.Id= @id1 or Promocoes.Id= @id2
return
go

