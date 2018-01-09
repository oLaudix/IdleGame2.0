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
        public List<Sound> explosion_sound_library = new List<Sound>();
        public Sound sound;
        public Spritemap<Animation> spritemap;
        public bool isVehicleDestruction;
        public Explosions(float x, float y, ExplosionType type, int delay, bool isVehicleDestruction) : base(x, y)
        {
            explosion_sound_library.Add(new Sound("Assets/Sounds/explosion_big.ogg") { Loop = false });
            explosion_sound_library.Add(new Sound("Assets/Sounds/explosion_09.ogg") { Loop = false });
            explosion_sound_library.Add(new Sound("Assets/Sounds/explosion_10.ogg") { Loop = false });
            explosion_sound_library.Add(new Sound("Assets/Sounds/explosion_11.ogg") { Loop = false });
            explosion_sound_library.Add(new Sound("Assets/Sounds/explosion_12.ogg") { Loop = false });
            this.type = type;
            this.isVehicleDestruction = isVehicleDestruction;
            sound = explosion_sound_library[scene.random.Next(1, 5)];
            if (this.type == ExplosionType.small)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_small.png", 32, 48);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 27), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
            }
            else if (this.type == ExplosionType.medium)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_medium.png", 70, 70);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 27), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
            }
            else if (this.type == ExplosionType.big)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_big.png", 80, 96);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 27), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
            }
            else if (this.type == ExplosionType.huge)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_huge.png", 113, 137);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 23), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
                Hitbox.SetPosition(0, -137 / 2);
                spritemap.SetPosition(0, -137/2);
                sound = explosion_sound_library[0];
                //Console.WriteLine(X + "," + Y);
            }
            else if (this.type == ExplosionType.shell_normal)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_shell_normal.png", 60, 112);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 26), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
                Hitbox.SetPosition(0, -112 / 2);
                spritemap.SetPosition(0, -112 / 2);
            }
            else if (this.type == ExplosionType.shell_big)
            {
                spritemap = new Spritemap<Animation>("Assets/Img/Sprites/Explosions/explosion_shell_big.png", 68, 170);
                spritemap.Add(Animation.explosion, scene.GetAnimationString(0, 29), 3).NoRepeat();
                SetHitbox(spritemap.TextureRegion.Width, spritemap.TextureRegion.Height, ColliderTags.Garbage);
                Hitbox.SetPosition(0, -170 / 2);
                spritemap.SetPosition(0, -170 / 2);
            }
            if (isVehicleDestruction)
            {
                Hitbox.Width *= 2;
                Hitbox.Height *= 2;
            }
            spritemap.CenterOrigin();
            Hitbox.CenterOrigin();
            this.delay = delay;
            Layer = -600;
        }
        public override void Added()
        {
            //scene.LayerEnemies();
            base.Added();
        }
        public override void Update()
        {
            if (this.Timer == delay)
            {
                this.LifeSpan = this.Timer + (int)spritemap.Anim(Animation.explosion).TotalDuration;
                AddGraphic(spritemap);
                sound.Play();
                spritemap.Play(Animation.explosion);
            }
            base.Update();
        }
    }
}
