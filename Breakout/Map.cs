using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
using System.Collections.Generic;


namespace Breakout{
    public class Map{
        private EntityContainer<Block> blocks;
        public Map(String[] levelStorageSplitMap, Dictionary<char, IBaseImage> textures){
            blocks = new EntityContainer<Block>();
            float x = 0.0f;
            float yCoord = 1.0f / (levelStorageSplitMap.Length-2);


            for (int i = 1; i < levelStorageSplitMap.Length - 1; i++){
                float y = 1.0f - yCoord * i;
                for (int j = 0; j < levelStorageSplitMap[j].Length; j++){
                    x = 1.0f / (levelStorageSplitMap[j].Length - 1);
                    if (textures.ContainsKey(levelStorageSplitMap[i][j])){
                        blocks.AddEntity(new Block(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),textures[levelStorageSplitMap[i][j]], 1));
                    }
                }
                
                
            }
        }

        public void Render()
        {
            blocks.RenderEntities();
        }

        public EntityContainer<Block> GetBlocks() {
            return blocks;
        }
    }
}