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
    class ModelDemo2 : IModel
    {
        IMap Map = null;
        IDictionary<int, IPlayer> Players = new Dictionary<int, IPlayer>();
        IDictionary<int, IModelObject> Objects = new Dictionary<int, IModelObject>();
        IDictionary<int, ICollectable> CollectableObjects = new Dictionary<int, ICollectable>();
        IDictionary<int, IMobile> MobileObjects = new Dictionary<int, IMobile>();
        IList<Type> modelObjectsTypes = new List<Type>();

        IList<string> ResourceTypes = new List<string>();
        int PlayersCount = 0;
        int ObjectsCount = 0;

        public IList<string> GetResources()
        {
            return ResourceTypes;
        }
        public IDictionary<int, IModelObject> GetObjects()
        {
            return Objects;
        }
        public IDictionary<int, IPlayer> GetPlayers()
        {
            return Players;
        }


        public int AddObject(Type T, string json)
        {
            var temp = JsonConvert.DeserializeObject<ModelObjectDemo>(json);
            //(ModelObjectDemo)temp;
            ObjectsCount++;
            temp.SetID(ObjectsCount);
            temp.Parent = this;
            Objects.Add(new KeyValuePair<int, IModelObject>(temp.GetID(), temp));
            return temp.GetID();
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
            if (Objects.ContainsKey(id))
            {
                Objects.Remove(id);
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

        public void SetMapWidth(int x)
        {
            Map.SetSize(x, Map.GetYSize());
        }

        public void SetMapHeight(int y)
        {
            Map.SetSize(Map.GetXSize(), y);
        }

        public int AddObject(IModelObject modelObject)
        {
            ObjectsCount++;
            modelObject.SetID(ObjectsCount);
            Objects.Add(new KeyValuePair<int, IModelObject>(modelObject.GetID(), modelObject));
            return modelObject.GetID();
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

        public void SetDestination(int objectID, ICoordinate to)
        {
            bool canStep = true;
            List<IModelObject> objAtPos = new List<IModelObject>();

            foreach (var col in Objects.Values)
            {
                if (col.GetPosition().Equal(to))
                {
                    objAtPos.Add(col);
                    if(col is IWall)
                    {
                        canStep = false;
                        return;
                    }
                }
            }
            if(canStep)
            {
                //MobileObjects[objectID].Move(to);
                for (int i = 0; i < objAtPos.Count; ++i)
                {
                    //for(int j = 0; j < modelObjectsTypes.Count; ++j)
                    //{
                    //    Type t = modelObjectsTypes[j];
                    //    string temp = objAtPos[i].GetType().Name;
                    //    if (objAtPos[i].GetType().GetInterfaces().ToList().Contains(t))
                    //    {
                    //        Interract(MobileObjects[objectID], objAtPos[i] as t);
                    //    }
                    //}
                    if(objAtPos[i] is IResourceSet)
                    {
                        Interract(MobileObjects[objectID], (IResourceSet)objAtPos[i]);
                    }
                    else
                    if (objAtPos[i] is IOwnable)
                    {
                        Interract(MobileObjects[objectID], (IOwnable)objAtPos[i]);
                    }
                }
            }
            Iter();
        }

        public void RemoveObject(IModelObject modelObject)
        {
            Objects.Remove(modelObject.GetID());
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

        public void Iteration()
        {
            throw new NotImplementedException();
        }

        private void Iter()
        {
            foreach(var obj1 in MobileObjects)
            {
                foreach (var obj2 in Objects)
                {
                    if(obj1.Key == obj2.Key)
                    {
                        continue;
                    }
                    if (obj2.Value.Disable)
                    {
                        continue;
                    }
                    if (obj1.Value.GetPosition().Equal(obj2.Value.GetPosition()))
                    {
                        if(obj2.Value is IResourceSet)
                        {
                            IResourceSet temp = (IResourceSet)obj2.Value;
                            Players[obj1.Value.GetOwnerID()].AddResources(temp.GetResources());
                            obj2.Value.Disable = true;
                            //ObjectsByPosition.Remove(obj2.Key);
                        }
                        //obj1.Value.Interract(obj2.Value);
                    }
                }
            }
        }

        public void AddObjectType(Type t)
        {
                modelObjectsTypes.Add(t);
        }

        public void SetMapSize(int xNew, int yNew)
        {
            throw new NotImplementedException();
        }

        public void SetDestination(IMobile iM, ICoordinate to)
        {
            throw new NotImplementedException();
        }

        public void EndTurn(int PlayerId)
        {
            throw new NotImplementedException();
        }
    }
}
