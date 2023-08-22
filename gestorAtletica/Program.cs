using System;
using Npgsql;

namespace MembrosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=database-1.cqgole4d5zz1.us-east-1.rds.amazonaws.com;Port=5432;Database=postgres;Username=postgres;Password=admin123;SSL Mode=Prefer;Timeout=10;";

            using (var connection = new NpgsqlConnection(connectionString))
            {

                connection.Open();

                Console.WriteLine("Escolha uma operação:");
                Console.WriteLine("1. Incluir Membro");
                Console.WriteLine("2. Visualizar Lista de Membros");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Digite a matrícula do membro: ");
                        string matricula = Console.ReadLine();
                        Console.Write("Digite o nome do membro: ");
                        string nome = Console.ReadLine();
                        Console.Write("Digite o sobrenome do membro: ");
                        string sobrenome = Console.ReadLine();
                        Console.Write("Digite a data de nascimento do membro (yyyy-MM-dd): ");
                        string dataNascimento = Console.ReadLine();
                        Console.Write("Digite o email do membro: ");
                        string email = Console.ReadLine();
                        Console.Write("Digite o telefone do membro: ");
                        string telefone = Console.ReadLine();
                        Console.Write("Digite o curso do membro: ");
                        string curso = Console.ReadLine();
                        Console.Write("Digite o status do membro: ");
                        string status = Console.ReadLine();
                        Console.Write("Digite o CPF do membro: ");
                        string cpf = Console.ReadLine();
                        Console.Write("Digite a senha do membro: ");
                        string senha = Console.ReadLine();
                        Console.Write("Digite a rua do membro: ");
                        string rua = Console.ReadLine();
                        Console.Write("Digite o bairro do membro: ");
                        string bairro = Console.ReadLine();
                        Console.Write("Digite o número do membro: ");
                        string numero = Console.ReadLine();
                        Console.Write("Digite o complemento do membro: ");
                        string complemento = Console.ReadLine();
                        IncluirMembro(connection, matricula, nome, sobrenome, dataNascimento, email, telefone, curso, status, cpf, senha, rua, bairro, numero, complemento);
                        break;
                    case 2:
                        VisualizarMembros(connection);
                        break;
                    default:
                        Console.WriteLine("Escolha inválida!");
                        break;
                }
            }
        }

        static void IncluirMembro(NpgsqlConnection connection, string matricula, string nome, string sobrenome, string dataNascimento, string email, string telefone, string curso, string status, string cpf, string senha, string rua, string bairro, string numero, string complemento)
        {
            using (var command = new NpgsqlCommand("INSERT INTO atleticagestor.membro(matricula, nome, sobrenome, data_nascimento, email, telefone, curso, status, cpf, senha, rua, bairro, numero, complemento) VALUES (@matricula, @nome, @sobrenome, @dataNascimento, @email, @telefone, @curso, @status, @cpf, @senha, @rua, @bairro, @numero, @complemento)", connection))
            {
                command.Parameters.AddWithValue("matricula", matricula);
                command.Parameters.AddWithValue("nome", nome);
                command.Parameters.AddWithValue("sobrenome", sobrenome);
                command.Parameters.AddWithValue("dataNascimento", DateTime.Parse(dataNascimento));
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("telefone", telefone);
                command.Parameters.AddWithValue("curso", curso);
                command.Parameters.AddWithValue("status", status);
                command.Parameters.AddWithValue("cpf", cpf);
                command.Parameters.AddWithValue("senha", senha);
                command.Parameters.AddWithValue("rua", rua);
                command.Parameters.AddWithValue("bairro", bairro);
                command.Parameters.AddWithValue("numero", numero);
                command.Parameters.AddWithValue("complemento", complemento);
                command.ExecuteNonQuery();
                Console.WriteLine("Membro incluído com sucesso!");
            }
        }

        static void VisualizarMembros(NpgsqlConnection connection)
        {
            using (var command = new NpgsqlCommand("SELECT * FROM atleticagestor.membro", connection))
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de Membros:");
                while (reader.Read())
                {
                    Console.WriteLine($"Matrícula: {reader["matricula"]}, Nome: {reader["nome"]}, Sobrenome: {reader["sobrenome"]}");
                }
            }
        }
    }
}
