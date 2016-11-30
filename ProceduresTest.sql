--------------------------------------------------- TESTES DOS PROC
begin tran 

-- Teste Alteracao do Precario
-- select * from PrecoAluguer
exec alteracoesPrecario '2016-01-01','2017-01-01',30,15,1
-- é espectavel que na tabela PrecoAluguer com a data inicio-> 2016-01-01, duracao-> 30 e EquipId-> 1
-- o valor seja mudificado para 15 balas

---------------------------------------------------------------//---------------------------------------------

-- Teste Inserir Aluguer com Cliente
--select * from Aluguer
declare @id int
exec InserirAluguerComCliente '2016-11-20','2017-11-21',40,1,1,@id output
select @id
-- é espectavel inserir na tabela Aluguer 
-- com a data inicio->2016-11-20, data fim->2017-11-21, duracao-> 40, Numero do Empregado->1 e Codigo do Cliente-> 1

---------------------------------------------------------------//---------------------------------------------

-- Teste Inserir Aluguer sem Cliente
--select * from Cliente select * from Aluguer select * from Empregado
declare @idCliente int
declare @idAluguer int 
exec InserirAluguerSemCliente 100,'NomeTeste','MoradaTeste',@idCliente output,25,1,'2016-10-20','2017-10-21',@idAluguer output
select @idCliente
select @idAluguer
-- é espectavel criar um Cliente e inseri-lo na tabela Aluguer 
-- o Cliente pussui data inicio ->2016-10-20, data fim->2017-10-21, duracao->25, Num empregado->1
-- inseriu-se este Cliente na tabela Aluguer com
-- nif->100, nome->NomeTeste, morada->MoradaTeste

---------------------------------------------------------------//---------------------------------------------

-- Teste listar Equipamentos sem aluguer na ultima semana
--select * from AluguerEquipamentos select * from Aluguer 
exec EquipamentosSemAluguerUltimaSemana
-- mostra só um Equipamento codigo->4, descricao->Banana Grande, Tipo->Banana

---------------------------------------------------------------//---------------------------------------------

-- Teste listar todos os equipamentos livres para um determinado Tempo e Tipo
-- select * from Equipamentos select * from Aluguer select * from AluguerEquipamentos
exec listarEquipamentos '2020-10-20','2022-10-21','Gaivota'
-- mostra 2 Equipamentos 
-- codigo->1, descricao->Gaivota Azul, Tipo->Gaivota
-- codigo->2, descricao->Gaivota Verde, Tipo->Gaivota

---------------------------------------------------------------//---------------------------------------------


rollback
