using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes.Models.ModelObjects
{
    class Shahta : ModelObjectDemo, IOwnable
    {
        public Shahta(ICoordinate coordinate) : base(coordinate)
        {
        }
        public override void Interract(IModelObject modelObject)
        {
            return;
        }
    }
}
