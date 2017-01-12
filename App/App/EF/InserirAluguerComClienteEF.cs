using App.EF;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace App
{
    class InserirAluguerComClienteEF
    {
        private static int numEmp, niff, duracaoo, tuplos, preco, duracao;
        private static string dI, dF, idEq;

        public static void procInserirAluguerComCliente()
        {
            try { 
                using (var ctx = new TestesSI2Entities())
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

                    int num = 0;
                    foreach (var i in ctx.Cliente1.Where(x => x.nif == niff).Select(x => x.codigo)) {
                        num = i;
                    }

                    var id = new ObjectParameter("id", 0);
                    tuplos = ctx.InserirAluguerComCliente(Convert.ToDateTime(dI), Convert.ToDateTime(dF), duracaoo, numEmp, num, id);

                    Console.WriteLine("ID gerado : " + id.Value);
         
                    do
                    {
                        printEquipamentos(ctx);
                        Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ?? (para sair persione -> q)");
                        idEq = Console.ReadLine();

                        if (idEq.Equals("q"))
                            break;

                        printEspecificos(ctx);
                        Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ?? (coloque a duracao) ");
                        string aux = Console.ReadLine();

                        if (aux.Equals(""))
                            break;
                        duracao = Convert.ToInt32(aux);

                        buscarPreco(ctx);
                        tuplos += ctx.InserirAluguerEquipamentos(preco, Convert.ToInt32(id.Value), Convert.ToInt32(idEq));


                    } while (true);
                }
                Console.WriteLine("Adicao concluida, foram afectados " + tuplos + " tuplos");
                Console.ReadKey();
            }catch (Exception ex)
            {
                Console.WriteLine("E R R O : " + ex.InnerException.Message);
                Console.WriteLine("***********************************************************************");
            }
        }

        private static void buscarPreco(TestesSI2Entities ctx)
        {
            foreach (var row in ctx.BuscarPrecoEspecifico(Convert.ToDateTime(dI), Convert.ToDateTime(dF), Convert.ToInt32(idEq), duracao))
                preco = Convert.ToInt32(row.Value);
        }

        private static void printEquipamentos(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Equipamentos existentes -------------------\nCodigo |       Descricao   |  Tipo ");
            foreach ( var row in ctx.ShowEquipamentos(Convert.ToDateTime(dI), Convert.ToDateTime(dF)) )
                Console.WriteLine(row.Codigo + "   |  " + row.Descricao + "  |  " + row.Tipo);
        }

        private static void printEspecificos(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Equipamentos existentes -------------------\nCodigo |  Descricao |  Duracao  |  Preco ");
            foreach (var row in ctx.EquipamentosEspecificos(Convert.ToDateTime(dI), Convert.ToDateTime(dF), Convert.ToInt32(idEq) ) )
            {
                Console.WriteLine(row.Codigo + "   |  " + row.Descricao + "  |  " + row.Duracao + "  |  " + row.Valor);
            }                
        }

        private static void printClientes(TestesSI2Entities ctx)
        {
            foreach (var row in ctx.Cliente1)
                if(row.nif != 0)
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
