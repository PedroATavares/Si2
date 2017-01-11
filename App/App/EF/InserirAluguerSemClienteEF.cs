using App.EF;
using System;
using System.Data.Entity.Core.Objects;


namespace App
{
    class InserirAluguerSemClienteEF
    {
        private static int numEmp, niff, duracaoo;
        private static string dI, dF, moradaa, nomee;

        public static void procInserirAluguerSemCliente()
        {
            using (var ctx = new TestesSI2Entities())
            {
                Console.WriteLine("Dados do novo Cliente  -----------------");
                printQuestoesCliente();
                Console.WriteLine("Dados do novo Aluguer  -----------------");
                printQuestoesAluguer();

                var idC = new ObjectParameter("idCliente", 0);            
                var id = new ObjectParameter("idAluguer", 0);  
                ctx.InserirAluguerSemCliente(niff, nomee, moradaa, idC, duracaoo, numEmp, Convert.ToDateTime(dI), Convert.ToDateTime(dF), id);

                Console.WriteLine("id Cliente : " + idC.Value + "  id Aluguer : " + id.Value);
            }
            Console.WriteLine("Insercao feita com sucesso !!");

        }

        private static void printQuestoesCliente()
        {
            Console.WriteLine("\n NIF do Cliente :");
            niff = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Nome do Cliente :");
            nomee = Console.ReadLine();
            Console.WriteLine("\n Morada do Cliente :");
            moradaa = Console.ReadLine();
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
