using TextRPG_Maple._04._Manager;

namespace TextRPG_Maple._05._Usable.Skill
{
    enum SkillType
    {
        WarriorSkill,
        ThiefSkill
    }

    internal class Skill : Usable
    {
        public SkillType SkillType { get; set; }
        public int NeedLv { get; set; }

        public Skill(string name, int needLv, SkillType skillType, float value, string descrip, int cost) : base(name, value, descrip, cost)
        {
            SkillType = skillType;
            NeedLv = needLv;
        }

        public void SetSkillType()
        {
            switch(GameManager.Instance.player.Class)
            {
                case "전사":
                    //스킬타입을 워리어로
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
            new Skill("슬래시 블러스트", 3, SkillType.WarriorSkill, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10),
            new Skill("브랜디쉬", 6, SkillType.WarriorSkill, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20),
            new Skill("오라블레이드", 9, SkillType.WarriorSkill, 3f, "계수 3배.", 0)
        };

        public static List<Skill> Thief_skillSet = new List<Skill>
        {
            new Skill("스킬명1", 3, SkillType.ThiefSkill, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10),
            new Skill("스킬명2", 6, SkillType.ThiefSkill, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20),
            new Skill("스킬명3", 9, SkillType.ThiefSkill, 0.5f, "살살 때립니다.", 0)
        };
    }
}
