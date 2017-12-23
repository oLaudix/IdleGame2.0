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
        public int level;
        public double gold;
        public double honor;
        public double critChance = 0.01;
        public double critMagnitude = 10;
        int state = 0;
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/playerUnit.png", 140, 61);
        public MyPlayer(int x, int y)
        {
            this.level = 1;
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

        public double GetPlayerAttackDamageByLevel(int iLevel, double allDamageStat, double playerDamageStat, double playerDamageDPSStat, double totalDPS, double gearDamageBonus, double critChance, double critM, Random random)
        {
            double num = (double)iLevel * Math.Pow(1.05, (double)iLevel);
            double num3 = (num + (playerDamageDPSStat * totalDPS)) * (1.0 + playerDamageStat) * (1.0 + gearDamageBonus) * (1.0 + allDamageStat);
            if (num3 <= 1.0)
            {
                num3 = 1.0;
            }
            double isCrit = random.NextDouble();
            //Console.WriteLine((this.critChance + critChance) + " " + (isCrit));
            if (this.critChance + critChance > isCrit)
                num3 = num3 * (critM + this.critMagnitude);
            //Console.WriteLine((this.critChance + critChance) + " " + (isCrit) + " " + num3);
            return num3;
        }

        public double GetUpgradeCostByLevel(int iLevel, double upgradeCostStat)
        {
            double num = (double)Math.Min(25, 3 + iLevel) * Math.Pow(1.074, (double)iLevel);
            double a = num * (1.0 - upgradeCostStat);
            return Math.Ceiling(a);
        }

        public override void Update()
        {
            if (spritemap.CurrentFrame == 19 && state == 1)
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
            }
            base.Update();
        }
    }
}
