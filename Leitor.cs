namespace Biblioteca
{
    public class Leitor
    {
        public string nome;
        public int idade;

        public List<Livro> LivrosLeitor = new List<Livro>();
        //o list acima funciona como uma coleção de objetos do tipo Livro,
        //permitindo que um leitor possa ter uma lista de livros associados a ele.

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
            if (LivrosLeitor.Contains(livro))
            {
                LivrosLeitor.Remove(livro);
                destinatario.AdicionarLivro(livro);
                //o método acima permite que um leitor doe um livro para outro leitor,
                //removendo o livro da lista do doador e adicionando-o à lista do destinatário.
            }
            else
            {
                Console.WriteLine("O livro não está na lista do leitor.");
                //caso o livro não esteja na lista do leitor, uma mensagem de erro é exibida.
            }
        }

    }
}