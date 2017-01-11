use TestesSI2

--Listar todos os equipamentos livres para um determinado Tempo e Tipo
if object_id('listarEquipamentos') is not null drop proc listarEquipamentos
go
create proc listarEquipamentos
@ValidadeI datetime,
@ValidadeF datetime,
@tipo varchar(50)
as
if @ValidadeF is not null and @ValidadeI is not null and @tipo is not null
begin
			select * from Equipamentos 
				where Tipo=@tipo and Codigo not in(		
					select CodEquip from (
				select Num from Aluguer 
				where DataInicio between @ValidadeI and @ValidadeF and DataFim between @ValidadeI and @ValidadeF
			)as temp1 inner join AluguerEquipamentos on Num=NumAluguer
			)	
end

