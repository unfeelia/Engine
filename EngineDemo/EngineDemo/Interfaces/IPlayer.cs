using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces
{
    public interface IPlayer
    {
        int GetID();
        void SetID(int id);
        void CollectResource(IResourceSet resource);
        void AddResource(string type, int quantity);
        void AddResourceType(string type);
        Dictionary<string, int> GetResources();
        void AddResources(IDictionary<string, int> dictionary);
        void Collect(ICollectable collectable);
    }
}
