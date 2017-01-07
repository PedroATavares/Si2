using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class AlteracaoPrecario
    {
        static Handler handler;
        public static void ExecProcedure(string dataI, string dataF, int duration, int price, int idEquip)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("alteracoesPrecario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter validadeI = new SqlParameter("@ValidadeI", SqlDbType.Date);
                        validadeI.Value = dataI;
                        cmd.Parameters.Add(validadeI);
                        SqlParameter validadeF = new SqlParameter("@ValidadeF", SqlDbType.Date);
                        validadeF.Value = dataF;
                        cmd.Parameters.Add(validadeF);
                        SqlParameter duracao = new SqlParameter("@duracao", SqlDbType.Int);
                        duracao.Value = duration;
                        cmd.Parameters.Add(duracao);
                        SqlParameter valor = new SqlParameter("@valor", SqlDbType.SmallMoney);
                        valor.Value = price;
                        cmd.Parameters.Add(valor);
                        SqlParameter equipId = new SqlParameter("@EquipId", SqlDbType.Int);
                        equipId.Value = idEquip;
                        cmd.Parameters.Add(equipId);

                        int i = cmd.ExecuteNonQuery();

                        Console.WriteLine(i + "tuplo(s) afetado(s)");
                        Console.WriteLine("***********************************************************************");
                        
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.WriteLine("***********************************************************************");
                }
                   
            }    
        }

       public static void GetParamsFromConsole(Handler h)
        {
            if (handler == null) handler = h;
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Insira o Id do Equipamento");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Insira a Data Inicial");
            string dataI = Console.ReadLine();
            Console.WriteLine("Insira a duraçao");
            int duration = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Insira a nova Data Final, caso seja o que pretenda alterar");
            string dataF = Console.ReadLine();
            Console.WriteLine("Insira o novo Preço, caso seja o que pretenda alterar");
            int price = Int32.Parse(Console.ReadLine());
            ExecProcedure(dataI, dataF, duration,price,id);
        }
    }
}
