use Si2

go
create proc dbo.InsertEquipamentos
@descr varchar(200),
@tipo varchar(50),
@toRet int output
as
	insert into dbo.Equipamentos values (@descr,@tipo)
	set @toRet = SCOPE_IDENTITY()
	return

go
create proc dbo.UpdateEquipamentos
@id int = null,
@descr varchar(200) = null,
@tipo varchar(50) = null
as
	if(@descr is not null)
	update Equipamentos
	set Descrip=@descr
	where Codigo=@id

	if(@tipo is not null)
	update Equipamentos
	set Tipo=@tipo
	where Codigo=@id

go
create proc dbo.DeleteEquipamentos
@id int
as
	delete from Equipamentos
	where Codigo = @id
