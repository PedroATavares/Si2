using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using App.EF;
using System.Data.Entity.Core.Objects;

namespace App
{
    class Program
    {
        static readonly string connectionString1 = @"Server=localhost;Database=TestesSI2;User=jdbcuser;Password=jdbcuser;";
        static readonly string connectionString2 = @"Server=localhost;Database=SI2.SInverno;User=sa;Password=plastico.2";
        static readonly string connectionString3 = @"Server=CAROLINA;Database=TestesSI2;User=ls;Password=ls;";

        static void Main(string[] args)
        {
                    
            Handler handler = new Handler(connectionString1);
            String key;
            do
            {
                Console.WriteLine("1 - Inserir Promoção");
                Console.WriteLine("2 - Remover Promoção");
                Console.WriteLine("3 - Actualizar informação de uma Promoção");
                Console.WriteLine("4 - Inserir Aluguer com Cliente Novo");
                Console.WriteLine("5 - Inserir Aluguer com Cliente Existente");
                Console.WriteLine("6 - Remover Aluguer");
                Console.WriteLine("7 - Alterar Preçário");
                Console.WriteLine("8 - Listar todos os equipamentos livres, para um determinado tempo e tipo");
                Console.WriteLine("9 - Listar os equipamentos sem alugueres na última semana");
                Console.WriteLine("10 - Sair");
                Console.WriteLine("Insira o numero da operação que pretende");
                Console.Write(">");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": EditPromocaoInfo.InserirPromoção(handler); break;
                    case "2": EditPromocaoInfo.RemoverPromoção(handler); break;
                    case "3": EditPromocaoInfo.AlterarPromoção(handler); break;
                    case "4": InserirAluguer.InserirAluguerSemCliente(handler); break;
                        //inserçao com cliente nao funca, diz que falta um parametro
                    case "5": InserirAluguer.InserirAluguerComCliente(handler); break;
                    case "6": RemoverAluguer.procRemoverAluger(handler); break;
                    case "7": AlteracaoPrecario.GetParamsFromConsole(handler); break;
                    case "8": ListarEquipamentos.GetParamsFromConsole(handler); break;
                    case "9": EquipSemAluguerUltimaSemana.GetParamsFromConsole(handler); break;
                 
                    default: Console.WriteLine("Por favor insira um numero valido"); break;
                }
            } while (key != "10");
            
            /*
            using(var context = new SI2Entities()) {
                // var blog = new Cliente { NIF=1213243,Nome="Test",Morada="test",Removido=0};
                //context.Clientes.Add(blog);
                //context.SaveChanges();

                var query = from emp in context.Empregadoes
                            where emp.Codigo == 1
                            select emp;


                Console.WriteLine("All Empregados in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Codigo);
                }
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                
            }
            */

        }
    }
}



