if object_id('AluguerPromocao') is not null  DROP TABLE AluguerPromocao
if object_id('AluguerEquipamentos') is not null  DROP TABLE AluguerEquipamentos
--if object_id('fbo.DeleteAluguer') is not null drop trigger fbo.DeleteAluguer
--if object_id('fbo.UpdateAluguer') is not null drop trigger fbo.UpdateAluguer
if object_id('fbo.Aluguer') is not null  DROP TABLE fbo.Aluguer
if object_id('dbo.Aluguer') is not null  DROP View dbo.Aluguer
if object_id('fbo.Empregado') is not null  DROP TABLE fbo.Empregado
if object_id('dbo.Empregado') is not null  DROP view dbo.Empregado
if object_id('dbo.Cliente') is not null  DROP View dbo.cliente
if object_id('fbo.Cliente') is not null  DROP TABLE fbo.Cliente
if object_id('TempoExtra') is not null  DROP TABLE TempoExtra
if object_id('Descontos') is not null  DROP TABLE Descontos
if object_id('fbo.Promocoes') is not null  DROP TABLE fbo.Promocoes
if object_id('dbo.Promocoes') is not null  DROP View dbo.Promocoes
if object_id('PrecoAluguer') is not null  DROP TABLE PrecoAluguer
if object_id('fbo.Equipamentos') is not null  DROP TABLE fbo.Equipamentos
if object_id('dbo.Equipamentos') is not null  DROP view dbo.Equipamentos
if object_id('Tipo') is not null  DROP TABLE Tipo
if schema_id('fbo') is not null  drop schema fbo
