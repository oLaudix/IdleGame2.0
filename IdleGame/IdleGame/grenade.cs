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
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/grenade.png", 26, 24);
        Vector2 destination;
        Vector2 start;
        int delay = 0;
        int counter = 60;
        bool ally;
        public grenade(float x, float y, float ex, float ey, int delay, bool ally) : base(x, y)
        {
            spritemap.Add(Animation.rotate, "1-15", 2);
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
}
