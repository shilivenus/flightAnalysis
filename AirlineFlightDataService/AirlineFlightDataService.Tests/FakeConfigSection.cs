using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace AirlineFlightDataService.Tests
{
    class FakeConfigSection : IConfigurationSection
    {
        private readonly Dictionary<string, string> _myData;

        public FakeConfigSection(Dictionary<string, string> myData)
        {
            _myData = myData;
        }

        public FakeConfigSection(Dictionary<string, string> myData, string key, string value)
        {
            _myData = myData;
            Key = key;
            Value = value;
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public string this[string key]
        {
            get => _myData[key];
            set => _myData[key] = key;
        }

        public string Key { get; }
        public string Path { get; }
        public string Value { get; set; }
    }
}
