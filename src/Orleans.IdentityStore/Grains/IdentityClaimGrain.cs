﻿using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans.IdentityStore.Interfaces;

namespace Orleans.IdentityStore.Grains
{
    internal class IdentityClaimGrain : Grain, IIdentityClaimGrainInternal
    {
        private readonly IPersistentState<HashSet<Guid>> _data;

        public IdentityClaimGrain(
                    [PersistentState("IdentityClaim", OrleansIdentityConstants.OrleansStorageProvider)] 
                    IPersistentState<HashSet<Guid>> data)
        {
            _data = data;
        }

        public Task AddUserId(Guid id)
        {
            if (_data.State.Add(id))
                return _data.WriteStateAsync();

            return Task.CompletedTask;
        }

        public Task<IList<Guid>> GetUserIds()
        {
            return Task.FromResult<IList<Guid>>(_data.State.ToList());
        }

        public Task RemoveUserId(Guid id)
        {
            if (_data.State.Remove(id))
                return _data.WriteStateAsync();

            return Task.CompletedTask;
        }
    }
}