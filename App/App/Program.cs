using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using App.EF;

namespace App
{
    class Program
    {
        static readonly string connectionString1 = @"Server=localhost;Database=TestesSI2;User=jdbcuser;Password=jdbcuser;";
        static readonly string connectionString2 = @"Server=localhost;Database=SI2.SInverno;User=sa;Password=plastico.2";
        static readonly string connectionString3 = @"Server=CAROLINA;Database=Si2;User=ls;Password=ls;";

        static void Main(string[] args)
        {
        
            Handler handler = new Handler(connectionString3);
            String key;
            do
            {
                Console.WriteLine("1 - Inserir Cliente");
                Console.WriteLine("2 - Remover Cliente");
                Console.WriteLine("3 - Actualizar informação de um Cliente");
                Console.WriteLine("4 - Inserir Equipamento");
                Console.WriteLine("5 - Remover Equipamento");
                Console.WriteLine("6 - Actualizar informação de um Equipamento");
                Console.WriteLine("7 - Inserir Promoção");
                Console.WriteLine("8 - Remover Promoção");
                Console.WriteLine("9 - Actualizar informação de uma Promoção");
                Console.WriteLine("10 - Inserir Aluguer com Cliente Novo");
                Console.WriteLine("11 - Inserir Aluguer com Cliente Existente");
                Console.WriteLine("12 - Remover Aluguer");
                Console.WriteLine("13 - Alterar Preçário");
                Console.WriteLine("14- Listar todos os equipamentos livres, para um determinado tempo e tipo");
                Console.WriteLine("15 - Listar os equipamentos sem alugueres na última semana");
                Console.WriteLine("16 - Sair");
                Console.WriteLine("Insira o numero da operação que pretende");
                Console.Write(">");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": break;
                    case "2": break;
                    case "3": break;
                    case "4": break;
                    case "5": break;
                    case "6": break;
                    case "7": EditPromocaoInfo.inserirPromoção(handler); break;
                    case "8": EditPromocaoInfo.removerPromoção(handler); break;
                    case "9": break;
                    case "10": InserirAluguer.InserirAluguerSemCliente(handler); break;
                        //inserçao com cliente nao funca, diz que falta um parametro
                    case "11": InserirAluguer.InserirAluguerComCliente(handler); break;
                    case "12": RemoverAluguer.procRemoverAluger(handler); break;
                    case "13": AlteracaoPrecario.GetParamsFromConsole(handler); break;
                    case "14": ListarEquipamentos.GetParamsFromConsole(handler); break;
                    case "15": EquipSemAluguerUltimaSemana.GetParamsFromConsole(handler); break;
                 
                    default: Console.WriteLine("Por favor insira um numero valido"); break;
                }
            } while (key != "16");
            
            /*
            using(var context = new SI2Entities()) {
                var blog = new Cliente { NIF=1213243,Nome="Test",Morada="test",Removido=0};
                context.Clientes.Add(blog);
                context.SaveChanges();
                
                // Display all Blogs from the database 
                var query = from b in context.ClienteViews
                            orderby b.nome
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.nif);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                
            }
            */
        }
    }
}


