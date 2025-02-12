using TextRPG_Maple._04._Manager;
using TextRPG_Maple._04._Manager._05._Object;

namespace TextRPG_Maple._05._Usable.Skill
{
    enum SkillType
    {
        전사,
        도적,
        버프
    }

    internal class Skill : Usable
    {
        public SkillType SkillType { get; }
        public int NeedLv { get; set; }
        public bool isAeraSkill { get; set; }

        public Skill() : base("", 0, "", 0)
        {

        }

        public Skill(string name, int needLv, SkillType skillType, float value, string descrip, int cost, bool isAreaSkill) : base(name, value, descrip, cost)
        {
            isAeraSkill = isAreaSkill;
            SkillType = skillType;
            NeedLv = needLv;
        }

        public List<Skill> SetSkillType(string strClass)
        {
            switch (strClass)
            {
                case "전사":
                    return SetWarriorSkill();
                case "도적":
                    return SetThief_skillSet();
                case "마법사":
                    return SetWarriorSkill();
                default:
                    Console.WriteLine("예외가 발생했습니다.");
                    Thread.Sleep(1000);
                    return null;
            }
        }

        public override string GetTypeString()
        {
            return SkillType.ToString();
        }

        public List<Skill> SetWarriorSkill()
        {
            List<Skill> Warrior_skillSet = new List<Skill>();

            Warrior_skillSet.Add(new Skill("슬래시 블러스트", 3, SkillType.전사, 2, "Mp를 소비하여 주위의 적 다수를 동시에 공격한다.", 10, false));
            Warrior_skillSet.Add(new Skill("브랜디쉬", 5, SkillType.전사, 2.8f, "눈 앞의 적 여러명을 두 번 연속 공격한다.", 16, true));
            Warrior_skillSet.Add(new Skill("인사이징", 8, SkillType.전사, 4f, "정신을 집중하여 전방으로 검을 크게 내리긋는다. ", 36, true));

            return Warrior_skillSet;
        }

        public List<Skill> SetThief_skillSet()
        {
            List<Skill> Thief_skillSet = new List<Skill>();

            Thief_skillSet.Add(new Skill("럭키 세븐", 3, SkillType.도적, 1.1f, "행운을 담은 7개의 표창을 던진다.", 14, false));
            Thief_skillSet.Add(new Skill("트리플 스로우", 6, SkillType.도적, 2.9f, "3개의 표창을 동시에 던진다.", 20, true));
            Thief_skillSet.Add(new Skill("쇼다운 챌린지", 9, SkillType.도적, 7.8f, "살살 때립니다.", 50, false));

            return Thief_skillSet;
        }
    }
}
