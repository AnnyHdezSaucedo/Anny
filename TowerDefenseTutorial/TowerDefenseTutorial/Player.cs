using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseTutorial
{
    public class Player
    {
        // Player state.
        private int money = 50;
        private int lives = 30;
        private int olas = 3;

        // The textures used to draw our tower.
        private Texture2D[] towerTextures;
        // The texture used to draw bullets.
        private Texture2D[] bulletTexture;

        // A list of the players towers
        private List<Tower> towers = new List<Tower>();

        // Mouse state for the current frame.
        private MouseState mouseState;
        // Mouse state for the previous frame.
        private MouseState oldState;

        // Tower placement.
        private int cellX;
        private int cellY;

        private int tileX;
        private int tileY;

        // The type of tower to add.
        private string newTowerType;
        // The index of the new towers texture.
        private int newTowerIndex;

        // A reference to the level.
        private Level level;

        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Olas {
            get { return olas; }
            set { olas = value; }
        }

        public string NewTowerType
        {
            set { newTowerType = value; }
        }
        public int NewTowerIndex
        {
            set { newTowerIndex = value; }
        }

        /// <summary>
        /// Construct a new player.
        /// </summary>
        public Player(Level level, Texture2D[] towerTextures, Texture2D[] bulletTexture)
        {
            this.level = level;

            this.towerTextures = towerTextures;
            this.bulletTexture = bulletTexture;
        }

        /// <summary>
        /// Returns wether the current cell is clear
        /// </summary>
        private bool IsCellClear()
        {
            // Make sure tower is within limits
            bool inBounds = cellX >= 0 && cellY >= 0 && 
                cellX < level.Width && cellY < level.Height; 

            bool spaceClear = true;

            // Check that there is no tower in this spot
            foreach (Tower tower in towers) 
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                {
                    break;
                }
            }

            bool onPath = (level.GetIndex(cellX, cellY) != 1);

            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        /// <summary>
        /// Adds a tower to the player's collection.
        /// </summary>
        public void AddTower()
        {
            Tower towerToAdd = null;

            switch (newTowerType)
            {
                case "Arrow Tower":
                {
                    towerToAdd = new ArrowTower(towerTextures[0],
                        bulletTexture[0], new Vector2(tileX, tileY));
                    break;
                }
                case "Spike Tower":
                {
                    
                    //towerToAdd = new SpikeTower(towerTextures[1], bulletTexture, new Vector2(tileX, tileY));
                    towerToAdd = new SpikeTower(towerTextures[1], bulletTexture[1], new Vector2(tileX, tileY));
                    break;
                }
                case "Slow Tower":
                {
                    towerToAdd = new SlowTower(towerTextures[2],
                        bulletTexture[2], new Vector2(tileX, tileY));
                    break;
                }
            }

            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= money)
            {
                towers.Add(towerToAdd);
                money -= towerToAdd.Cost;

                // Reset the newTowerType field.
                newTowerType = string.Empty;
            }

            else
            {
                newTowerType = string.Empty;
            }
        }

        /// <summary>
        /// Updates the player.
        /// </summary>
        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            cellX = (int)(mouseState.X / 64); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 64); // from array space to level space

            tileX = cellX * 64; // Convert from array space to level space
            tileY = cellY * 64; // Convert from array space to level space

            if (mouseState.LeftButton == ButtonState.Released
                && oldState.LeftButton == ButtonState.Pressed)
            {
                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }

            foreach (Tower tower in towers)
            {
                // Make sure the tower has no targets.
                if (tower.HasTarget == false)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }

        public void DrawPreview(SpriteBatch spriteBatch)
        {
            // Draw the tower preview.
            if (string.IsNullOrEmpty(newTowerType) == false)
            {
                int cellX = (int)(mouseState.X / 64); // Convert the position of the mouse
                int cellY = (int)(mouseState.Y / 64); // from array space to level space

                int tileX = cellX * 64; // Convert from array space to level space
                int tileY = cellY * 64; // Convert from array space to level space

                Texture2D previewTexture = towerTextures[newTowerIndex];

                spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY,
                    previewTexture.Width, previewTexture.Height), Color.White);
            }
        }
    }
}
