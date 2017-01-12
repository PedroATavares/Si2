use TestesSI2

if object_id('ShowPromocoes') is not null drop proc ShowPromocoes
go
create procedure ShowPromocoes
@DataI DateTime,
@DataF DateTime
as
	select * from Promocoes where DataInicio<=@DataI and DataFim>=@DataF
return
go

