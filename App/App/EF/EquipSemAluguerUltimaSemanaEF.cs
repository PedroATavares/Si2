using App.EF;
using System;
using System.Data.Entity.Core.Objects;


namespace App
{
    class EquipSemAluguerUltimaSemanaEF
    {
        public static void procEquipamentosSemAluguerUltimaSemana()
        {

            using (var ctx = new TestesSI2Entities())
            {
                Console.WriteLine("Estes sao os Equipamentos existentes entre na ultima semana -------------"  + "\nCODIGO|  Descricao      |     Tipo  ");

                foreach (var row in ctx.EquipamentosSemAluguerUltimaSemana())
                    Console.WriteLine(row.Codigo + "   |  " + row.Descricao + "   |  " + row.Tipo);

                Console.ReadKey();
            }
        }
    }
}
