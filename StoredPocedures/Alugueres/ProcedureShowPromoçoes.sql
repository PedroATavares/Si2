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

exec ShowPromocoes '2017-01-12 20:00:00', '2017-01-12 20:30:00'
select * from Promocoes
select * from Descontos
select * from TempoExtra

--Apresenta a Promoçao 5
