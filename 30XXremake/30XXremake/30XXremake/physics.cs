using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXremake
{
    class physics
    {
        //so unlike Flixel, XNA doesn't have all the fancy physics stuff alreadys set up, so I'll have to make my own (aka copy Flixel's)
        /* Todo:
         * - Gravity
         * - Calculate velocity
         * - Hitboxes
         */
        
        //For now I'll also put the initialization stuff here for organization. Might be a good idea to move this stuff to the one that XNA comes with.
        static public void initialize()
        {
            //stage.img
        }

        /* calcVel: Exactly what it says, it calculates velocity
         * 
         */
        static public float calcVel(float vel, float accel, float maxVel, GameTime gt)
        {
            if (accel != 0)
            {
                vel += accel * (float)gt.ElapsedGameTime.TotalMilliseconds;
            }

            if (vel > maxVel)
            {
                vel = maxVel;
            }

            return vel;
        }

        static public void gravity(Vector2 pos)
        {
            pos.Y += 5;
        }

        /* hitboxOwners refers to the names of the object that a hitbox belongs to. It accepts any object because stages and fighters are different types, and I'm not sure if I can make the same list have two types.
         * 
         * addToCollisions() and collisions() work together.
         * addToCollisions() is called by fighters and the stage to add their hitboxes to allHitboxes, and their "names" are added to hitboxOwners
         * collisions() checks for collisions between everything (usually the stage and a fighter).
         */

        static private Rectangle stageHitbox;
        static private List<Tuple<fighter, Rectangle>> hitboxesAndOwners = new List<Tuple<fighter, Rectangle>>();

        static public void addToCollisions(fighter source, Rectangle hitbox)
        {
            hitboxesAndOwners.Add(Tuple.Create(source, hitbox));
        }

        static public void collisions()
        {
            //collision between stage and fighters. Note: The stage will ALWAYS be added first, so the loop that checks from 2nd element on works.
            for (int i = 0; i < hitboxesAndOwners.Count; i++)
            {
                if (hitboxesAndOwners[i].Item2.Intersects(stageHitbox))
                {
                    hitboxesAndOwners[i].Item1.Vel = new Vector2(hitboxesAndOwners[i].Item1.Vel.X, 0);
                }
            }
        }

        static public Rectangle StageHitbox
        {
            get { return stageHitbox; }
            set { stageHitbox = value; }
        }
    }
}
