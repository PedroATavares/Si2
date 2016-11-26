insert into Tipo(Nome,Descricao) values ('Gaivota', 'Tem pedais e um escorrega') 
insert into Equipamentos(Descrip,Tipo) values ('Gaivota Azul', 'Gaivota') --Id  1
insert into PrecoAluguer(DataI,DataF,Duration,Valor,EquipId) values ('2016-01-01','2017-01-01',60,35,1)
insert into PrecoAluguer(DataI,DataF,Duration,Valor,EquipId) values ('2016-01-01','2017-01-01',30,20,1)

insert into Equipamentos values ('Gaivota Verde', 'Gaivota') --Id 2
insert into PrecoAluguer(DataI,DataF,Duration,Valor,EquipId) values ('2016-01-01','2017-01-01',60,35,2)
insert into PrecoAluguer(DataI,DataF,Duration,Valor,EquipId) values ('2016-01-01','2017-01-01',30,20,2)

insert into Tipo(Nome,Descricao) values ('Mota de Agua', 'Mota de agua motorizada')
insert into Equipamentos(Descrip,Tipo) values ('Mota Vermelha', 'Mota de Agua') --Id 3
insert into PrecoAluguer(DataI,DataF,Duration,Valor,EquipId) values ('2016-01-01','2017-01-01',60,35,3)

insert into Empregado values('Maria Papoila') --Id 1
insert into Empregado values('Asdrubal Mauricio') --Id 2

--Passar de nome para Nome e por o NIF a unique
insert into Cliente (NIF,nome,Morada) values (0,'cliente final','')
insert into Cliente (NIF, nome,	Morada) values (123456789, 'Rita Catita', 'Avenida Morais')
insert into Cliente (NIF, nome,	Morada) values (987654321, 'Gervasio Manel', 'Rua de Sesamo')

insert into Promocoes values ('2016-11-01','2017-01-01','Promoçao de inverno, 15 minutos a mais dentro de agua gelada')
insert into Promocoes values ('2016-11-01','2017-01-01','Promoçao de inverno, desconto de 20% em todos os equipamentos')

insert into Descontos values (20,2) --pede um small money, devia de pedir um int
insert into TempoExtra values (15,1)

select * from Tipo
select * from Equipamentos
select * from Empregado
select * from Cliente
select * from Promocoes

insert into Aluguer(DataInicio ,DataFim,Duracao,NumEmp,CodCli) values('2016-11-22 15:00:00','2016-11-22 15:30:00',30,1,1)
insert into AluguerEquipamentos values(20,1,1)
insert into AluguerEquipamentos values(20,1,2)
insert into AluguerPromocao values (1,2)

insert into Aluguer(DataInicio ,DataFim,Duracao,NumEmp,CodCli) values('2016-11-22 15:00:00','2016-11-22 16:00:00',60,2,2)
insert into AluguerEquipamentos values(35,2,3)
insert into AluguerPromocao values (2,1)




