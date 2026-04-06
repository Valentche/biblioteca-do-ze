using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca;

namespace Cadastro
{
    /// <summary>
    /// Classe responsável pela lógica de cadastro de livros.
    /// Não possui dependência com console ou interface de usuário.
    /// Todas as validações e mensagens de erro são feitas via exceções.
    /// </summary>
    public class CadastroLivro
    {
        /// <summary>
        /// Cria e adiciona um novo livro à lista de um leitor.
        /// Lança exceção se houver erro (dados inválidos, leitor não encontrado, etc).
        /// </summary>
        public void CadastrarLivro(List<Leitor> leitores, string cpfLeitor, string isbn, string titulo,
                                   string subtitulo, string escritor, string editora, string genero,
                                   string tipoDaCapa, int anoPublicacao, int numeroDePaginas)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpfLeitor);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Livro novoLivro = new Livro(isbn)
            {
                Titulo = titulo,
                Subtitulo = subtitulo,
                Escritor = escritor,
                Editora = editora,
                Genero = genero,
                TipoDaCapa = tipoDaCapa,
                AnoPublicacao = anoPublicacao,
                NumeroDePaginas = numeroDePaginas
            };

            leitor.AdicionarLivro(novoLivro);
        }

        /// <summary>
        /// Busca um livro pelo titulo no acervo de um leitor.
        /// Lança exceção se não encontrar.
        /// </summary>
        private Livro EncontrarLivroPorTitulo(Leitor leitor, string titulo)
        {
            Livro livro = leitor.LivrosLeitor.FirstOrDefault(lv =>
                lv.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

            if (livro == null)
                throw new KeyNotFoundException($"Livro com titulo '{titulo}' nao encontrado.");

            return livro;
        }

        /// <summary>
        /// Edita os dados de um livro (exceto ISBN, que é imutável).
        /// Lança exceção se não encontrar leitor ou livro.
        /// </summary>
        public void EditarLivro(List<Leitor> leitores, string cpfLeitor, string tituloAtual,
                                string novoTitulo, string novoSubtitulo, string novoEscritor,
                                string novaEditora, string novoGenero, string novoTipoDaCapa,
                                int novoAnoPublicacao, int novoNumeroDePaginas)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpfLeitor);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Livro livro = EncontrarLivroPorTitulo(leitor, tituloAtual);

            livro.Titulo = novoTitulo;
            livro.Subtitulo = novoSubtitulo;
            livro.Escritor = novoEscritor;
            livro.Editora = novaEditora;
            livro.Genero = novoGenero;
            livro.TipoDaCapa = novoTipoDaCapa;
            livro.AnoPublicacao = novoAnoPublicacao;
            livro.NumeroDePaginas = novoNumeroDePaginas;
        }

        /// <summary>
        /// Remove um livro do acervo de um leitor (simulando perda).
        /// Lança exceção se não encontrar leitor ou livro.
        /// </summary>
        public void RemoverLivro(List<Leitor> leitores, string cpfLeitor, string tituloLivro)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpfLeitor);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpfLeitor}' nao encontrado.");

            Livro livro = EncontrarLivroPorTitulo(leitor, tituloLivro);
            leitor.RemoverLivro(livro);
        }

        /// <summary>
        /// Realiza a doação de um livro entre dois leitores.
        /// Lança exceção se algum não for encontrado.
        /// </summary>
        public void DoarLivro(List<Leitor> leitores, string cpfDoador, string cpfDestinatario,
                              string tituloLivro)
        {
            Leitor doador = leitores.FirstOrDefault(l => l.Cpf == cpfDoador);

            if (doador == null)
                throw new KeyNotFoundException($"Doador (CPF '{cpfDoador}') nao encontrado.");

            Leitor destinatario = leitores.FirstOrDefault(l => l.Cpf == cpfDestinatario);

            if (destinatario == null)
                throw new KeyNotFoundException($"Destinatario (CPF '{cpfDestinatario}') nao encontrado.");

            Livro livro = EncontrarLivroPorTitulo(doador, tituloLivro);

            doador.DoarLivro(livro, destinatario);
        }

        /// <summary>
        /// Pesquisa qual(is) leitor(es) possui um livro com o titulo especificado.
        /// Retorna string com o resultado da busca.
        /// </summary>
        public string PesquisarLivro(List<Leitor> leitores, string tituloBusca)
        {
            var sb = new System.Text.StringBuilder();

            bool encontrado = false;

            foreach (Leitor leitor in leitores)
            {
                if (leitor.LivrosLeitor.Any(lv =>
                    lv.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase)))
                {
                    sb.AppendLine($"-> O leitor {leitor.Nome} (CPF: {leitor.Cpf}) possui este livro em sua estante!");
                    encontrado = true;
                }
            }

            if (!encontrado)
                sb.AppendLine("Nenhum leitor possui esse livro no momento.");

            return sb.ToString();
        }
    }
}