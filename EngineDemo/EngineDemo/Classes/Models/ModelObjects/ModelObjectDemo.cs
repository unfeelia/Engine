using EngineDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    public class ModelObjectDemo: IModelObject
    {
        protected int OwnerId = 0;
        protected ICoordinate coordinates;
        private IModel _Parent;
        public IModel Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                if(_Parent == null)
                {
                    _Parent = value;
                }
                else
                {
                    throw new Exception("");
                }
            }
        }

        int ID;
        public bool Disable { get; set; }
        
        public ModelObjectDemo(ICoordinate coordinate)
        {
            coordinates = coordinate;
        }
        public void SetOwnerID(int id)
        {
            OwnerId = id;
        }
        public ICoordinate GetPosition()
        {
            return coordinates;
        }

        public int GetOwnerID()
        {
            return OwnerId;
        }

        public void SetID(int id)
        {
            ID = id;
        }

        public int GetID()
        {
            return ID;
        }

        public virtual void Interract(IModelObject modelObject)
        {
            return;
        }
    }
}
