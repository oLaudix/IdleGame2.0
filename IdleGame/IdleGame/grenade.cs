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
        float hight;
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap = new Spritemap<Animation>("Assets/Img/Sprites/grenade.png", 26, 24);
        Vector2 destination;
        Vector2 start;
        int counter = 60;
        public grenade(float x, float y, float ex, float ey) : base(x, y)
        {
            spritemap.Add(Animation.rotate, "1-15", 2);
            destination = new Vector2(ex, ey);
            start = new Vector2(x, y);
            spritemap.CenterOrigin();
            Layer = -600;
            hight = start.Y - 300;
        }

        public override void Update()
        {
            if (this.Timer >= 40)
            {
                if (this.Timer == 40)
                {
                    spritemap.Play(Animation.rotate);
                    AddGraphic(spritemap);
                }
                X += (destination.X - start.X) / 120;
                if (this.Timer <= 100)
                {
                    Y -= ((start.Y - hight) /1830)*counter;
                    counter--;
                }
                else
                {
                    Y += ((destination.Y - hight) / 1830) * counter;
                    counter++;
                }
            }
            if (X >= destination.X)
            {
                scene.Add(new Explosions(X, Y, Explosions.ExplosionType.small, 0));
                RemoveSelf();
            }
            base.Update();
        }
    }
}
