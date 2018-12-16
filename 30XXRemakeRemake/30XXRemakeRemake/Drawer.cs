using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _30XXRemakeRemake
{
    static class Drawer
    {
		public static List<Tuple<Move, SpriteEffects>> drawListMoves = new List<Tuple<Move, SpriteEffects>>(); //A list of moves to draw
        public static double m = 0;

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
		public static void AddToDrawList(Move move, bool flip)
		{
			SpriteEffects f = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			drawListMoves.Add(new Tuple<Move, SpriteEffects>(move, f));
		}

        public static void Draw(SpriteBatch spriteBatch, GameTime gt)
        {
	        int i = drawListMoves.Count - 1;
			//Loop through all the items in drawListMoves and draw them all, but only if they're not flagged for remove.
			while (i >= 0)
            {
	            if (!drawListMoves[i].Item1.SpriteTexture.Finished)
	            {
		            spriteBatch.Draw(drawListMoves[i].Item1.SpriteTexture.SpriteTexture,
			            drawListMoves[i].Item1.Position, drawListMoves[i].Item1.SpriteTexture.SourceRect, Color.White,
			            0, Vector2.Zero, drawListMoves[i].Item2, 0);
		            drawListMoves[i].Item1.SpriteTexture.Animate(gt);
	            }
	            else
	            {
		            Physics.RemoveFromUpdateList(drawListMoves[i].Item1);
					drawListMoves.RemoveAt(i);
				}

	            i--;
            }
        }
    }
}
