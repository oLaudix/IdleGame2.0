using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Stage : Entity
    {
        public Double MaxHP;
        public Double CurrentHP;
        public int Wave;
        public Double Prize;
        public bool Chest;
        public int stage;
        public MainScene scene = (MainScene)MainScene.Instance;
        Dictionary<BonusType, double> Bonuses;
        public Stage(int stage, Dictionary<BonusType, double> Bonuses, Random random)
        {
            this.stage = stage;
            this.Bonuses = Bonuses;
            this.MaxHP = GetStageBaseHP();
            if (GetTreasureSpawnChance() >= random.NextDouble())
            {
                this.Prize = GetTreasureGold();
                this.Chest = true;
            }
            else
            {
                this.Prize = GetStageBaseGold();
                this.Chest = false;
            }
            this.CurrentHP = MaxHP;
            this.Wave = 1;
        }

        public double GetStageBaseHP()
        {
            return 18.5 * Math.Pow(1.57, (double)Math.Min((float)this.stage, 156)) * Math.Pow(1.17, (double)Math.Max((float)this.stage - 156, 0f));
        }
        public double GetStageBaseGold()
        {
            double stageBaseHP = this.GetStageBaseHP();
            double num = stageBaseHP * (double)(0.02 + 0.00045 * Math.Min((float)this.stage, 150));
            return Math.Round(num * Math.Ceiling(1.0 + scene.Bonuses[BonusType.AllGold]));
        }
        public double GetTreasureSpawnChance()
        {
            return 0.021f * (1f + this.Bonuses[BonusType.ChestChance]);
        }

        public double GetTreasureGold()
        {
            return Math.Round(this.GetStageBaseGold() * 50 * (1f + this.Bonuses[BonusType.ChestGold]));
        }
    }
}
