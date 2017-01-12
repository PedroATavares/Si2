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

exec ShowPromocoes '2016-12-12 12:00:00', '2016-12-12 13:00:00'
--Apresenta a Promoçao 5
