using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._05._Usable.Skill
{
    enum SkillType
    {
        전사,
        도적
    }

    internal class Skill : Usable
    {
        public SkillType SkillType { get; }
        public int NeedLv { get; set; }

        public Skill(string name, int needLv, SkillType skillType, float value, string descrip, int cost, bool IsOwned) : base(name, value, descrip, cost, IsOwned)
        {
            SkillType = skillType;
            NeedLv = needLv;
        }

        public void SetSkillType()
        {
            Player? player = GameObjectManager.Instance.GetGameObject(ObjectType.PLAYER, "MainPlayer") as Player;
            switch (player.Class)
            {
                case "전사":
                    break;
                case "도적":
                    break;
                case "마법사":
                    break;
            }
        }

        public override string GetTypeString()
        {
            return SkillType.ToString();
        }




        // 하드 코딩, 정적으로 만들어야 스택 오버플로우 미발생 << SKillType 리스트를 초기화 하면서 Skill 객체가 생성되고, Skill 객체들도 Type리스트를 만듦
        public static List<Skill> Warrior_skillSet = new List<Skill>
        {
            new Skill("슬래시 블러스트", 3, SkillType.전사, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10, false),
            new Skill("브랜디쉬", 6, SkillType.전사, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20, false),
            new Skill("오라블레이드", 9, SkillType.전사, 3f, "계수 3배.", 0, false)
        };

        public static List<Skill> Thief_skillSet = new List<Skill>
        {
            new Skill("스킬명1", 3, SkillType.도적, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10, false),
            new Skill("스킬명2", 6, SkillType.도적, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20, false),
            new Skill("스킬명3", 9, SkillType.도적, 0.5f, "살살 때립니다.", 0, false)
        };
    }
}
