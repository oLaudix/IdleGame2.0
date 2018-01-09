using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Unit : Entity
    {
        public double purchaseCost;
        public double currentDPS;
        public double nextUpgradeCost;
        public double nextLevelDPSDiff;
        public UnitSkill nextSkillToUnlock;
        public List<UnitSkill> unitSkills;
        public string icon;
        public int heroID;
        public string name;
        public int level;
        public int NumLevelsToUnlock = 0;
        MainScene scene = (MainScene)MainScene.Instance;

        public Unit(int heroID, string name, double purchaseCost, string icon)
        {
            unitSkills = new List<UnitSkill>();
            this.heroID = heroID;
            this.name = name;
            this.purchaseCost = purchaseCost;
            this.level = 0;
            this.icon = icon;
            GetUnitSkills();
            this.nextSkillToUnlock = GetNextSkill();
            UpdateUnitStats();
        }

        public int GetNumLevelsToUnlockByGivenGoldAmount()
        {
            int counter = 0;
            int tmplevel = this.level;
            double tmpgold = scene.player.gold;
            while (tmpgold > GetUpgradeCostByLevel(tmplevel))
            {
                tmpgold -= GetUpgradeCostByLevel(tmplevel);
                counter++;
                tmplevel++;
                //Console.WriteLine(GetUpgradeCostByLevel(tmplevel));
            }
            return counter;
        }

        public bool IsEvolved()
        {
            return this.level >= 1001;
        }

        void GetUnitSkills()
        {
            foreach (var Skill in scene.skillList)
            {
                if (Skill.ownerID == this.heroID)
                {
                    unitSkills.Add(Skill);
                }
            }
        }

        public void UpgradeHero(int iLevels = 1)
        {
            if (!this.IsEvolved() && this.level + iLevels >= 1001)
            {
                //this.EvolveHero(false);
            }

            if (this.level == 0)
            {
                switch(this.heroID)
                {
                    case 1:
                        new Soldier(1280, 510);
                        break;
                    case 2:
                        new Sniper(1112, 610);
                        break;
                    case 3:
                        new Mortar(1730, 722);
                        break;
                    case 4:
                        new Turret(930, 500);
                        break;
                    case 5:
                        new Dicokka(1550, 500);
                        break;
                    case 6:
                        new FatTonk(1500, 550);
                        break;
                    case 7:
                        new Minigun(1320, 700);
                        break;
                    case 8:
                        new BigTonk(1460, 480);
                        break;
                    case 9:
                        new BiggestTonk(1770, 660);
                        break;
                    case 10:
                        new Heli(1080, 280);
                        break;
                    case 11:
                        new Hover(1450, 320);
                        break;
                    case 12:
                        new Rocket(1700, 550);
                        break;
                    default:
                        break;
                }
            }
            this.level += iLevels;
            //this.UpdateNextToBeBoughtSkill();
            this.UpdateUnitStats();
            scene.needUpdate = true;
        }

        public void UpdateUnitStats()
        {
            if (this.level == nextSkillToUnlock.requiredLevel)
            {
                nextSkillToUnlock.isUnlocked = true;
                nextSkillToUnlock = GetNextSkill();
            }
            this.currentDPS = GetDPSByLevel(this.level);
            this.nextUpgradeCost = GetUpgradeCostByLevel(this.level);
            if (this.level + 1 == nextSkillToUnlock.requiredLevel)
            {
                double tmpDPS = GetDPSByLevel(this.level);
                nextSkillToUnlock.isUnlocked = true;
                this.nextLevelDPSDiff = GetDPSByLevel(this.level + 1) - tmpDPS;
                nextSkillToUnlock.isUnlocked = false;
            }
            else
                this.nextLevelDPSDiff = GetDPSByLevel(this.level + 1) - GetDPSByLevel(this.level);
        }

        public UnitSkill GetNextSkill()
        {
            UnitSkill skill = new UnitSkill(100, BonusType.AllDamage, 0, 0);
            foreach (var Skill in scene.skillList)
            {
                if (Skill.ownerID == this.heroID && !Skill.isUnlocked)
                {
                    return Skill;
                }
            }
            return skill;
        }

        public double GetDPSByLevel(int iLevel)
        {
            double num2;
            if (this.IsEvolved())
            {
                num2 = (double)Math.Pow((double)0.904f, (double)(iLevel - 1001)) * (double)Math.Pow((double)(1f - 0.019f * 15f), (double)(this.heroID + 33));
            }
            else
            {
                num2 = (double)Math.Pow((double)0.904f, (double)(iLevel - 1)) * (double)Math.Pow((double)(1f - 0.019f * Math.Min((double)this.heroID, 15f)), (double)this.heroID);
            }
            double num3;
            if (this.IsEvolved())
            {
                num3 = num2 * 0.1f * this.GetBaseUpgradeCostByLevel(iLevel - 1) * (double)(Math.Pow((double)1.075f, (double)(iLevel - (1001 - 1))) - 1.0) / (1.075f - 1f);
            }
            else
            {
                num3 = num2 * 0.1f * this.GetBaseUpgradeCostByLevel(iLevel - 1) * (double)(Math.Pow((double)1.075f, (double)iLevel) - 1.0) / (1.075f - 1f);
            }
            
            return num3 * (1.0f + scene.GetHeroAdditionlDamage(this.heroID)) * (1.0f + scene.Bonuses[BonusType.AllDamage]) * (1.0f + scene.GetBonusArtifactDamage());
        }

        private double GetBaseUpgradeCostByLevel(int iLevel)
        {
            return this.GetHeroBaseCost(iLevel) * (double)Math.Pow((double)1.075f, (double)iLevel);
        }

        private double GetHeroBaseCost(int iLevel = -1)
        {
            double num = this.purchaseCost;
            if (iLevel == -1)
            {
                iLevel = this.level;
            }
            if (iLevel >= 1001 - 1)
            {
                num *= 10f;
            }
            return num;
        }

        public double GetUpgradeCostByLevel(int iLevel)
        {
            double baseUpgradeCostByLevel = this.GetBaseUpgradeCostByLevel(iLevel);
            double a = baseUpgradeCostByLevel * (1.0 - scene.Bonuses[BonusType.UpgradeCost]);
            return (double)Math.Ceiling(a);
        }
    }
}
