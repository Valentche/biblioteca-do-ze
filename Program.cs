using Cadastro;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criando a Lista global de Leitores (nossa "Base de dados" na memória)
            List<Leitor> leitores = new List<Leitor>();
            
            CadastroLeitor cadastroLeitor = new CadastroLeitor(leitores);

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
                        cadastroLeitor.CadastrarLeitor();
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
                        CadastrarLivro(leitores);
                        break;
                    case "7":
                        EditarLivro(leitores);
                        break;
                    case "8":
                        RemoverLivro(leitores);
                        break;
                    case "9":
                        DoarLivro(leitores);
                        break;
                    case "10":
                        PesquisarLivro(leitores);
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

        static void CadastrarLivro(List<Leitor> leitores)
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

        static void EditarLivro(List<Leitor> leitores)
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

        static void RemoverLivro(List<Leitor> leitores)
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

        static void DoarLivro(List<Leitor> leitores)
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

        static void PesquisarLivro(List<Leitor> leitores)
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