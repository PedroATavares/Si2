if object_id('alteracoesPrecario') is not null drop proc alteracoesPrecario
go
create proc alteracoesPrecario
@ValidadeI date,
@ValidadeF date = null,
@duracao int,
@valor SmallMoney = null,
@EquipId int
as

if @duracao is not null
begin
	update PrecoAluguer
	set Duracao=@duracao
	where ValidadeI=@ValidadeI and Duracao=@duracao and EquipId=@EquipId
end

if @valor is not null
begin
	update PrecoAluguer
	set Valor=@valor
	where ValidadeI=@ValidadeI and Duracao=@duracao and EquipId=@EquipId
end

if @EquipId is not null
begin
	update PrecoAluguer
	set EquipId=@EquipId
	where ValidadeI=@ValidadeI and Duracao=@duracao and EquipId=@EquipId
end
go