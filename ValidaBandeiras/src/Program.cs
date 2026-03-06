using System;
using ValidaBandeiras.Entities;
using ValidaBandeiras.Service;

namespace ValidaBandeiras
{
    class Program
    {
        static void Main(string[] args)
        {
            ICartaoService cartaoService = new CartaoService();

            Console.WriteLine("=== Validador de Bandeiras de Cartão ===");
            Console.Write("Digite o número do cartão: ");

            string numero = Console.ReadLine()?.Trim() ?? "";

            Cartao cartao = new Cartao
            {
                Numero = numero
            };

            try
            {
                string primeirosDigitos = cartaoService.RecuperaPrimeirosDigitos(cartao.Numero);
                string bandeira = cartaoService.ValidaBandeira(cartao.Numero);

                cartao.Bandeira = bandeira;

                Console.WriteLine("\nResultado:");
                Console.WriteLine($"Número: {cartao.Numero}");
                Console.WriteLine($"Primeiros dígitos: {primeirosDigitos}");
                Console.WriteLine($"Bandeira: {cartao.Bandeira}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}