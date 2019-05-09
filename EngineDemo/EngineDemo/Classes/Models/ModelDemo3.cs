using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDemo.Classes.Models
{
    class ModelDemo3 : IModel
    {
        IMap Map = null;
        IDictionary<ICoordinate, IModelObject> ObjectsByPosition = new Dictionary<ICoordinate, IModelObject>();
        IDictionary<int, IModelObject> ObjectsByID = new Dictionary<int, IModelObject>();

        IList<string> ResourceTypes = new List<string>();
        IDictionary<int, ICollectable> CollectableObjects = new Dictionary<int, ICollectable>();
        IDictionary<int, IMobile> MobileObjects = new Dictionary<int, IMobile>();

        IDictionary<int, IPlayer> Players = new Dictionary<int, IPlayer>();

        int PlayersCount = 0;
        int ObjectsCount = 0;

        public ModelDemo3()
        {
            Map = new SimpleMap();
            Map.SetSize(1, 1);
            Map.SetType(MapType.Square);
        }
        public ModelDemo3(IMap map)
        {
            Map = map;
        }
        public ModelDemo3(MapType mt, int x, int y)
        {
            Map = new SimpleMap();
            Map.SetSize(x, y);
            Map.SetType(mt);
        }
        public IList<string> GetResourceTypes()
        {
            return ResourceTypes;
        }
        public IDictionary<int, IModelObject> GetObjects()
        {
            var objs = new Dictionary<int, IModelObject>();
            foreach (var val in ObjectsByPosition.Values) 
            {
                objs.Add(val.GetID(), val);
            }
            return objs;
        }
        public IDictionary<int, IPlayer> GetPlayers()
        {
            return Players;
        }

        public int AddPlayer()
        {
            var temp = new Player();
            PlayersCount++;
            temp.SetID(PlayersCount);
            foreach(var res in ResourceTypes)
            {
                temp.AddResourceType(res);
            }
            Players.Add(new KeyValuePair<int, IPlayer>(temp.GetID(), temp));
            return temp.GetID();
        }

        public void AddResourceType(string resourceTypeName)
        {
            ResourceTypes.Add(resourceTypeName);
            foreach(var pl in Players.Values)
            {
                pl.AddResourceType(resourceTypeName);
            }
        }

        public void RemoveObject(int id)
        {
            if (ObjectsByID.ContainsKey(id))
            {
                ObjectsByPosition[ObjectsByID[id].GetPosition()] = null;
                ObjectsByID.Remove(id);
            }
        }

        public void RemovePlayer(int id)
        {
            if (Players.ContainsKey(id))
            {
                Players.Remove(id);
            }
        }

        public void RemoveResourceType(string resourceTypeName)
        {
            if (ResourceTypes.Contains(resourceTypeName))
            {
                ResourceTypes.Remove(resourceTypeName);
            }
        }

        public void SetMap(MapType T, int x, int y)
        {
            if (Map == null)
            {
                IMap map = new SimpleMap();
                map.SetSize(x, y);
                map.SetType(T);
                this.Map = map;
            }
            else
            {
                Map.SetSize(x, y);
                Map.SetType(T);
            }
            return;
        }

        public void SetMapType(MapType T)
        {
            Map.SetType(T);
        }

        public void SetMapSize(int xNew, int yNew)
        {
            int xOld = Map.GetXSize();
            int yOld = Map.GetYSize();

            Map.SetSize(xNew, yNew);

            var positions = ObjectsByPosition.Keys.ToList();
            for(int i = 0; i < positions.Count; ++i)
            {
                if (!Map.CanInclude(positions[i]))
                {
                    RemoveObject(ObjectsByPosition[positions[i]]);
                }
            }
        }

        public int AddObject(IModelObject modelObject)
        {
            if (!ObjectsByPosition.ContainsKey(modelObject.GetPosition()))
            {
                ObjectsCount++;
                modelObject.SetID(ObjectsCount);
                modelObject.Parent = this;
                ObjectsByID.Add(new KeyValuePair<int, IModelObject>(modelObject.GetID(), modelObject));
                ObjectsByPosition.Add(modelObject.GetPosition(), modelObject);
                return modelObject.GetID();
            }
            else
            {
                throw new Exception("Duplicate Position Object.");
            }
        }

        public int AddObject(ICollectable collectableObject)
        {
            AddObject((IModelObject)collectableObject);
            CollectableObjects.Add(new KeyValuePair<int, ICollectable>(collectableObject.GetID(), collectableObject));
            return collectableObject.GetID();
        }

        public int AddObject(IMobile mobileObject)
        {
            AddObject((IModelObject)mobileObject);
            MobileObjects.Add(new KeyValuePair<int, IMobile>(mobileObject.GetID(), mobileObject));
            return mobileObject.GetID();
        }

        public int AddPlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void SetDestination(IMobile obj, ICoordinate to)
        {
            if(!obj.GetPosition().Equal(to))
            {
                var path = PathFinder(obj.GetPosition(), to);
                if (path != null && path.Count > 0)
                {
                    for (int i = 0; i < path.Count; ++i)
                    {
                        obj.Path = path;
                    }
                }
            }
        }

        public IList<string> GetResources()
        {
            return ResourceTypes;
        }

        public void RemoveObject(IModelObject modelObject)
        {
            ObjectsByID.Remove(modelObject.GetID());
            ObjectsByPosition.Remove(modelObject.GetPosition());
        }

        public void Interract(IModelObject from, IModelObject to)
        {
            throw new NotImplementedException();
        }
        public void Interract(IMobile from, IResourceSet to)
        {
            if(to.Disable)
            {
                return;
            }
            else
            {
                Players[from.GetOwnerID()].AddResources(to.ExtractResources());
                //ObjectsByPosition.Remove(to.GetID());
                to.Disable = true;
            }
        }
        public void Interract(IMobile from, IOwnable to)
        {
            if (to.Disable)
            {
                return;
            }
            else
            {
                to.SetOwnerID(from.GetOwnerID());
            }
        }
        public void Interract(int fromId)
        {
            var obj = MobileObjects[fromId];
            var Player = Players[obj.GetOwnerID()];

            foreach(var col in CollectableObjects)
            {
                if(!col.Value.Disable && col.Value.GetPosition().Equal(obj.GetPosition()))
                {
                    Players[obj.GetOwnerID()].Collect(col.Value);
                    //obj.Collect(col.Value);
                    col.Value.Disable = true;
                }
            }

            foreach (var anotherObj in ObjectsByID.Values)
            {
                if (!anotherObj.Disable && anotherObj.GetPosition().Equal(obj.GetPosition()))
                {
                    if (anotherObj is IOwnable)
                    {
                        ((IOwnable)anotherObj).SetOwnerID(Player.GetID());
                    }
                }
            }
        }

        public void Iteration()
        {
            foreach(var mob in MobileObjects.Keys)
            {
                Interract(mob);
            }
        }

        private void Iter()
        {
            
        }

        public void AddObjectType(Type t)
        {
                //modelObjectsTypes.Add(t);
        }

        public IList<ICoordinate> PathFinder(ICoordinate from, ICoordinate to)
        {
            if(!Map.CanInclude(to))
            {
                return new List<ICoordinate>();
            }
            return new List<ICoordinate>() { to };
        }

        public void EndTurn(int PlayerId)
        {
            foreach(IMobile mob in MobileObjects.Values)
            {
                while(mob.CanMove())
                {
                    mob.Move();

                }
            }
        }
    }
}
