using EngineDemo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces
{
    public interface IMap
    {
        bool CanInclude(ICoordinate position);
        void SetSize(int x, int y);
        void SetType(MapType T);
        int GetYSize();
        int GetXSize();
    }
}
