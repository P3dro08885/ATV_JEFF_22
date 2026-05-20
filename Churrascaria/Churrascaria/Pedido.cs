using System;
using System.Collections.Generic;

namespace Churrascaria
{
    public class Pedido
    {
        public int Numero { get; set; }
        public string NomeMesa { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public string Status { get; set; } // "Aberto" ou "Encerrado"

        public Pedido(int numero, string nomeMesa)
        {
            Numero = numero;
            NomeMesa = nomeMesa;
            Itens = new List<ItemPedido>();
            Status = "Aberto";
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
            Console.WriteLine($"Item adicionado: {item.Produto.Nome}{item.Quantidade}");
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
                total += item.Subtotal;
            return total;
        }

        public void ExibirPedido()
        {
            Console.WriteLine($"\n===== Pedido #{Numero} - Mesa: {NomeMesa} ({Status}) =====");
            if (Itens.Count == 0)
            {
                Console.WriteLine("  Nenhum item no pedido.");
            }
            else
            {
                foreach (var item in Itens)
                    item.ExibirItem();
                Console.WriteLine($"  TOTAL: R$ {CalcularTotal():F2}");
            }
            Console.WriteLine("=".PadRight(46, '='));
        }
    }
}