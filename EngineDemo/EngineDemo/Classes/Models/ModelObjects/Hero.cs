using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes
{
    class Hero : ModelObjectDemo, IMobile
    {
        private IList<ICoordinate> _Path = new List<ICoordinate>();
        public IList<ICoordinate> Path
        {
            get
            {
                IList<ICoordinate> path = new List<ICoordinate>();
                for(int i = 0; i < path.Count; ++i)
                {
                    path.Add(_Path[i].Clone());
                }
                return path;
            }
            set
            {
                _Path = new List<ICoordinate>();
                for (int i = 0; i < value.Count; ++i)
                {
                    _Path.Add(value[i].Clone());
                }
            }
        }
        
        protected int DefaultMovePoints = 5;
        protected int MovePoints = 5;

        public Hero(ICoordinate coordinate) : base(coordinate)
        {

        }
        public void Move()
        {
            if(MovePoints > 0 && _Path.Count > 0)
            {
                base.coordinates = _Path[0];
                MovePoints--;
                _Path.RemoveAt(0);
            }
            //while(MovePoints > 0 && _Path.Count > 0)
            //{
            //}
        }
        public override void Interract(IModelObject modelObject)
        {
            if(modelObject is ICollectable)
            {
                if (modelObject is IResourceSet)
                {
                    IResourceSet myTest = modelObject as IResourceSet;
                    myTest.GetResources();
                }
            }
        }

        public bool CanMove()
        {
            return Path.Count > 0 && MovePoints > 0;
        }
    }
}
