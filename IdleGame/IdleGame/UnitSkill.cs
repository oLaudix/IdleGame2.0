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
        public double unlockCost;
        public double magnitude;
        public int requiredLevel;
        public int ownerID;
        public bool isUnlocked;
        public BonusType bonusType;
        public UnitSkill(int ownerID, BonusType bonusType, double magnitude, int requiredLevel, double unlockCost)
        {
            this.isUnlocked = false;
            //this.description = description;
            this.bonusType = bonusType;
            this.magnitude = magnitude;
            this.requiredLevel = requiredLevel;
            this.unlockCost = unlockCost;
            this.ownerID = ownerID;
        }
    }
}
