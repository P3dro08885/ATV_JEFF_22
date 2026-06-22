using System;
using System.Collections.Generic;

namespace Churrascaria
{
    internal class Program
    {
        static List<Produto> produtos = new List<Produto>();
        static List<Pedido> pedidos = new List<Pedido>();
        static int proximoNumeroPedido = 1;

        static void Main(string[] args)
        {
            produtos.Add(new Produto(1, "Picanha", 89.90m, "Carnes"));
            produtos.Add(new Produto(2, "Costela Bovina", 69.90m, "Carnes"));
            produtos.Add(new Produto(3, "Frango Inteiro", 49.90m, "Carnes"));
            produtos.Add(new Produto(4, "Linguiça", 29.90m, "Carnes"));
            produtos.Add(new Produto(5, "Pão de Alho", 12.00m, "Acompanhamento"));
            produtos.Add(new Produto(6, "Farofa", 9.00m, "Acompanhamento"));
            produtos.Add(new Produto(7, "Refrigerante", 8.00m, "Bebida"));
            produtos.Add(new Produto(8, "Suco Natural", 10.00m, "Bebida"));

            bool rodando = true;
            while (rodando)
            {
                ExibirMenu();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": NovoPedido(); break;
                    case "2": AdicionarItem(); break;
                    case "3": ListarPedidos(); break;
                    case "4": EncerrarPedido(); break;
                    case "5": ListarCardapio(); break;
                    case "Q":
                    case "q":
                        rodando = false;
                        Console.WriteLine("\nSistema encerrado. Até logo!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void ExibirMenu()
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("====CHURRASCARIA====");
            Console.WriteLine("[1] Novo Pedido ");
            Console.WriteLine("[2] Adicionar Item ao Pedido");
            Console.WriteLine("[3] Listar Pedidos");
            Console.WriteLine("[4] Encerrar Pedido");
            Console.WriteLine("[5] Ver Cardápio ");
            Console.WriteLine("[Q] Sair ");
            Console.WriteLine("\n=============================");
            Console.Write("Escolha uma opção: ");
        }

        static void NovoPedido()
        {
            Console.Write("Nome da mesa ou cliente: ");
            string nome = Console.ReadLine();

            Pedido pedido = new Pedido(proximoNumeroPedido++, nome);
            pedidos.Add(pedido);
            Console.WriteLine($"Pedido #{pedido.Numero} aberto para '{nome}'!");
        }

        static void AdicionarItem()
        {
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido aberto. Crie um pedido primeiro.");
                return;
            }

            ListarPedidosAbertos();
            Console.Write("Número do pedido: ");
            if (!int.TryParse(Console.ReadLine(), out int numPedido))
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            Pedido pedido = pedidos.Find(p => p.Numero == numPedido && p.Status == "Aberto");
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado ou já encerrado.");
                return;
            }

            ListarCardapio();
            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int idProduto))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Produto produto = produtos.Find(p => p.Id == idProduto);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return;
            }

            Console.Write("Quantidade: ");
            if (!int.TryParse(Console.ReadLine(), out int qtd) || qtd <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            ItemPedido item = new ItemPedido(produto, qtd);
            pedido.AdicionarItem(item);
        }

        static void ListarPedidos()
        {
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido registrado.");
                return;
            }
            foreach (var p in pedidos)
                p.ExibirPedido();
        }

        static void EncerrarPedido()
        {
            ListarPedidosAbertos();
            if (pedidos.FindAll(p => p.Status == "Aberto").Count == 0) return;

            Console.Write("Número do pedido para encerrar: ");
            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            Pedido pedido = pedidos.Find(p => p.Numero == num && p.Status == "Aberto");
            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado ou já encerrado.");
                return;
            }

            pedido.ExibirPedido();
            pedido.Status = "Encerrado";
            Console.WriteLine($"Pedido #{pedido.Numero} encerrado. Total cobrado: R$ {pedido.CalcularTotal():F2}");
        }

        static void ListarCardapio()
        {
            Console.WriteLine("\n--- CARDÁPIO ---");
            foreach (var p in produtos)
                p.ExibirInfo();
            Console.WriteLine("----------------");
        }

        static void ListarPedidosAbertos()
        {
            var abertos = pedidos.FindAll(p => p.Status == "Aberto");
            if (abertos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido aberto no momento.");
                return;
            }
            Console.WriteLine("\nPedidos abertos:");
            foreach (var p in abertos)
                Console.WriteLine($"  #{p.Numero} - Mesa: {p.NomeMesa}");
        }
    }
}
