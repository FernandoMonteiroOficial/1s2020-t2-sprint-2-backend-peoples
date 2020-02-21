using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string stringConexao = "Data Source=DESKTOP-0KUTKNU\\SQLEXPRESS; initial catalog=T_Peoples; user Id=sa; pwd=sa@132;";

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> list_funcionario = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelecAll = "SELECT * FROM Funcionario";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelecAll, con))
                {
                    con.Open();

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            Id_Funcionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        list_funcionario.Add(funcionario);
                    }
                }
            }

            return list_funcionario;

        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string selectWId = "EXEC listaFuncionarioPorId @ID";

                    con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(selectWId, con))
                {

                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionarioDomain func = new FuncionarioDomain
                        {
                            Id_Funcionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };
                        return func;
                    }
                    return null;
                }
            }
        }

        public string Cadastrar(FuncionarioDomain funcionario)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Funcionario(Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";

                // Validação
                if (funcionario.Nome == null || funcionario.Sobrenome == null)
                {
                    return "Faltou algum campo!";
                }

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(queryInsert, con);
                

                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();        
            }

            return "Funcionario Cadastrado";
        }

        public string AtualizarIdUrl(int id, FuncionarioDomain funcionario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string insert = "EXEC atualizarFuncionario @Nome, @Sobrenome, @ID";

                using (SqlCommand cmd = new SqlCommand(insert, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                    cmd.ExecuteNonQuery();
                }
            }

            return "Atualização Completa!";
        }

        public string Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string delete = "EXEC deletarFuncionario @ID";

                using (SqlCommand cmd = new SqlCommand(delete, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.ExecuteNonQuery();
                }
            }

            return "Funcionário Deletado!";
        }
    }
}