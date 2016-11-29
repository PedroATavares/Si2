--Listar todos os equipamentos livres para um determinado Tempo e Tipo

create proc listarEquipamentos
@ValidadeI date,
@ValidadeF date = null,
@tipo varchar(50)
as
if @ValidadeF is not null and @ValidadeI is not null and @tipo is not null
begin
	select * from(
		select CodEquip from (
			select Num from Aluguer 
			where DataInicio>@ValidadeI and DataFim<@ValidadeF
		)as temp1 cross join AluguerEquipamentos 
		where Num=NumAluguer
	)as temp2 cross join Equipamentos
	where CodEquip = Codigo and Tipo=@tipo
end

