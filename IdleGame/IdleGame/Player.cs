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
            Spin,
            Shoot
        }
        MainScene scene = (MainScene)MainScene.Instance;
        public Sound Shooting = new Sound("Assets/Sounds/minigun2.ogg") { Loop = true };
        public Sound wind_up = new Sound("Assets/Sounds/wind_up.ogg") { Loop = false };
        public Sound wind_down = new Sound("Assets/Sounds/wind_down.ogg") { Loop = false };
        public Sound spin = new Sound("Assets/Sounds/minigun_spin.ogg") { Loop = true };
        public int level;
        public double gold;
        public double honor;
        public double critChance = 0.01;
        public double critMagnitude = 10;
        public double currentDamage = 0;
        public double upgradeCost = 0;
        public double nextLevelDamageDiff = 0;
        public int NumLevelsToUnlock = 0;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/playerUnit.png", 140, 61);
        int animating = -1;
        public bool isWindingUp = false;
        public MyPlayer(int x, int y)
        {
            this.level = 0;
            //this.stage = 0;
            this.gold = 0;
            this.honor = 0;
            spritemap.Add(Animation.Idle, "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15", 4);
            spritemap.Add(Animation.IdleToShooting, "16,17,18,19", 2).NoRepeat();
            spritemap.Add(Animation.Spin, "19", 2).NoRepeat();
            spritemap.Add(Animation.ShootingToIdle, "19,18,17,16", 2).NoRepeat();
            spritemap.Add(Animation.Shoot, "20,21,22,23,24", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            this.X = x;
            this.Y = y;
            SetHitbox(140, 61, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
            //Sound.GlobalVolume = 0.1f;
        }

        public void UpdatePlayerStats()
        {
            critChance = -100;
            this.currentDamage = GetPlayerAttackDamageByLevel(this.level);
            this.upgradeCost = GetUpgradeCostByLevel(this.level);
            this.nextLevelDamageDiff = GetPlayerAttackDamageByLevel(this.level + 1) - GetPlayerAttackDamageByLevel(this.level);
            critChance = 0.01;
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
            if (this.critChance + scene.Bonuses[BonusType.CriticalChance] > isCrit)
            {
                num3 = num3 * ((1 + scene.Bonuses[BonusType.CriticalDamage]) * this.critMagnitude);
                Console.WriteLine((this.critChance + scene.Bonuses[BonusType.CriticalChance]) + " " + (isCrit));
            }
            if (scene.activeSkillList[4].activated)
                num3 = num3 * (1 + (scene.activeSkillList[4].magnitude / 100));
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
            if (this.Timer % 60 == 0)
                this.NumLevelsToUnlock = this.GetNumLevelsToUnlockByGivenGoldAmount();

            if (Overlap(X, Y, ColliderTags.Garbage))
                if (Hitbox.Bottom > Overlapped.Hitbox.Bottom)
                    Layer = Overlapped.Layer - 1;
            if (Input.MouseButtonPressed(MouseButton.Left) || Input.MouseButtonReleased(MouseButton.Left))
            {
                //Console.WriteLine(Math.Atan2(Input.MouseX, Input.MouseY));
                Shooting.Stop();
                wind_up.Stop();
                wind_down.Stop();
            }
            if (Input.MouseButtonDown(MouseButton.Left) && Input.MouseY < 1080 - 261)
            {
                if (animating > 0)
                {
                    animating--;
                }
                else if (animating == 0)
                {
                    animating--;
                    spritemap.Play(Animation.Shoot);
                    wind_up.Stop();
                    //Shooting.Volume = Sound.GlobalVolume * 0.5f;
                    Shooting.Play();
                    isWindingUp = false;
                }
                else if (animating == -1 && spritemap.CurrentAnim == Animation.Idle)
                {
                    //wind_up.Volume = Sound.GlobalVolume * 0.2f;
                    wind_up.Play();
                    spritemap.Play(Animation.IdleToShooting);
                    animating = (int)(wind_up.Duration/60);
                    isWindingUp = true;
                }
            }
            if (Input.MouseButtonUp(MouseButton.Left) || Input.MouseY > 1080 - 261)
            {
                if (animating == -1 && (spritemap.CurrentAnim == Animation.Shoot || spritemap.CurrentAnim == Animation.Spin))
                {
                    spritemap.Play(Animation.ShootingToIdle);
                    animating = (int)spritemap.Anim(Animation.ShootingToIdle).TotalDuration;
                    Shooting.Stop();
                    //wind_down.Volume = Sound.GlobalVolume * 0.2f;
                    wind_down.Play();
                }
                else if (animating > 0)
                {
                    animating--;
                }
                else if (animating == 0)
                {
                    animating--;
                    spritemap.Play(Animation.Idle);
                }
            }
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
            if (num2 < 1)
                num2 = 0;
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
            this.honor += GetPrestigeRelicCount();
            this.level = 1;
            foreach (var unit in scene.unitsList)
            {
                unit.level = 0;
            }
            foreach (var skill in scene.skillList)
                skill.isUnlocked = false;
            this.gold = 0;
            scene.currentStage = 0;
            foreach (var enemy in scene.enemyList)
                enemy.RemoveSelf();
            scene.enemyList.Clear();
            Console.WriteLine(scene.enemyList.Count);
            scene.StartStage();
            scene.needUpdate = true;
        }
    }
}
