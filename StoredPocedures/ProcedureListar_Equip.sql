if object_id('EquipamentosSemAluguerUltimaSemana') is not null drop proc EquipamentosSemAluguerUltimaSemana
go
create proc EquipamentosSemAluguerUltimaSemana
as
select * from Equipamentos where Codigo not in (
					select AluguerEquipamentos.CodEquip from Aluguer inner join AluguerEquipamentos
						on AluguerEquipamentos.NumAluguer= Aluguer.Num
						where Aluguer.DataFim >= DATEADD(wk, DATEDIFF(wk, 0, CURRENT_TIMESTAMP),-7) 
						and Aluguer.DataInicio <= DATEADD(wk, DATEDIFF(wk, 0, CURRENT_TIMESTAMP),-1) 
					)	
return
go