use TestesSI2

if object_id('AluguerPromocao') is not null  DROP TABLE AluguerPromocao
if object_id('AluguerEquipamentos') is not null  DROP TABLE AluguerEquipamentos
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

---------------------------------------Procedures
if object_id('alteracoesPrecario') is not null drop proc alteracoesPrecario
if object_id('dbo.InsertCliente') is not null drop proc dbo.InsertCliente
if object_id('dbo.deleteCliente') is not null drop proc dbo.deleteCliente
if object_id('dbo.updateCliente') is not null drop proc dbo.updateCliente
if object_id('dbo.InsertEquipamentos') is not null drop proc dbo.InsertEquipamentos
if object_id('dbo.UpdateEquipamentos') is not null drop proc dbo.UpdateEquipamentos
if object_id('dbo.DeleteEquipamentos') is not null drop proc dbo.DeleteEquipamentos
if object_id('InserirAluguerComCliente') is not null drop proc InserirAluguerComCliente
if object_id('InserirAluguerSemCliente') is not null  DROP PROCEDURE InserirAluguerSemCliente
if object_id('EquipamentosSemAluguerUltimaSemana') is not null drop proc EquipamentosSemAluguerUltimaSemana
if object_id('listarEquipamentos') is not null drop proc listarEquipamentos
if object_id('InsertPromocaoDesconto') is not null  DROP PROCEDURE InsertPromocaoDesconto
if object_id('InsertPromocaoTempo') is not null  DROP PROCEDURE InsertPromocaoTempo
if object_id('DeletePromocoes') is not null  DROP PROCEDURE DeletePromocoes
if object_id('UpdatePromocoesDescontos') is not null  DROP PROCEDURE UpdatePromocoesDescontos
if object_id('UpdatePromocoesTempo') is not null  DROP PROCEDURE UpdatePromocoesTempo
if object_id('RemoverAluger') is not null drop proc RemoverAluger
