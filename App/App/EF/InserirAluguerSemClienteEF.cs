using App.EF;
using System;
using System.Data.Entity.Core.Objects;


namespace App
{
    class InserirAluguerSemClienteEF
    {
        private static int numEmp, niff, duracaoo, tuplos, preco;
        private static string dI, dF, moradaa, nomee, idEq;

        public static void procInserirAluguerSemCliente()
        {
            try { 
                using (var ctx = new TestesSI2Entities())
                {
                    Console.WriteLine("Dados do novo Cliente  -----------------");
                    printQuestoesCliente();
                    Console.WriteLine("Dados do novo Aluguer  -----------------");
                    printQuestoesAluguer();

                    var idC = new ObjectParameter("idCliente", 0);            
                    var id = new ObjectParameter("idAluguer", 0);  
                    tuplos = ctx.InserirAluguerSemCliente(niff, nomee, moradaa, idC, duracaoo, numEmp, Convert.ToDateTime(dI), Convert.ToDateTime(dF), id);

                    Console.WriteLine("id Cliente gerado : " + idC.Value + "  id Aluguer gerado : " + id.Value);

                    printEquipamentos(ctx);

                    do
                    {
                        Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ?? (para sair persione -> q)");
                        idEq = Console.ReadLine();

                        if (idEq.Equals("q"))
                            break;

                        Console.WriteLine("Preco desejado :");
                        preco = Convert.ToInt32(Console.ReadLine());

                        var aux = new ObjectParameter("id", 0);
                        tuplos += ctx.InserirAluguerEquipamentos(preco, Convert.ToInt32(id.Value), Convert.ToInt32(idEq));


                    } while (true);

                }
                Console.WriteLine("Insercao concluida, foram afectados " + tuplos + " tuplos");
                Console.ReadKey();
            }catch (Exception ex)
            {
                Console.WriteLine("E R R O : " + ex.InnerException.Message);
                Console.WriteLine("***********************************************************************");
            }
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

        private static void printEquipamentos(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Equipamentos existentes -------------------\nCodigo |       Descricao   |  Tipo ");
            foreach (var row in ctx.Equipamentos1)
                Console.WriteLine(row.Codigo + "   |  " + row.Descricao + "  |  " + row.Tipo );
        }
    }
}
