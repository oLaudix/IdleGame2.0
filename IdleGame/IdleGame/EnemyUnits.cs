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
            spritemap.Add(Animation.Death1, scene.GetAnimationString(0, 21), 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, scene.GetAnimationString(33, 41), 4).NoRepeat();
            spritemap.Add(Animation.Death4, scene.GetAnimationString(42, 52), 4).NoRepeat();
            spritemap.Add(Animation.Death5, scene.GetAnimationString(53, 67), 4).NoRepeat();
            spritemap.Add(Animation.Death6, scene.GetAnimationString(68, 80), 4).NoRepeat();
            spritemap.Add(Animation.Death7, scene.GetAnimationString(81, 95), 4).NoRepeat();
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(116, 119), 4);
            spritemap.Add(Animation.WeaponOut, scene.GetAnimationString(96, 103), 3).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(104, 115), 4);
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
            Kneel,
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
            spritemap.Add(Animation.Death1, scene.GetAnimationString(0, 21), 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, scene.GetAnimationString(33, 41), 4).NoRepeat();
            spritemap.Add(Animation.Death4, scene.GetAnimationString(42, 52), 4).NoRepeat();
            spritemap.Add(Animation.Death5, scene.GetAnimationString(53, 67), 4).NoRepeat();
            spritemap.Add(Animation.Death6, scene.GetAnimationString(68, 80), 4).NoRepeat();
            spritemap.Add(Animation.Death7, scene.GetAnimationString(81, 95), 4).NoRepeat();
            spritemap.Add(Animation.Kneel, scene.GetAnimationString(115, 132), 4).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(100, 111), 4);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(112, 132), 4).NoRepeat();
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
            if (CurrentHP > 0)
            {
                runtime--;
                if (spritemap.CurrentAnim == Animation.Run)
                {
                    X++;
                    if (runtime == 0)
                    {
                        spritemap.Play(Animation.Kneel);
                        runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                    }
                }
                else if((spritemap.CurrentAnim == Animation.Kneel || spritemap.CurrentAnim == Animation.Shoot) && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                }
                if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left))
                {
                    double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                    if (!scene.isHit)
                        this.scene.stage.CurrentHP -= hit;
                    CurrentHP -= hit;
                    scene.isHit = true;
                }
            }
            else
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

    class Enemy_Riflemon : Entity
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
            Kneel,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_riflemon.png", 88, 68);
        public Enemy_Riflemon(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP / 4;
            SetHitbox(33, 40, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(28, 27);
            spritemap.Add(Animation.Death1, scene.GetAnimationString(0, 21), 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, scene.GetAnimationString(33, 41), 4).NoRepeat();
            spritemap.Add(Animation.Death4, scene.GetAnimationString(42, 52), 4).NoRepeat();
            spritemap.Add(Animation.Death5, scene.GetAnimationString(53, 67), 4).NoRepeat();
            spritemap.Add(Animation.Death6, scene.GetAnimationString(68, 80), 4).NoRepeat();
            spritemap.Add(Animation.Death7, scene.GetAnimationString(81, 95), 4).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(96, 107), 4);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(108, 127), 4).NoRepeat();
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
            if (CurrentHP > 0)
            {
                runtime--;
                if (spritemap.CurrentAnim == Animation.Run)
                {
                    X++;
                    if (runtime == 0)
                    {
                        spritemap.Play(Animation.Shoot);
                        runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                    }
                }
                else if (spritemap.CurrentAnim == Animation.Shoot && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                }
                if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left))
                {
                    double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                    if (!scene.isHit)
                        this.scene.stage.CurrentHP -= hit;
                    CurrentHP -= hit;
                    scene.isHit = true;
                }
            }
            else
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

    class Enemy_Shield : Entity
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
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_shield.png", 88, 68);
        public Enemy_Shield(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP / 4;
            SetHitbox(33, 40, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(28, 27);
            spritemap.Add(Animation.Death1, scene.GetAnimationString(0, 21), 4).NoRepeat();
            spritemap.Add(Animation.Death2, "22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 31", 4).NoRepeat();
            spritemap.Add(Animation.Death3, scene.GetAnimationString(33, 41), 4).NoRepeat();
            spritemap.Add(Animation.Death4, scene.GetAnimationString(42, 52), 4).NoRepeat();
            spritemap.Add(Animation.Death5, scene.GetAnimationString(53, 67), 4).NoRepeat();
            spritemap.Add(Animation.Death6, scene.GetAnimationString(68, 80), 4).NoRepeat();
            spritemap.Add(Animation.Death7, scene.GetAnimationString(81, 95), 4).NoRepeat();
            spritemap.Add(Animation.WeaponOut, scene.GetAnimationString(96, 99), 4).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(100, 111), 4);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(112, 117), 4);
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
            if (CurrentHP > 0)
            {
                runtime--;
                if (spritemap.CurrentAnim == Animation.Run)
                {
                    X++;
                    if (runtime == 0)
                    {
                        spritemap.Play(Animation.WeaponOut);
                        runtime = (int)spritemap.Anim(Animation.WeaponOut).TotalDuration * 1;
                    }
                }
                else if ((spritemap.CurrentAnim == Animation.WeaponOut || spritemap.CurrentAnim == Animation.Shoot) && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                }
                if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left))
                {
                    double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                    if (!scene.isHit)
                        this.scene.stage.CurrentHP -= hit;
                    CurrentHP -= hit;
                    scene.isHit = true;
                }
            }
            else
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
