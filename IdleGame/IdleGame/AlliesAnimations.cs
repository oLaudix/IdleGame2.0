using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Dicokka : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public Sound sound = new Sound("Assets/Sounds/small_tank.ogg") { Loop = false };
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/dicokka.png", 124, 76);
        public Dicokka(float x, float y) : base (x, y)
        {
            spritemap.Add(Animation.Idle, "17,18", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
            SetHitbox(124, 76, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                if (spritemap.CurrentAnim == Animation.Idle)
                {
                    spritemap.Play(Animation.Shoot);
                    sound.Play();
                    cooldown = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                    scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60, false));
                }
                else if (spritemap.CurrentAnim == Animation.Shoot)
                {
                    spritemap.Play(Animation.Idle);
                    cooldown = scene.random.Next(60 * 2, 60 * 3);
                }
            }
            base.Update();
        }
    }

    class BiggestTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public Sound sound = new Sound("Assets/Sounds/biggest_tank.ogg") { Loop = false };
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/biggest_tonk.png", 187, 80);
        public BiggestTonk(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0-16", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 2, 60 * 3);
            scene.Add(this);
            SetHitbox(187, 80, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                sound.Play();
                cooldown = scene.random.Next(60 * 3, 60 * 4);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_big, 60, false));
            }
            base.Update();
        }
    }

    class FatTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public Sound sound = new Sound("Assets/Sounds/fat_tank.ogg") { Loop = false };
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/fat_tonk.png", 130, 62);
        public FatTonk(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0, 1", 4);
            spritemap.Add(Animation.Shoot, "2-17", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
            SetHitbox(130, 62, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                if (spritemap.CurrentAnim == Animation.Idle)
                {
                    spritemap.Play(Animation.Shoot);
                    sound.Play();
                    cooldown = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                    scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60, false));
                }
                else if (spritemap.CurrentAnim == Animation.Shoot)
                {
                    spritemap.Play(Animation.Idle);
                    cooldown = scene.random.Next(60 * 2, 60 * 3);
                }
            }
            base.Update();
        }
    }

    class Heli : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        int fire;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/heli.png", 93, 63);
        public Heli(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0-15", 1);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = 40;
            scene.Add(this);
            fire = scene.random.Next(60 * 1, 60 * 2);
        }
        public override void Update()
        {
            cooldown--;
            fire--;
            if (cooldown < 0 && cooldown % 10 == 0)
            {
                this.Y--;
            }
            else if (cooldown >= 0 && cooldown % 10 == 0)
            {
                this.Y++;
            }
            if (cooldown == -40)
            {
                cooldown = 40;
            }

            if (fire == 0)
            {
                new Projectile_heli_rocket(X + 65 - (93/2), Y + 46 - (63/2), scene.random.Next(50, 801), scene.random.Next(520, 750), true);
                fire = scene.random.Next(60 * 2, 60 * 3);
            }

            base.Update();
        }
    }

    class BigTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public Sound sound = new Sound("Assets/Sounds/bigger_tank.ogg") { Loop = false };
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/bigtonk.png", 148, 78);
        public BigTonk(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
            SetHitbox(148, 78, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                sound.Play();
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60, false));
            }
            base.Update();
        }
    }

    class Minigun : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/wierd_tonk.png", 128, 63);
        public Sound sound = new Sound("Assets/Sounds/human_tonk.ogg") { Loop = false };
        public Minigun(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0, 1, 2, 3", 4);
            spritemap.Add(Animation.Shoot, "4-20", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            scene.Add(this);
            this.cooldown = scene.random.Next(60 * 4, 60 * 6);
            SetHitbox(128, 63, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }

        public override void Update()
        {
            cooldown--;
            if (cooldown == 0 && spritemap.CurrentAnim == Animation.Idle)
            {
                spritemap.Play(Animation.Shoot);
                sound.Play();
                cooldown = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60, false));
            }
            else if (cooldown == 0 && spritemap.CurrentAnim == Animation.Shoot)
            {
                spritemap.Play(Animation.Idle);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
            }
            base.Update();
        }
    }

    class Mortar : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/mortar.png", 40, 42);
        public Mortar(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
            SetHitbox(40, 42, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                scene.Add(new projectile_mortar(X + 11 - 20, Y + 27 - 21, scene.random.Next(50, 801), scene.random.Next(520, 750), 12*4));
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.small, 60, false));
            }
            base.Update();
        }
    }

    class Rocket : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        int soundDelay = 0;
        public enum Animation
        {
            Idle,
            Shoot
        }
        public Sound sound = new Sound("Assets/Sounds/rocket_launch.ogg") { Loop = false };
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/rocket.png", 124, 110);
        public Rocket(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0,1,2,3,4,5,6,7, 8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10, 11,12,13,14,15,16,11,12,13,14,15,16,11,12,13,14,15,16,11,12,13,14,15,16, 17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 4, 60 * 5);
            scene.Add(this);
            SetHitbox(124, 110, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            soundDelay--;
            if (cooldown == 0)
            {
                soundDelay = 148;
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 7, 60 * 8);
                new Bradley_rocket(X + 66 - 124 / 2, Y + 46 - 110 / 2, 160, true);
                new Bradley_rocket(X + 66 - 124 / 2, Y + 45 - 110 / 2, 184, true);
                new Bradley_rocket(X + 63 - 124 / 2, Y + 45 - 110 / 2, 208, true);
                new Bradley_rocket(X + 63 - 124 / 2, Y + 46 - 110 / 2, 232, true);
            }
            if (soundDelay == 0)
                sound.Play();
            base.Update();
        }
    }

    class Hover : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        public enum Animation
        {
            Idle,
            Shoot
        }
        int fire;
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/hover.png", 81, 65);
        public Hover(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Shoot);
            AddGraphic(spritemap);
            scene.Add(this);
            fire = scene.random.Next(60 * 2, 60 * 3);
        }

        public override void Update()
        {
            fire--;
            if (fire == 0)
            {
                new Projectile_heli_rocket(X + 65 - (93 / 2), Y + 46 - (63 / 2), scene.random.Next(50, 801), scene.random.Next(520, 750), false);
                fire = scene.random.Next(60 * 2, 60 * 3);
            }
            base.Update();
        }
    }

    class Sniper : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        public Sound sound = new Sound("Assets/Sounds/sniper.ogg") { Loop = false };
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/sniper.png", 66, 27);
        public Sniper(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22", 3).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 2, 60 * 3);
            scene.Add(this);
            SetHitbox(66, 27, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                sound.Play();
                cooldown = scene.random.Next(60 * 4, 60 * 5);
            }
            base.Update();
        }
    }

    class Turret : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        public enum Animation
        {
            Idle,
            Shoot
        }
        int cooldown = 0;
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/scrap_turret.png", 170, 80);
        public Sound sound = new Sound("Assets/Sounds/scrap_cannon.ogg") { Loop = false };
        public Turret(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0, 1", 4);
            spritemap.Add(Animation.Shoot, "2-17", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            scene.Add(this);
            cooldown = scene.random.Next(2 * 60, 3 * 60);
            SetHitbox(170, 80, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }
        public override void Update()
        {
            cooldown--;
            if (cooldown == 0)
            {
                if (spritemap.CurrentAnim == Animation.Idle)
                {
                    spritemap.Play(Animation.Shoot);
                    sound.Play();
                    cooldown = (int)spritemap.Anim(Animation.Shoot).TotalDuration;
                }
                else if (spritemap.CurrentAnim == Animation.Shoot)
                {
                    spritemap.Play(Animation.Idle);
                    cooldown = scene.random.Next(3 * 60, 5 * 60);
                }
            }
            base.Update();
        }
    }

    class Soldier : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        public enum Animation
        {
            Idle,
            Throw
        }
        int runtime;
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/soldier.png", 43, 42);
        public Soldier(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, scene.GetAnimationString(0, 0), 5).NoRepeat();
            spritemap.Add(Animation.Throw, scene.GetAnimationString(0, 11), 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration * scene.random.Next(3, 4);
            AddGraphic(spritemap);
            scene.Add(this);
            SetHitbox(43, 42, ColliderTags.Garbage);
            Hitbox.CenterOrigin();
        }

        public override void Update()
        {
            if (spritemap.CurrentAnim == Animation.Idle)
            {
                runtime--;
                if (runtime == 0)
                {
                    scene.Add(new grenade(X + 10 - 21, Y + 9 - 21, scene.random.Next(50, 801), scene.random.Next(520, 750), 40, true));
                    spritemap.Play(Animation.Throw);
                    runtime = (int)spritemap.Anim(Animation.Throw).TotalDuration + scene.random.Next(3 * 60, 4 * 60);
                }
            }
            if (spritemap.CurrentAnim == Animation.Throw)
            {
                runtime--;
                if (runtime == 0)
                {
                    spritemap.Play(Animation.Idle);
                    runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration * scene.random.Next(3, 7);
                }
            }
            base.Update();
        }
    }
}
