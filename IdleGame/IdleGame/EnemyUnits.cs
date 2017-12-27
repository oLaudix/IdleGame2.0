using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Enemy_Soldier : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        double MaxHP;
        double CurrentHP;
        bool isDead = false;
        public enum Animation
        {
            Death1,
            Death2,
            Death3,
            Death4,
            Death5,
            Death6,
            Death7,
            Shoot,
            WeaponOut,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_soldier.png", 88, 68);
        public Enemy_Soldier(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP/4;
            SetHitbox(33, 40, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(28, 27);
            spritemap.Add(Animation.Death1, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21", 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, "33, 34, 35, 36, 37, 38, 39, 40, 41", 4).NoRepeat();
            spritemap.Add(Animation.Death4, "42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52", 4).NoRepeat();
            spritemap.Add(Animation.Death5, "53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67", 4).NoRepeat();
            spritemap.Add(Animation.Death6, "68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80", 4).NoRepeat();
            spritemap.Add(Animation.Death7, "81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95", 4).NoRepeat();
            spritemap.Add(Animation.Shoot, "116, 117, 118, 119", 4);
            spritemap.Add(Animation.WeaponOut, "96, 97, 98, 99, 100, 101, 102, 103", 3).NoRepeat();
            spritemap.Add(Animation.Run, "104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115", 4);
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);
            Console.WriteLine("");

        }

        public override void Render()
        {
            base.Render();
            Hitbox.Render();
        }

        public override void Update()
        {
            if (spritemap.CurrentAnim == Animation.Run && CurrentHP > 0)
            {
                runtime--;
                X++;
                if (runtime == 0)
                {
                    runtime = (int)spritemap.Anim(Animation.WeaponOut).TotalDuration;
                    spritemap.Play(Animation.WeaponOut);
                }
            }
            if (spritemap.CurrentAnim == Animation.WeaponOut && !isDead)
            {
                runtime--;
                if (runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                }
            }
            if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left) && CurrentHP > 0)
            {
                double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                if (!scene.isHit)
                    this.scene.stage.CurrentHP -= hit;
                CurrentHP -= hit;
                scene.isHit = true;
            }
            if (CurrentHP <= 0)
            {
                if (!isDead)
                {
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Bazooka : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        double MaxHP;
        double CurrentHP;
        bool isDead = false;
        public enum Animation
        {
            Death1,
            Death2,
            Death3,
            Death4,
            Death5,
            Death6,
            Death7,
            Shoot,
            WeaponOut,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_bazooka.png", 88, 68);
        public Enemy_Bazooka(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP / 4;
            SetHitbox(33, 40, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(28, 27);
            spritemap.Add(Animation.Death1, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21", 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, "33, 34, 35, 36, 37, 38, 39, 40, 41", 4).NoRepeat();
            spritemap.Add(Animation.Death4, "42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52", 4).NoRepeat();
            spritemap.Add(Animation.Death5, "53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67", 4).NoRepeat();
            spritemap.Add(Animation.Death6, "68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80", 4).NoRepeat();
            spritemap.Add(Animation.Death7, "81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95", 4).NoRepeat();
            spritemap.Add(Animation.Run, "96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106 107", 4);
            spritemap.Add(Animation.Shoot, "108, 109, 110", 4);
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);
            Console.WriteLine("");

        }

        public override void Render()
        {
            base.Render();
            Hitbox.Render();
        }

        public override void Update()
        {
            if (spritemap.CurrentAnim == Animation.Run && CurrentHP > 0)
            {
                runtime--;
                X++;
                if (runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                }
            }
            if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left) && CurrentHP > 0)
            {
                double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                if (!scene.isHit)
                    this.scene.stage.CurrentHP -= hit;
                CurrentHP -= hit;
                scene.isHit = true;
            }
            if (CurrentHP <= 0)
            {
                if (!isDead)
                {
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }
}
