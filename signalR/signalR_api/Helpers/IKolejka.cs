using System;
using System.Collections.Generic;

namespace signalR_api.Helpers
{
    public interface IKolejka
    {
        public Dictionary<string, IList<int>> KolejkowaKolekcja { get; set; }
        public void Add(string groupName);
        public int GetNextNumber(string groupName);
    }
}