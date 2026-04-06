using Cadastro;

//ALUNOS
//Pablo Valente Neto
//Gabriel Francisco

namespace Biblioteca
{
    internal class Program
    {
        static CadastroLeitor cadastroLeitor = new CadastroLeitor();
        static CadastroLivro cadastroLivro = new CadastroLivro();

        static void Main(string[] args)
        {
            List<Leitor> leitores = new List<Leitor>();

            bool executando = true;

            while (executando)
            {
                ExibirMenu();
                string opcao = Console.ReadLine();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            CadastrarLeitor(leitores);
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
                            Console.WriteLine("Opcao invalida! Tente novamente.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"ERRO: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERRO inesperado: {ex.Message}");
                }

                if (executando && opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        SISTEMA BIBLIOTECA DO ZE         ");
            Console.WriteLine("========================================");
            Console.WriteLine("1 - Cadastrar Leitor");
            Console.WriteLine("2 - Listar todos os Leitores (e livros)");
            Console.WriteLine("3 - Listar um Leitor especifico");
            Console.WriteLine("4 - Editar Leitor");
            Console.WriteLine("5 - Excluir Leitor");
            Console.WriteLine("6 - Cadastrar Livro para um Leitor");
            Console.WriteLine("7 - Editar um Livro de um Leitor");
            Console.WriteLine("8 - Remover Livro (perda)");
            Console.WriteLine("9 - Doar Livro para outro Leitor");
            Console.WriteLine("10 - Pesquisar Leitor por Titulo de Livro");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("========================================");
            Console.Write("Escolha uma opcao: ");
        }

        // ── OPERAÇÕES COM LEITOR ────────────────────────────────────────────

        static void CadastrarLeitor(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- CADASTRAR LEITOR ---");

            Console.Write("Digite o CPF do Leitor (apenas numeros): ");
            string cpf = Console.ReadLine();

            // Validacao na camada de interface: impede que o usuario digitar vários dados
            // se o CPF ja existe
            if (cadastroLeitor.CpfJaExiste(leitores, cpf))
            {
                throw new ArgumentException("Ja existe um leitor cadastrado com este CPF.");
            }

            Console.Write("Digite o Nome do Leitor: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a Idade do Leitor: ");
            if (!int.TryParse(Console.ReadLine(), out int idade))
            {
                throw new ArgumentException("Idade invalida.");
            }

            // A classe CadastroLeitor faz apenas a lógica, lancando exceção se erro
            cadastroLeitor.CadastrarLeitor(leitores, cpf, nome, idade);
            Console.WriteLine("Leitor cadastrado com sucesso!");
        }

        static void ListarTodosLeitores(List<Leitor> leitores)
        {
            string resultado = cadastroLeitor.ListarTodosLeitores(leitores);
            Console.Clear();
            Console.WriteLine(resultado);
        }

        static void ListarLeitorEspecifico(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- LISTAR LEITOR ESPECIFICO ---");
            Console.Write("Digite o CPF do Leitor que deseja buscar: ");
            string cpf = Console.ReadLine();

            string resultado = cadastroLeitor.ListarLeitorEspecifico(leitores, cpf);
            Console.WriteLine(resultado);
        }

        static void EditarLeitor(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR LEITOR ---");
            Console.Write("Digite o CPF do Leitor: ");
            string cpf = Console.ReadLine();

            // Valida se existe antes de pedir os dados
            Leitor leitor = cadastroLeitor.EncontrarLeitor(leitores, cpf);
            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpf}' nao encontrado.");

            Console.WriteLine($"Editando leitor atual: {leitor.Nome}");

            Console.Write("Novo Nome: ");
            string novoNome = Console.ReadLine();

            Console.Write("Nova Idade: ");
            if (!int.TryParse(Console.ReadLine(), out int novaIdade))
            {
                throw new ArgumentException("Idade invalida.");
            }

            cadastroLeitor.EditarLeitor(leitores, cpf, novoNome, novaIdade);
            Console.WriteLine("Leitor atualizado com sucesso!");
        }

        static void ExcluirLeitor(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EXCLUIR LEITOR ---");
            Console.Write("Digite o CPF do Leitor a ser excluido: ");
            string cpf = Console.ReadLine();

            cadastroLeitor.ExcluirLeitor(leitores, cpf);
            Console.WriteLine("Leitor excluido com sucesso!");
        }

        // ── OPERAÇÕES COM LIVRO ─────────────────────────────────────────────

        static void CadastrarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- CADASTRAR LIVRO ---");

            Console.Write("Digite o CPF do Leitor que recebera o livro: ");
            string cpfLeitor = Console.ReadLine();

            // Valida se existe antes de pedir os outros dados
            if (cadastroLeitor.EncontrarLeitor(leitores, cpfLeitor) == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Console.Write("Digite o ISBN do Livro: ");
            string isbn = Console.ReadLine();

            Console.Write("Digite o Titulo do Livro: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o Subtitulo do Livro: ");
            string subtitulo = Console.ReadLine();

            Console.Write("Digite o Escritor do Livro: ");
            string escritor = Console.ReadLine();

            Console.Write("Digite a Editora do Livro: ");
            string editora = Console.ReadLine();

            Console.Write("Digite o Genero do Livro: ");
            string genero = Console.ReadLine();

            Console.Write("Digite o Tipo da Capa (Ex: Dura, Brochura): ");
            string tipoDaCapa = Console.ReadLine();

            Console.Write("Digite o Ano de Publicacao: ");
            if (!int.TryParse(Console.ReadLine(), out int ano))
                throw new ArgumentException("Ano invalido.");

            Console.Write("Digite o Numero de Paginas: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroDePaginas))
                throw new ArgumentException("Numero de paginas invalido.");

            cadastroLivro.CadastrarLivro(leitores, cpfLeitor, isbn, titulo, subtitulo, 
                                         escritor, editora, genero, tipoDaCapa, ano, numeroDePaginas);

            Console.WriteLine("Livro cadastrado com sucesso!");
        }

        static void EditarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- EDITAR LIVRO ---");

            Console.Write("Digite o CPF do Leitor dono do livro: ");
            string cpfLeitor = Console.ReadLine();

            if (cadastroLeitor.EncontrarLeitor(leitores, cpfLeitor) == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Console.Write("Digite o Titulo do Livro que deseja editar: ");
            string tituloAtual = Console.ReadLine();

            Console.WriteLine("(ISBN nao pode ser alterado)");

            Console.Write("Novo Titulo: ");
            string novoTitulo = Console.ReadLine();

            Console.Write("Novo Subtitulo: ");
            string novoSubtitulo = Console.ReadLine();

            Console.Write("Novo Escritor: ");
            string novoEscritor = Console.ReadLine();

            Console.Write("Nova Editora: ");
            string novaEditora = Console.ReadLine();

            Console.Write("Novo Genero: ");
            string novoGenero = Console.ReadLine();

            Console.Write("Novo Tipo da Capa: ");
            string novoTipoDaCapa = Console.ReadLine();

            Console.Write("Novo Ano de Publicacao: ");
            if (!int.TryParse(Console.ReadLine(), out int novoAno))
                throw new ArgumentException("Ano invalido.");

            Console.Write("Novo Numero de Paginas: ");
            if (!int.TryParse(Console.ReadLine(), out int novoNumeroDePaginas))
                throw new ArgumentException("Numero de paginas invalido.");

            cadastroLivro.EditarLivro(leitores, cpfLeitor, tituloAtual, novoTitulo, novoSubtitulo,
                                      novoEscritor, novaEditora, novoGenero, novoTipoDaCapa,
                                      novoAno, novoNumeroDePaginas);

            Console.WriteLine("Livro editado com sucesso!");
        }

        static void RemoverLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- REMOVER LIVRO (PERDA) ---");

            Console.Write("Digite o CPF do Leitor que perdeu o livro: ");
            string cpfLeitor = Console.ReadLine();

            if (cadastroLeitor.EncontrarLeitor(leitores, cpfLeitor) == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Console.Write("Digite o Titulo do Livro perdido: ");
            string titulo = Console.ReadLine();

            cadastroLivro.RemoverLivro(leitores, cpfLeitor, titulo);
            Console.WriteLine("O livro foi removido com sucesso!");
        }

        static void DoarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- DOAR LIVRO ---");

            Console.Write("Digite o CPF do Leitor DOADOR: ");
            string cpfDoador = Console.ReadLine();

            if (cadastroLeitor.EncontrarLeitor(leitores, cpfDoador) == null)
                throw new KeyNotFoundException($"Doador (CPF '{cpfDoador}') nao encontrado.");

            Console.Write("Digite o CPF do Leitor DESTINATARIO: ");
            string cpfDestinatario = Console.ReadLine();

            if (cadastroLeitor.EncontrarLeitor(leitores, cpfDestinatario) == null)
                throw new KeyNotFoundException($"Destinatario (CPF '{cpfDestinatario}') nao encontrado.");

            Console.Write("Digite o Titulo do Livro a ser doado: ");
            string titulo = Console.ReadLine();

            cadastroLivro.DoarLivro(leitores, cpfDoador, cpfDestinatario, titulo);
            Console.WriteLine("Livro doado com sucesso!");
        }

        static void PesquisarLivro(List<Leitor> leitores)
        {
            Console.Clear();
            Console.WriteLine("--- PESQUISAR LEITOR POR LIVRO ---");
            Console.Write("Digite o Titulo do Livro para ver quem o possui: ");
            string titulo = Console.ReadLine();

            string resultado = cadastroLivro.PesquisarLivro(leitores, titulo);
            Console.WriteLine(resultado);
        }
    }
}