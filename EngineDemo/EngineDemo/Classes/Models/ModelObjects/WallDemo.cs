using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes.Models.ModelObjects
{
    public class WallDemo: ModelObjectDemo, IWall
    {
        public WallDemo(ICoordinate coordinate) : base(coordinate)
        {

        }
    }
}
