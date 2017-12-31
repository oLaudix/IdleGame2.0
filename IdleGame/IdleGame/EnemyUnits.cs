﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Enemy_Units : Entity
    {
        public List<Sound> soldier_death_list = new List<Sound>();
        MainScene scene = (MainScene)MainScene.Instance;
        public double MaxHP;
        public double CurrentHP;
        public bool isDead = false;
        public double prize;
        public Enemy_Units(float x, float y) : base(x, y)
        {
            this.prize = scene.stage.Prize/5;
            soldier_death_list.Add(new Sound("Assets/Sounds/soldier_death_fire.ogg"){Loop = false});
            soldier_death_list.Add(new Sound("Assets/Sounds/soldier_death_1.ogg"){ Loop = false });
            soldier_death_list.Add(new Sound("Assets/Sounds/soldier_death_2.ogg"){ Loop = false });
            soldier_death_list.Add(new Sound("Assets/Sounds/soldier_death_3.ogg"){ Loop = false });
        }

        public override void Update()
        {
            GetPlayerDamage();
            base.Update();
        }

        public void GetDamage(double dmg)
        {
            this.scene.stage.CurrentHP -= dmg;
            CurrentHP -= dmg;
        }

        public void GetPlayerDamage()
        {
            if (Overlap(X, Y, ColliderTags.Crosshair) && Input.MouseButtonDown(MouseButton.Left) && !scene.player.isWindingUp && !isDead)
            {
                double hit = scene.player.GetPlayerAttackDamageByLevel(scene.player.level) * 15 / 60;
                if (!scene.isHit)
                    this.scene.stage.CurrentHP -= hit;
                CurrentHP -= hit;
                scene.isHit = true;
            }
        }
    }
    class Enemy_Soldier : Enemy_Units
    {
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
        public enum Animation
        {
            Death1,
            Death2,
            Death3,
            Death4,
            Death5,
            Death6,
            Death7,
            Throw,
            Idle,
            Run
        }
        int runtime;
        //int sounddelay;
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
            spritemap.Add(Animation.Throw, scene.GetAnimationString(115, 129), 4).NoRepeat();
            spritemap.Add(Animation.Idle, scene.GetAnimationString(96, 102), 5);
            spritemap.Add(Animation.Run, scene.GetAnimationString(103, 114), 4);
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);
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
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration*3;
                    spritemap.Play(Animation.Idle);
                }
            }
            if (spritemap.CurrentAnim == Animation.Idle && !isDead)
            {
                runtime--;
                if (runtime == 0)
                {
                    scene.Add(new grenade(X + 45, Y + 32, scene.random.Next(900, 1701), scene.random.Next(520, 750), 40, false));
                    spritemap.Play(Animation.Throw);
                    runtime = (int)spritemap.Anim(Animation.Throw).TotalDuration;
                }
            }
            if (spritemap.CurrentAnim == Animation.Throw && !isDead)
            {
                runtime--;
                if (runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration * scene.random.Next(3, 7);
                }
            }
            if (CurrentHP <= 0)
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    if ((int)test == 0)
                        soldier_death_list[0].Play();
                    else
                        soldier_death_list[scene.random.Next(1, 4)].Play();
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Bazooka : Enemy_Units
    {
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
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
            spritemap.Add(Animation.Kneel, "115, 114, 113", 4).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(100, 111), 4);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(112, 132), 4).NoRepeat();
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);

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
                else if ((spritemap.CurrentAnim == Animation.Shoot && runtime == ((int)spritemap.Anim(Animation.Shoot).TotalDuration * 2 - 24)))
                {
                    new Projectile_rocket(X + 69, Y + 37, scene.random.Next(900, 1701), scene.random.Next(520, 750));
                }
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    if ((int)test == 0)
                        soldier_death_list[0].Play();
                    else
                        soldier_death_list[scene.random.Next(1, 4)].Play();
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Riflemon : Enemy_Units
    {
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
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
            Idle,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_riflemon.png", 88, 68);
        public Sound sound = new Sound("Assets/Sounds/sniper.ogg") { Loop = false };
        //public Sound sound = new Sound("Assets/Sounds/machinegun.ogg") { Loop = true };
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
            spritemap.Add(Animation.Idle, "96-99", 8);
            spritemap.Add(Animation.Run, "101-111", 4);
            spritemap.Add(Animation.Shoot, "112-131", 4).NoRepeat();
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);

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
                        sound.Play();
                        runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                    }
                }
                else if (spritemap.CurrentAnim == Animation.Idle && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    sound.Play();
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 2;
                }
                else if (spritemap.CurrentAnim == Animation.Shoot && runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration + scene.random.Next(60 * 4, 60 * 6);
                }
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    sound.Stop();
                    scene.enemyList.RemoveIfContains(this);
                    //Hitbox.Width = 0;
                    //Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    if ((int)test == 0)
                        soldier_death_list[0].Play();
                    else
                        soldier_death_list[scene.random.Next(1, 4)].Play();
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Shield : Enemy_Units
    {
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
        public Sound sound = new Sound("Assets/Sounds/shield_ire.ogg") { Loop = false };
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
            WeaponBack,
            Run,
            Idle
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
            spritemap.Add(Animation.WeaponOut, "96-99", 4).NoRepeat();
            spritemap.Add(Animation.WeaponBack, "99-96", 4).NoRepeat();
            spritemap.Add(Animation.Idle, "100-105", 8);
            spritemap.Add(Animation.Run, "106-117", 4);
            spritemap.Add(Animation.Shoot, "118-123", 4).NoRepeat();
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);

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
                else if (spritemap.CurrentAnim == Animation.WeaponOut && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    sound.Play();
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                }
                else if (spritemap.CurrentAnim == Animation.Shoot && runtime == 0)
                {
                    spritemap.Play(Animation.WeaponBack);
                    runtime = (int)spritemap.Anim(Animation.WeaponBack).TotalDuration;
                }
                else if (spritemap.CurrentAnim == Animation.WeaponBack && runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration + scene.random.Next(5 * 60, 8 * 60);
                }
                else if (spritemap.CurrentAnim == Animation.Idle && runtime == 0)
                {
                    spritemap.Play(Animation.WeaponOut);
                    runtime = (int)spritemap.Anim(Animation.WeaponOut).TotalDuration;
                }
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    Animation test = (Animation)scene.random.Next(0, 7);
                    runtime = 60 * 2;
                    if ((int)test == 0)
                        soldier_death_list[0].Play();
                    else
                        soldier_death_list[scene.random.Next(1, 4)].Play();
                    spritemap.Play(test);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Cokka : Enemy_Units
    {
        public Sound cokka_shoot = new Sound("Assets/Sounds/small_tank.ogg")
        {
            Loop = false
        };
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
        public enum Animation
        {
            Death,
            Shoot,
            WeaponOut,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_cokka.png", 133, 76);
        public Enemy_Cokka(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP * 5;
            SetHitbox(72, 57, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(12, 20);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(0, 16), 4).NoRepeat();
            spritemap.Add(Animation.Death, scene.GetAnimationString(17, 25), 4).NoRepeat();
            spritemap.Add(Animation.WeaponOut, scene.GetAnimationString(32, 38), 4).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(26, 31), 4);
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);

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
                    cokka_shoot.Play();
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 5;
                }
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    scene.Add(new Explosions(X + 42, Y + 39, Explosions.ExplosionType.small, 0));
                    scene.Add(new Explosions(X + 60, Y + 32, Explosions.ExplosionType.small, 6));
                    scene.Add(new Explosions(X + 20, Y + 43, Explosions.ExplosionType.small, 12));
                    isDead = true;
                    runtime = 60 * 2;
                    spritemap.Play(Animation.Death);
                    this.LifeSpan = this.Timer + runtime;
                    scene.Add(new Explosions(X + 45, Y + 45, Explosions.ExplosionType.big, runtime - 6));
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_Mummy : Enemy_Units
    {
        public Sound DeathSound = new Sound("Assets/Sounds/mummydeath.ogg")
        {
            Loop = false
        };
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
        public enum Animation
        {
            Death,
            Shoot,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_mummy.png", 94, 62);
        public Enemy_Mummy(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP;
            SetHitbox(35, 46, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(9, 17);
            spritemap.Add(Animation.Shoot, scene.GetAnimationString(0, 21), 3).NoRepeat();
            spritemap.Add(Animation.Death, scene.GetAnimationString(22, 67), 3).NoRepeat();
            spritemap.Add(Animation.Run, scene.GetAnimationString(68, 85), 3);
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);
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
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    DeathSound.Play();
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    isDead = true;
                    runtime = (int)spritemap.Anim(Animation.Death).TotalDuration * 2;
                    spritemap.Play(Animation.Death);
                    this.LifeSpan = this.Timer + runtime;
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }

    class Enemy_high_tonk : Enemy_Units
    {
        public Sound cokka_shoot = new Sound("Assets/Sounds/small_tank.ogg")
        {
            Loop = false
        };
        MainScene scene = (MainScene)MainScene.Instance;
        //bool isDead = false;
        public enum Animation
        {
            Death,
            Shoot,
            Idle,
            Stop,
            Run
        }
        int runtime;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_high_tonk.png", 161, 105);
        public Enemy_high_tonk(float x, float y) : base(x, y)
        {
            Layer = -500;
            this.MaxHP = scene.stage.MaxHP;
            this.CurrentHP = this.MaxHP/5;
            SetHitbox(72, 97, ColliderTags.EnemyUnit);
            Hitbox.SetPosition(40, 10);
            spritemap.Add(Animation.Death, "43, 44, 45, 0", 8).NoRepeat();
            spritemap.Add(Animation.Run, "1-6", 4);
            spritemap.Add(Animation.Idle, "7, 8", 4);
            spritemap.Add(Animation.Shoot, "19-35,9-18", 4).NoRepeat();
            //spritemap.Add(Animation.Shoot, "9-35", 4).NoRepeat();
            spritemap.Add(Animation.Stop, "36-42", 4).NoRepeat();
            spritemap.Play(Animation.Run);
            AddGraphic(spritemap);
            scene.Add(this);
            scene.enemyList.Add(this);
            runtime = (int)spritemap.Anim(Animation.Run).TotalDuration * scene.random.Next(3, 12);

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
                        spritemap.Play(Animation.Stop);
                        runtime = (int)spritemap.Anim(Animation.Stop).TotalDuration * 1;
                    }
                }
                else if ((spritemap.CurrentAnim == Animation.Stop || spritemap.CurrentAnim == Animation.Shoot) && runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                    //cokka_shoot.Play();
                    Console.Write("test");
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration + scene.random.Next(5 * 60, 7 * 60);
                }
                else if (spritemap.CurrentAnim == Animation.Idle && runtime == 0)
                {
                    spritemap.Play(Animation.Shoot);
                    cokka_shoot.Play();
                    runtime = (int)spritemap.Anim(Animation.Shoot).TotalDuration * 1;
                }
            }
            else
            {
                if (!isDead)
                {
                    scene.player.gold += prize * (1 + scene.Bonuses[BonusType.MonsterGold]);
                    scene.enemyList.RemoveIfContains(this);
                    Hitbox.Width = 0;
                    Hitbox.Height = 0;
                    scene.Add(new Explosions(X + 54, Y + 83, Explosions.ExplosionType.medium, 0));
                    scene.Add(new Explosions(X + 89, Y + 36, Explosions.ExplosionType.medium, 3));
                    scene.Add(new Explosions(X + 87, Y + 83, Explosions.ExplosionType.medium, 12));
                    scene.Add(new Explosions(X + 73, Y + 60, Explosions.ExplosionType.medium, 9));
                    scene.Add(new Explosions(X + 65, Y + 29, Explosions.ExplosionType.medium, 6));
                    isDead = true;
                    runtime = 60 * 2;
                    spritemap.Play(Animation.Death);
                    this.LifeSpan = this.Timer + runtime;
                    scene.Add(new Explosions(X + 78, Y + 105, Explosions.ExplosionType.huge, runtime - 6));
                    //new Enemy_Soldier(scene.random.Next(-50, -10), scene.random.Next(511, 754));
                }
            }
            base.Update();
        }
    }
}
