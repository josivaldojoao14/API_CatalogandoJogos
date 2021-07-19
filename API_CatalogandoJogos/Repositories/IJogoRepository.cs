using API_CatalogandoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CatalogandoJogos.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> Obter(int pagina, int quantidade);

        Task<Jogo> ObterPorId(Guid id);

        Task<List<Jogo>> Obter(string nome, string produtora);

        Task Inserir(Jogo jogo);

        Task AtualizarJogo(Jogo jogo);

        Task Deletar(Guid id);
    }
}
