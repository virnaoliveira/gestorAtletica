using System;
using Npgsql;

namespace MembrosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=database-1.cqgole4d5zz1.us-east-1.rds.amazonaws.com;Port=5432;Database=postgres;Username=postgres;Password=admin123;Timeout=10;";

            using (var connection = new NpgsqlConnection(connectionString))
            {

                connection.Open();

                Console.WriteLine("Escolha uma operação:");
                Console.WriteLine("1. Incluir Plano");
                Console.WriteLine("2. Visualizar Lista de Planos");

                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o nome do plano: ");
                        string plano = Console.ReadLine();
                        Console.Write("Digite as vantagens do plano: ");
                        string vantagens = Console.ReadLine();
                        Console.Write("Digite a duração do plano em meses: ");
                        int duracao = int.Parse(Console.ReadLine());
                        Console.Write("Digite o preço do plano: (Ex: 49,99)");
                        decimal preco = decimal.Parse(Console.ReadLine());
                        IncluirPlano(connection, plano, vantagens, duracao, preco);
                        break;
                    case 2:
                        VisualizarPlanos(connection);
                        break;
                    default:
                        Console.WriteLine("Escolha inválida!");
                        break;
                }
            }
        }

        static void IncluirPlano(NpgsqlConnection connection, string plano, string vantagens, int duracao, decimal preco)
        {
            using (var command = new NpgsqlCommand("INSERT INTO atleticagestor.plano(nome, vantagens, duracao, preco) VALUES (@plano, @vantagens, @duracao, @preco)", connection))
            {
                command.Parameters.AddWithValue("plano", plano);
                command.Parameters.AddWithValue("vantagens", vantagens);
                command.Parameters.AddWithValue("duracao", duracao);
                command.Parameters.AddWithValue("preco", preco);
                command.ExecuteNonQuery();
                Console.WriteLine("Plano incluído com sucesso!");
            }
        }

        static void VisualizarPlanos(NpgsqlConnection connection)
        {
            using (var command = new NpgsqlCommand("SELECT * FROM atleticagestor.plano", connection))
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("Ainda não há planos cadastrados.");
                    return;
                } else
                {
                    Console.WriteLine("Lista de Planos:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Plano: {reader["nome"]}, Vantagens: {reader["vantagens"]}, Duração: {reader["duracao"]}, Preço: {reader["preco"]}");    
                    }
                }
            }
        }
    }
}
