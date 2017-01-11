using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class EditTempoExtraInfo
    {
        private static Handler handler;

        public static void RemoverPromoção(Handler h)
        {
            if (handler == null) handler = h;
            int id;
            do
            {
                Console.Write("Id da Promoção a Remover:");
                id = Convert.ToInt32(Console.ReadLine());
            } while (id <= 0);
            RemoverPromoção(id);
        }

        private static void RemoverPromoção(int id)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeletePromocoes",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter ident = new SqlParameter("@id", SqlDbType.Int);
                        ident.Value = id;
                        cmd.Parameters.Add(ident);

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

        public static void InserirPromoçãoTempo(Handler h)
        {
            if (handler == null) handler = h;

            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            Console.Write("Tempo extra (em minutos):");
            int tempo = Int32.Parse(Console.ReadLine());
            InserirPromoção(dataInicio, dataFim, desc, tempo);
        }

        private static void InserirPromoção(String dataInicio, String dataFim, String desc, int temp)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertPromocaoTempo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                        SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                        SqlParameter tempo = new SqlParameter("@Tempo", SqlDbType.Int);
                        SqlParameter toRet = new SqlParameter("@id", SqlDbType.Int);
                        toRet.Direction = ParameterDirection.Output;
                        
                        dataI.Value = dataInicio;
                        dataF.Value = dataFim;
                        descr.Value = desc;
                        tempo.Value = temp;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        cmd.Parameters.Add(tempo);
                        cmd.Parameters.Add(toRet);
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            Console.Write(toRet.ToString());
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

        public static void AlterarPromoção(Handler h)
        {
            if (handler == null) handler = h;

            Console.Write("Id da Promoção a alterar:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres):");
            String desc = Console.ReadLine();
            AlterarPromoção(id, dataInicio, dataFim, desc);
        }

        private static void AlterarPromoção(int num, String dataInicio, String dataFim, String desc)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdatePromocoes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                        SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                        SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                        SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                        id.Value = num;
                        dataI.Value = dataInicio == "" ? null : dataInicio;
                        dataF.Value = dataFim == "" ? null : dataFim;
                        descr.Value = desc == "" ? null : desc;

                        cmd.Parameters.Add(id);
                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        //cmd.CommandText = "declare @id int; exec InsertPromocoes @DataInicio,@DataFim,@Descricao,@id output; select @id;";

                        int i = cmd.ExecuteNonQuery();
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
