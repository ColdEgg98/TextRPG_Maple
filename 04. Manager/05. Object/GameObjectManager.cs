using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Maple._04._Manager._05._Object
{
    enum ObjectType
    {
        PLAYER,
        MONSTER,
        EQUIPMENT,
        END
    }

    internal class GameObjectManager
    {
        private static GameObjectManager? _instance;
        public static GameObjectManager Instance => _instance ??= new GameObjectManager();

        private readonly Dictionary<ObjectType, Dictionary<string, GameObject>> _objects;

        private GameObjectManager()
        {
            _objects = new Dictionary<ObjectType, Dictionary<string, GameObject>>();

            // ObjectType 개수만큼 초기화
            foreach (ObjectType type in Enum.GetValues(typeof(ObjectType)))
            {
                if (type != ObjectType.END)
                    _objects[type] = new Dictionary<string, GameObject>();
            }
        }

        public void AddGameObject(ObjectType type, string name, GameObject obj)
        {
            if (!_objects.ContainsKey(type))
                _objects[type] = new Dictionary<string, GameObject>();

            if (!_objects[type].ContainsKey(name))
                _objects[type][name] = obj;
            else
                Console.WriteLine($"Error : ObjectManager::AddGameObject, {type}의 {name}이 이미 존재함");
        }

        public GameObject GetGameObject(ObjectType type, string name)
        {
            if (_objects.ContainsKey(type) && _objects[type].ContainsKey(name))
                return _objects[type][name];

            return null;
        }

        public bool RemoveGameObject(ObjectType type, string name)
        {
            if (_objects.ContainsKey(type) && _objects[type].ContainsKey(name))
            {
                _objects[type].Remove(name);
                return true;
            }
            return false;
        }
    }
}
