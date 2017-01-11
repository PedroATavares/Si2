if object_id('ShowPromocoes') is not null drop proc ShowPromocoes
go
create procedure ShowPromocoes
@DataI DateTime,
@DataF DateTime
as
	select * from Promocoes where DataInicio<=@DataI and DataFim>=@DataF
return
go

--exec ShowPromocoesAluguer '2017-01-01 00:00:00', '2017-01-01 01:00:00'
--Apresenta a Promoçao 5
