use TestesSI2

if object_id('AplicarPromo') is not null drop proc AplicarPromo
go
create procedure AplicarPromo
@NumAluguer int,
@CodPromo int
as
insert into AluguerPromocao values(@NumAluguer, @CodPromo)
go
