using App.EF;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace App
{
    class InserirAluguerComClienteEF
    {
        private static int numEmp, niff, duracaoo;
        private static string dI, dF;

        public static void procInserirAluguerComClienteEF()
        {

            using (var ctx = new SI2Entities() )
            {
                Console.WriteLine("Estes sao os Clientes existentes -------------------\nCODIGO|  NIF   |     NOME   |      MORADA");
                printClientesEF(ctx);

                Console.WriteLine("\nEscolha um dos Clientes (codigo NIF):");
                niff = Convert.ToInt32(Console.ReadLine());

                if (niff <= 0)
                {
                    Console.WriteLine("O NIF que colocou esta incorrecto, volte a tentar");
                    printClientesEF(ctx);
                }

                printQuestoesAluguer();

                int num = 0 ;
                foreach ( var i in ctx.ClienteView.Where(x => x.nif == niff).Select(x => x.codigo)){
                    num = i;
                }
                
                var id = new ObjectParameter("id", 0);
                ctx.InserirAluguerComCliente(Convert.ToDateTime(dI), Convert.ToDateTime(dF), duracaoo, numEmp, num, id);

                Console.WriteLine(id);
            }
            Console.WriteLine("Adicao concluida !!! ");
        }

        private static void printClientesEF(SI2Entities ctx)
        {
            foreach (var row in ctx.ClienteView)
                if (row.nif > 0)
                    Console.WriteLine(row.codigo + "   |  " + row.nif + "  |  " + row.nome + "  |  " + row.morada);
        }

        private static void printQuestoesAluguer()
        {
            Console.WriteLine("\n Coloque a Data Inicial");
            dI = Console.ReadLine();
            Console.WriteLine("\n Coloque a Data Final");
            dF = Console.ReadLine();
            Console.WriteLine("\n Coloque a Duracao");
            duracaoo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Coloque o Nº Empregado");
            numEmp = Convert.ToInt32(Console.ReadLine());
        }
    }
}
