using System;
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
        private const string CAMINHO = "Database/post.csv";

        public Post(){
            CriarPastaEArquivo(CAMINHO);
        }
        public string Preparar(Post P)
        {
            return $"{P.IDPost};{P.Imagem};{P.Legenda};{P.Likes};{P.IDUsuario}";
        }

        public void Alterar(Post P)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[0] == P.IDPost.ToString()); 
            Linhas.Add(Preparar(P));
            ReescreverCSV(CAMINHO, Linhas);
        }

        public void Criar(Post P)
        {
            string[] Linha = {Preparar(P)};
            File.AppendAllLines(CAMINHO, Linha);
        }

        public void Deletar(int ID)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[4] == ID.ToString()); 
            ReescreverCSV(CAMINHO, Linhas);
        }

        public List<Post> LerTodas()
        {
            List<Post> Posts = new List<Post>();
            string[] Linhas = File.ReadAllLines(CAMINHO);

            foreach (var item in Linhas)
            {
                string[] linha = item.Split(";");
                Post p = new Post();
                p.IDPost = Int32.Parse(linha[0]);
                p.Imagem = linha[1];
                p.Legenda = linha[2];
                p.Likes = Int32.Parse(linha[3]);
                p.IDUsuario = Int32.Parse(linha[4]);

                Posts.Add(p);
            }
            return Posts;
        }
    }
}