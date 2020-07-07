using ProjetoEntrevista2_1.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEntrevista2_1.Repositories
{
    public class UsuarioPfRepository
    {

        private string StringConexao = "Data Source=LAB08DESK2301\\SQLEXPRESS; initial catalog=Projeto; user id=sa; pwd=sa@132;";

        // declaracao do metodo que preciso criar
        public List<UsuarioPFDomains> Listar()
        {

            List<UsuarioPFDomains> UsuariosPF = new List<UsuarioPFDomains>();
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM UsuarioPF";

                //Abre o banco de dados
                con.Open();

                //Declaro um SqlDataReader para percorrer a lista
                SqlDataReader rdr;

                //Declaro um command passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryaSerExecutada, con))
                {
                    //Executa a query
                    rdr = cmd.ExecuteReader();

                    //Percorre os dados 
                    while (rdr.Read())
                    {
                        UsuarioPFDomains UsuarioPF = new UsuarioPFDomains
                        {
                            IdUsuarioPf = Convert.ToInt32(rdr["IdUsuarioPF"]),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            IdTipo = Convert.ToInt32(rdr["IdTipo"].ToString()),
                            NumeroCpf = Convert.ToInt32(rdr["NumeroCpf"]),
                            Telefone = Convert.ToInt32(rdr["Telefone"])
                        };
                        UsuariosPF.Add(UsuarioPF);
                    }
                }
            }
            return UsuariosPF;
        }
        public UsuarioPFDomains BuscarPorCpf(int cpf)
        {
            // string QuerySelect = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionario = @IdFuncionario";
            string QuerySelect = "SELECT * FROM UsuarioPF WHERE NumeroCpf = @NumeroCpf";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroCpf", cpf);
                    sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            UsuarioPFDomains UsuarioPF = new UsuarioPFDomains
                            {
                                IdUsuarioPf = Convert.ToInt32(sdr["IdUsuarioPF"]),
                                NomeUsuario = sdr["NomeUsuario"].ToString(),
                                IdTipo = Convert.ToInt32(sdr["IdTipo"].ToString()),
                                NumeroCpf = Convert.ToInt32(sdr["NumeroCpf"]),
                                Telefone = Convert.ToInt32(sdr["Telefone"])
                            };
                            return UsuarioPF;
                        }
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(UsuarioPFDomains UsuarioPFDomains)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor como parametro
                // string QueryASerExecutada = "INSERT INTO Funcionarios (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";
                string QueryASerExecutada = "INSERT INTO UsuarioPF (NomeUsuario, IdTipo, NumeroCpf, Telefone)" +
                                            "VALUES(@NomeUsuario, @IdTipo, @NumeroCpf, @Telefone)";
                //Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryASerExecutada, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", UsuarioPFDomains.NomeUsuario);
                cmd.Parameters.AddWithValue("@NumeroCpf", UsuarioPFDomains.NumeroCpf);
                cmd.Parameters.AddWithValue("@IdTipo", UsuarioPFDomains.IdTipo);
                cmd.Parameters.AddWithValue("@Telefone", UsuarioPFDomains.Telefone);
                //abre a conexão
                con.Open();
                //Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(UsuarioPFDomains UsuarioPF)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // string Query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @IdFuncionario";
                string Query = "UPDATE UsuarioPF SET NomeUsuario = @NomeUsuario, IdTipo = @IdTipo, NumeroCpf = @NumeroCpf, Telefone = @Telefone where IdUsuarioPf = @IdUsuarioPf";

                SqlCommand cmd = new SqlCommand(Query, con);
                //Passa o valor do parametro
                cmd.Parameters.AddWithValue("@NomeUsuario", UsuarioPF.NomeUsuario);
                cmd.Parameters.AddWithValue("@NumeroCpf", UsuarioPF.NumeroCpf);
                cmd.Parameters.AddWithValue("@IdTipo", UsuarioPF.IdTipo);
                cmd.Parameters.AddWithValue("@Telefone", UsuarioPF.Telefone);

            }
        }

        public void Deletar(int cpf)
        {
            string QueryDelete = "DELETE FROM UsuarioPF WHERE NumeroCpf = @NumeroCpf";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QueryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroCpf", cpf);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        /// <summary>
        /// Buscar todos os funcionários que possuam um determinado nome
        /// </summary>
        /// <returns>Lista de Funcionários</returns>
        public List<UsuarioPFDomains> BuscarPorNome(string nomeProcurado)
        {
            List<UsuarioPFDomains> UsuariosPF = new List<UsuarioPFDomains>();

            //Declaro a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Declara a instrução a ser executada
                // string QueryaSerExecutada = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios";
                string QueryaSerExecutada = "SELECT * FROM UsuarioPF WHERE NomeUsuario LIKE '%' + @NomeUsuario + '%'";
                //Abre o banco de dados
                con.Open();
                //Declaro um SqlDataReader para percorrer a lista
                SqlDataReader rdr;
                //Declaro um command passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryaSerExecutada, con))
                {
                    cmd.Parameters.AddWithValue("@NomeProcurado", nomeProcurado);

                    //Executa a query
                    rdr = cmd.ExecuteReader();

                    //Percorre os dados 
                    while (rdr.Read())
                    {
                        UsuarioPFDomains UsuarioPF = new UsuarioPFDomains
                        {
                            IdUsuarioPf = Convert.ToInt32(rdr["IdUsuarioPf"]),
                            NomeUsuario = rdr["NomeUsuario"].ToString(),
                            NumeroCpf = Convert.ToInt32(rdr["NumeroCpf"]),
                            Telefone = Convert.ToInt32(rdr["Telefone"]),
                            IdTipo = Convert.ToInt32(rdr["IdTipo"].ToString())
                        };

                        UsuariosPF.Add(UsuarioPF);
                    }
                }
            }

            return UsuariosPF;
        }
    }
}
