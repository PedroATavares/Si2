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
        private static Handler handler;

        public static void GetParamsFromConsole(Handler h)
        {
            if (handler == null) handler = h;
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Insira o Id do Equipamento (obrigatório):");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Insira a Data Inicial  (obrigatório):");
            string dataI = Console.ReadLine();
            Console.WriteLine("Insira a duraçao  (obrigatório):");
            int duration = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Insira a nova Data Final (opcional):");
            string dataF = Console.ReadLine();
            Console.WriteLine("Insira o novo Preço (opcional)");
            string s = Console.ReadLine();
            int price = s == "" ? -1 : Int32.Parse(s);
            Console.WriteLine("***********************************************************************");
            ExecProcedure(dataI, dataF, duration, price, id);
        }

        private static void ExecProcedure(string dataI, string dataF, int duration, int price, int idEquip)
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
                        if (dataF != "")
                        {
                            SqlParameter validadeF = new SqlParameter("@ValidadeF", SqlDbType.Date);
                            validadeF.Value = dataF;
                            cmd.Parameters.Add(validadeF);
                        }
                        SqlParameter duracao = new SqlParameter("@duracao", SqlDbType.Int);
                        duracao.Value = duration;
                        cmd.Parameters.Add(duracao);
                        SqlParameter valor = new SqlParameter("@valor", SqlDbType.SmallMoney);
                        if(price >-1)
                        {
                            valor.Value = price;
                            cmd.Parameters.Add(valor);
                        }
                        SqlParameter equipId = new SqlParameter("@EquipId", SqlDbType.Int);
                        equipId.Value = idEquip;
                        cmd.Parameters.Add(equipId);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alterado com Sucesso");
                        Console.Write("***********************************************************************\n");
                        
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.WriteLine("***********************************************************************");
                }
                   
            }    
        }

    }
}
