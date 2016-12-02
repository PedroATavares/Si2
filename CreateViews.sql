go

create view dbo.Equipamentos as
select Codigo,Descricao,Tipo
from fbo.Equipamentos
where Removido=0
go

create view dbo.Promocoes as
select Id,Descricao,DataInicio,DataFim
from fbo.Promocoes
where Removido=0
go

create view dbo.Cliente as
select codigo,nif,nome,morada
from fbo.cliente
where Removido=0
go

create view dbo.Empregado as
select codigo,nome
from fbo.Empregado
where Removido=0
go

create view dbo.Aluguer as
select Num, DataInicio, DataFim, Duracao, NumEmp, CodCli
from fbo.Aluguer
where Removido=0
go
