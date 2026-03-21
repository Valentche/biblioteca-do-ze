using Cadastro;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criando a Lista global de Leitores (nossa "Base de dados" na memória)
            List<Leitor> leitores = new List<Leitor>();

            bool executando = true;

            while (executando)
            {
                // Limpa o console para facilitar a visualização (opcional)
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("        SISTEMA BIBLIOTECA DO ZÉ         ");
                Console.WriteLine("========================================");
                Console.WriteLine("1 - Cadastrar Leitor");
                Console.WriteLine("2 - Listar todos os Leitores (e livros)");
                Console.WriteLine("3 - Listar um Leitor específico");
                Console.WriteLine("4 - Editar Leitor");
                Console.WriteLine("5 - Excluir Leitor");
                Console.WriteLine("6 - Cadastrar Livro para um Leitor");
                Console.WriteLine("7 - Editar um Livro de um Leitor");
                Console.WriteLine("8 - Remover Livro (perda)");
                Console.WriteLine("9 - Doar Livro para outro Leitor");
                Console.WriteLine("10 - Pesquisar Leitor por Título de Livro");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("========================================");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        cadastroLeitor.CadastrarLeitor(leitores);
                        break;
                    case "2":
                        ListarTodosLeitores(leitores);
                        break;
                    case "3":
                        ListarLeitorEspecifico(leitores);
                        break;
                    case "4":
                        EditarLeitor(leitores);
                        break;
                    case "5":
                        ExcluirLeitor(leitores);
                        break;
                    case "6":
                        cadastroLivro.CadastrarLivro(leitores);
                        break;
                    case "7":
                        cadastroLivro.EditarLivro(leitores);
                        break;
                    case "8":
                        cadastroLivro.RemoverLivro(leitores);
                        break;
                    case "9":
                        cadastroLivro.DoarLivro(leitores);
                        break;
                    case "10":
                        cadastroLivro.PesquisarLivro(leitores);
                        break;
                    case "0":
                        executando = false;
                        Console.WriteLine("Saindo do sistema...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }

                if (executando)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // ==========================================
        //  MÉTODOS DAS FUNCIONALIDADES (CRUD, ETC)
        // ==========================================


        static CadastroLeitor cadastroLeitor = new CadastroLeitor();
        static CadastroLivro cadastroLivro = new CadastroLivro();

        static void ListarTodosLeitores(List<Leitor> leitores)
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

        static void ListarLeitorEspecifico(List<Leitor> leitores)
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

        static void EditarLeitor(List<Leitor> leitores)
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

        static void ExcluirLeitor(List<Leitor> leitores)
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