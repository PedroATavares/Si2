using App.EF;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace App
{
    class InserirAluguerComClienteEF
    {
        private static int numEmp, niff, duracaoo, tuplos;
        private static string dI, dF;

        public static void procInserirAluguerComCliente()
        {

            using (var ctx = new TestesSI2Entities() )
            {
                Console.WriteLine("Estes sao os Clientes existentes -------------------\nCODIGO|  NIF   |     NOME   |      MORADA");
                printClientes(ctx);

                Console.WriteLine("\nEscolha um dos Clientes (codigo NIF):");
                niff = Convert.ToInt32(Console.ReadLine());

                if (niff <= 0)
                {
                    Console.WriteLine("O NIF que colocou esta incorrecto, volte a tentar");
                    printClientes(ctx);
                }

                printQuestoesAluguer();

                int num = 0 ;
                foreach ( var i in ctx.Cliente1.Where(x => x.nif == niff).Select(x => x.codigo)){
                    num = i;
                }
                
                var id = new ObjectParameter("id", 0);
                tuplos = ctx.InserirAluguerComCliente(Convert.ToDateTime(dI), Convert.ToDateTime(dF), duracaoo, numEmp, num, id);

                Console.WriteLine("ID gerado : " + id);
            }
            Console.WriteLine("Adicao concluida, foram afectados " + tuplos + " tuplos" );
            Console.ReadKey();
        }

        private static void printClientes(TestesSI2Entities ctx)
        {
            foreach (var row in ctx.Cliente1)
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
