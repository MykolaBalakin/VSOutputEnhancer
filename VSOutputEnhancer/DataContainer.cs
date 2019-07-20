using System;
using System.Collections.Generic;

namespace Balakin.VSOutputEnhancer
{
    public class DataContainer
    {
        private readonly IDictionary<Type, Object> data = new Dictionary<Type, Object>();

        public TData Get<TData>()
            where TData : new()
        {
            var type = typeof(TData);
            if (data.TryGetValue(type, out var existingItem))
            {
                return (TData)existingItem;
            }

            var newItem = new TData();
            Set(newItem);
            return newItem;
        }

        public void Set<TData>(TData item)
        {
            var type = typeof(TData);
            data.Add(type, item);
        }
    }
}