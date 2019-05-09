using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces.ModelObjectInterfaces
{
    public interface IResourceSet : ICollectable
    {
        void AddResource(string type, int quantity);
        void AddResources(IDictionary<string, int> resources);

        void SetResource(string type, int quantity);
        void SetResources(IDictionary<string, int> resources);

        IDictionary<string, int> GetResources();
        IDictionary<string, int> ExtractResources();
    }
}
