using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca;

namespace Cadastro
{
    public class CadastroLeitor
    {
        private List<Leitor> leitores = new List<Leitor>();

        public void CadastrarLeitor()
        {
            Console.Clear();
            Console.WriteLine("--- CADASTRAR LEITOR ---");
            Console.Write("Digite o CPF do Leitor (apenas números): ");
            string cpf = Console.ReadLine();

            // Validação usando LINQ: Verifica se o cpf digitado já existe na lista
            // Isso cobre a exigência "validar se o CPF informado já não esta em uso".
            // .Any() retorna true se encontrar alguém com a mesma condição.
            if (leitores.Any(l => l.cpf == cpf))
            {
                Console.WriteLine("ERRO: Já existe um leitor cadastrado com este CPF.");
                return; // O return interrompe o fluxo e volta pro menu
            }

            Console.Write("Digite o Nome do Leitor: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a Idade do Leitor: ");
            if (!int.TryParse(Console.ReadLine(), out int idade))
            {
                Console.WriteLine("ERRO: Idade inválida.");
                return;
            }

            // Utilizamos o construtor com parâmetros (sobrecarga) que acabamos de criar!
            Leitor novoLeitor = new Leitor(nome, idade, cpf);
            leitores.Add(novoLeitor);

            Console.WriteLine("Leitor cadastrado com sucesso!");
        }
    }
}