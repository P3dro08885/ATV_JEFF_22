using System;

namespace Churrascaria
{
    public class ItemPedido
    {
        // ItemPedido TEM um Produto (associação), não É um Produto (herança)
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;
        }

        public void ExibirItem()
        {
            Console.WriteLine($"  - {Produto.Nome} x{Quantidade} = R$ {Subtotal:F2}");
        }
    }
}