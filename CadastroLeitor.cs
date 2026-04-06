using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca;

namespace Cadastro
{
    /// <summary>
    /// Classe responsável pela lógica de cadastro de leitores.
    /// Não possui dependência com console ou Interface de usuário.
    /// Todas as validações e mensagens de erro são feitas via exceções.
    /// </summary>
    public class CadastroLeitor
    {
        /// <summary>
        /// Verifica se um CPF já está registrado na lista.
        /// </summary>
        public bool CpfJaExiste(List<Leitor> leitores, string cpf)
        {
            return leitores.Any(l => l.Cpf == cpf);
        }

        /// <summary>
        /// Cria e adiciona um novo leitor à lista.
        /// Lança exceção se houver erro (CPF duplicado, dados inválidos, etc).
        /// </summary>
        public void CadastrarLeitor(List<Leitor> leitores, string cpf, string nome, int idade)
        {
            Leitor novoLeitor = new Leitor(nome, idade, cpf);
            leitores.Add(novoLeitor);
        }

        /// <summary>
        /// Retorna string formatada com todos os leitores e seus livros.
        /// </summary>
        public string ListarTodosLeitores(List<Leitor> leitores)
        {
            if (leitores.Count == 0)
                return "Nenhum leitor cadastrado.";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("\n--- LISTA DE LEITORES E LIVROS ---");

            foreach (Leitor leitor in leitores)
            {
                sb.AppendLine($"> CPF: {leitor.Cpf} | Nome: {leitor.Nome} | Idade: {leitor.Idade}");
                sb.AppendLine("  Livros:");

                if (leitor.LivrosLeitor.Count == 0)
                {
                    sb.AppendLine("    [Nenhum livro cadastrado]");
                }
                else
                {
                    foreach (Livro livro in leitor.LivrosLeitor)
                    {
                        sb.AppendLine($"    - ISBN: {livro.Isbn} | Titulo: {livro.Titulo} | Escritor: {livro.Escritor}");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Busca um leitor pelo CPF e retorna string formatada com seus dados.
        /// Lança KeyNotFoundException se não encontrar.
        /// </summary>
        public string ListarLeitorEspecifico(List<Leitor> leitores, string cpf)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpf);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpf}' nao encontrado.");

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"\n> CPF: {leitor.Cpf} | Nome: {leitor.Nome} | Idade: {leitor.Idade}");
            sb.AppendLine("  Livros:");

            foreach (Livro livro in leitor.LivrosLeitor)
            {
                sb.AppendLine($"    - ISBN: {livro.Isbn} | Titulo: {livro.Titulo} | Escritor: {livro.Escritor}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Edita o nome e idade de um leitor.
        /// Lança exceção se não encontrar ou se os dados forem inválidos.
        /// </summary>
        public void EditarLeitor(List<Leitor> leitores, string cpf, string novoNome, int novaIdade)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpf);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpf}' nao encontrado.");

            // Os setters vão validar e lançar exceção se necessário
            leitor.Nome = novoNome;
            leitor.Idade = novaIdade;
        }

        /// <summary>
        /// Remove um leitor da lista.
        /// Lança exceção se não encontrar.
        /// </summary>
        public void ExcluirLeitor(List<Leitor> leitores, string cpf)
        {
            Leitor leitor = leitores.FirstOrDefault(l => l.Cpf == cpf);

            if (leitor == null)
                throw new KeyNotFoundException($"Leitor com CPF '{cpf}' nao encontrado.");

            leitor.LiberarCpf();
            leitores.Remove(leitor);
        }

        /// <summary>
        /// Busca um leitor pelo CPF.
        /// Retorna null se não encontrar.
        /// </summary>
        public Leitor EncontrarLeitor(List<Leitor> leitores, string cpf)
        {
            return leitores.FirstOrDefault(l => l.Cpf == cpf);
        }
    }
}