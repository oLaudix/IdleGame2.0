using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame
{
    class UnitSkill
    {
        public string description;
        public double magnitude;
        public int requiredLevel;
        public int ownerID;
        public bool isUnlocked;
        public BonusType bonusType;
        public UnitSkill(int ownerID, BonusType bonusType, double magnitude, int requiredLevel)
        {
            this.isUnlocked = false;
            //this.description = description;
            this.bonusType = bonusType;
            this.magnitude = magnitude;
            this.requiredLevel = requiredLevel;
            this.ownerID = ownerID;
            this.description = GetDescritopion();
        }

        public string GetDescritopion()
        {
            string text = "";
            switch(this.bonusType)
            {
                case BonusType.AllDamage:
                    text = "Level " + requiredLevel + ": All damage increased by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.HeroDamage:
                    text = "Level " + requiredLevel + ": Increases this unit/research damage by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.CriticalDamage:
                    text = "Level " + requiredLevel + ": Increases Critical Damage by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.CriticalChance:
                    text = "Level " + requiredLevel + ": Increases Critical Chance by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.PlayerDamage:
                    text = "Level " + requiredLevel + ": Increase player damage by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.PlayerDamageDPS:
                    text = "Level " + requiredLevel + ": Increase player damage by " + Math.Round(this.magnitude * 100) + "% of army total damage";
                    break;
                case BonusType.ChestGold:
                    text = "Level " + requiredLevel + ": Increase Manpower gained from bonus rounds by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                case BonusType.AllGold:
                    text = "Level " + requiredLevel + ": Increase all Manpower gained by " + Math.Round(this.magnitude * 100) + "%";
                    break;
                default:
                    text = "Level " + requiredLevel + ":  " + Math.Round(this.magnitude * 100) + "%";
                    break;
            }
            return text;
        }
    }
}
