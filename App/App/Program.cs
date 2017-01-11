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
            do
            {
                Console.WriteLine("\n*******************************************************");
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
                    case "1": if (ado) EditDescontoInfo.InserirDesconto(handler); else EditDescontoInfoEF.InserirPromocao(); break;
                    case "2": if (ado) EditDescontoInfo.RemoverDesconto(handler); else EditDescontoInfoEF.RemoverPromocao(); break;
                    case "3": if (ado) EditDescontoInfo.AlterarDesconto(handler); else EditDescontoInfoEF.AlterarPromocao(); break;
                    case "4": if (ado) EditTempoExtraInfo.InserirTempoExtra(handler); else EditPromocaoInfoEF.InserirPromocao(); break;
                    case "5": if (ado) EditTempoExtraInfo.RemoverTempoExtra(handler); else EditPromocaoInfoEF.RemoverPromocao(); break;
                    case "6": if (ado) EditTempoExtraInfo.AlterarTempoExtra(handler); else EditPromocaoInfoEF.AlterarPromocao(); break;
                    case "7": if (ado) InserirAluguer.PrintsSemCliente(handler); else InserirAluguerSemClienteEF.procInserirAluguerSemCliente(); break;
                    case "8": if (ado) InserirAluguer.InserirAluguerComCliente(handler); else InserirAluguerComClienteEF.procInserirAluguerComCliente(); break;
                    case "9": if (ado) RemoverAluguer.procRemoverAluger(handler); else RemoverAluguerEF.procRemoverAluger(); break;
                    case "10": if (ado) AlteracaoPrecario.GetParamsFromConsole(handler); else AlteracaoPrecarioEF.procAlteracaoPrecario(); break;
                    case "11": if (ado) ListarEquipamentos.GetParamsFromConsole(handler); else ListarEquipamentosEF.proclistarEquipamentos(); break;
                    case "12": if (ado) EquipSemAluguerUltimaSemana.GetParamsFromConsole(handler); else EquipSemAluguerUltimaSemanaEF.procEquipamentosSemAluguerUltimaSemana(); break;
                    case "13": if (ado) XmlAlugueres.GetParamsFromConsole(handler); else XmlAlugueresEF.GetParamsFromConsole(handler); break;

                    default: Console.WriteLine("Por favor insira um numero valido"); break;
                }

            } while (key != "14");

        }
    }
}



