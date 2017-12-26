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
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to all power!!";
                    break;
                case BonusType.HeroDamage:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to this thingy power!";
                    break;
                case BonusType.CriticalDamage:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to critical strike power!";
                    break;
                case BonusType.CriticalChance:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to critical strike chance!";
                    break;
                case BonusType.PlayerDamage:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to players power!";
                    break;
                case BonusType.PlayerDamageDPS:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% of army power added to players power!!";
                    break;
                case BonusType.ChestGold:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% more dosh from bonus rounds";
                    break;
                case BonusType.AllGold:
                    text = "Level " + requiredLevel + ": " + Math.Round(this.magnitude * 100) + "% to all dosh gained";
                    break;
                default:
                    text = "Level " + requiredLevel + ":  " + Math.Round(this.magnitude * 100) + "%";
                    break;
            }
            return text;
        }
    }
}
