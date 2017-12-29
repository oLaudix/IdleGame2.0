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
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/dicokka.png", 124, 76);
        public Dicokka(float x, float y) : base (x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60));
            }
            base.Update();
        }
    }

    class BiggestTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/biggest_tonk.png", 187, 80);
        public BiggestTonk(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "4", 4);
            spritemap.Add(Animation.Shoot, "4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 , 0, 1, 2, 3", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 2, 60 * 3);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 3, 60 * 4);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_big, 60));
            }
            base.Update();
        }
    }

    class FatTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/fat_tonk.png", 130, 62);
        public FatTonk(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60));
            }
            base.Update();
        }
    }

    class Heli : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/heli.png", 88, 91);
        public Heli(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Shoot);
            AddGraphic(spritemap);
            cooldown = 40;
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
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
            base.Update();
        }
    }

    class BigTonk : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
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
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.shell_normal, 60));
            }
            base.Update();
        }
    }

    class Minigun : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/minigun.png", 110, 45);
        public Minigun(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11", 4);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Shoot);
            AddGraphic(spritemap);
            scene.Add(this);
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
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/mortar.png", 53, 54);
        public Mortar(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 1, 60 * 2);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 2, 60 * 3);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.small, 60));
            }
            base.Update();
        }
    }

    class Rocket : Entity
    {
        MainScene scene = (MainScene)MainScene.Instance;
        int cooldown;
        public enum Animation
        {
            Idle,
            Shoot
        }
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/rocket.png", 124, 110);
        public Rocket(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0,1,2,3,4,5,6,7, 8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10,8,9,10, 11,12,13,14,15,16,11,12,13,14,15,16,11,12,13,14,15,16,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26, 27,28,29,30,31,32,33,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,34,", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 4, 60 * 5);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
                cooldown = scene.random.Next(60 * 15, 60 * 20);
                scene.Add(new Explosions(scene.random.Next(50, 801), scene.random.Next(520, 750), Explosions.ExplosionType.huge, 30));
            }
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
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/hover.png", 104, 70);
        public Hover(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35", 2);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Shoot);
            AddGraphic(spritemap);
            scene.Add(this);
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
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/sniper.png", 66, 27);
        public Sniper(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22", 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            AddGraphic(spritemap);
            cooldown = scene.random.Next(60 * 2, 60 * 3);
            scene.Add(this);
        }
        public override void Update()
        {
            cooldown--;
            //Console.WriteLine
            if (cooldown == 0)
            {
                spritemap.Play(Animation.Shoot);
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
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/turret.png", 90, 42);
        public Turret(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, "0", 4);
            spritemap.Add(Animation.Shoot, "0, 1, 2, 3", 4);
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Shoot);
            AddGraphic(spritemap);
            scene.Add(this);
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
        Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/enemy_soldier.png", 88, 68);
        public Soldier(float x, float y) : base(x, y)
        {
            spritemap.Add(Animation.Idle, scene.GetAnimationString(96, 102), 5);
            spritemap.Add(Animation.Throw, scene.GetAnimationString(115, 129), 4).NoRepeat();
            spritemap.CenterOrigin();
            spritemap.Play(Animation.Idle);
            runtime = (int)spritemap.Anim(Animation.Idle).TotalDuration * scene.random.Next(3, 4);
            AddGraphic(spritemap);
            spritemap.FlippedX = true;
            scene.Add(this);
        }

        public override void Update()
        {
            if (spritemap.CurrentAnim == Animation.Idle)
            {
                runtime--;
                if (runtime == 0)
                {
                    Console.WriteLine(X + 45);
                    scene.Add(new grenade(X + 42 - 44, Y + 32 - 34, scene.random.Next(50, 801), scene.random.Next(520, 750), 40, true));
                    spritemap.Play(Animation.Throw);
                    runtime = (int)spritemap.Anim(Animation.Throw).TotalDuration;
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
