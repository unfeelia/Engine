using EngineDemo.Classes;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Interfaces
{
    public interface IModel
    {
        //utility finctions
        void SetMap(MapType T, int x, int y);
        void SetMapType(MapType T);
        void SetMapSize(int xNew, int yNew);

        void AddResourceType(string resourceTypeName);
        void RemoveResourceType(string resourceTypeName);

        int AddObject(IModelObject modelObject);
        int AddObject(ICollectable collectableObject);
        int AddObject(IMobile mobileObject);

        void RemoveObject(int id);
        void RemoveObject(IModelObject modelObject);

        int AddPlayer(IPlayer player);
        void RemovePlayer(int id);



        //ingame functions
        void SetDestination(IMobile iM, ICoordinate to);
        void EndTurn(int PlayerId);
        
        //void Iteration();

        //void AddObjectType(Type t);
    }
}
