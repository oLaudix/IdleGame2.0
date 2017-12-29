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
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0));
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
        }
        public override void Update()
        {
            X += ((end.X - start.X) / 100000) * (float)Math.Pow(this.Timer, 2);
            Y += ((end.Y - start.Y) / 100000) * (float)Math.Pow(this.Timer, 2);
            if (X > end.X)
            {
                sound.Stop();
                RemoveSelf();
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0));
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
}
