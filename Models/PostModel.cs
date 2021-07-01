using System.Collections.Generic;
using System.IO;
using Instadev.Interfaces;

namespace Instadev.Models
{
    public class Post : InstadevBase, IPost
    {
        public int IDPost { get; set; }
        public string Imagem { get; set; }
        public string Legenda { get; set; }
        public int Likes {get; set;}
        public int IDUsuario {get; set;}
        private const string CAMINHO = "Database/equipe.csv";

        public Post(){
            this.CriarPastaEArquivo(CAMINHO);
        }
        public string Preparar(Post P)
        {
            return $"";
        }

        public void Alterar(Post P)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[0] == P.IDPost.ToString()); 
            Linhas.Add(Preparar(P));
            this.ReescreverCSV(CAMINHO, Linhas);
        }

        public void Criar(Post P)
        {
            string[] Linha = {Preparar(P)};
            File.AppendAllLines(CAMINHO, Linha);
        }

        public void Deletar(int ID)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[0] == ID.ToString()); 
            this.ReescreverCSV(CAMINHO, Linhas);
        }

        public List<Post> LerTodas()
        {
            List<Post> Posts = new List<Post>();
            string[] Linhas = File.ReadAllLines(CAMINHO);
            foreach (var item in Linhas)
            {
                Post p = new Post();
                
            }
            return Posts;
        }
    }
}