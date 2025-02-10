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
            switch(GameManager.player.Class)
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

        public void AddSkill(Player player, Skill addSkill)
        {
            player.Skills.Add(addSkill);
        }

        // 하드 코딩
        public List<Skill> Warrior_skillSet = new List<Skill>
        {
            new Skill("스킬명1", 3, SkillType.WarriorSkill, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10),
            new Skill("스킬명2", 6, SkillType.WarriorSkill, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20),
            new Skill("스킬명3", 9, SkillType.WarriorSkill, 0.5f, "살살 때립니다.", 0)
        };

        public List<Skill> Thief_skillSet = new List<Skill>
        {
            new Skill("스킬명1", 3, SkillType.ThiefSkill, 2, "적에게 공격력의 2배만큼 피해를 입힙니다.", 10),
            new Skill("스킬명2", 6, SkillType.ThiefSkill, 2.5f, "적에게 공격력의 2.5배만큼 피해를 입힙니다.", 20),
            new Skill("스킬명3", 9, SkillType.ThiefSkill, 0.5f, "살살 때립니다.", 0)
        };
    }
}
