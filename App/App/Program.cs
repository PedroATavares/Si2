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
                    
            Handler handler = new Handler(connectionString3);
            String key;
            do
            {
                Console.WriteLine("1 - Inserir Promoção do tipo Desconto");
                Console.WriteLine("2 - Remover Promoção do tipo Desconto");
                Console.WriteLine("3 - Actualizar informação de uma Promoção do tipo Desconto");
                Console.WriteLine("4 - Inserir Promoção do tipo Tempo Extra");
                Console.WriteLine("5 - Remover Promoção do tipo Tempo Extra");
                Console.WriteLine("6 - Actualizar informação de uma Promoção do tipo Tempo Extra");
                Console.WriteLine("7 - Inserir Aluguer com Cliente Novo");
                Console.WriteLine("8 - Inserir Aluguer com Cliente Existente");
                Console.WriteLine("9 - Remover Aluguer");
                Console.WriteLine("10 - Alterar Preçário");
                Console.WriteLine("11 - Listar todos os equipamentos livres, para um determinado tempo e tipo");
                Console.WriteLine("12 - Listar os equipamentos sem alugueres na última semana");
                Console.WriteLine("13 - Produzir XML de Alugueres num determinado intervalo");
                Console.WriteLine("14 - Sair");
                Console.WriteLine("Insira o numero da operação que pretende");
                Console.Write(">");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": EditDescontoInfo.InserirDesconto(handler); break;
                    case "2": EditDescontoInfo.RemoverDesconto(handler); break;
                    case "3": EditDescontoInfo.AlterarDesconto(handler); break;
                    case "4": EditTempoExtraInfo.InserirTempoExtra(handler); break;
                    case "5": EditTempoExtraInfo.RemoverTempoExtra(handler); break;
                    case "6": EditTempoExtraInfo.AlterarTempoExtra(handler); break;
                    case "7": InserirAluguer.PrintsSemCliente(handler); break;
                    case "8": InserirAluguer.InserirAluguerComCliente(handler); break;
                    case "9": RemoverAluguer.procRemoverAluger(handler); break;
                    case "10": AlteracaoPrecario.GetParamsFromConsole(handler); break;
                    case "11": ListarEquipamentos.GetParamsFromConsole(handler); break;
                    case "12": EquipSemAluguerUltimaSemana.GetParamsFromConsole(handler); break;
                    case "13": XmlAlugueres.GetParamsFromConsole(handler); break;

                    default: Console.WriteLine("Por favor insira um numero valido"); break;
                }
<<<<<<< HEAD
            } while (key != "13");
=======
            } while (key != "14");
            
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
>>>>>>> 82d00cf289198209b3416bbc677d6967966c3973

        }

        
    }
}



