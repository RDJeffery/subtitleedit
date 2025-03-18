using System;
using System.Collections.Generic;

namespace SubtitleEdit.Avalonia.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator? _current;
        private readonly Dictionary<Type, object> _services = new();

        public static ServiceLocator Current => _current ??= new ServiceLocator();

        public void RegisterService<T>(T service) where T : class
        {
            _services[typeof(T)] = service ?? throw new ArgumentNullException(nameof(service));
        }

        public T GetService<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.");
        }

        public bool IsRegistered<T>() where T : class
        {
            return _services.ContainsKey(typeof(T));
        }
    }
} 