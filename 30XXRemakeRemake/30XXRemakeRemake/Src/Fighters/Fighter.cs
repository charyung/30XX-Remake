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

		internal enum FighterStates
		{
			Normal, // Default
			Helpless, //Whether the fighter is in helpless. This is slightly different than onCooldown because a helpless fighter can't do anything but move.
			Paused, //Pauses the fighter (i.e. can't do anything at all), usually is used as part of a stationary move like Whirlpool on Omastar.
			Walking
		}

		protected enum ActionTypes
		{
			Idle,
			Walking,
			NeutralSp,
			UpSp,
			SideSp,
			DownSp,
		}

		protected float cdTimer; //How long the fighter is on cooldown for. Measured in milliseconds.
		protected int jumpCount = 2; //Amount of jumps the fighter has. 2 for most but will probably have more for species like birb mons.
		protected float jumpHeight;
		protected float speed = 0; // The fighter's own moving speed.

		public int percent = 0; //The percentage the fighter is at. The higher means the more injured they are.

		protected Vector2 vel = new Vector2(0, 0);
		protected Vector2 accel = new Vector2(0, 3);
		//The Y parameter of maxVel is basically max jump height here, combined with accel.Y.
		protected Vector2 maxVel = new Vector2(5, 7);
		protected float airResist = 1; // Air resistance

		protected Vector2 position;
		protected Animation idle;
		public Animation walking;
		protected Animation currAnimation;
		protected string facing = "Right";
		private SpriteEffects facingVisual = SpriteEffects.None;
		public Rectangle hitbox;
		public bool isColliding = false;
		//onGround is a special isColliding to see if this fighter's bottom is colliding with a ground.
		private bool onGround = false; //In Flixel, there was some weird ass bitwise operation magic here. Let's learn about that more before copying.

		internal bool isJumping = false; //Simply, whether the fighter is jumping.
		internal FighterStates state = FighterStates.Normal;

		KeyboardState prevKBS;
		KeyboardState currKBS;

		protected bool isPlayer;

		protected List<(ActionTypes type, Attack attack)> activeAttacks = new List<(ActionTypes type, Attack attack)>();

		///<summary>
		///Constructor for the Fighter class.
		///</summary>
		///<param name="isPlayer">Whether the fighter is player controlled</param>
		///<param name="position"> The position on the screen where the fighter is.</param>
		///<param name="sWidth"> The width of this fighter's sprite. </param>
		///<param name="sHeight"> The height of thie fighter's sprite. </param>
		///<param name="speed"> The speed of this fighter. </param>
		protected Fighter(bool isPlayer, Vector2 position, int sWidth, int sHeight, float speed, float jumpHeight)
		{

			this.position = position;
			this.speed = speed;
			this.jumpHeight = jumpHeight;
			this.isPlayer = isPlayer;

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
			if (!isPlayer) return;

			prevKBS = currKBS;
			currKBS = Keyboard.GetState();

			if (currKBS.IsKeyDown(Keys.Left))
			{
				accel.X = -4;
				currAnimation = walking;
				facing = "Left";
				facingVisual = SpriteEffects.FlipHorizontally;
				state = FighterStates.Walking;
			}
			else if (currKBS.IsKeyDown(Keys.Right))
			{
				accel.X = 4;
				currAnimation = walking;
				facing = "Right";
				facingVisual = SpriteEffects.None;
				state = FighterStates.Walking;
			}
			else
			{
				state = FighterStates.Normal;
				accel.X = 0;
			}

			if (state != FighterStates.Normal && state != FighterStates.Walking)
				return;

			if (currKBS.IsKeyDown(Keys.X))
			{
				//this is a really shitty jump, fix plz

				if (jumpCount > 0 && prevKBS.IsKeyUp(Keys.X))
				{
					isJumping = true;
					vel.Y = jumpHeight;
					jumpCount--;
				}
			}
			else if (currKBS.IsKeyDown(Keys.Down))
			{
				//Some fastfalling stuff, but let's deal with this later.
				//position.Y += Physics.CalcVel(vel.Y, accel.Y, maxVel.Y, gt) * speed;
			}
		}

		//A function for all the attacks
		private void Attack(GameTime gt)
		{
			if (!isPlayer) return;

			if ((state != FighterStates.Normal && state != FighterStates.Walking) || cdTimer > 0)
				return;
			
			if (!prevKBS.IsKeyDown(Keys.Z) && currKBS.IsKeyDown(Keys.Z))
			{
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

		//How long the fighter goes on cooldown for. Calculated in milliseconds.
		protected void Cooldown(GameTime gt)
		{
			cdTimer -= Math.Min((float) gt.ElapsedGameTime.TotalMilliseconds, cdTimer);
		}

		internal void Unpause()
		{
			state = FighterStates.Normal;
		}

		/// <summary>
		///		Set velocity according to the given knockback values
		/// </summary>
		/// <param name="angle">Angle in radians. -pi &lt;= x &lt;= pi</param>
		/// <param name="amount">The hypotenuse of the triangle</param>
		internal void TakeKnockback(double angle, double amount)
		{
			int xMod = 1;
			int yMod = 1;

			if (angle < Math.PI / 2)
			{
				// 3rd quadrant
				xMod = -1;
				angle = Math.PI - (angle * -1);
			}
			else if (angle < 0)
			{
				// 4th quadrant
				angle *= -1;
			}
			else if (angle < Math.PI / 2)
			{
				// 1st quadrant
				yMod = -1;
			}
			else
			{
				// 2nd quadrant
				xMod = -1;
				yMod = -1;
				angle = Math.PI - angle;
			}

			vel.X = (float) (Math.Sin(angle) * amount * xMod);
			vel.Y = (float) (Math.Cos(angle) * amount * yMod);
		}

		protected abstract void NeutralB();
		protected abstract void SideB();
		protected abstract void UpB();
		protected abstract void DownB();

		public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach ((_, Attack attack) in activeAttacks)
			{
				attack.Draw(spriteBatch, gameTime);
			}

			currAnimation?.Animate(gameTime);
			spriteBatch.Draw(SpriteTexture, Position, currAnimation?.SourceRect ?? idle.SourceRect, Color.White, 0f, Vector2.Zero, 1, Facing, 0);
		}

		public virtual void Update(GameTime gt)
		{
			if (state == FighterStates.Paused)
			{
				vel = Vector2.Zero;
			}
			else
			{
				//if this fighter isn't colliding with the stage, then gravity does its thing
				if (!hitbox.Intersects(Physics.StageHitbox))
				{
					//vel.Y += Physics.Gravity(position, vel, accel, maxVel.Y, gt);
					vel.Y += 1f; // Todo: Remove hardcode
				}
				else
				{
					isJumping = false;
					jumpCount = 2;
					vel.Y = Math.Min(vel.Y, 0);
					// Prevent the player from falling into the ground. The -1 makes sure that the player doesn't keep hovering just above the ground, causing them to vibrate up and down
					position.Y -= (position.Y + hitbox.Height - Physics.StageHitbox.Y - 1);

					if (state == FighterStates.Helpless)
					{
						state = FighterStates.Normal;
					}
				}

				Movement(gt);

				Attack(gt);

				vel.X = Physics.CalcVel(vel.X, accel.X, maxVel.X, airResist, gt);

				position.X += (int)vel.X;
				position.Y += (int)vel.Y;
			}

			if (state == FighterStates.Normal)
			{
				currAnimation = idle;
			}
			
			hitbox.X = (int)position.X;
			hitbox.Y = (int)position.Y;

			if (cdTimer > 0)
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
