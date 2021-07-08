using System;
using System.Collections.Generic;
using System.IO;

namespace Instadev.Models
{
    public class Comentario : InstadevBase
    {
        private const string CAMINHO = "Database/Comentario.csv";
        public int IDComentario {get; set;}
        public int IDPost {get; set;}
        public int IDUsuario {get; set;}
        public string TextoComentario {get; set;}
        public Comentario(){
            CriarPastaEArquivo(CAMINHO);
        }
        public string Preparar(Comentario C)
        {
            return $"{C.IDComentario};{C.IDUsuario};{C.IDPost};{C.TextoComentario}";
        }

        public void Alterar(Comentario C)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[0] == C.IDComentario.ToString()); 
            Linhas.Add(Preparar(C));
            ReescreverCSV(CAMINHO, Linhas);
        }

        public void Criar(Comentario C)
        {
            string[] Linha = {Preparar(C)};
            File.AppendAllLines(CAMINHO, Linha);
        }

        public void Deletar(int ID)
        {
            List <string> Linhas = this.LerTodasLinhasCSV(CAMINHO);
            Linhas.RemoveAll(x => x.Split(";")[0] == ID.ToString()); 
            ReescreverCSV(CAMINHO, Linhas);
        }

        public List<Comentario> LerTodas()
        {
            List<Comentario> Comentarios = new List<Comentario>();
            string[] Linhas = File.ReadAllLines(CAMINHO);

            foreach (var item in Linhas)
            {
                string[] linha = item.Split(";");
                Comentario C = new Comentario();
                C.IDComentario = Int32.Parse(linha[0]);
                C.IDUsuario = Int32.Parse(linha[1]);
                C.IDPost = Int32.Parse(linha[2]);
                C.TextoComentario = linha[3];

                Comentarios.Add(C);
            }
            return Comentarios;
        }
    }
}