using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CatalogandoJogos.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            : base("Este jogo já foi cadastrado.") 
        { }
    }
}
