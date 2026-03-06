using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidaBandeiras.Service
{
    public interface ICartaoService
    {
        /// <summary>
        /// Recupera 4 primeiros digitos do numero do cartao
        /// </summary>
        /// <param name="numeroCartao"></param>
        /// <returns>String contendo os 4 primeiros dígitos</returns>
        public string RecuperaPrimeirosDigitos(string numeroCartao);

        //Valida a bandeira do cartão com base nos 4 primeiros dígitos
        public string ValidaBandeira(string numeroCartao);
    }

    public class CartaoService : ICartaoService
    {
        public string RecuperaPrimeirosDigitos(string numeroCartao)
        {
            if (string.IsNullOrEmpty(numeroCartao) || numeroCartao.Length < 4)
            {
                throw new ArgumentException("Número do cartão inválido. Deve conter pelo menos 4 dígitos.");
            }

            return numeroCartao.Substring(0, 4);
        }

        public string ValidaBandeira(string numeroCartao)
        {
            string primeirosDigitos = RecuperaPrimeirosDigitos(numeroCartao);
            if (!int.TryParse(primeirosDigitos, out int bin))
            {
                throw new ArgumentException("Número do cartão contém caracteres inválidos.");
            }

            // Visa: 4000-4999
            if (bin >= 4000 && bin <= 4999)
                return "Visa";

            // Mastercard: 5100-5599 or 2221-2720
            if ((bin >= 5100 && bin <= 5599) || (bin >= 2221 && bin <= 2720))
                return "Mastercard";

            // American Express: 3400-3499 or 3700-3799
            if ((bin >= 3400 && bin <= 3499) || (bin >= 3700 && bin <= 3799))
                return "American Express";

            // Discover: 6011, 65xx
            if (primeirosDigitos.StartsWith("6011") || primeirosDigitos.StartsWith("65"))
                return "Discover";

            // Diners Club: 36xx or 38xx
            if (primeirosDigitos.StartsWith("36") || primeirosDigitos.StartsWith("38"))
                return "Diners Club";

            // JCB: 35xx
            if (primeirosDigitos.StartsWith("35"))
                return "JCB";

            return "Bandeira Desconhecida";
        }
    }
}

