using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = @"Data Source=.;Initial Catalog=SI2_VLAB;Integrated Security=True";
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "select value from DEMO";
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                Console.Write(dr["value"] + "\n");
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
