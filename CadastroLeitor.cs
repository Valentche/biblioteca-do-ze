using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca;

namespace Cadastro
{
    public class CadastroLeitor
    {
        public void CadastrarLeitor(List<Leitor> leitores)
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

        public void ListarTodosLeitores(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- LISTA DE LEITORES E LIVROS ---");

            if (leitores.Count == 0)
            {
                Console.WriteLine("Nenhum leitor cadastrado.");
                return;
            }

            foreach (Leitor leitor in leitores)
            {
                Console.WriteLine($"\n> CPF: {leitor.cpf} | Nome: {leitor.nome} | Idade: {leitor.idade}");
                Console.WriteLine("  Livros:");

                if (leitor.LivrosLeitor.Count == 0)
                {
                    Console.WriteLine("    [Nenhum livro cadastrado]");
                }
                else
                {
                    foreach (Livro livro in leitor.LivrosLeitor)
                    {
                        Console.WriteLine($"    - Título: {livro.Titulo} | Autor: {livro.Autor}");
                    }
                }
            }
        }

        public void ListarLeitorEspecifico(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- LISTAR LEITOR ESPECÍFICO ---");
            Console.Write("Digite o CPF do Leitor que deseja buscar: ");
            string cpfBusca = Console.ReadLine();

            // Busca pelo Leitor com Expressão Lambda (LINQ) exigida no laboratório
            Leitor leitorEncontrado = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitorEncontrado != null)
            {
                Console.WriteLine($"\n> CPF: {leitorEncontrado.cpf} | Nome: {leitorEncontrado.nome} | Idade: {leitorEncontrado.idade}");
                Console.WriteLine("  Livros:");
                foreach (Livro livro in leitorEncontrado.LivrosLeitor)
                {
                    Console.WriteLine($"    - Título: {livro.Titulo} | Autor: {livro.Autor}");
                }
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }

        public void EditarLeitor(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR LEITOR ---");
            Console.Write("Digite o CPF do Leitor: ");
            string cpfBusca = Console.ReadLine();

            // Utilizamos o LINQ (FirstOrDefault) para encontrar o primeiro leitor que bate com o CPF digitado
            Leitor leitor = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitor != null)
            {
                Console.WriteLine($"Editando leitor atual: {leitor.nome}");

                Console.Write("Novo Nome: ");
                leitor.nome = Console.ReadLine();

                Console.Write("Nova Idade: ");
                if (int.TryParse(Console.ReadLine(), out int novaIdade))
                {
                    leitor.idade = novaIdade;
                    Console.WriteLine("Leitor atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Idade inválida. Edição cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }

        public void ExcluirLeitor(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EXCLUIR LEITOR ---");
            Console.Write("Digite o CPF do Leitor a ser excluído: ");
            string cpfBusca = Console.ReadLine();

            Leitor leitor = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitor != null)
            {
                leitores.Remove(leitor);
                Console.WriteLine("Leitor excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }
    }
}