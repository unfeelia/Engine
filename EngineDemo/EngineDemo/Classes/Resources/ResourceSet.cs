using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    class ResourceSet: ModelObjectDemo, IResourceSet
    {
        IDictionary<string, int> Resources;
        int ID;

        public ResourceSet(ICoordinate coor): base(coor)
        {
            Resources = new Dictionary<string, int>();
        }
        public ResourceSet(ICoordinate coor, Dictionary<string, int> res): base(coor)
        {
            Resources = new Dictionary<string, int>();
            foreach(var temp in res)
            {
                Resources.Add(temp.Key, temp.Value);
            }
        }

        public void AddResource(string type, int quantity)
        {
            if(Resources.ContainsKey(type))
            {
                Resources[type] += quantity;
            }
            else
            {
                Resources.Add(type, quantity);
            }
        }
        public void AddResources(IDictionary<string, int> resources)
        {
            foreach(var temp in resources)
            {
                AddResource(temp.Key, temp.Value);
            }
        }

        public void SetResource(string type, int quantity)
        {
            if (Resources.ContainsKey(type))
            {
                Resources[type] = quantity;
            }
            else
            {
                Resources.Add(type, quantity);
            }
        }
        public void SetResources(IDictionary<string, int> resources)
        {
            foreach (var temp in resources)
            {
                SetResource(temp.Key, temp.Value);
            }
        }
        
        public IDictionary<string, int> GetResources()
        {
            return Resources;
        }

        public IDictionary<string, int> ExtractResources()
        {
            var temp = Resources;
            Resources = new Dictionary<string, int>();
            return temp;
        }
    }
}
