using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class MyPlayer : Entity
    {
        public enum Animation
        {
            Idle,
            IdleToShooting,
            ShootingToIdle,
            Shoot
        }
        MainScene scene = (MainScene)MainScene.Instance;
        public int level;
        public double gold;
        public double honor;
        public double critChance = 0.01;
        public double critMagnitude = 10;
        public double currentDamage = 0;
        public double upgradeCost = 0;
        public double nextLevelDamageDiff = 0;
        int state = 0;
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/playerUnit.png", 140, 61);
        public MyPlayer(int x, int y)
        {
            this.level = 0;
            //this.stage = 0;
            this.gold = 0;
            this.honor = 0;
            spritemap.Add(Animation.Idle, "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15", 4);
            spritemap.Add(Animation.IdleToShooting, "16,17,18,19", 2).NoRepeat();
            spritemap.Add(Animation.ShootingToIdle, "19,18,17,16", 2).NoRepeat();
            spritemap.Add(Animation.Shoot, "20,21,22,23,24", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            this.X = x;
            this.Y = y;
        }

        public void UpdatePlayerStats()
        {
            this.currentDamage = GetPlayerAttackDamageByLevel(this.level);
            this.upgradeCost = GetUpgradeCostByLevel(this.level);
            this.nextLevelDamageDiff = GetPlayerAttackDamageByLevel(this.level + 1) - GetPlayerAttackDamageByLevel(this.level);
        }

        public void UpgradePlayer()
        {
            this.level++;
            UpdatePlayerStats();
            scene.needUpdate = true;
        }

        public double GetPlayerAttackDamageByLevel(int iLevel)
        {
            double num = (double)iLevel * Math.Pow(1.05, (double)iLevel);
            double num3 = (num + (scene.Bonuses[BonusType.PlayerDamageDPS] * scene.totalDPS)) * (1.0 + scene.Bonuses[BonusType.PlayerDamage]) * (1.0 + scene.GetBonusArtifactDamage()) * (1.0 + scene.Bonuses[BonusType.AllDamage]);
            double isCrit = scene.random.NextDouble();
            //Console.WriteLine(scene.GetBonusArtifactDamage());
            //Console.WriteLine((this.critChance + critChance) + " " + (isCrit));
            if (this.critChance + scene.Bonuses[BonusType.CriticalChance] > isCrit)
                num3 = num3 * ((1 + scene.Bonuses[BonusType.CriticalDamage]) * this.critMagnitude);
            //Console.WriteLine((this.critChance + critChance) + " " + (isCrit) + " " + num3);
            return num3;
        }

        public int GetNumLevelsToUnlockByGivenGoldAmount()
        {
            int counter = 0;
            int tmplevel = this.level;
            double tmpgold = this.gold;
            while (tmpgold > GetUpgradeCostByLevel(tmplevel))
            {
                tmpgold -= GetUpgradeCostByLevel(tmplevel);
                counter++;
                tmplevel++;
                //Console.WriteLine(GetUpgradeCostByLevel(tmplevel));
            }
            return counter;
        }

        public double GetUpgradeCostByLevel(int iLevel)
        {
            double num = (double)Math.Min(25, 3 + iLevel) * Math.Pow(1.074, (double)iLevel);
            double a = num * (1.0 - scene.Bonuses[BonusType.UpgradeCost]);
            return Math.Ceiling(a);
        }

        public override void Update()
        {
            if (Input.MouseY < 1080 - 261)
            {
                if (Input.MouseButtonDown(MouseButton.Left) && spritemap.CurrentAnim == Animation.Idle)
                    spritemap.Play(Animation.Shoot);
            }
            if (Input.MouseButtonReleased(MouseButton.Left) || (Input.MouseY > 1080 - 261 && spritemap.CurrentAnim == Animation.Shoot))
                spritemap.Play(Animation.Idle);
            /*if (spritemap.CurrentFrame == 19 && state == 1)
            {
                spritemap.Play(Animation.Shoot);
                state = 2;
            }
            else if (spritemap.CurrentFrame == 16 && state == 3)
            {
                spritemap.Play(Animation.Idle);
                state = 0;
            }
            else if (Input.MouseButtonPressed(MouseButton.Left))
            {
                state = 1;
                spritemap.Play(Animation.IdleToShooting);
            }
            else if (Input.MouseButtonReleased(MouseButton.Left))
            {
                spritemap.Play(Animation.ShootingToIdle);
                state = 3;
            }*/
            base.Update();
        }

        public double GetHeroLevelPrestigeRelics()
        {
            int num = 0;
            foreach (var unit in scene.unitsList)
            {
                num += unit.level;
            }
            double num2 = (double)num / (double)500;
            num2 *= 1.0 + scene.Bonuses[BonusType.BonusRelic];
            num2 = Math.Ceiling(num2);
            return num2;
        }

        public double GetUnlockedStagePrestigeRelics()
        {
            double num = 0.0;
            int unlockedStage = scene.stage.stage;
            num += Math.Pow((double)(scene.stage.stage / 15), 1.7);
            num *= 1.0 + scene.Bonuses[BonusType.BonusRelic];
            return Math.Ceiling(num);
        }
        public double GetPrestigeRelicCount()
        {
            double num = (double)Math.Round((float)this.GetHeroLevelPrestigeRelics());
            num += (double)Math.Round((float)this.GetUnlockedStagePrestigeRelics());
            return 2.0 * num;
        }
        public void Prestige()
        {
            foreach (var unit in scene.unitsList)
            {
                unit.level = 0;
            }
            foreach (var skill in scene.skillList)
                skill.isUnlocked = false;
            this.gold = 0;
            scene.stage.stage = 0;
            this.honor += GetPrestigeRelicCount();
            scene.StartStage();
        }
    }
}
