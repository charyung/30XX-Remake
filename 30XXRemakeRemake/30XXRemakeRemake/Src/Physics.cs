using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _30XXRemakeRemake
{
    internal static class Physics
    {
	    internal static readonly HashSet<Fighter> Fighters = new HashSet<Fighter>();

	    private static readonly List<IUpdatable> UpdateList = new List<IUpdatable>();
	    //so unlike Flixel, XNA doesn't have all the fancy physics stuff already set up, so I'll have to make my own (aka copy Flixel's)
	    /* Todo:
         * - Gravity
         * - Calculate velocity
         * - Hitboxes (especially between fighters, too many edge and corner cases)
         */

	    //For now I'll also put the initialization stuff here for organization. Might be a good idea to move this stuff to the one that XNA comes with.
	    static Physics()
        {
            //stage.img
        }

	    public static Rectangle StageHitbox { get; set; }

	    /// <summary>
        /// Calculates the object's velocity.
        /// </summary>
        /// <param name="vel"> The object's current velocity. </param>
        /// <param name="accel"> The object's current acceleration. </param>
        /// <param name="maxVel"> The quickest the object can go. </param>
        /// <param name="airResist">The object's air resistance.</param>
        /// <param name="gt"> The games' GameTime. It's probably called "gameTime" or "gt". </param>
        /// <returns></returns>
        public static float CalcVel(float vel, float accel, float maxVel, float airResist, GameTime gt)
	    {
		    airResist *= -(vel / 2); // Make air resistance always opposite of velocity, and scale it to velocity
			//this is kind of shit lol
			//Try googling some physics formulas
            vel += (accel + airResist) * (float)gt.ElapsedGameTime.TotalSeconds;

            vel = (float) (Math.Round(vel * 100) / 100);

            /*if (vel > maxVel)
            {
                vel = maxVel;
            }*/

            return vel;
        }

	    public static float Gravity(Vector2 pos, Vector2 vel, Vector2 accel, float maxVel, GameTime gt)
        {
            accel.Y += 9.8f;
	        vel.Y = CalcVel(vel.Y, accel.Y, maxVel, 0, gt);
	        return vel.Y;
        }

	    /* addToCollisions() and collisions() work together.
         * addToCollisions() is called by fighters to add their hitboxes to allHitboxes, and their "names" are added to hitboxOwners
         * The stage hitbox is referred to separately. 
         * collisions() checks for collisions between everything (usually the stage and a fighter).
         */

	    /// <summary>
        /// Adds an entry of the Fighter and its hitbox to the dict of hitboxes, to be used to check for collision.
        /// </summary>
        /// <param name="source"> The variable that refers to the Fighter to be added. </param>
        /// <param name="hitbox"> The Fighter's hitbox. </param>
        public static void AddToCollisions(Fighter source, Rectangle hitbox)
        {
            Fighters.Add(source);
        }

	    /// <summary>
        /// A method that checks whether each Fighter is colliding with the stage. If yes, set their vertical velocity to 0.
        /// </summary>
        public static void CollisionsWithStage()
        {
            //Possibly find a method that isn't highly inefficient?
            foreach (Fighter fighter in Fighters)
            {
                if (fighter.hitbox.Intersects(StageHitbox))
                {
                    fighter.Vel = new Vector2(fighter.Vel.X, 0);
                }
            }
        }

	    public static void AddToUpdateList(IUpdatable item)
        {
            UpdateList.Add(item);
        }

	    public static void RemoveFromUpdateList(IUpdatable item)
	    {
		    UpdateList.Remove(item);
	    }

	    //A function to update everything
	    public static void Update(GameTime gt)
        {
            foreach (IUpdatable item in UpdateList)
            {
                item.Update(gt);
            }
        }
    }
}
