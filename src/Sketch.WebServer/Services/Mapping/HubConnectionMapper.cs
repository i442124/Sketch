﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebServer.Services
{
    public class HubConnectionMapper<T> : IHubConnectionMapper<T>, IEnumerable<T>
    {
        private readonly ConcurrentDictionary<string, T> _connections =
        new ConcurrentDictionary<string, T>();

        public int Count
        {
            get { return _connections.Count; }
        }

        public void Add(string connectionId, T value)
        {
            _connections[connectionId] = value;
        }

        public Task AddAsync(string connectionId, T value)
        {
            return Task.Run(() => Add(connectionId, value));
        }

        public void Remove(string connectionId)
        {
            if (!_connections.TryRemove(connectionId, out _))
            {
                throw new ArgumentException("Connection does not exists.");
            }
        }

        public Task RemoveAsync(string connectionId)
        {
            return Task.Run(() => Remove(connectionId));
        }

        public T GetUserInfo(string connectionId)
        {
            if (!_connections.TryGetValue(connectionId, out T value))
            {
                throw new ArgumentException("Connection does not exists.");
            }

            return value;
        }

        public Task<T> GetUserInfoAsync(string connectionId)
        {
            return Task.Run(() => GetUserInfo(connectionId));
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var userStore in _connections.Values)
            {
                yield return userStore;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var userStore in _connections.Values)
            {
                yield return userStore;
            }
        }
    }
}
