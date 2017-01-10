use TestesSI2

go
create trigger fbo.DeletePromocoes
on fbo.Promocoes
Instead of delete
as
DELETE a
FROM 
    fbo.Promocoes as a
    JOIN deleted as b
        ON a.Id = b.Id
        AND a.Id not in (select id from deleted inner join  AluguerPromocao 
							on deleted.id= AluguerPromocao.IdProm)

UPDATE fbo.Promocoes
    SET Removido = 1
	WHERE id IN (SELECT t.id 
                  FROM deleted t)

go

create trigger fbo.DeleteCliente
on fbo.cliente
Instead of delete
as
DELETE a
FROM 
    fbo.Cliente as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo
        AND a.Codigo not in (select Codigo from deleted inner join  Aluguer 
							on deleted.Codigo= Aluguer.CodCli)

UPDATE fbo.Cliente 
    SET Removido = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)
go

create trigger fbo.DeleteAluguer
on fbo.Aluguer
Instead of delete
as
DELETE a
FROM 
    fbo.Aluguer as a
    JOIN deleted as b
        ON a.Num = b.Num
        AND b.DataInicio > getdate()

UPDATE fbo.Aluguer 
    SET Removido = 1
	WHERE Num IN (SELECT t.Num 
                  FROM deleted t)
go

create trigger fbo.UpdateAluguer
on fbo.Aluguer
Instead of update
as
UPDATE fbo.Aluguer 
   SET fbo.Aluguer.CodCli = inserted.CodCli,
       fbo.Aluguer.DataFim=inserted.DataFim,
	   fbo.Aluguer.DataInicio=inserted.DataInicio,
	   fbo.Aluguer.Duracao=inserted.Duracao,
	   fbo.Aluguer.NumEmp=inserted.NumEmp 
   FROM fbo.Aluguer  INNER JOIN  inserted
   ON fbo.Aluguer.Num = inserted.Num
   inner join
   deleted
   on fbo.Aluguer.DataInicio> GETDATE() and inserted.DataInicio>GETDATE()
go

create trigger fbo.DeleteEmpregado
on fbo.Empregado
Instead of delete
as
DELETE a
FROM 
    fbo.Empregado as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo
        AND a.Codigo not in (select Codigo from deleted inner join  Aluguer 
							on deleted.Codigo= Aluguer.NumEmp)

UPDATE fbo.Empregado 
    SET Removido = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)

go

create trigger fbo.DeleteEquipamento
on fbo.Equipamentos
Instead of delete
as
DELETE a
FROM 
    fbo.Equipamentos as a
    JOIN deleted as b
        ON a.Codigo = b.Codigo
        AND a.Codigo not in (select Codigo from deleted inner join  AluguerEquipamentos 
							on deleted.Codigo= AluguerEquipamentos.CodEquip)

UPDATE fbo.Equipamentos
    SET Removido = 1
	WHERE Codigo IN (SELECT t.Codigo 
                  FROM deleted t)

go
