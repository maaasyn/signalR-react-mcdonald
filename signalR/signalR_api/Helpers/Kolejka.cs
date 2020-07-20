using System;
using System.Collections.Generic;
using System.Linq;

namespace signalR_api.Helpers
{
    public class Kolejka : IKolejka
    {
        public Dictionary<string, IList<int>> KolejkowaKolekcja { get; set; }

        public Kolejka()
        {
            KolejkowaKolekcja = new Dictionary<string, IList<int>>();
        }
        public void Add(string groupName)
        {
            if (!KolejkowaKolekcja.ContainsKey(groupName))
            {
                KolejkowaKolekcja.Add(groupName, Enumerable.Range(1, 10).ToList()); 
            }
        }

        public int GetNextNumber(string groupName)
        {
            IList<int> list;
            KolejkowaKolekcja.TryGetValue(groupName, out list);
            if (list.Count == 0)
            {
                KolejkowaKolekcja[groupName] = Enumerable.Range(1, 10).ToList();
                KolejkowaKolekcja.TryGetValue(groupName, out list);
            }
            int result = list.First();
            list.RemoveAt(0);
            return result;
        }
    }
}