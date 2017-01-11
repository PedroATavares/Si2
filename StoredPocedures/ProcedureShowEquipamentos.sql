if object_id('ShowEquipamentos') is not null drop proc ShowEquipamentos
go
create procedure ShowEquipamentos
@DataI DateTime,
@DataF DateTime
as
	select * from Equipamentos 
				where Codigo not in(		
					select CodEquip from (
				select Num from Aluguer 
				where DataInicio between @DataI and @DataF and DataFim between @DataI and @DataF
			)as temp1 inner join AluguerEquipamentos on Num=NumAluguer
			)
return
go
