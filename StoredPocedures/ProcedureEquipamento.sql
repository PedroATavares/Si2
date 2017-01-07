--Falta verificar se o objeto é not null

if object_id('dbo.InsertEquipamentos') is not null drop proc dbo.InsertEquipamentos

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

if object_id('dbo.UpdateEquipamentos') is not null drop proc dbo.UpdateEquipamentos

go
create proc dbo.UpdateEquipamentos
@id int = null,
@descr varchar(200) = null,
@tipo varchar(50) = null
as
	if(@descr is not null)
	update Equipamentos
	set Descricao=@descr
	where Codigo=@id

	if(@tipo is not null)
	update Equipamentos
	set Tipo=@tipo
	where Codigo=@id
go

if object_id('dbo.DeleteEquipamentos') is not null drop proc dbo.DeleteEquipamentos

go
create proc dbo.DeleteEquipamentos
@id int
as
	delete from Equipamentos
	where Codigo = @id
