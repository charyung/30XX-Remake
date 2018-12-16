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
		static public List<Tuple<Move, SpriteEffects>> drawListMoves = new List<Tuple<Move, SpriteEffects>>(); //A list of moves to draw
        static List<int> removeList = new List<int>(); //A list of the indices of the animations to remove from the list
        static public double m = 0;

        //Like its name suggests, this class draws things. Its purpose is mainly to keep clutter away from the main code in Game1.cs.
        static Drawer()
        {
            //nothing here so far
        }

        /// <summary>
        /// Adds an animation to the list of animations to draw.
        /// </summary>
		/// <param name="move"> The move to add to the list. </param>
        /// <param name="flip"> Whether the sprite is flipped. If not, then the sprite faces right. </param>
		static public void AddToDrawList(Move move, bool flip)
		{
			SpriteEffects f;

			if (flip)
			{
				f = SpriteEffects.FlipHorizontally;
			}
			else
			{
				f = SpriteEffects.None;
			}

			drawListMoves.Add(new Tuple<Move, SpriteEffects>(move, f));
		}

        static public void Draw(SpriteBatch spriteBatch, GameTime gt)
        {
            //Loop through all the items in drawListMoves and draw them all, but only if they're not flagged for remove.
            for (int i = 0; i < drawListMoves.Count; i++)
            {
				spriteBatch.Draw(drawListMoves[i].Item1.SpriteTexture.SpriteTexture, drawListMoves[i].Item1.Position, drawListMoves[i].Item1.SpriteTexture.SourceRect, Color.White, 0, Vector2.Zero, drawListMoves[i].Item2, 0);
				drawListMoves[i].Item1.SpriteTexture.Animate(gt);
                /*}
                else
                {
                    //removeList.Add(i);
                }*/
            }

            //Loop through the removeList and remove all the items at those indices from drawListMoves. This is because it's kinda weird to remove something from a list while it's being enumerated through.
            foreach (int index in removeList)
            {
                drawListMoves.RemoveAt(index);
            }

            //Then clear the removeList so we don't remove something we shouldn't in the next iteration.
            removeList.Clear();
        }
    }
}
