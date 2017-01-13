using App.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class ListarEquipamentosEF
    {
        private static string dataI, dataF, tipo;

        public static void proclistarEquipamentos()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printQuestoes();

                Console.WriteLine("\nEstes sao os Equipamentos existentes entre : " + dataI + " e " + dataF + "\nCODIGO|  Descricao      |     Tipo  ");

                foreach (var row in ctx.listarEquipamentos(Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), tipo) )
                        Console.WriteLine(row.Codigo + "   |  " + row.Descricao + "   |  " + row.Tipo);
            }
            Console.ReadKey();
        }

        private static void printQuestoes()
        {
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Insira a Data Inicial");
            dataI = Console.ReadLine();
            Console.WriteLine("Insira a Data Final");
            dataF = Console.ReadLine();
            Console.WriteLine("Insira o Tipo do Equipamento");
            tipo = Console.ReadLine();
        }
    }
}
