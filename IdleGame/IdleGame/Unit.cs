using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class Unit
    {
        public double purchaseCost;
        public double currentDPS;
        public double nextUpgradeCost;
        public double nextLevelDPSDiff;
        public double nextToBeBoughtSkillCost;
        public int heroID;
        public string name;
        public int level;

        public Unit(int heroID, string name, double costMultiplier, double purchaseCost)
        {
            this.heroID = heroID;
            this.name = name;
            this.purchaseCost = purchaseCost;
            this.level = 0;
        }

        public bool IsEvolved()
        {
            return this.level >= 1001;
        }

        public void UpgradeHero(int iLevels = 1)
        {
            if (!this.IsEvolved() && this.level + iLevels >= 1001)
            {
                //this.EvolveHero(false);
            }
            this.level += iLevels;
            //this.UpdateNextToBeBoughtSkill();
            //this.UpdateHeroStats(true);
        }

        public double GetDPSByLevel(int iLevel, double heroPassiveStat)
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
            return num3 * (1.0f + heroPassiveStat);
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

        public double GetUpgradeCostByLevel(int iLevel, double upgradeCostStat)
        {
            double baseUpgradeCostByLevel = this.GetBaseUpgradeCostByLevel(iLevel);
            double a = baseUpgradeCostByLevel * (1.0 - upgradeCostStat);
            return (double)Math.Ceiling(a);
        }
    }
}
