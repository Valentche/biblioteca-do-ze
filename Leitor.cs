namespace Biblioteca
{
    public class Leitor
    {
        public string nome;
        public int idade;
        
        // Atributo adicionado para atender a exigência do Laboratório: 
        // "No leitor incluir um atributo para o Cpf, garantindo assim um identificador único"
        public string cpf;

        public List<Livro> LivrosLeitor = new List<Livro>();
        //o list acima funciona como uma coleção de objetos do tipo Livro,
        //permitindo que um leitor possa ter uma lista de livros associados a ele.

        // Construtor vazio: Útil para criar o leitor passo a passo enquanto lemos as informações do terminal
        public Leitor()
        {
        }

        // Construtor com sobrecarga: Já constrói o leitor com as propriedades preenchidas
        public Leitor(string nome, int idade, string cpf)
        {
            this.nome = nome;
            this.idade = idade;
            this.cpf = cpf;
        }

        public void AdicionarLivro(Livro livro)
        {
            LivrosLeitor.Add(livro);
            //o método acima adiciona um livro à lista de livros do leitor,
            //permitindo que o leitor registre os livros que leu.

        }

        public void RemoverLivro(Livro livro)
        {
            LivrosLeitor.Remove(livro);
            //o método acima remove um livro da lista de livros do leitor,
            //permitindo que o leitor atualize sua lista de livros lidos.

        }

        public void DoarLivro(Livro livro, Leitor destinatario)
        {
            // Verificamos se o doador realmente tem o livro na lista usando o método .Contains()
            if (LivrosLeitor.Contains(livro))
            {
                LivrosLeitor.Remove(livro); // Tira da lista do doador
                destinatario.AdicionarLivro(livro); // Adiciona na lista do destinatário
                //o método acima permite que um leitor doe um livro para outro leitor,
                //removendo o livro da lista do doador e adicionando-o à lista do destinatário.
            }
            else
            {
                Console.WriteLine("O livro não está na lista do leitor doador.");
                //caso o livro não esteja na lista do leitor, uma mensagem de erro é exibida.
            }
        }

    }
}