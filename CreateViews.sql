go

create view dbo.Equipamentos as
select Codigo,Descrip,Tipo
from fbo.Equipamentos
where Removed=0
go

create view dbo.Promocoes as
select Id,Descrip,DataInicio,DataFim
from fbo.Promocoes
where Removed=0
go

create view dbo.Cliente as
select codigo,nif,nome,morada
from fbo.cliente
where Removed=0
go

create view dbo.Empregado as
select codigo,nome
from fbo.Empregado
where Removed=0
go

create view dbo.Aluguer as
select Num, DataInicio, DataFim, Duracao, NumEmp, CodCli
from fbo.Aluguer
where Removed=0
go
