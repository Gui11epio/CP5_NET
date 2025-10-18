using System;
using System.Collections.Generic;
using System.IO;
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
    public class PatioRepository : IPatioRepository
    {
        private readonly IMongoCollection<Patio> _collection;

        public PatioRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Patio>("Patio");
        }

        public async Task<List<Patio>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Patio?> ObterPorIdAsync(int id)
        {
            var filter = Builders<Patio>.Filter.Eq(e => e.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<(List<Patio> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var total = (int)await _collection.CountDocumentsAsync(_ => true);
            var itens = await _collection.Find(_ => true)
                                .Skip((numeroPag - 1) * tamanhoPag)
                                .Limit(tamanhoPag)
                                .ToListAsync();
            return (itens, total);
        }

        public async Task<Patio> CriarAsync(Patio obj)
        {
            if (obj.Id == 0)
            {
                var sort = Builders<Patio>.Sort.Descending(e => e.Id);
                var last = await _collection.Find(_ => true).Sort(sort).Limit(1).FirstOrDefaultAsync();
                obj.Id = (last?.Id ?? 0) + 1;
            }

            await _collection.InsertOneAsync(obj);
            return obj;
        }

        public async Task<bool> AtualizarAsync(Patio obj)
        {
            var filter = Builders<Patio>.Filter.Eq(e => e.Id, obj.Id);
            var result = await _collection.ReplaceOneAsync(filter, obj);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var filter = Builders<Patio>.Filter.Eq(e => e.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
