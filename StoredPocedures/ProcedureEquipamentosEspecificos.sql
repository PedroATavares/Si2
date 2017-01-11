use TestesSI2

if object_id('EquipamentosEspecificos') is not null drop proc EquipamentosEspecificos
go
create procedure EquipamentosEspecificos
@DataI DateTime,
@DataF DateTime,
@CodEquip int
as
	select * from Equipamentos inner join PrecoAluguer on 
PrecoAluguer.EquipId = Equipamentos.Codigo
				where Equipamentos.Codigo not in(		
					select CodEquip from (
				select Num from Aluguer 
				where DataInicio between @DataI and @DataF and DataFim between @DataI and @DataF
			)as temp1 inner join AluguerEquipamentos on Num=NumAluguer
			) and Equipamentos.Codigo = @CodEquip 
return
go
