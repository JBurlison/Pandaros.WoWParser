using AutoMapper;
using Pandaros.WoWParser.Parser.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandaros.WoWParser.Parser.Repositories
{
    public class RepoBase<T, DTO, DB> where DTO : IdEquatable<DTO> 
                                      where DB : MongoBase<DTO> 
    {
        internal DB _dataAccess;
        internal IMapper _mapper;

        internal RepoBase(DB dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess;
            _mapper = mapper;
        }

        public async virtual Task<T> GetAsync(string id)
        {
            var dto = await _dataAccess.GetAsync(id);
            return _mapper.Map<T>(dto);
        }

        public async virtual Task DeleteAsync(string id)
        {
            await _dataAccess.Delete(id);
        }

        public async virtual Task UpsertAsync(T obj)
        {
            await _dataAccess.Upsert(_mapper.Map<DTO>(obj));
        }
    }
}
