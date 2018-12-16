﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    static class Physics
    {
        //so unlike Flixel, XNA doesn't have all the fancy physics stuff alreadys set up, so I'll have to make my own (aka copy Flixel's)
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

        static private Rectangle stageHitbox;
        static private Dictionary<Fighter, Rectangle> hitboxesAndOwners = new Dictionary<Fighter, Rectangle>();

        static private List<IUpdatable> UpdateList = new List<IUpdatable>();

        /// <summary>
        /// Calculates the object's velocity.
        /// </summary>
        /// <param name="vel"> The object's current velocity. </param>
        /// <param name="accel"> The object's current acceleration. </param>
        /// <param name="maxVel"> The quickest the object can go. </param>
        /// <param name="gt"> The games' GameTime. It's probably called "gameTime" or "gt". </param>
        /// <returns></returns>
        static public float CalcVel(float vel, float accel, float maxVel, GameTime gt)
        {
			//this is kind of shit lol
			//Try googling some physics formulas
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

        static public void Gravity(Vector2 pos)
        {
            pos.Y += 5;
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
        static public void AddToCollisions(Fighter source, Rectangle hitbox)
        {
            hitboxesAndOwners.Add(source, hitbox);
        }

        /// <summary>
        /// A method that checks whether each Fighter is colliding with the stage. If yes, set their vertical velocity to 0.
        /// </summary>
        static public void CollisionsWithStage()
        {
            //Possibly find a method that isn't highly inefficient?
            foreach (KeyValuePair<Fighter, Rectangle> hbObject in hitboxesAndOwners)
            {
                if (hbObject.Value.Intersects(stageHitbox))
                {
                    hbObject.Key.Vel = new Vector2(hbObject.Key.Vel.X, 0);
                }
            }
        }

        static public void AddToUpdateList(IUpdatable item)
        {
            UpdateList.Add(item);
        }

        //A function to update everything
        static public void Update(GameTime gt)
        {
            foreach (IUpdatable item in UpdateList)
            {
                item.Update(gt);
            }
        }

        static public Rectangle StageHitbox
        {
            get { return stageHitbox; }
            set { stageHitbox = value; }
        }

    }
}
