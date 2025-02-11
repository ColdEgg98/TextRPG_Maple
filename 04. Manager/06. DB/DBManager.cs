using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Maple._04._Manager._05._Object;
using TextRPG_Maple._06._DB;

namespace TextRPG_Maple._04._Manager._06._DB
{
    // 파일로 부터 관련 자료를 읽어오는 매니저
    internal class DBManager
    {
        private static DBManager? _instance;
        public static DBManager Instance => _instance ??= new DBManager();

        // !!!임의로 값을 넣어둠. 나중에 CSV로 교체할 예정
        private Dictionary<string, MonsterData> monsterData = new Dictionary<string, MonsterData>
        {
            { "고블린", new MonsterData("고블린", 50, 5, 2) },
            { "오크", new MonsterData("오크", 80, 10, 5) },
            { "늑대", new MonsterData("늑대", 60, 7, 3) }
        };

        public MonsterData? GetMonsterData(string name)
        {
            if (monsterData.TryGetValue(name, out MonsterData? data))
            {
                return data;
            }
            return null; // 존재하지 않는 몬스터일 경우
        }

        // 마찬가지로 아이템도 이렇게 가져올 예정
    }
}
