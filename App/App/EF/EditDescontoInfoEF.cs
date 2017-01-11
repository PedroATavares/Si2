using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EF
{
    class EditDescontoInfoEF
    {
        private static int id, desconto, tuplos;
        private static string dataI, dataF, descricao;

        //------------------Inserir Pomocao ---------------------        

        public static void procInsertPromocaoDesconto()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printQuestoesInsert();

                var id = new ObjectParameter("id", 0);
                ctx.InsertPromocaoDesconto(Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, desconto, id);
            }
        }

        private static void printQuestoesInsert()
        {
            Console.WriteLine("Data de Inicio (AAAA-MM-DD):");
            dataI = Console.ReadLine();
            Console.WriteLine("Data de Fim (AAAA-MM-DD):");
            dataF = Console.ReadLine();
            Console.WriteLine("Descrição (max 200 caracteres):");
            descricao = Console.ReadLine();
            Console.WriteLine("Valor em percentagem do Desconto (por exemplo 20):");
            desconto = Int32.Parse(Console.ReadLine());
        }

        //----------------- Alterar Pomocoes -----------------------

        public static void procUpdatePromocoesDescontos()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printPromocoesDesconto(ctx);
                printQuestoesUpdate();

                if (dataI.Equals("") && dataI.Equals("") && descricao.Equals("") && desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, null, null, null);

                else if (dataI.Equals("") && dataI.Equals("") && desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, null, descricao, null);

                else if (dataI.Equals("") && descricao.Equals("") && desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, Convert.ToDateTime(dataF), null, null);

                else if (dataF.Equals("") && descricao.Equals("") && desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), null, null, null);

                else if (dataI.Equals("") && dataI.Equals("") && descricao.Equals(""))
                    ctx.UpdatePromocoesDescontos(id, null, null, null, desconto);

                else if (dataI.Equals("") && dataI.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, null, descricao, desconto);

                else if (dataI.Equals("") && desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, Convert.ToDateTime(dataF), descricao, null);

                else if (dataI.Equals("") && descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, Convert.ToDateTime(dataF), null, desconto);

                else if (dataF.Equals("") && descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), null, null, desconto);

                else if (desconto == -1)
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, null);

                else if (descricao.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), null, desconto);

                else if (dataF.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), null, descricao, desconto);

                else if (dataI.Equals(""))
                    tuplos = ctx.UpdatePromocoesDescontos(id, null, Convert.ToDateTime(dataF), descricao, desconto);

                else
                    tuplos = ctx.UpdatePromocoesDescontos(id, Convert.ToDateTime(dataI), Convert.ToDateTime(dataF), descricao, desconto);
            }
            Console.WriteLine("Update concluido, foram afectados " + tuplos + " tuplos");
        }

        private static void printQuestoesUpdate()
        {
            Console.WriteLine("Id da Promoção a alterar:");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Data de Inicio (AAAA-MM-DD):");
            dataI = Console.ReadLine();
            Console.WriteLine("Data de Fim (AAAA-MM-DD):");
            dataF = Console.ReadLine();
            Console.WriteLine("Descrição (max 200 caracteres):");
            descricao = Console.ReadLine();
            Console.WriteLine("Valor em percentagem do Desconto (por exemplo 20):");
            desconto = Int32.Parse(Console.ReadLine());
        }

        // ----------------- Delete Promocoes ----------------------

        public static void procDeletePromocoes()
        {

            using (var ctx = new TestesSI2Entities())
            {
                printPromocoesDesconto(ctx);
                printQuestaoRemover();

                tuplos = ctx.DeletePromocoes(id);

                Console.WriteLine("Remocao concluida, foram afectados " + tuplos + " tuplos");
            }
        }

        private static void printQuestaoRemover()
        {
            Console.WriteLine("Id da Promoção a Remover:");
            id = Convert.ToInt32(Console.ReadLine());
        }

        private static void printPromocoesDesconto(TestesSI2Entities ctx)
        {
            Console.WriteLine("Estes sao as Procoes Desconto existentes -------------------\nID|  Descricao  |   Data Inicio   |   Data Fim   |  Id desconto  |  Percentagem  ");

            foreach (var row in ctx.Descontos )
                Console.WriteLine(row.Promocoes.Id + "   |  " + row.Promocoes.Descricao + "  |  " + row.Promocoes.DataInicio + "  |  " + row.Promocoes.DataFim + "  |  " + row.Id + "  |  " + row.Percentagem);
        }


    }
}

