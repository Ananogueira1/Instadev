using System.Collections.Generic;
using Instadev.Models;


namespace Instadev.Interfaces
{
    public interface IUsuario
    {
      
      void Cadastrar(Usuario u);

            List<Usuario> ExibirInfo();

            void EditarPerfil(Usuario u);

            void DeletarPerfil(int id);

    }
}