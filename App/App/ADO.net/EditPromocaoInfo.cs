using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class EditTempoExtraInfo
    {
        private static Handler handler;

        public static void RemoverTempoExtra(Handler h)
        {
            if (handler == null) handler = h;
            ShowDescontos();
            Console.Write("Id da Promoção a Remover:");
            int id = Convert.ToInt32(Console.ReadLine());
            RemoverTempoExtra(id);
        }

        private static void ShowDescontos()
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select Promocoes.Id as Id, Promocoes.Descricao as Descr, Promocoes.DataInicio as DI, Promocoes.DataFim as DF, TempoExtra.TempoExtra as TE " +
                                           "from TempoExtra " +
	                                       "inner join Promocoes " +
	                                       "on TempoExtra.Id = Promocoes.Id ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                if (!dr["Id"].Equals(0))
                                    Console.Write("-ID:" + dr["Id"] + "\t Tempo Extra:" + dr["TE"] + "\t Descr:" + dr["Descr"] + "\t Data Inicio:" + dr["DI"] + "\t Data Fim:" + dr["DF"] + "\n");
                        }

                    }

                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);

                }
            }
        }

        private static void RemoverTempoExtra(int id)
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

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Removido com sucesso");
                        Console.WriteLine("***********************************************************************");
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.WriteLine("***********************************************************************\n");
                }
            }
        }

        public static void InserirTempoExtra(Handler h)
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
            Console.Write("***********************************************************************\n");
            InserirTempoExtra(dataInicio, dataFim, desc, tempo);
        }

        private static void InserirTempoExtra(String dataInicio, String dataFim, String desc, int temp)
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
                        SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                        id.Direction = ParameterDirection.Output;
                        
                        dataI.Value = dataInicio;
                        dataF.Value = dataFim;
                        descr.Value = desc;
                        tempo.Value = temp;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(descr);
                        cmd.Parameters.Add(tempo);
                        cmd.Parameters.Add(id);

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

        public static void AlterarTempoExtra(Handler h)
        {
            if (handler == null) handler = h;
            ShowDescontos();
            Console.Write("Id da Promoção a alterar (obrigatório):");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de Inicio (AAAA-MM-DD) (opcional):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD) (opcional):");
            String dataFim = Console.ReadLine();
            Console.Write("Descrição (max 200 caracteres) (opcional):");
            String desc = Console.ReadLine();
            Console.Write("Valor da percentagem (opcional):");
            string s = Console.ReadLine();
            int tempo = s == "" ? -1 : Convert.ToInt32(s);
            Console.Write("***********************************************************************\n");
            AlterarTempoExtra(id, dataInicio, dataFim, desc, tempo);
        }

        private static void AlterarTempoExtra(int num, String dataInicio, String dataFim, String desc, int t)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdatePromocoesTempo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter id = new SqlParameter("@Id", SqlDbType.Int);
                        id.Value = num;
                        cmd.Parameters.Add(id);

                        if (dataInicio != "")
                        {
                            SqlParameter dataI = new SqlParameter("@DataInicio", SqlDbType.Date);
                            dataI.Value = dataInicio;
                            cmd.Parameters.Add(dataI);
                        }
                        if (dataFim != "")
                        {
                            SqlParameter dataF = new SqlParameter("@DataFim", SqlDbType.Date);
                            dataF.Value = dataFim;
                            cmd.Parameters.Add(dataF);
                        }
                        if (dataInicio != "")
                        {
                            SqlParameter descr = new SqlParameter("@Descricao", SqlDbType.VarChar, 200);
                            descr.Value = desc;
                            cmd.Parameters.Add(descr);

                        }
                        if (t > 0)
                        {
                            SqlParameter tempo = new SqlParameter("@Tempo", SqlDbType.Int);
                            tempo.Value = t;
                            cmd.Parameters.Add(tempo);
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
