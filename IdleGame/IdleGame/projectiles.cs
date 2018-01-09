using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class grenade : Entity
    {
        public enum Animation
        {
            rotate
        }
        float height;
        Sound Throw = new Sound("Assets/Sounds/grenade_throw.ogg") { Loop = false };
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/grenade.png", 26, 24);
        Vector2 destination;
        Vector2 start;
        int delay = 0;
        int counter = 60;
        bool ally;
        public grenade(float x, float y, float ex, float ey, int delay, bool ally) : base(x, y)
        {
            spritemap.Add(Animation.rotate, "0-15", 2);
            destination = new Vector2(ex, ey);
            start = new Vector2(x, y);
            spritemap.CenterOrigin();
            Layer = -600;
            height = start.Y - 400;
            this.delay = delay;
            this.ally = ally;
        }

        public override void Update()
        {
            if (this.Timer >= this.delay)
            {
                if (this.Timer == this.delay)
                {
                    spritemap.Play(Animation.rotate);
                    AddGraphic(spritemap);
                    Throw.Play();
                }
                if (!ally)
                    X += (destination.X - start.X) / 120;
                else
                    X -= (start.X - destination.X) / 120;
                if (this.Timer <= this.delay + 60)
                {
                    //Y -= ((start.Y - hight) /1830)*counter;
                    Y -= ((start.Y - height) / 1830) * counter;
                    counter--;
                }
                else
                {
                    Y += ((destination.Y - height) / 1830) * counter;
                    counter++;
                }
            }
            if ((X >= destination.X && !ally) || (X <= destination.X && ally))
            {
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0, false));
                RemoveSelf();
            }
            base.Update();
        }
    }

    class Projectile_rocket : Entity
    {
        public enum Animation
        {
            Fly
        }
        public Sound sound = new Sound("Assets/Sounds/rocket_projectile.ogg") { Loop = false };
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/projectile_rocket.png", 54, 19);
        Vector2 end;
        Vector2 start;
        MainScene scene = (MainScene)MainScene.Instance;
        public Projectile_rocket(float x, float y, float ex, float ey) : base(x, y)
        {
            spritemap.Add(Animation.Fly, "0-1", 3);
            start = new Vector2(x, y);
            end = new Vector2(ex, ey);
            //spritemap.Angle = (float)((Math.Atan2(start.X - end.X, start.Y - end.Y)) * (180 / Math.PI)) + 90;
            spritemap.CenterOrigin();
            spritemap.Angle = (float)((Math.Atan2(start.X - end.X, start.Y - end.Y)) * (180 / Math.PI)) + 90;
            AddGraphic(spritemap);
            spritemap.Play(Animation.Fly);
            scene.Add(this);
            sound.Play();
            Layer = -600;
        }
        public override void Update()
        {
            X += ((end.X - start.X) / 100000) * (float)Math.Pow(this.Timer, 2);
            Y += ((end.Y - start.Y) / 100000) * (float)Math.Pow(this.Timer, 2);
            if (X > end.X)
            {
                sound.Stop();
                RemoveSelf();
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0, false));
            }
            base.Update();
            /*if (Input.MouseButtonDown(MouseButton.Right))
            {
                start = new Vector2(X, Y);
                end = new Vector2(Input.MouseX, Input.MouseY);
                spritemap.Angle = (float)((Math.Atan2(start.X - end.X, start.Y - end.Y)) * (180 / Math.PI)) + 90;
            }*/
        }
    }

    class Projectile_heli_rocket : Entity
    {
        public enum Animation
        {
            Fly,
            Idle
        }
        public Sound sound = new Sound("Assets/Sounds/rocket_projectile_heli.ogg") { Loop = false };
        public Spritemap<Animation> spritemap;
        Vector2 end;
        Vector2 start;
        MainScene scene = (MainScene)MainScene.Instance;
        bool heli;
        public Projectile_heli_rocket(float x, float y, float ex, float ey, bool heli) : base(x, y)
        {
            this.heli = heli;
            if (heli)
                spritemap = new Spritemap<Animation>("Assets/Img/projectile_heli_rocket.png", 55, 16);
            else
                spritemap = new Spritemap<Animation>("Assets/Img/projectile_hover_rocket.png", 60, 15);
            spritemap.Add(Animation.Fly, "1-3", 1);
            spritemap.Add(Animation.Idle, "0", 4).NoRepeat();
            start = new Vector2(x, y);
            end = new Vector2(ex, ey);
            //spritemap.Angle = (float)((Math.Atan2(start.X - end.X, start.Y - end.Y)) * (180 / Math.PI)) + 90;
            spritemap.CenterOrigin();
            spritemap.Angle = (float)((Math.Atan2(start.X - end.X, start.Y - end.Y)) * (180 / Math.PI)) - 90;
            AddGraphic(spritemap);
            spritemap.Play(Animation.Idle);
            Layer = -600;
            scene.Add(this);
        }
        public override void Update()
        {
            if (this.Timer == 30)
            {
                spritemap.Play(Animation.Fly);
                sound.Play();
            }
            if (this.Timer > 30)
            {
                X -= ((start.X - end.X) / 100000) * (float)Math.Pow(this.Timer, 2);
            }
            else
            {
                X -= ((start.X - end.X) / 10000000) * (float)Math.Pow(this.Timer, 2);
            }
            Y += ((end.Y - start.Y) / 100000) * (float)Math.Pow(this.Timer, 2);
            if (X < end.X)
            {
                RemoveSelf();
                sound.Stop();
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.medium, 0, false));
            }
            base.Update();
        }
    }

    class projectile_mortar : Entity
    {
        public enum Animation
        {
            rotate
        }
        float height;
        Sound Throw = new Sound("Assets/Sounds/mortar.ogg") { Loop = false };
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/projectile_mortar.png", 19, 7);
        Vector2 destination;
        Vector2 start;
        int counter = 60;
        int delay = 0;
        public projectile_mortar(float x, float y, float ex, float ey, int delay) : base(x, y)
        {
            spritemap.Add(Animation.rotate, "0", 2);
            destination = new Vector2(ex, ey);
            start = new Vector2(x, y);
            spritemap.CenterOrigin();
            Layer = -600;
            height = start.Y - 400;
            this.delay = delay;
            spritemap.Angle = -68;
        }

        public override void Update()
        {
            if (this.Timer >= this.delay)
            {
                if (this.Timer == this.delay)
                {
                    spritemap.Play(Animation.rotate);
                    AddGraphic(spritemap);
                    //Throw.Volume = Sound.GlobalVolume * 0.2f;
                    Throw.Play();
                }
                if (this.Timer <= this.delay + 60)
                {
                    //Y -= ((start.Y - hight) /1830)*counter;
                    spritemap.Angle += (68f/1830) * counter;
                    Y -= ((start.Y - height) / 1830) * counter;
                    counter--;
                }
                else
                {
                    spritemap.Angle += (68f / 1830) * counter;
                    Y += ((destination.Y - height) / 1830) * counter;
                    counter++;;
                }
                X -= (start.X - destination.X) / 120;
            }
            if (X <= destination.X)
            {
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0, false));
                RemoveSelf();
            }
            base.Update();
        }
    }

    class Bradley_rocket : Entity
    {
        public enum Animation
        {
            up,
            down
        }
        Sound Throw = new Sound("Assets/Sounds/rocket_launch.ogg") { Loop = false };
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/bradley_rocket_start.png", 69, 69);
        public Spritemap<Animation> spritemap2 = new Spritemap<Animation>("Assets/Img/Sprites/bradley_rocket_down.png", 29, 92);
        float  counter;
        int delay = 0;
        int stage = 1;
        float hitY;
        bool sound = true;
        public Bradley_rocket(float x, float y, int delay, bool sound) : base(x, y)
        {
            spritemap.Add(Animation.up, "0-2", 1);
            spritemap2.Add(Animation.down, "0-2", 1);
            spritemap.CenterOrigin();
            spritemap2.CenterOrigin();
            Layer = +600;
            this.delay = delay;
            scene.Add(this);
            this.sound = sound;
        }

        public override void Update()
        {
            if (this.Timer >= this.delay)
            {
                if (stage == 1)
                {
                    if (this.Timer == this.delay)
                    {
                        spritemap.Play(Animation.up);
                        AddGraphic(spritemap);
                        //Throw.Volume = Sound.GlobalVolume * 0.2f;
                        if (sound)
                            Throw.Play();
                    }
                    if (this.Timer > 30)
                    {
                        X -= ((100f) / 100000) * (float)Math.Pow(this.Timer - this.delay, 2);
                        Y -= ((100f) / 100000) * (float)Math.Pow(this.Timer - this.delay, 2);
                    }
                    else
                    {
                        X -= ((100f) / 10000000) * (float)Math.Pow(this.Timer - this.delay, 2);
                        Y -= ((100f) / 10000000) * (float)Math.Pow(this.Timer - this.delay, 2);
                    }
                    if (Y < -200)
                        stage = 2;
                }
                else if (stage == 2)
                {
                    RemoveGraphic(spritemap);
                    spritemap2.Play(Animation.down);
                    AddGraphic(spritemap2);
                    SetPosition(scene.random.Next(50, 801), -1000);
                    stage = 3;
                    counter = this.Timer;
                    hitY = scene.random.Next(520, 750);
                    //scene.random.Next(50, 801), scene.random.Next(520, 750)
                }
                else if (stage == 3)
                {
                    Y += ((100f) / 10000) * (float)Math.Pow(this.Timer - counter, 2);
                    if (Y + 92/2 > hitY)
                    {
                        scene.Add(new Explosions(X, Y + 92 / 2, Explosions.ExplosionType.huge, 0, false));
                        RemoveSelf();
                    }
                }
            }
            base.Update();
        }
    }

}
