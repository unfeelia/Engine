using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces.ModelObjectInterfaces
{
    public interface IMobile : IOwnable
    {
        void Move();
        IList<ICoordinate> Path { get; set; }
        bool CanMove();
    }
}
