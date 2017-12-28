using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace IdleGame
{
    class Explosions : Entity
    {
        public enum ExplosionType
        {
            small,
            medium,
            big,
            huge,
            shell_normal,
            shell_big
        }
        public enum Animation
        {
            explosion
        }
        ExplosionType type;
        int delay;
        MainScene scene = (MainScene)MainScene.Instance;
        public Spritemap<Animation> spritemap;// = new Spritemap<Animation>("Assets/Img/Sprites/fat_tonk.png", 130, 62);
        public Explosions(float x, float y, ExplosionType type, int delay) : base(x, y)
        {
            this.type = type;
            if (this.type == ExplosionType.small)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_small.png", 32, 48);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 27), 3).NoRepeat();
            }
            else if (this.type == ExplosionType.medium)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_medium.png", 70, 70);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 28), 3).NoRepeat();
            }
            else if (this.type == ExplosionType.big)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_big.png", 80, 96);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 27), 3).NoRepeat();
            }
            else if (this.type == ExplosionType.huge)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_huge.png", 113, 137);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 23), 3).NoRepeat();
                spritemap.SetPosition(0, -137/2);
                //Console.WriteLine(X + "," + Y);
            }
            else if (this.type == ExplosionType.shell_normal)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_shell_normal.png", 60, 112);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 26), 3).NoRepeat();
                spritemap.SetPosition(0, -112 / 2);
            }
            else if (this.type == ExplosionType.shell_big)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_shell_big.png", 68, 170);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 29), 3).NoRepeat();
                spritemap.SetPosition(0, -170 / 2);
            }
            spritemap.CenterOrigin();

            this.delay = delay;
            Layer = -600;
        }
        public override void Update()
        {
            if (this.Timer == delay)
            {
                this.LifeSpan = this.Timer + (int)spritemap.Anim(Animation.explosion).TotalDuration;
                AddGraphic(spritemap);
                spritemap.Play(Animation.explosion);
            }
            base.Update();
        }
    }
}
