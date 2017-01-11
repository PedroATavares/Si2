use TestesSI2

if object_id('BuscarPrecoEspecifico') is not null drop proc BuscarPrecoEspecifico
go
create procedure BuscarPrecoEspecifico
@DataI DateTime,
@DataF DateTime,
@CodEquip int,
@Duracao int
as
	select Valor from Equipamentos inner join PrecoAluguer on 
PrecoAluguer.EquipId = Equipamentos.Codigo
				where Equipamentos.Codigo not in(		
					select CodEquip from (
				select Num from Aluguer 
				where DataInicio between @DataI and @DataF and DataFim between @DataI and @DataF
			)as temp1 inner join AluguerEquipamentos on Num=NumAluguer
			) and Equipamentos.Codigo = @CodEquip and PrecoAluguer.Duracao = @Duracao 
return
go