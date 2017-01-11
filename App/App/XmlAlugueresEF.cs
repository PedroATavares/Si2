using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.EF;

class MyRow
{
    public int NumAluguer { get; set; }
    public int CodCli { get; set; }
    public String Tipo { get; set; }
    public int CodEquip { get; set; }
}

namespace App
{
    class XmlAlugueresEF
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
            XmlAluguerEF(dataInicio,dataFim);
        }

        private static void XmlAluguerEF(String dataInicio,String dataFim)
        {

            using (var context = new TestesSI2Entities())
            {
                var blogs = context.Database.SqlQuery<MyRow>( "select NumAluguer,CodCli,Tipo,CodEquip from aluguer \n" +
                                                            "inner join AluguerEquipamentos\n" +
                                                            "on NumAluguer = Num\n" +
                                                            "inner join Equipamentos\n" +
                                                            "on CodEquip = Codigo\n" +
                                                            "where DataInicio > @DtI and DataFim < @DtF",new object[]{ new SqlParameter("@DtI", dataInicio), new SqlParameter("@DtF", dataFim)});

                String xml = String.Format("<xml>\n<alugueres dataInicio=\"{0}\" dataFim=\"{1}\">", dataInicio, dataFim);
                foreach (var item in blogs) {
                    xml+=createXmlAluguer(item.NumAluguer,item.Tipo,item.CodCli,item.CodEquip,xml);
                }
                xml += "</alugueres> \n </xml > ";
                Console.WriteLine(xml);
                Console.WriteLine("***********************************************************************");
            }

        }

        private static String createXmlAluguer(int NumAluguer,String Tipo,int CodCli, int CodEquip,String xml)
        {
            return String.Format("<aluguer id=\"{0}\" tipo=\"{1}\"> \n" +
                   "<cliente>{2}</cliente>\n" +
                   "<equipamento>{3}</equipamento>\n" +
                   "</aluguer>\n", NumAluguer, Tipo, CodCli,CodEquip);
        }
    }
}
