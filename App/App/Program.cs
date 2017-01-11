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
        static readonly string connectionString = @"Server=localhost;Database=TestesSI2;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            bool ado=false;
            Handler handler = new Handler(connectionString);
            String key;
            do
            {
                Console.WriteLine("1 - Usar ADO.NET");
                Console.WriteLine("2 - Usar Entity Frameworks");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": ado=true; break;
                    case "2": ado=false; break;
                }
            } while (key != "1" && key != "2");
            Console.WriteLine("*******************************************************");
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
                    case "13": if (ado) XmlAlugueres.GetParamsFromConsole(handler); else XmlAlugueresEF.GetParamsFromConsole(handler); break;

                    default: Console.WriteLine("Por favor insira um numero valido"); break;
                }

            } while (key != "14");

        }
    }
}



