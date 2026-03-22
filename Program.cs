using Cadastro;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cria a Lista global de Leitores
            List<Leitor> leitores = new List<Leitor>();

            bool executando = true;

            while (executando)
            {
                // menuzinho bonitinho pra rodar no console
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
                        cadastroLeitor.ListarTodosLeitores(leitores);
                        break;
                    case "3":
                        cadastroLeitor.ListarLeitorEspecifico(leitores);
                        break;
                    case "4":
                        cadastroLeitor.EditarLeitor(leitores);
                        break;
                    case "5":
                        cadastroLeitor.ExcluirLeitor(leitores);
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
        //  Aqui jás os métodos, foram todos pros outros arquivos kk.
        // ==========================================


        static CadastroLeitor cadastroLeitor = new CadastroLeitor();
        static CadastroLivro cadastroLivro = new CadastroLivro();



    }
}