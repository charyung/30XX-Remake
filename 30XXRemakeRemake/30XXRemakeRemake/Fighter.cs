using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _30XXRemakeRemake
{
	//abstract, in case I forget in the future:
	//- As a class, it means it can't be called on its own
	//- As a method, it means that it has to be derived by a child class before being called
	internal abstract class Fighter : IUpdatable
	{
		///<summary>
		///A class for each species of fighters.
		///</summary>

		public bool charging = false; //Whether the fighter is charging an attack (e.g. Aura Sphere on Lucario)
		public bool onCooldown = false; //Whether the fighter is on cooldown. If true, the fighter can't use any move.
		protected float cdLength; //How long the fighter is on cooldown for. Measured in milliseconds.
		protected double jumpCount = 2; //Amount of jumps the fighter has. 2 for most but will probably have more for species like birb mons.
		protected bool isJumping = false; //Simply, whether the fighter is jumping.
		protected float speed = 0; // The fighter's own moving speed.
		protected bool helpless = false; //Whether the fighter is in helpless. This is slightly different than onCooldown because a helpless fighter can't do anything but move.

		public int percent = 0; //The percentage the fighter is at. The higher means the more injured they are.
		public bool paused = false; //Pauses the fighter (i.e. can't do anything at all), usually is used as part of a stationary move like Whirlpool on Omastar.

		protected Vector2 vel = new Vector2(0, 0);
		protected Vector2 accel = new Vector2(1, 0.25f);
		//The Y parameter of maxVel is basically max jump height here, combined with accel.Y.
		protected Vector2 maxVel = new Vector2(3, 7);

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
		protected List<Attack> moves = new List<Attack>();

		///<summary>
		///Constructor for the Fighter class.
		///</summary>
		///<param name="position"> The position on the screen where the fighter is.</param>
		///<param name="sWidth"> The width of this fighter's sprite. </param>
		///<param name="sHeight"> The height of thie fighter's sprite. </param>
		///<param name="speed"> The speed of this fighter. </param>
		protected Fighter(Vector2 position, int sWidth, int sHeight, float speed)
		{

			this.position = position;
			this.speed = speed;

			hitbox = new Rectangle((int)position.X, (int)position.Y, sWidth, sHeight);
		}

		internal Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		//for debugging purposes, delete later
		internal Vector2 Vel
		{
			get { return vel; }
			set { vel = value; }
		}

		internal Vector2 Accel
		{
			get { return accel; }
			set { accel = value; }
		}

		//For all intents and purposes, the string version will never be accessed outside of this and its children classes (and facing, the variable, is protected rather than private anyways) so for the sake of convenicence, we call this Facing but actually refer to the SpriteEffects.
		public SpriteEffects Facing
		{
			get { return facingVisual; }
		}

		public Texture2D SpriteTexture
		{
			get { return idle.SpriteTexture; }
		}

		public void Movement(GameTime gt)
		{
			prevKBS = currKBS;
			currKBS = Keyboard.GetState();

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

					//position.Y -= Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed * 3;
				if (!isJumping && jumpCount > 0 && prevKBS.IsKeyUp(Keys.Up))
				{
					//position.Y = vel.Y * (float)gt.ElapsedGameTime.TotalSeconds;
					Jump(gt);
					jumpCount--;
				}
			}
			else if (currKBS.IsKeyDown(Keys.Down))
			{
				//Some fastfalling stuff, but let's deal with this later.
				//position.Y += Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
			}

			//position.Y += accel.Y;
		}

		//A function for all the attacks
		private void Attack(GameTime gt)
		{
			if (paused || onCooldown)
				return;
			
			if (currKBS.IsKeyDown(Keys.Z))
			{
				//if ((!prevKBS.IsKeyDown(Keys.Left) && !prevKBS.IsKeyDown(Keys.Right)) && )
				if (currKBS.IsKeyDown(Keys.Left) || currKBS.IsKeyDown(Keys.Right))
				{
					SideB();
				}
				else if (currKBS.IsKeyDown(Keys.Down))
				{
					DownB();
				}
				else if (currKBS.IsKeyDown(Keys.Up))
				{
					UpB();
				}
				else
				{
					NeutralB();
				}

			}
		}
		
		//A function for jumping. TODO: Implement multi jumps.
		private void Jump(GameTime gt)
		{
			isJumping = true;
			vel.Y = Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * -speed * 5;
		}

		//How long the fighter goes on cooldown for. Calculated in milliseconds.
		protected void Cooldown(GameTime gt)
		{
			cdLength -= Math.Min((float) gt.ElapsedGameTime.TotalMilliseconds, cdLength);

			if (cdLength == 0)
			{
				onCooldown = false;
			}
		}

		//Change the paused status to the opposite. Just that simple.
		protected void PauseUnpause(GameTime gt)
		{
			paused = !paused;
		}

		protected void CheckPause(Attack move)
		{
			while (!move.SpriteTexture.Finished)
			{
				paused = true;
			}
		}

		protected abstract void NeutralB();
		protected abstract void SideB();
		protected abstract void UpB();
		protected abstract void DownB();

		public void Update(GameTime gt)
		{
			//if this fighter isn't colliding with the stage, then gravity does its thing
			if (!hitbox.Intersects(Physics.StageHitbox))
			{
				Physics.Gravity(position, vel, accel, maxVel.Y, gt);
				//Physics.Gravity(this, gt);
				isJumping = false;
			}
			else
			{
				jumpCount = 2;
			}

			if (!paused)
			{
				Movement(gt);
			}
			
			hitbox.X = (int)position.X;
			hitbox.Y = (int)position.Y;

			Attack(gt);

			if (onCooldown)
			{
				Cooldown(gt);
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
		public void addToAnimationsList()
		{
			//Walking
		}
	}
}
