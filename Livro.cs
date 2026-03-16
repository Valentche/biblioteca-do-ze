namespace Biblioteca
{

    public class Livro
    {
        public string Titulo;
        public string Subtitulo;
        public string Genero;
        public string Autor;
        public string Editora;
        public int AnoPublicacao;
        public int Paginas;
        public int Capitulos;

        // com o public antes do tipo da variável, 
        // ela se torna acessível de fora da classe, ou seja, 
        // pode ser acessada e modificada por outras partes do 
        // código que utilizam a classe Livro. 
        // Isso é útil para permitir que outras partes do programa 
        // possam ler e alterar as informações dos livros, como título,
        // autor, etc.

        // Construtor vazio: Permite criar um livro sem passar dados logo de cara (ex: `new Livro()`)
        // Muito útil quando vamos ler os dados do teclado um por um depois de criar o objeto.
        public Livro() 
        { 
        }

        // Construtor com parâmetros: Permite criar um livro já passando os dados básicos.
        // Isso é o que o professor chamou de "Sobrecarga de construtores" nos slides!
        public Livro(string titulo, string autor)
        {
            this.Titulo = titulo;
            this.Autor = autor;
        }
    }

}