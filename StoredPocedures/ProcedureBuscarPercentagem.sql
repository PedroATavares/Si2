use TestesSI2

if object_id('BuscarPercentagem') is not null drop proc BuscarPercentagem
go
create procedure BuscarPercentagem
@id1 int,
@id2 int
as select Percentagem from Descontos where Id= @id1 or Id= @id2
return
go