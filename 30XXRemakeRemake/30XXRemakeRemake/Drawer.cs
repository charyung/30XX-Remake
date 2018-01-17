using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    static class Drawer
    {
        //
        static List<Tuple<Animation, Rectangle>> drawList = new List<Tuple<Animation, Rectangle>>(); //A list of animations to draw, along with their locations
        static List<int> removeList = new List<int>(); //A list of the indices of the animations to remove from the list

        //Like its name suggests, this class draws things. Its purpose is mainly to keep clutter away from the main code in Game1.cs.
        static Drawer()
        {
            //nothing here so far
        }

        /// <summary>
        /// Adds an animation to the list of animations to draw.
        /// </summary>
        /// <param name="animation"> The animation to draw. </param>
        /// <param name="position"> The destinationRectangle of the animation. </param>
        static public void AddToDrawList(Animation animation, Rectangle position)
        {
            drawList.Add(new Tuple<Animation, Rectangle>(animation, position));
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            //Loop through all the items in drawList and draw them all, but only if they're not flagged for remove.
            for (int i = 0; i < drawList.Count; i++)
            {
                if (!drawList[i].Item1.Finished)
                {
                    spriteBatch.Draw(drawList[i].Item1.SpriteTexture, drawList[i].Item2, Color.White);
                }
                else
                {
                    removeList.Add(i);
                }
            }

            //Loop through the removeList and remove all the items at those indices from drawList. This is because it's kinda weird to remove something from a list while it's being enumerated through.
            foreach (int index in removeList)
            {
                drawList.RemoveAt(index);
            }

            //Then clear the removeList so we don't remove something we shouldn't in the next iteration.
            removeList.Clear();
        }
    }
}
