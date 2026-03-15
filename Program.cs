

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        //o programa sempre procura o metodo Main para iniciar
        //a execução do código.
        {

            Livro livro1;
            livro1 = new Livro();
            livro1.Titulo = "O Senhor dos Anéis";
            livro1.Subtitulo = "A Sociedade do Anel";
            livro1.Genero = "Fantasia";
            livro1.AnoPublicacao = 1954;
            livro1.Capitulos = 22;
            livro1.Autor = "J.R.R. Tolkien";
            livro1.Editora = "HarperCollins";

            Livro livro2 = new Livro();
            livro2.Titulo = "Harry Potter e a Pedra Filosofal";
            livro2.Subtitulo = "Harry Potter e a Pedra Filosofal";
            livro2.Genero = "Fantasia";
            livro2.AnoPublicacao = 1997;
            livro2.Capitulos = 17;
            livro2.Autor = "J.K. Rowling";
            livro2.Editora = "Bloomsbury";

            //os comandos acima criam um objeto do tipo Livro, atribuem 
            //valores às suas propriedades e permitem que essas informações
            //sejam acessadas e modificadas por outras partes do programa.

            Leitor leitor1 = new Leitor();
            leitor1.nome = "Zé";
            leitor1.idade = 30;
            leitor1.AdicionarLivro(livro1);
            leitor1.AdicionarLivro(livro2);

            Leitor leitor2 = new Leitor();
            leitor2.nome = "Maria";
            leitor2.idade = 25;
            leitor2.AdicionarLivro(livro1);

            //os comandos acima criam objetos do tipo Leitor, 
            //atribuem valores às suas propriedades e associam livros a cada 
            //leitor usando o método AdicionarLivro do arquivo Leitor.cs.

            Console.WriteLine($"Leitor: {leitor1.nome}, Idade: {leitor1.idade}");
            Console.WriteLine("Livros do " + leitor1.nome + ":");
            foreach (Livro x in leitor1.LivrosLeitor)
            {
                //o foreach acima percorre a lista de livros associada ao leitor1 e 
                //imprime o título de cada livro, que estão sendo puxados do objeto Livro.
                Console.WriteLine("Título: " + x.Titulo);
            }

            Console.WriteLine($"Leitor: {leitor2.nome}, Idade: {leitor2.idade}");
            Console.WriteLine("Livros do " + leitor2.nome + ":");
            foreach (Livro x in leitor2.LivrosLeitor)
            {
                //o foreach acima percorre a lista de livros associada ao leitor2 e 
                //imprime o título de cada livro, que estão sendo puxados do objeto Livro.
                Console.WriteLine("Título: " + x.Titulo);
            }



        }
    }
}