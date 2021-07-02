using System.Collections.Generic;
using Instadev.Models;

namespace Instadev.Interfaces
{
    public interface IPost
    {
        void Criar(Post P);
        List<Post> LerTodas();
        void Alterar(Post P);
        void Deletar(int ID);
    }
}