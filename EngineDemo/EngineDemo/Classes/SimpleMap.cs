using EngineDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    public enum MapType { Square, Hexagon };
    public class SimpleMap : IMap
    {
        int xSize = -1, ySize = -1;
        MapType T;
        public SimpleMap()
        {
        }
        public int GetX()
        {
            return xSize;
        }
        public int GetY()
        {
            return ySize;
        }
        public bool CanInclude(ICoordinate position)
        {
            int x = position.GetX(), y = position.GetY();
            if(x > -1 && y > -1 && x < xSize && y < ySize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetSize(int x, int y)
        {
            xSize = x;
            ySize = y;
        }

        public void SetType(MapType T)
        {
            this.T = T;
        }

        public int GetYSize()
        {
            return ySize;
        }

        public int GetXSize()
        {
            return xSize;
        }
        //public bool CanInclude(IMapObject)
    }
}
