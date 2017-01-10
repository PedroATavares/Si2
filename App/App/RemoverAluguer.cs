using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class RemoverAluguer
    {
        //------------------ REMOVE ALUGUER ----------------------------------------------------------
        static Handler handler;
        public static void procRemoverAluger(Handler h)
        {
            if (handler == null) handler = h;
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("RemoverAluguer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        printAluguer(con);

                        Console.WriteLine("Escolha o Aluguer (id) que deseja eliminar : ");
                        int idAluguerr = Convert.ToInt32(Console.ReadLine());

                        SqlParameter idAluguer = new SqlParameter("@idAluguer", SqlDbType.Int);
                        idAluguer.Value = idAluguerr;
                        cmd.Parameters.Add(idAluguer);

                       // cmd.CommandText = " exec RemoverAluger @idAluguer";

                        int i = cmd.ExecuteNonQuery();

                        Console.WriteLine(i + "tuplo(s) afetado(s)");
                        //printAluguer(cmd);

                        //Console.ReadLine();
                    };
                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.ReadLine();

                }
            }
        }

        private static void printAluguer(SqlConnection con)
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select * from Aluguer";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Console.WriteLine("Estes sao os Alugueres existentes -------------------\nNum | DataInicio  | DataFim   |  Duracao | Nº Empregado | Codigo Cliente ");
                    while (dr.Read())
                        Console.Write(dr["Num"] + " | " + dr["DataInicio"] + " | " + dr["DataFim"] + " |  " + dr["Duracao"] + " |  " + dr["NumEmp"] + " |  " + dr["CodCli"] + "\n");
                }
            }
        }

    }
}
