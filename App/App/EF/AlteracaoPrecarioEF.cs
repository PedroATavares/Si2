using App.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class AlteracaoPrecarioEF
    {
        private static int idC, valor, duracao, tuplos;
        private static string dataI, dataF;

        public static void procAlteracaoPrecario()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printPrecoAluguer(ctx);
                printQuestoesPrecario();

                if(dataF.Equals("") && valor == -1)
                    tuplos = ctx.alteracoesPrecario(Convert.ToDateTime(dataI), null, duracao, null, idC);

                else if(dataF.Equals(""))
                    tuplos = ctx.alteracoesPrecario(Convert.ToDateTime(dataI), null, duracao, valor, idC);

                else if (valor == -1)
                    tuplos = ctx.alteracoesPrecario(Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), duracao, null, idC);

                else
                    tuplos = ctx.alteracoesPrecario(Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), duracao, valor, idC);               
            }
            Console.WriteLine("Alteracao concluida, foram afectados " + tuplos + " tuplos");
            Console.ReadKey();

        }

        private static void printQuestoesPrecario()
        {
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Insira o Id do Equipamento");
            idC = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Insira a Data Inicial");
            dataI = Console.ReadLine();
            Console.WriteLine("Insira a duraçao");
            duracao = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Insira a nova Data Final, caso seja o que pretenda alterar");
            dataF = Console.ReadLine();
            Console.WriteLine("Insira o novo Preço, caso seja o que pretenda alterar");
            string aux = Console.ReadLine();
            valor =aux.Equals("") ? -1 : Convert.ToInt32(aux);
        }

        private static void printPrecoAluguer(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os PrecoAlugueres existentes -------------------\nValidadeI    |  ValidadeF   |  Duracao   | Valor  |  EquipaID ");
            foreach (var row in ctx.PrecoAluguer)
                    Console.WriteLine(row.ValidadeI + "   |  " + row.ValidadeF + "  |  " + row.Duracao + "  |  " + row.Valor + "  |  " + row.EquipId);
        }
    }
}
