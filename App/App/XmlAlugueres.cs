using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class XmlAlugueres
    {
        static Handler handler;
        public static void GetParamsFromConsole(Handler h)
        {
            if (handler == null) handler = h;
            Console.WriteLine("***********************************************************************");
            Console.Write("Data de Inicio (AAAA-MM-DD):");
            String dataInicio = Console.ReadLine();
            Console.Write("Data de Fim (AAAA-MM-DD):");
            String dataFim = Console.ReadLine();
            XmlAluguer(dataInicio,dataFim);
        }

        private static void XmlAluguer(String dataInicio,String dataFim)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select NumAluguer,CodCli,Tipo,CodEquip from aluguer \n" +
                                                            "inner join AluguerEquipamentos\n" +
                                                            "on NumAluguer = Num\n" +
                                                            "inner join Equipamentos\n" +
                                                            "on CodEquip = Codigo\n" +
                                                            "where DataInicio > @DtI and DataFim < @DtF", con))
                    {

                        SqlParameter DtI = new SqlParameter("@DtI", SqlDbType.Date);
                        DtI.Value = dataInicio;
                        cmd.Parameters.Add(DtI);

                        SqlParameter DtF = new SqlParameter("@DtF", SqlDbType.Date);
                        DtF.Value = dataFim;
                        cmd.Parameters.Add(DtF);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            String xml = String.Format("<xml>\n<alugueres dataInicio=\"{0}\" dataFim=\"{1}\">", dataInicio, dataFim);
                            while (dr.Read())
                            {
                                xml = createXmlAluguer(dr, xml);

                            }

                            xml += "</alugueres> \n </xml > ";
                            Console.WriteLine(xml);
                            Console.WriteLine("***********************************************************************");
                        }
                    }

                }
                catch (DbException ex)
                {
                    Console.WriteLine("E R R O : " + ex.Message);
                    Console.WriteLine("***********************************************************************");
                }

            }
        }

        private static String createXmlAluguer(SqlDataReader dr, String xml)
        {
            return xml += String.Format("<aluguer id=\"{0}\" tipo=\"{1}\"> \n" +
                   "<cliente>{2}</cliente>\n" +
                   "<equipamento>{3}</equipamento>\n" +
                   "</aluguer>\n", dr["NumAluguer"], dr["Tipo"], dr["CodCli"], dr["CodEquip"]);
        }
    }
}
