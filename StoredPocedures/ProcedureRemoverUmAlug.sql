use TestesSI2

if object_id('RemoverAluger') is not null drop proc RemoverAluger
go
create proc RemoverAluger
@id int
as
delete from AluguerEquipamentos where NumAluguer=@id
delete from AluguerPromocao where NumAluguer=@id
delete from Aluguer where Num=@id
return
go