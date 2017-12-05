using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _30XXremake
{
    //each type of fighter (ie each Pkmn species) will inherit from this class. The only things that each specific species class will add are: data for each property, animations, moves
    class fighter
    {
        private move atk;
        private string species;

        public bool charging = false;
        public bool onCooldown = false;
        protected double cdLength = 0.25;
        protected double cdTimer = 0; //some weird makeshift timer thing? try figuring out ElapsedGameTime first
        protected double jumpCount = 2;
        protected float speed = 0;
        protected bool helpless = false;

        public int percent = 0;
        public bool paused = false;

        private Vector2 vel = new Vector2(0, 0);
        private Vector2 accel = new Vector2(1, 2);
        private Vector2 maxVel = new Vector2(3, 5);

        private Vector2 position;
        private string sprite;
        public Rectangle hitbox;
        public bool isColliding = false;

        KeyboardState prevKBS;
        KeyboardState currKBS;

        //public List<animation> animations = new List<animation>();

        public fighter(Vector2 position, string sprite, float speed, int sWidth, int sHeight)
        {
            this.position = position;
            this.sprite = sprite;
            this.speed = speed;

            hitbox = new Rectangle((int)position.X, (int)position.Y, sWidth, sHeight);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        //for debugging purposes, delete later
        public Vector2 Vel
        {
            get { return vel; }
            set { vel = value; }
        }

        public void movement(GameTime gt)
        {
            prevKBS = currKBS;
            currKBS = Keyboard.GetState();

            if (!paused && !onCooldown)
            {
                if (currKBS.IsKeyDown(Keys.Left))
                {
                    vel.X = physics.calcVel(vel.X, accel.X, maxVel.X, gt);
                    position.X += -vel.X * speed;
                }
                else if (currKBS.IsKeyDown(Keys.Right))
                {
                    //position.X += physics.calcVel(vel.X, accel.X, maxVel.X, gt) * speed;
                    vel.X = physics.calcVel(vel.X, accel.X, maxVel.X, gt);
                    position.X += vel.X * speed;
                }
                else
                {
                    vel.X = 0;
                }

                if (currKBS.IsKeyDown(Keys.Up))
                {
                    //this is a really shitty jump, fix plz
                    position.Y -= physics.calcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed * 2;
                }
                else if (currKBS.IsKeyDown(Keys.Down))
                {
                    position.Y += physics.calcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
                }
            }

            //position.Y += accel.Y;
        }

        //lower case update to avoid confusion with the default one
        public void update(GameTime gt)
        {
            //if this fighter isn't colliding with the stage, then gravity does its thing
            if (!hitbox.Intersects(physics.StageHitbox))
            {
                position.Y += physics.calcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
            }
            
        }

         /*animations for fighters, basically a class which each fighter.cs child uses to add movement and attack animations, to avoid having a really long animation.cs class. Also stores them for neat storage and stuff
         * Structure (which I think would be best put into fighters.cs for easy access there, like in Flixel):
         * - List of animations
         *  - Walk animations
         *   - [0]: Sideways
         *   - [1]: Idle (Maybe it's possible to just set the sprite to the first frame of the animation)
         *  - Attack animations
         *   - [2]: NeutralB
         *   - [3]: SideB
         *   - [3]: UpB
         *   - [4]: DownB
         * 
         * Todo:
         * - Regular attacks, aerials, tilts
         * - Shielding, rolling, spot dodging, air dodging
         */ 

        //Somehow fix this having a really long perimeters list?
        /*public void addToAnimationsList()
        {
        }*/
    }
}
