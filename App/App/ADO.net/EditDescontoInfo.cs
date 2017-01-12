using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class EditDescontoInfo
    {
        private static Handler handler;

        public static void RemoverDesconto(Handler h)
        {
            if (handler == null) handler = h;
            EntitiesUtils.ShowDescontos(handler);
            Console.Write("Id da Promoção Desconto a Remover:");
            int id = Convert.ToInt32(Console.ReadLine());
            RemoverDesconto(id);
        }

        

        private static void RemoverDesconto(int id)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeletePromocoes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ident = new SqlParameter("@id", SqlDbType.Int);
                        ident.Value = id;
                        cmd.Parameters.Add(ident);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Removido com sucesso");
                        Console.WriteLine("***********************************************************************");
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.Write("***********************************************************************\n");
                }
            }
        }

        public static void InserirDesconto(Handler h)
        {
            if (handler == null) handler = h;

            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            Console.Write("Valor em percentagem do Desconto (por exemplo 20):");
            int desconto = Int32.Parse(Console.ReadLine());
            Console.Write("***********************************************************************\n");
            InserirDesconto(dataInicio, dataFim, desc, desconto);
        }

        private static void InserirDesconto(String dataInicio, String dataFim, String desc, int descon)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertPromocaoDesconto", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                        SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                        SqlParameter percentagem = new SqlParameter("@Percentagem", SqlDbType.SmallMoney);
                        SqlParameter toRet = new SqlParameter("@id", SqlDbType.Int);
                        toRet.Direction = ParameterDirection.Output;

                        dataI.Value = dataInicio;
                        dataF.Value = dataFim;
                        descr.Value = desc;
                        percentagem.Value = descon;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        cmd.Parameters.Add(percentagem);
                        cmd.Parameters.Add(toRet);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserido com Sucesso");
                        Console.Write("***********************************************************************\n");
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.Write("***********************************************************************\n");
                }
            }
        }

        public static void AlterarDesconto(Handler h)
        {
            if (handler == null) handler = h;
            EntitiesUtils.ShowDescontos(handler);
            Console.Write("Id da Promoção a alterar (obrigatório):");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de Inicio (AAAA-MM-DD)(opcional):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD)(opcional):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres)(opcional):");
            String desc = Console.ReadLine();
            Console.Write("Valor da percentagem (opcional):");
            string s = Console.ReadLine();
            int percentagem = s == "" ? -1 : Convert.ToInt32(s);
            Console.Write("***********************************************************************\n");
            AlterarDesconto(id, dataInicio, dataFim, desc, percentagem);
        }

        private static void AlterarDesconto(int num, String dataInicio, String dataFim, String desc, int percentagem)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdatePromocoesDescontos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                        id.Value = num;
                        cmd.Parameters.Add(id);

                        if (dataInicio != ""){
                            SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                            dataI.Value = dataInicio;
                            cmd.Parameters.Add(dataI);   
                        }
                        if (dataFim != ""){
                            SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                            dataF.Value = dataFim;
                            cmd.Parameters.Add(dataF);
                        }
                        if (dataInicio != ""){
                            SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                            descr.Value = desc;
                            cmd.Parameters.Add(descr);

                        }
                        if (percentagem > 0){
                            SqlParameter perc = new SqlParameter("@Percentagem", SqlDbType.Int);
                            perc.Value = percentagem;
                            cmd.Parameters.Add(perc);
                        }
                        if (desc != "")
                        {
                            SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                            descr.Value = desc;
                            cmd.Parameters.Add(descr);
                        }

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alterado com Sucesso");
                        Console.Write("***********************************************************************\n");
                    }

                }
                catch (DbException ex)
                {

                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.Write("***********************************************************************\n");
                }
            }
        }
    }
}
