using API_CatalogandoJogos.InputModel;
using API_CatalogandoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CatalogandoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> ObterPorId(Guid id);

        Task<JogoViewModel> PostarJogo(JogoInputModel jogo);

        Task AtualizarJogo(Guid id, JogoInputModel jogo);

        Task AtualizarJogo(Guid id, double preco);

        Task Deletar(Guid id);
    }
}
