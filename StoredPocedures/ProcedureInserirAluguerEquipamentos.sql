use TestesSI2

if object_id('InserirAluguerEquipamentos') is not null drop proc InserirAluguerEquipamentos
go
create procedure InserirAluguerEquipamentos
@Preco int,
@NumAluguer int,
@CodEquip int
as
insert into AluguerEquipamentos values(@Preco, @NumAluguer, @CodEquip)
go

