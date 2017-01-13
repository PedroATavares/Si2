using App.EF;
using System;
using System.Data.Entity.Core.Objects;


namespace App
{
    class RemoverAluguerEF
    {
        private static int tuplos;

        public static void procRemoverAluger()
        {
            using (var ctx = new TestesSI2Entities())
            {
                printAluguer(ctx);
                Console.WriteLine("Escolha o Aluguer (id) que deseja eliminar : ");

                tuplos = ctx.RemoverAluger(Convert.ToInt32(Console.ReadLine()));
            }
            Console.WriteLine("Remocao concluida, foram afectados " + tuplos + " tuplos");
            Console.ReadKey();
        }

        private static void printAluguer(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Alugueres existentes -------------------\nNum | DataInicio  | DataFim   |  Duracao | Nº Empregado | Codigo Cliente ");
            foreach (var row in ctx.Aluguer1)
                Console.WriteLine(row.Num + "   |  " + row.DataInicio + "  |  " + row.DataFim + "  |  " + row.Duracao + "  |  " + row.NumEmp + "  |  " + row.CodCli);
        }

    }
}
