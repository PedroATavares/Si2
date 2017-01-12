using App.EF;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace App
{
    class InserirAluguerComClienteEF
    {
        private static int numEmp, niff, duracaoAlg, tuplos, preco, duracaoEq, idDesconto, idTmpEx, id2Promocoes;
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

                    printPromocoes(ctx);
                    printQuestoesPromocao();
                    aplicarTempoExtra(ctx);

                    var id = new ObjectParameter("id", 0);
                    tuplos += ctx.InserirAluguerComCliente(Convert.ToDateTime(dI), Convert.ToDateTime(dF), duracaoAlg, numEmp, num, id);

                    Console.WriteLine("ID gerado : " + id.Value);

                    float percentagem = 0;
                    foreach (var row in ctx.BuscarPercentagem(idTmpEx, id2Promocoes))
                        percentagem += Convert.ToInt16(row.Value);

                    preco = Convert.ToInt32( ((100 - percentagem) / 100) * preco );

                    do
                    {
                        printEquipamentos(ctx);
                        Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ? (para sair pressione -> q)");
                        idEq = Console.ReadLine();

                        if (idEq.Equals("q"))
                            break;

                        printEspecificos(ctx);
                        Console.WriteLine("Que equipamentos quer adicionar ao Alguer criado ? (coloque a duracao) ");
                        string aux = Console.ReadLine();

                        if (aux.Equals(""))
                            break;
                        duracaoEq = Convert.ToInt32(aux);

                        buscarPreco(ctx);
                        tuplos += ctx.InserirAluguerEquipamentos(Convert.ToDecimal(preco), Convert.ToInt32(id.Value), Convert.ToInt32(idEq));


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

        private static void buscarPercentagem(TestesSI2Entities ctx)
        {
            
        }

        private static void aplicarTempoExtra(TestesSI2Entities ctx)
        {
            foreach (var row in ctx.BuscarTempoExtra(idTmpEx, id2Promocoes))
                duracaoAlg += Convert.ToInt32(row.Value);            
        }

        private static void printPromocoes(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao as Promocoes existentes -------------------\nId |  Data Inicial  |  Data Final   |  Descricao ");
            foreach (var row in ctx.ShowPromocoes(Convert.ToDateTime(dI), Convert.ToDateTime(dF)))
                    Console.WriteLine(row.Id + "   |  " + row.DataInicio + "  |  " + row.DataFim + "  |  " + row.Descricao);
        }

        private static void printQuestoesPromocao()
        {
            Console.WriteLine("Aplicar Promoçao? (S/N)");
            String result = Console.ReadLine();
            if (result.Equals("S") || result.Equals("s"))
            {
                Console.WriteLine("Insira o Id de uma Promoção do tipo Desconto, caso nao queira aplicar, insira 0: ");
                idDesconto = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Insira o Id de uma Promoção do tipo Tempo Extra, caso nao queira aplicar, insira 0: ");
                idTmpEx = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Insira o Id de uma Promoção do tipo Desconto e TempoExtra, caso nao queira aplicar, insira 0: ");
                id2Promocoes = Int32.Parse(Console.ReadLine());
            }
        }

        private static void buscarPreco(TestesSI2Entities ctx)
        {
            foreach (var row in ctx.BuscarPrecoEspecifico(Convert.ToDateTime(dI), Convert.ToDateTime(dF), Convert.ToInt32(idEq), duracaoEq))
                preco = Convert.ToInt32(row.Value);
        }

        private static void printEquipamentos(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao os Equipamentos existentes -------------------\nCodigo |       Descricao   |  Tipo ");
            foreach ( var row in ctx.ShowEquipamentosEspecificos(Convert.ToDateTime(dI), Convert.ToDateTime(dF)) )
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
            duracaoAlg = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Coloque o Nº Empregado");
            numEmp = Convert.ToInt32(Console.ReadLine());
        }

    }
}
