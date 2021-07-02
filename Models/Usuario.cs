using System.Collections.Generic;
using Instadev.Interfaces;

namespace Instadev.Models
{
    public class Usuario : InstadevBase , IUsuario 
    {
        public int IdUsuario { get; set; }
        private string Email;
        private string Senha;
        public string Nome { get; set; }

        public string NomeDeUsuario { get; set; }

        // List<Post> Postagem = new List<Post>();

        public string ImagemDePerfil { get; set; }
        private const string CAMINHO = "Database/Usuario.csv";

          public Usuario()
        {
            CriarPastaEArquivo(CAMINHO);
        }

        private string PrepararLinha(Usuario u)
        {
            return $"{u.ImagemDePerfil};{u.IdUsuario};{u.Nome};{u.NomeDeUsuario};{u.Email};{u.Senha}";
        }

        public void Cadastrar(Usuario u)
        {
            throw new System.NotImplementedException();
        }

        public List<Usuario> ExibirInfo()
        {
            throw new System.NotImplementedException();
        }

        public void EditarPerfil(Usuario u)
        {
            throw new System.NotImplementedException();
        }

        public void DeletarPerfil(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}