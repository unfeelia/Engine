using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces
{
    public interface IModelObject
    {
        IModel Parent { get; set; }
        bool Disable { get; set; }
        ICoordinate GetPosition();
        void SetID(int id);
        int GetID();
        void Interract(IModelObject modelObject);
    }
}
