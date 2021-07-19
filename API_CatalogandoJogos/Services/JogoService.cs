using API_CatalogandoJogos.Entities;
using API_CatalogandoJogos.Exceptions;
using API_CatalogandoJogos.InputModel;
using API_CatalogandoJogos.Repositories;
using API_CatalogandoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CatalogandoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> ObterPorId(Guid id)
        {
            var jogo = await _jogoRepository.ObterPorId(id);

            if (jogo == null) return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> PostarJogo(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
            {
                throw new JogoJaCadastradoException();
            }

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Produtora = jogoInsert.Produtora,
                Preco = jogoInsert.Preco
            };
        }

        public async Task AtualizarJogo(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.ObterPorId(id);

            if (entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.AtualizarJogo(entidadeJogo);

        }

        public async Task AtualizarJogo(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.ObterPorId(id);

            if(entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            entidadeJogo.Preco = preco;
            await _jogoRepository.AtualizarJogo(entidadeJogo);
        }

        public async Task Deletar(Guid id)
        {
            var jogo = await _jogoRepository.ObterPorId(id);

            if (jogo == null)
            {
                throw new JogoNaoCadastradoException();
            }

            await _jogoRepository.Deletar(id);
        }
        
        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
