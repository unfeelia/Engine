using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces.ModelObjectInterfaces
{
    public interface IOwnable: IModelObject
    {
        void SetOwnerID(int id);
        int GetOwnerID();
    }
}
