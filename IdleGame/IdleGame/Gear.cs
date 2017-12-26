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
        public string icon;
        public Gear(string name, int maxLevel, BonusType bonusType2, double bonusPerLevel2, double damageBonusBase, double bonusPerLevel, string icon)
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
            this.icon = icon;
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

        public string GetDescritopion()
        {
            string text = "";
            switch (this.bonusType2)
            {
                case BonusType.CriticalDamage:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% to critical strike power";
                    break;
                case BonusType.CriticalChance:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% to critical strike chance";
                    break;
                case BonusType.PlayerDamage:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% to player damage";
                    break;
                case BonusType.ChestGold:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% more Dosh from bonus rounds";
                    break;
                case BonusType.AllGold:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% more Dosh";
                    break;
                case BonusType.BerserkerRageDuration:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased Berserker Rage duration";
                    break;
                case BonusType.BerserkerRageCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased Berserker Rage cooldown";
                    break;
                case BonusType.CriticalStrikeDuration:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased Critical Strike duration";
                    break;
                case BonusType.CriticalStrikeCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased Critical Strike cooldown";
                    break;
                case BonusType.HandOfMidasCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased Money Shot duration";
                    break;
                case BonusType.HandOfMidasDuration:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased Money Shot cooldown";
                    break;
                case BonusType.HeavenlyStrikeCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased Artillery Strike cooldown";
                    break;
                case BonusType.ShadowCloneCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased Reinforcements cooldown";
                    break;
                case BonusType.ShadowCloneDuration:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased Reinforcements duration";
                    break;
                case BonusType.WarCryCooldown:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% decreased High Morale cooldown";
                    break;
                case BonusType.WarCryDuration:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased High Morale duration";
                    break;
                case BonusType.BonusRelic:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased Honor gained during Prestige";
                    break;
                case BonusType.AllDamage:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% all power increase";
                    break;
                case BonusType.ChestChance:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased chance for bonus gold from round";
                    break;
                case BonusType.ChanceFor10xGold:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% increased chance for 10 times bonus gold for kill";
                    break;
                case BonusType.UpgradeCost:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% upgrade cost decrease";
                    break;
                case BonusType.MonsterGold:
                    text = Math.Round(GetBonusMagnitude() * 100) + "% more money gained for kills";
                    break;
                default:
                    text = "nieznany bonus";
                    break;
            }
            return text;
        }
    }
}
