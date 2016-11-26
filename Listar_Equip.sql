

if object_id('EquipamentosSemAluguerUltimaSemana') is not null drop proc EquipamentosSemAluguerUltimaSemana
go
create proc EquipamentosSemAluguerUltimaSemana
as
select * from Equipamentos where Codigo not in (
					select AluguerEquipamentos.CodEquip from Aluguer inner join AluguerEquipamentos
						--on Equipamentos.Codigo = AluguerEquipamentos.CodEquip
						--inner join Aluguer 
						on AluguerEquipamentos.NumAluguer= Aluguer.Num
						where Aluguer.DataFim >= DATEADD(wk, DATEDIFF(wk, 0, CURRENT_TIMESTAMP),-7) 
						and Aluguer.DataInicio <= DATEADD(wk, DATEDIFF(wk, 0, CURRENT_TIMESTAMP),-1) 
					)	
return
go


exec EquipamentosSemAluguerUltimaSemana

insert into AluguerEquipamentos values (10,3,2)
insert into Aluguer values ('2016-11-1','2016-11-14',5,1,1)-- 5 duracao,1 empreg,1 cliente,0 remove
insert into Cliente values (12,'cliente1','morada1')
insert into Empregado values ('empregado1')
insert into Equipamentos values ('descricao Equ1', 'tipoDoGajo')

insert into AluguerEquipamentos values (10,4,3)
insert into Aluguer values ('2016-11-1','2016-11-13',5,2,2)-- 5 duracao,1 empreg,1 cliente,0 remove
insert into Cliente values (12,'cliente2','morada2')
insert into Empregado values ('empregado2')
insert into Equipamentos values ('descricao Equ2', 'tipoDoGajo')

insert into Tipo values ('tipoDoGajo','descricao do tipo1')


select * from Equipamentos
select * from Empregado
select * from Cliente
select * from Aluguer
select * from AluguerEquipamentos