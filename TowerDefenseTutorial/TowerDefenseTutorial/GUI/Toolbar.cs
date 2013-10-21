using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseTutorial
{
    public class Toolbar
    {
        private Texture2D texture;
        // A class to access the font we created
        private SpriteFont font;

        // The position of the toolbar
        private Vector2 position;
        // The position of the text
        private Vector2 textPosition;

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
        {
            this.texture = texture;
            this.font = font;

            this.position = position;
            // Offset the text to the bottom right corner
            textPosition = new Vector2(200, position.Y + 20);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(texture, position, Color.White);

            string text = string.Format("Level 1       Olas : {0}         Gold : {1}        Lives : {2}",player.Olas, player.Money, player.Lives);
            spriteBatch.DrawString(font, text, textPosition, Color.White);
        }
    }
}
