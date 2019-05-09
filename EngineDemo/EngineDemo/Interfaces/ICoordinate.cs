using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces
{
    public interface ICoordinate
    {
        int GetX();
        int GetY();
        int GetZ();
        bool Equal(ICoordinate coordinate);
        ICoordinate Clone();
    }
}
