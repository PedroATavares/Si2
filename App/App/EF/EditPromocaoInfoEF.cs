using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EF
{
    class EditPromocaoInfoEF
    {
        private static int id, tempo, tuplos;
        private static string dataI, dataF, descricao;

        //------------------Inserir Pomocao ---------------------        
        public static void InserirPromocao()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printQuestoesInsert();

                var id = new ObjectParameter("id", 0);
                tuplos = ctx.InsertPromocaoTempo(Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, tempo, id);                
            }
            Console.WriteLine("Insercao concluida, foram afectados " + tuplos + " tuplos");
            Console.ReadKey();
        }

        private static void printQuestoesInsert()
        {
            Console.WriteLine("Crie uma Promacao  ------------------------");
            Console.WriteLine("Data de Inicio (AAAA-MM-DD):");
            dataI = Console.ReadLine();
            Console.WriteLine("Data de Fim (AAAA-MM-DD):");
            dataF = Console.ReadLine();
            Console.WriteLine("Descrição (max 200 caracteres):");
            descricao = Console.ReadLine();
            Console.WriteLine("Tempo extra (em minutos):");
            tempo = Int32.Parse(Console.ReadLine());
        }

        // ----------------- Delete Promocoes ----------------------
        public static void RemoverPromocao()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printPromocoes(ctx);
                printQuestaoRemover();

                tuplos = ctx.DeletePromocoes(id);
            }
            Console.WriteLine("Remocao concluida, foram afectados " + tuplos + " tuplos");
            Console.ReadKey();
        }

        private static void printQuestaoRemover()
        {
            Console.WriteLine("Id da Promoção a Remover:");
            id = Convert.ToInt32(Console.ReadLine());
        }

        //----------------- Alterar Pomocoes -----------------------
        public static void AlterarPromocao()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printPromocoes(ctx);
                printQuestaoUpdate();


                if (dataI.Equals("") && dataI.Equals("") && descricao.Equals("") && tempo == -1 )
                    tuplos = ctx.UpdatePromocoesTempo(id, null, null, null, null);

                else if (dataI.Equals("") && dataI.Equals("") && tempo == -1)
                    tuplos = ctx.UpdatePromocoesTempo(id, null, null, descricao, null);

                else if (dataI.Equals("") && descricao.Equals("") && tempo == -1)
                    tuplos = ctx.UpdatePromocoesTempo(id, null, Convert.ToDateTime(dataF), null, null);

                else if (dataF.Equals("") && descricao.Equals("") && tempo == -1)
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), null , null, null);

                else if(dataI.Equals("") && dataI.Equals("") && descricao.Equals("") )
                    ctx.UpdatePromocoesTempo(id, null, null, null, tempo);

                else if (dataI.Equals("") && dataI.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, null, null, descricao, tempo);

                else if (dataI.Equals("") && tempo == -1)
                    tuplos = ctx.UpdatePromocoesTempo(id, null, Convert.ToDateTime(dataF), descricao, null);

                else if (dataI.Equals("") && descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, null, Convert.ToDateTime(dataF), null, tempo);

                else if (dataF.Equals("") && descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), null, null, tempo);

                else if (tempo == -1)
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, null);

                else if (descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), null, tempo);
 
                else if (dataF.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), null, descricao, tempo);

                else if (dataI.Equals(""))
                    tuplos = ctx.UpdatePromocoesTempo(id, null, Convert.ToDateTime(dataF), descricao, tempo);
            
                else
                    tuplos = ctx.UpdatePromocoesTempo(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, tempo);
                    
            }
            Console.WriteLine("Alteracao concluida, foram afectados " + tuplos + " tuplos");
            Console.ReadKey();
        }

        private static void printQuestaoUpdate()
        {
            Console.WriteLine("Id da Promoção a alterar:");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Data de Inicio (AAAA-MM-DD):");
            dataI = Console.ReadLine();
            Console.WriteLine("Data de Fim (AAAA-MM-DD):");
            dataF = Console.ReadLine();
            Console.WriteLine("Descrição (max 200 caracteres):");
            descricao = Console.ReadLine();
            Console.WriteLine("Tempo extra (em minutos):");
            string aux = Console.ReadLine();
            tempo = aux.Equals("") ? -1 : Convert.ToInt32(aux);
        }

        private static void printPromocoes(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao as Procoes existentes -------------------\nID|  Descricao  |   Data Inicio   |   Data Fim  | Id TempoExtra | Tempo Extra");

            foreach (var row in ctx.TempoExtra)
                Console.WriteLine(row.Promocoes.Id + "   |  " + row.Promocoes.Descricao + "  |  " + row.Promocoes.DataInicio + "  |  " + row.Promocoes.DataFim + "  |  " + row.Id + "  |  " + row.TempoExtra1 );
        }

    }
}
