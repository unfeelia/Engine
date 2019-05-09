using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes.Resources
{
    class ResourceTypeDemo : IResourceType
    {
        string Name;
        public ResourceTypeDemo(string name)
        {
            Name = name;
        }
        public string GetName()
        {
            return Name;
        }
        ~ResourceTypeDemo()
        {

        }
    }
}
