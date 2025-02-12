using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager._06._DB;

namespace TextRPG_Maple._04._Manager._05._Object
{
    enum ObjectType
    {
        PLAYER,
        MONSTER,
        BOSS,
        EQUIPMENT,
        END
    }

    internal class GameObjectManager
    {
        private static GameObjectManager? instance;
        public static GameObjectManager Instance => instance ??= new GameObjectManager();

        private readonly Dictionary<ObjectType, Dictionary<string, GameObject>> Objects = new Dictionary<ObjectType, Dictionary<string, GameObject>>();
        private readonly Dictionary<ObjectType, Dictionary<string, GameObject>> PrototypeObjects = new Dictionary<ObjectType, Dictionary<string, GameObject>>();

        private GameObjectManager()
        {
            // ObjectType 개수만큼 초기화
            foreach (ObjectType type in Enum.GetValues(typeof(ObjectType)))
            {
                if (type != ObjectType.END)
                    Objects[type] = new Dictionary<string, GameObject>();
            }
        }

        //========================================================================================================
        // 게임 오브젝트 관련
        //========================================================================================================
        public void AddGameObject(ObjectType type, string name, GameObject obj)
        {
            if (!Objects.ContainsKey(type))
                Objects[type] = new Dictionary<string, GameObject>();

            if (!Objects[type].ContainsKey(name))
                Objects[type][name] = obj;
            else
                Console.WriteLine($"Error : ObjectManager::AddGameObject, {type}의 {name}이 이미 존재함");
        }

        public GameObject GetGameObject(ObjectType type, string name)
        {
            if (Objects.ContainsKey(type) && Objects[type].ContainsKey(name))
                return Objects[type][name];

            return null;
        }
        public bool RemoveGameObject(ObjectType type, string name)
        {
            if (Objects.ContainsKey(type) && Objects[type].ContainsKey(name))
            {
                Objects[type].Remove(name);
                return true;
            }
            return false;
        }

        //========================================================================================================
        // 프로토타입 관련
        //========================================================================================================
        public void AddPrototypeObject(ObjectType type, string name, GameObject obj)
        {
            if (!PrototypeObjects.ContainsKey(type))
                PrototypeObjects[type] = new Dictionary<string, GameObject>();

            if (!PrototypeObjects[type].ContainsKey(name))
                PrototypeObjects[type][name] = obj;
            else
                Console.WriteLine($"Error : ObjectManager::AddPrototypeObject, {type}의 {name}이 이미 존재함");
        }
        public GameObject? ClonePrototypeObject(ObjectType type, string name)
        {
            GameObject? cloneObject = null;
            if (PrototypeObjects.ContainsKey(type) && PrototypeObjects[type].ContainsKey(name))
                cloneObject = PrototypeObjects[type][name];

            if(cloneObject != null)
                cloneObject = cloneObject.Clone();

            return cloneObject;
        }

    }
}
