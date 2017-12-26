using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _30XXRemakeRemake
{
    class Fighter
    {
        ///<summary>
        ///A class for each species of fighters.
        ///</summary>

        public bool charging = false; //Whether the fighter is charging an attack (e.g. Aura Sphere on Lucario)
        public bool onCooldown = false; //Whether the fighter is on cooldown. If true, the fighter can't use any move.
        protected double cdLength = 0.25; //How long the fighter goes on cooldown for. Possibly move to Move so each move can have different CD?
        protected double cdTimer = 0; //some weird makeshift timer thing? try figuring out ElapsedGameTime first
        protected double jumpCount = 2; //Amount of jumps the fighter has. 2 for most but will probably have more for species like birb mons.
        protected bool isJumping = false; //Simply, whether the fighter is jumping.
        protected float speed = 0; // The fighter's current speed.
        protected bool helpless = false; //Whether the fighter is in helpless. This is slightly different than onCooldown because a helpless fighter can't do anything but move.

        public int percent = 0; //The percentage the fighter is at. The higher means the more injured they are.
        public bool paused = false; //Pauses the fighter (i.e. can't do anything at all), usually is used as part of a stationary move like Whirlpool on Omastar.

        private Vector2 vel = new Vector2(0, 0);
        private Vector2 accel = new Vector2(1, 0.25f);
        //The Y parameter of maxVel is basically max jump height here, combined with accel.Y.
        private Vector2 maxVel = new Vector2(3, 7);

        protected Vector2 position;
        protected Animation idle;
        public Animation walking;
        protected string facing = "Right";
        private SpriteEffects facingVisual = SpriteEffects.None;
        public Rectangle hitbox;
        public bool isColliding = false;
        //onGround is a special isColliding to see if this fighter's bottom is colliding with a ground.
        private bool onGround = false; //In Flixel, there was some weird ass bitwise operation magic here. Let's learn about that more before copying.

        KeyboardState prevKBS;
        KeyboardState currKBS;

        //public List<animation> animations = new List<animation>();
        protected List<Move> moves = new List<Move>();

        ///<summary>
        ///Constructor for the Fighter class.
        ///</summary>
        ///<param name="position"> The position on the screen where the fighter is.</param>
        ///<param name="speed"> The speed of this fighter. </param>
        ///<param name="sWidth"> The width of this fighter's sprite. </param>
        ///<param name="sHeight"> The height of thie fighter's sprite. </param>
        public Fighter(Vector2 position, int sWidth, int sHeight, float speed)
        {

            this.position = position;
            this.speed = speed;

            hitbox = new Rectangle((int)position.X, (int)position.Y, sWidth, sHeight);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //for debugging purposes, delete later
        public Vector2 Vel
        {
            get { return vel; }
            set { vel = value; }
        }

        //For all intents and purposes, the string version will never be accessed outside of this and its children classes (and facing, the variable, is protected rather than private anyways) so for the sake of convenicence, we call this Facing but actually refer to the SpriteEffects.
        public SpriteEffects Facing
        {
            get { return facingVisual;  }
        }

        public string Sprite
        {
            get { return idle.SpriteTexture; }
        }

        public void Movement(GameTime gt)
        {
            prevKBS = currKBS;
            currKBS = Keyboard.GetState();

            if (!paused && !onCooldown)
            {
                if (currKBS.IsKeyDown(Keys.Left))
                {
                    vel.X = Physics.CalcVel(vel.X, accel.X, maxVel.X, gt);
                    position.X += -vel.X * speed;
                    walking.Animate(gt);
                    facing = "Left";
                    facingVisual = SpriteEffects.FlipHorizontally;
                }
                else if (currKBS.IsKeyDown(Keys.Right))
                {
                    vel.X = Physics.CalcVel(vel.X, accel.X, maxVel.X, gt);
                    position.X += vel.X * speed;
                    walking.Animate(gt);
                    facing = "Right";
                    facingVisual = SpriteEffects.None;
                }
                else
                {
                    idle.Animate(gt);
                    vel.X = 0;
                }

                if (currKBS.IsKeyDown(Keys.Up))
                {
                    //this is a really shitty jump, fix plz

                    if (!isJumping)
                    {
                        //position.Y -= Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed * 3;
                        if (jumpCount > 0 && prevKBS.IsKeyUp(Keys.Up))
                        {
                            //position.Y = vel.Y * (float)gt.ElapsedGameTime.TotalSeconds;
                            Jump(gt);
                            jumpCount--;
                        }
                    }
                }
                else if (currKBS.IsKeyDown(Keys.Down))
                {
                    //Some fastfalling stuff, but let's deal with this later.
                    //position.Y += Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
                }
            }

            //position.Y += accel.Y;
        }

        private void Jump(GameTime gt)
        {
            isJumping = true;
            vel.Y = Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * -speed * 5;
        }

        public void Update(GameTime gt)
        {
            //if this fighter isn't colliding with the stage, then gravity does its thing
            float spriteBottom = this.position.Y + this.hitbox.Height;
            //if (spriteBottom <= Physics.StageHitbox.Y)
            if (!hitbox.Intersects(Physics.StageHitbox))
            {
                vel.Y = Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
                isJumping = false;
            }
            else
            {
                vel.Y = 0;
                jumpCount = 2;
            }

            this.Movement(gt);
            position.Y += vel.Y;
            this.hitbox.X = (int)this.position.X;
            this.hitbox.Y = (int)this.position.Y;


            //Physics.Gravity(this.position);
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
        public void addToAnimationsList()
        {
            //Walking
        }
    }
}
