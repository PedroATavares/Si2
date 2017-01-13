use TestesSI2

insert into Tipo(Nome,Descricao) values ('Gaivota', 'Tem pedais e um escorrega') 
insert into Equipamentos(Descricao,Tipo) values ('Gaivota Azul', 'Gaivota') --Id  1
insert into PrecoAluguer values ('2018-01-01','2019-01-01',60,35,1)
insert into PrecoAluguer values ('2018-01-01','2019-01-01',30,20,1)

insert into Equipamentos values ('Gaivota Verde', 'Gaivota') --Id 2
insert into PrecoAluguer values ('2018-01-01','2019-01-01',60,35,2)
insert into PrecoAluguer values ('2018-01-01','2019-01-01',30,20,2)

insert into Tipo(Nome,Descricao) values ('Mota de Agua', 'Mota de agua motorizada')
insert into Equipamentos(Descricao,Tipo) values ('Mota Vermelha', 'Mota de Agua') --Id 3
insert into PrecoAluguer values ('2018-01-01','2019-01-01',60,35,3)

insert into Empregado values('Maria Papoila') --Id 1
insert into Empregado values('Asdrubal Mauricio') --Id 2

insert into Cliente (NIF, nome,	Morada) values (123456789, 'Rita Catita', 'Avenida Morais')
insert into Cliente (NIF, nome,	Morada) values (987654321, 'Gervasio Manel', 'Rua de Sesamo')

insert into Promocoes (DataInicio,DataFim,Descricao) values ('2017-11-1','2018-01-01','Promoçao de inverno, 15 minutos a mais dentro de agua gelada')
insert into Promocoes (DataInicio,DataFim,Descricao)values ('2017-11-01','2018-01-01','Promoçao de inverno, desconto de 20% em todos os equipamentos')

insert into Descontos values (20,2) 
insert into TempoExtra values (15,1)

insert into Aluguer(DataInicio ,DataFim,Duracao,NumEmp,CodCli) values('2017-11-22 15:00:00','2018-11-22 15:30:00',30,1,1)
insert into AluguerEquipamentos values(20,1,1)
insert into AluguerEquipamentos values(20,1,2)
insert into AluguerPromocao values (1,2)

insert into Aluguer(DataInicio ,DataFim,Duracao,NumEmp,CodCli) values('2017-11-22 15:00:00','2017-11-22 16:00:00',60,2,2)
insert into AluguerEquipamentos values(35,2,3)
insert into AluguerPromocao values (2,1)


insert into Tipo values ('Banana','Bananas Aquaticas')
insert into Equipamentos values ('Banana Grande', 'Banana')
insert into Empregado values ('Joao Madeira')
insert into Cliente values (129364065,'','Quinta das Placas')
insert into Aluguer values ('2017-11-1','2017-11-13',5,2,2)
insert into Aluguer values ('2017-11-1','2017-11-14',5,1,1)
insert into AluguerEquipamentos values (10,4,3)

insert into Equipamentos values ('Banana Amarela', 'Banana')
insert into Empregado values ('empregado1')
insert into Cliente values (130253849,'cliente1','morada1')

insert into AluguerEquipamentos values (10,3,2)

/*
select * from Tipo
select * from Equipamentos
select * from Empregado
select * from Cliente
select * from Promocoes
*/




