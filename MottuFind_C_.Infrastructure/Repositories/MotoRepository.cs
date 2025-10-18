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
    public class MotoRepository : IMotoRepository
    {
        private readonly IMongoCollection<Moto> _collection;

        public MotoRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Moto>("Moto");
        }

        public async Task<List<Moto>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Moto?> ObterPorPlacaAsync(string placa)
        {
            var filter = Builders<Moto>.Filter.Eq(e => e.Placa, placa);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<(List<Moto> Itens, int Total)> ObterPorPaginaAsync(int numeroPag, int tamanhoPag)
        {
            var total = (int)await _collection.CountDocumentsAsync(_ => true);
            var itens = await _collection.Find(_ => true)
                                .Skip((numeroPag - 1) * tamanhoPag)
                                .Limit(tamanhoPag)
                                .ToListAsync();
            return (itens, total);
        }

        public async Task<Moto> CriarAsync(Moto moto, TagRfid tag)
        {
            moto.TagRfid = tag;
            await _collection.InsertOneAsync(moto);
            return moto;
        }

        public async Task<bool> AtualizarAsync(Moto moto)
        {
            var filter = Builders<Moto>.Filter.Eq(e => e.Placa, moto.Placa);
            var result = await _collection.ReplaceOneAsync(filter, moto);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> RemoverAsync(string placa)
        {
            var filter = Builders<Moto>.Filter.Eq(e => e.Placa, placa);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
