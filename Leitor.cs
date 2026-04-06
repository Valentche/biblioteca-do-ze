namespace Biblioteca
{
    public class Leitor
    {
        // Coleção estática compartilhada por TODOS os objetos Leitor.
        // Garante que nenhum CPF se repita, mesmo que a validação da camada de interface
        // seja esquecida por quem estiver programando o cadastro.
        private static HashSet<string> _cpfsRegistrados = new HashSet<string>();

        private string _nome;
        private string _cpf;
        private int _idade;

        // ── Propriedades encapsuladas ────────────────────────────────────────────────

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser nulo ou vazio.");
                _nome = value.Trim();
            }
        }

        public string Cpf
        {
            get => _cpf;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("CPF não pode ser nulo ou vazio.");

                string cpfTrimmed = value.Trim();

                // Regra de negócio: CPF deve ser único entre todos os leitores cadastrados.
                if (_cpfsRegistrados.Contains(cpfTrimmed))
                    throw new ArgumentException($"CPF '{cpfTrimmed}' já está em uso por outro leitor.");

                // Se o leitor já tinha um CPF (caso de edição), libera o anterior.
                if (_cpf != null)
                    _cpfsRegistrados.Remove(_cpf);

                _cpf = cpfTrimmed;
                _cpfsRegistrados.Add(_cpf);
            }
        }

        public int Idade
        {
            get => _idade;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Idade não pode ser negativa.");
                _idade = value;
            }
        }

        public List<Livro> LivrosLeitor = new List<Livro>();

        // ── Construtores ─────────────────────────────────────────────────────────────

        // Construtor vazio: permite criar o leitor passo a passo.
        public Leitor() { }

        // Construtor com sobrecarga: já constrói o leitor com as propriedades preenchidas.
        // Os setters são chamados aqui, aplicando todas as validações.
        public Leitor(string nome, int idade, string cpf)
        {
            Nome = nome;
            Idade = idade;
            Cpf = cpf;
        }

        // ── Métodos de controle ───────────────────────────────────────────────────────

        // Deve ser chamado ANTES de remover o leitor do sistema.
        // Libera o CPF da coleção estática para que possa ser reutilizado.
        public void LiberarCpf()
        {
            if (_cpf != null)
                _cpfsRegistrados.Remove(_cpf);
        }

        public void AdicionarLivro(Livro livro)
        {
            LivrosLeitor.Add(livro);
        }

        public void RemoverLivro(Livro livro)
        {
            LivrosLeitor.Remove(livro);
        }

        public void DoarLivro(Livro livro, Leitor destinatario)
        {
            // Regra de negócio movida para dentro da classe: erro lançado via exceção,
            // sem nenhuma dependência de Console (funciona em qualquer tipo de projeto).
            if (!LivrosLeitor.Contains(livro))
                throw new InvalidOperationException("O livro não está na lista do leitor doador.");

            LivrosLeitor.Remove(livro);
            destinatario.AdicionarLivro(livro);
        }
    }
}