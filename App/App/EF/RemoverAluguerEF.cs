using App.EF;
using System;
using System.Data.Entity.Core.Objects;


namespace App
{
    class RemoverAluguerEF
    {
        public static void procRemoverAluger()
        {
            using (var ctx = new TestesSI2Entities())
            {
                printAluguer(ctx);
                Console.WriteLine("Escolha o Aluguer (id) que deseja eliminar : ");
                int idAluguer = Convert.ToInt32(Console.ReadLine());

                ctx.RemoverAluger(idAluguer);

                printAluguer(ctx);
            }
            Console.WriteLine("Remocao concluida !!");
        }

        private static void printAluguer(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Alugueres existentes -------------------\nNum | DataInicio  | DataFim   |  Duracao | Nº Empregado | Codigo Cliente ");
            foreach (var row in ctx.Aluguer1)
                Console.WriteLine(row.Num + "   |  " + row.DataInicio + "  |  " + row.DataFim + "  |  " + row.Duracao + "  |  " + row.NumEmp + "  |  " + row.CodCli);
        }

    }
}
