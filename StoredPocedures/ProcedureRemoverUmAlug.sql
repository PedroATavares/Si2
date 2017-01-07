if object_id('RemoverAluger') is not null drop proc RemoverAluger
go
create proc RemoverAluger
@id int
as
delete from Aluguer where Num=@id
return
go