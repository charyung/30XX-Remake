using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _30XXRemakeRemake
{
    static class Physics
    {
	    private static readonly Dictionary<Fighter, Rectangle> HitboxesAndOwners = new Dictionary<Fighter, Rectangle>();

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
        /// <param name="gt"> The games' GameTime. It's probably called "gameTime" or "gt". </param>
        /// <returns></returns>
        public static float CalcVel(float vel, float accel, float maxVel, GameTime gt)
        {
			//this is kind of shit lol
			//Try googling some physics formulas
            if (Math.Abs(accel) > 0.0001)
            {
                vel += accel * (float)gt.ElapsedGameTime.TotalSeconds;
            }

            if (vel > maxVel)
            {
                vel = maxVel;
            }

            return vel;
        }

	    public static float Gravity(Vector2 pos, Vector2 vel, Vector2 accel, float maxVel, GameTime gt)
        {
            accel.Y += 9.8f;
	        vel.Y = CalcVel(vel.Y, accel.Y, maxVel, gt);
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
            HitboxesAndOwners.Add(source, hitbox);
        }

	    /// <summary>
        /// A method that checks whether each Fighter is colliding with the stage. If yes, set their vertical velocity to 0.
        /// </summary>
        public static void CollisionsWithStage()
        {
            //Possibly find a method that isn't highly inefficient?
            foreach (KeyValuePair<Fighter, Rectangle> hbObject in HitboxesAndOwners)
            {
                if (hbObject.Value.Intersects(StageHitbox))
                {
                    hbObject.Key.Vel = new Vector2(hbObject.Key.Vel.X, 0);
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
