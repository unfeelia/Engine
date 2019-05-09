using EngineDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    class SimpleSquareCoordinate : ICoordinate
    {
        int x, y, z;

        public SimpleSquareCoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public ICoordinate Clone()
        {
            return new SimpleSquareCoordinate(x, y, z);
        }

        public bool Equal(ICoordinate coordinate)
        {
            return (x == coordinate.GetX() && y == coordinate.GetY());
        }

        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public int GetZ()
        {
            return z;
        }
        void Move(ICoordinate to)
        {
            x = to.GetX();
            y = to.GetY();
            z = to.GetZ();
        }
    }
}
