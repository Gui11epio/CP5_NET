using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class LeituraRfidRepository : ILeituraRfidRepository
    {

        private readonly IMongoCollection<LeituraRfid> _collection;

        public LeituraRfidRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<LeituraRfid>("LeituraRfid");
        }

        public async Task<List<LeituraRfid>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<LeituraRfid?> ObterPorIdAsync(int id)
        {
            var filter = Builders<LeituraRfid>.Filter.Eq(e => e.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<(List<LeituraRfid> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var total = (int)await _collection.CountDocumentsAsync(_ => true);
            var itens = await _collection.Find(_ => true)
                                .Skip((numeroPag - 1) * tamanhoPag)
                                .Limit(tamanhoPag)
                                .ToListAsync();
            return (itens, total);
        }

        public async Task<LeituraRfid> CriarAsync(LeituraRfid obj)
        {
            if (obj.Id == 0)
            {
                var sort = Builders<LeituraRfid>.Sort.Descending(e => e.Id);
                var last = await _collection.Find(_ => true).Sort(sort).Limit(1).FirstOrDefaultAsync();
                obj.Id = (last?.Id ?? 0) + 1;
            }

            await _collection.InsertOneAsync(obj);
            return obj;
        }


        public async Task<bool> AtualizarAsync(LeituraRfid obj)
        {
            var filter = Builders<LeituraRfid>.Filter.Eq(e => e.Id, obj.Id);
            var result = await _collection.ReplaceOneAsync(filter, obj);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var filter = Builders<LeituraRfid>.Filter.Eq(e => e.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
