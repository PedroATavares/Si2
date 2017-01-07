using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class EditPromocaoInfo
    {
        private static Handler handler;

        public static void removerPromoção(Handler h)
        {
            if (handler == null) handler = h;
            int id;
            do
            {
                Console.Write("Id da Promoção a Remover:");
                id = Convert.ToInt32(Console.ReadLine());
            } while (id <= 0);
            removerPromoção(id);
        }

        private static void removerPromoção(int id)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        SqlParameter ident = new SqlParameter("@id", SqlDbType.Int);
                        ident.Value = id;

                        cmd.Parameters.Add(ident);

                        cmd.CommandText = "exec DeletePromocoes @id;";
                        con.Open();

                        int i = cmd.ExecuteNonQuery();

                        Console.WriteLine(i + "tuplo(s) afetado(s)");
                        Console.WriteLine("***********************************************************************");
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                }
            }
        }

        public static void inserirPromoção(Handler h)
        {
            if (handler == null) handler = h;

            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            inserirPromoção(dataInicio, dataFim, desc);
        }

        private static void inserirPromoção(String dataInicio, String dataFim, String desc)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                        SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                        dataI.Value = dataInicio;
                        dataF.Value = dataFim;
                        descr.Value = desc;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        cmd.CommandText = "declare @id int; exec InsertPromocoes @DataInicio,@DataFim,@Descricao,@id output; select @id;";
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                Console.Write("Id da promoçao:"+dr[0] + "\n");
                        }
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                }
            }
        }
    }
}
