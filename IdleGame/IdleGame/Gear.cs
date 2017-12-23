using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class Gear
    {
        public int level;
        public int maxLevel;
        public BonusType bonusType;
        public double bonusPerLevel;
        public BonusType bonusType2;
        public double bonusPerLevel2;
        public double damageBonusBase;
        public double costCoef = 0.7f;
        public double costExpo = 1.5f;
        public bool unlocked;
        public string name;
        public Gear(string name, int maxLevel, BonusType bonusType2, double bonusPerLevel2, double damageBonusBase, double bonusPerLevel)
        {
            this.name = name;
            this.maxLevel = maxLevel;
            this.bonusType = BonusType.AllDamageGear;
            this.bonusPerLevel = bonusPerLevel;
            this.bonusType2 = bonusType2;
            this.bonusPerLevel2 = bonusPerLevel2;
            this.damageBonusBase = damageBonusBase;
            this.unlocked = false;
            this.level = 1;
        }


        public double LevelUpCost()
        {
            return Math.Round((double)this.costCoef * Math.Pow((double)(this.level + 1), (double)this.costExpo));
        }

        public BonusType GetbonusType()
        {
            return this.bonusType2;
        }

        public double GetDamageBonus()
        {
            return this.damageBonusBase + this.bonusPerLevel * (this.level - 1);
        }

        public double GetBonusMagnitude()
        {
            return bonusPerLevel2 * this.level;
        }

        public void UpgradeGear()
        {
            this.level++;
        }
    }
}
