using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    class Player: IPlayer
    {
        int ID;
        Dictionary<string, int> resources = new Dictionary<string, int>();

        public Player()
        {

        }

        public Player(int id)
        {
            ID = id;
        }

        public void AddResource(string type, int quantity)
        {
            if (resources.ContainsKey(type))
            {
                resources[type] += quantity;
            }
        }

        public void AddResources(IDictionary<string, int> dictionary)
        {
            foreach(var re in dictionary)
            {
                AddResource(re.Key, re.Value);
            }
        }

        public void AddResourceType(string type)
        {
            if(!resources.ContainsKey(type))
            {
                resources.Add(type, 0);
            }
        }

        public void Collect(ICollectable collectable)
        {
            if(collectable is IResourceSet)
            {
                AddResources(((IResourceSet)collectable).ExtractResources());
            }
        }

        public void CollectResource(IResourceSet resource)
        {
            foreach(var res in resource.GetResources())
            {
                resources[res.Key] += res.Value;
            }
        }

        public int GetID()
        {
            return ID;
        }

        public Dictionary<string, int> GetResources()
        {
            return resources;
        }

        public void SetID(int id)
        {
            ID = id;
        }
    }
}
