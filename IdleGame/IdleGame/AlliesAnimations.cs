﻿using System;
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
            }
            base.Update();
        }
    }
}