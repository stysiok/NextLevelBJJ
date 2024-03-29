﻿using NextLevelBJJ.Core.Entities;
using NextLevelBJJ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelBJJ.Infrastructure.Caching.Repositories
{
    public class InMemoryPassTypeRepository : IPassTypeRepository
    {
        private readonly List<PassType> _passTypes = new List<PassType>
        {
            new PassType(Guid.NewGuid(), "Open", 140m, 1000, true),
            new PassType(Guid.NewGuid(), "8 wejść", 110m, 8, false),
            new PassType(Guid.NewGuid(), "Dzieci", 70m, 8, false)
        };

        public async Task AddAsync(PassType passType)
        {
            _passTypes.Add(passType);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var passType = await GetAsync(id);

            if (passType is null)
            {
                return;
            }

            _passTypes.Remove(passType);
        }

        public async Task<IEnumerable<PassType>> GetAsync()
            => await Task.FromResult(_passTypes);

        public async Task<PassType> GetAsync(Guid id)
            => await Task.FromResult(_passTypes.SingleOrDefault(pt => pt.Id == id));
    }
}
