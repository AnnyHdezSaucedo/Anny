using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseTutorial
{
    public class Level
    {
        private List<Texture2D> tileTextures = new List<Texture2D>();
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        int[,] map = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,},
            {1,1,1,1,1,1,1,0,0,0,},
            {0,0,0,0,0,0,1,0,0,0,},
            {0,1,1,1,1,0,1,0,0,0,},
            {0,1,0,0,1,0,1,0,0,0,},
            {0,1,0,1,1,0,1,0,0,0,},
            {0,1,0,0,0,0,1,0,0,0,},
            {0,1,1,1,1,1,1,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,},
        };

        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }

        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }

        public Level()
        {
            waypoints.Enqueue(new Vector2(0, 1) * 64);
            waypoints.Enqueue(new Vector2(5, 1) * 64);

            waypoints.Enqueue(new Vector2(6, 1) * 64);
            waypoints.Enqueue(new Vector2(6, 6) * 64);

            waypoints.Enqueue(new Vector2(6, 7) * 64);
            waypoints.Enqueue(new Vector2(1, 7) * 64);

            waypoints.Enqueue(new Vector2(1, 6) * 64);
            waypoints.Enqueue(new Vector2(1, 3) * 64);

            waypoints.Enqueue(new Vector2(2, 3) * 64);
            waypoints.Enqueue(new Vector2(4, 3) * 64);
            waypoints.Enqueue(new Vector2(4, 5) * 64);
            waypoints.Enqueue(new Vector2(3, 5) * 64);
        }

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];

                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }
            }
        }

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;
            return map[cellY, cellX];
        }
    }
}
