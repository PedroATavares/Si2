using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace App
{
    class EntitiesUtils
    {
        public static void PrintClientes(SqlConnection con)
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "select * from Cliente";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    Console.WriteLine(
                        "Estes sao os Clientes existentes -------------------\nCODIGO|  NIF   |     NOME   |      MORADA");
                    while (dr.Read())
                        if (!dr["nif"].Equals(0))
                            Console.Write(dr["codigo"] + " | " + dr["nif"] + " | " + dr["nome"] + " |  " + dr["morada"] +
                                          "\n");
                }
            }
        }

        public static void PrintPromocoesLivres(string dI, string dF, Handler handler)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("ShowPromocoes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
                        SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
                        dataI.Value = dI;
                        dataF.Value = dF;
                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);

                        Console.WriteLine("Promoçoes disponiveis entre " + dataI.Value + " e " +
                                          dataF.Value+ ":");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (!dr["Id"].Equals(0))
                                {
                                    Console.Write("-ID:" + dr["Id"] + "\t Descrição:" + dr["Descricao"] + "\t Data Inicio:" +
                                                  dr["DataInicio"] + "\t Data Fim:" + dr["DataFim"] + "\n");
                                }
                            }

                        }
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }

        }


        public static void ShowDescontos(Handler handler)
        {

            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            "select Promocoes.Id as Id, Promocoes.Descricao as Descr, Promocoes.DataInicio as DI, Promocoes.DataFim as DF, Descontos.Percentagem as perc " +
                            "from Descontos " +
                            "inner join Promocoes " +
                            "on Descontos.Id = Promocoes.Id ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                if (!dr["Id"].Equals(0))
                                    Console.Write("-ID:" + dr["Id"] + "\t Percentagem:" + dr["perc"] + "\t Descrição:" +
                                                  dr["Descr"] + "\t Data Inicio:" + dr["DI"] + "\t Data Fim:" + dr["DF"] +
                                                  "\n");
                        }

                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
        }

        public static void ShowTempoExtra(Handler handler)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            "select Promocoes.Id as Id, Promocoes.Descricao as Descr, Promocoes.DataInicio as DI, Promocoes.DataFim as DF, TempoExtra.TempoExtra as TE " +
                            "from TempoExtra " +
                            "inner join Promocoes " +
                            "on TempoExtra.Id = Promocoes.Id ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                                if (!dr["Id"].Equals(0))
                                    Console.Write("-ID:" + dr["Id"] + "\t Tempo Extra:" + dr["TE"] + "\t Descr:" +
                                                  dr["Descr"] + "\t Data Inicio:" + dr["DI"] + "\t Data Fim:" + dr["DF"] +
                                                  "\n");
                        }

                    }

                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);

                }
            }
        }

        public static void PrintEquipamentosLivres(string dI, string dF, Handler handler)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("ShowEquipamentos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
                        SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
                        dataI.Value = dI;
                        dataF.Value = dF;
                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);

                        Console.WriteLine("Equipamentos disponiveis entre " + dataI.Value + " e " +
                                          dataF.Value + ":");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (!dr["Codigo"].Equals(0))
                                    Console.Write("-Código:" + dr["Codigo"] + "\t Descrição:" + dr["Descricao"] +
                                                  "\t Tipo:" +
                                                  dr["Tipo"] + "\n");
                            }
                        }
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
        }


        public static void PrintPrecarioParaEquipamento(string dI, string dF, int idEq, Handler handler)
        {
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("EquipamentosEspecificos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
                        SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
                        SqlParameter id = new SqlParameter("@CodEquip", SqlDbType.Int);
                        dataI.Value = dI;
                        dataF.Value = dF;
                        id.Value = idEq;
                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(id);

                        Console.WriteLine("Preços disponiveis para esse Equipamento:");
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if (!dr["Codigo"].Equals(0))
                                    Console.Write("-Código:" + dr["Codigo"] + "\tDescrição:" + dr["Descricao"] +
                                                  "\tTipo:" + dr["Tipo"] + "\tValidade Inicial:" + dr["ValidadeI"] +
                                                  "\tValidade Final:" + dr["ValidadeF"] + "\tDuração:" + dr["Duracao"] +
                                                  "\tValor" + dr["Valor"] + "\n");
                            }

                        }
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
        }

        public static float PrintAndGetValor(string dI, string dF, int idEq, int dur, Handler handler)
        {
            float valor = 0;
            using (SqlConnection con = new SqlConnection())
            {
                try
                {
                    con.ConnectionString = handler.CONNECTION_STRING;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("BuscarPrecoEspecifico", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter dataI = new SqlParameter("@DataI", SqlDbType.DateTime);
                        SqlParameter dataF = new SqlParameter("@DataF", SqlDbType.DateTime);
                        SqlParameter id = new SqlParameter("@CodEquip", SqlDbType.Int);
                        SqlParameter duracao = new SqlParameter("@Duracao", SqlDbType.Int);

                        dataI.Value = dI;
                        dataF.Value = dF;
                        id.Value = idEq;
                        duracao.Value = dur;

                        cmd.Parameters.Add(dataI);
                        cmd.Parameters.Add(dataF);
                        cmd.Parameters.Add(id);
                        cmd.Parameters.Add(duracao);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                valor = float.Parse(dr["Valor"].ToString());
                                Console.WriteLine("Valor escolhido: "+valor);
                            }

                        }
                    }
                }
                catch (DbException e)
                {
                    Console.WriteLine("E R R O" + e.Message);
                }
            }
            return valor;
        }
    }



}
