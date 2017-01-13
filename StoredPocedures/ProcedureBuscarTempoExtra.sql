use TestesSI2

if object_id('BuscarTempoExtra') is not null drop proc BuscarTempoExtra
go
create procedure BuscarTempoExtra
@id1 int,
@id2 int
as select TempoExtra from TempoExtra inner join Promocoes on TempoExtra.Id = Promocoes.Id
 where Promocoes.Id= @id1 or Promocoes.Id= @id2
return
go
