using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca;

namespace Cadastro
{
    public class CadastroLivro
    {
        public void CadastrarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- CADASTRAR LIVRO ---");
            Console.Write("Digite o CPF do Leitor que receberá o livro: ");
            string cpfBusca = Console.ReadLine();

            Leitor leitorEncontrado = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitorEncontrado != null)
            {
                // Aqui criaremos um livro usando o Construtor Vazio
                Livro novoLivro = new Livro();

                Console.Write("Digite o Título do Livro: ");
                novoLivro.Titulo = Console.ReadLine();

                Console.Write("Digite o Autor do Livro: ");
                novoLivro.Autor = Console.ReadLine();

                Console.Write("Digite o Gênero do Livro: ");
                novoLivro.Genero = Console.ReadLine();

                Console.Write("Digite o Ano de Publicação: ");
                int.TryParse(Console.ReadLine(), out novoLivro.AnoPublicacao); // Caso falhe, ano será automaticamente 0.

                // Embutindo o Livro na lista do Leitor usando o método criado na Aula 03/04
                leitorEncontrado.AdicionarLivro(novoLivro);

                Console.WriteLine("Livro cadastrado com sucesso para o leitor " + leitorEncontrado.nome);
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }

        public void EditarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR LIVRO ---");
            Console.Write("Digite o CPF do Leitor dono do livro: ");
            string cpfBusca = Console.ReadLine();

            Leitor leitor = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitor != null)
            {
                Console.Write("Digite o Título do Livro que deseja editar: ");
                string tituloBusca = Console.ReadLine();

                // Busca o livro DENTRO da lista de livros DO LEITOR
                Livro livro = leitor.LivrosLeitor.FirstOrDefault(lv => lv.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase));

                if (livro != null)
                {
                    Console.Write("Novo Título: ");
                    livro.Titulo = Console.ReadLine();

                    Console.Write("Novo Autor: ");
                    livro.Autor = Console.ReadLine();

                    Console.WriteLine("Livro editado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Livro não encontrado na lista desse leitor.");
                }
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }

        public void RemoverLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- REMOVER LIVRO (PERDA) ---");
            Console.Write("Digite o CPF do Leitor que perdeu o livro: ");
            string cpfBusca = Console.ReadLine();

            Leitor leitor = leitores.FirstOrDefault(l => l.cpf == cpfBusca);

            if (leitor != null)
            {
                Console.Write("Digite o Título do Livro perdido: ");
                string tituloBusca = Console.ReadLine();

                Livro livro = leitor.LivrosLeitor.FirstOrDefault(lv => lv.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase));

                if (livro != null)
                {
                    // Usa o método presente na classe Leitor.cs
                    leitor.RemoverLivro(livro);
                    Console.WriteLine("O livro foi removido com sucesso!");
                }
                else
                {
                    Console.WriteLine("Livro não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Leitor não encontrado.");
            }
        }

        public void DoarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- DOAR LIVRO ---");

            Console.Write("Digite o CPF do Leitor DOADOR: ");
            string cpfDoador = Console.ReadLine();
            Leitor doador = leitores.FirstOrDefault(l => l.cpf == cpfDoador);

            if (doador == null)
            {
                Console.WriteLine("Doador não encontrado.");
                return;
            }

            Console.Write("Digite o CPF do Leitor DESTINATÁRIO: ");
            string cpfDestinatario = Console.ReadLine();
            Leitor destinatario = leitores.FirstOrDefault(l => l.cpf == cpfDestinatario);

            if (destinatario == null)
            {
                Console.WriteLine("Destinatário não encontrado.");
                return;
            }

            Console.Write("Digite o Título do Livro a ser doado: ");
            string tituloBusca = Console.ReadLine();
            Livro livro = doador.LivrosLeitor.FirstOrDefault(lv => lv.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase));

            if (livro != null)
            {
                // Aqui usamos a magia da Orientação a Objetos que vimos nos Slides (Aula 04 - Doar.cs)
                doador.DoarLivro(livro, destinatario);
                Console.WriteLine($"Livro '{livro.Titulo}' doado de {doador.nome} para {destinatario.nome} com sucesso!");
            }
            else
            {
                Console.WriteLine("Livro não encontrado na lista do doador.");
            }
        }

        public void PesquisarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- PESQUISAR LEITOR POR LIVRO ---");
            Console.Write("Digite o Título do Livro para ver quem o possui: ");
            string tituloBusca = Console.ReadLine();

            bool encontrado = false;

            // Navega por todos os leitores e verifica se em sua Lista interna "LivrosLeitor"
            // existe (Any) um livro cujo Título bata com a pesquisa
            foreach (Leitor leitor in leitores)
            {
                if (leitor.LivrosLeitor.Any(lv => lv.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"-> O leitor {leitor.nome} (CPF: {leitor.cpf}) possui este livro em sua estante!");
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Nenhum leitor possui esse livro no momento.");
            }
        }
    }
}