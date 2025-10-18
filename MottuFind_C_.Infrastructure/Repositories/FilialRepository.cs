using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MottuFind_C_.Domain.Repositories;
using MottuFind_C_.Infrastructure.Context;
using Sprint1_C_.Domain.Entities;
using Sprint1_C_.Infrastructure.Data;

namespace MottuFind_C_.Infrastructure.Repositories
{
    public class FilialRepository : IFilialRepository
    {
        private readonly IMongoCollection<Filial> _collection;

        public FilialRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Filial>("Filial");
        }

        public async Task<List<Filial>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Filial?> ObterPorIdAsync(int id)
        {
            var filter = Builders<Filial>.Filter.Eq(f => f.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<(List<Filial> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var total = (int)await _collection.CountDocumentsAsync(_ => true);
            var itens = await _collection.Find(_ => true)
                                .Skip((numeroPag - 1) * tamanhoPag)
                                .Limit(tamanhoPag)
                                .ToListAsync();
            return (itens, total);
        }

        public async Task<Filial> CriarAsync(Filial filial)
        {
            // assign Id if zero by calculating max+1
            if (filial.Id == 0)
            {
                var sort = Builders<Filial>.Sort.Descending(f => f.Id);
                var last = await _collection.Find(_ => true).Sort(sort).Limit(1).FirstOrDefaultAsync();
                filial.Id = (last?.Id ?? 0) + 1;
            }

            await _collection.InsertOneAsync(filial);
            return filial;
        }

        public async Task<bool> AtualizarAsync(Filial filial)
        {
            var filter = Builders<Filial>.Filter.Eq(f => f.Id, filial.Id);
            var result = await _collection.ReplaceOneAsync(filter, filial);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var filter = Builders<Filial>.Filter.Eq(f => f.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
