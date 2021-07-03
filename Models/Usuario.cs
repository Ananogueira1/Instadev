using System;
using System.Collections.Generic;
using System.IO;
using Instadev.Interfaces;

namespace Instadev.Models
{
    public class Usuario : InstadevBase, IUsuario
    {
        public int IdUsuario { get; set; }
        public string Email;
        private string Senha;
        public string Nome { get; set; }

        public string NomeDeUsuario { get; set; }

        // List<Post> Postagem = new List<Post>();

        List<int> IdsPosts { get; set; }

        public string ImagemDePerfil { get; set; }
        private const string CAMINHO = "Database/Usuario.csv";

        public Usuario()
        {
            CriarPastaEArquivo(CAMINHO);
        }

        private string PrepararLinha(Usuario u)
        {
            return $"{u.ImagemDePerfil};{u.IdUsuario};{u.Nome};{u.NomeDeUsuario};{u.Email};{u.Senha};{u.PrepararIdsPosts()}";
        }

        public void Cadastrar(Usuario u)
        {
            string[] linha = { PrepararLinha(u) };
            File.AppendAllLines(CAMINHO, linha);
        }

        public List<Usuario> ExibirInfo()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string[] linhas = File.ReadAllLines(CAMINHO);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Usuario usuario = new Usuario();
                usuario.ImagemDePerfil = linha[0];
                usuario.IdUsuario = int.Parse(linha[1]);
                usuario.Nome = linha[2];
                usuario.NomeDeUsuario = linha[3];
                usuario.Email = linha[4];
                usuario.Senha = linha[5];

                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public void EditarPerfil(Usuario u)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[0] == u.IdUsuario.ToString());
            linhas.Add(PrepararLinha(u));
            ReescreverCSV(CAMINHO, linhas);
        }

        public void DeletarPerfil(int id)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[0] == IdUsuario.ToString());
            ReescreverCSV(CAMINHO, linhas);
        }
        public string PrepararIdsPosts()
        {
            List<string> Ids = new List<string>();
            foreach (var item in IdsPosts)
            {
                string Id;
                Id = $"{item.ToString()},";
                Ids.Add(Id);
            }
            string IdsPreparados = string.Join("", Ids);
            
            return IdsPreparados;
        }
    }

}