using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
using System.Collections.Generic;
using Breakout.PowerUp;

namespace Breakout{
    public class Map{
        private EntityContainer<Block> Undestroyableblocks;
        private EntityContainer<Block> Destroyableblocks;
        private EntityContainer<PowerUp.PowerUp> PowerUpContainer;
        /// <summary>
        /// Creates the map with the block defined in the given
        /// ASCII and sets them in the correct place also defined
        /// by the ASCII
        /// </summary>
        /// <param name="levelStorageSplitMap"></param>
        /// <param name="textures"></param>
        /// <param name="data"></param>
        public Map(String[] levelStorageSplitMap, Dictionary<char, string> textures, MetaData data){
            Undestroyableblocks = new EntityContainer<Block>();
            Destroyableblocks = new EntityContainer<Block>();
            PowerUpContainer = new EntityContainer<PowerUp.PowerUp>();
            float x = 0.0f;
            float yCoord = 1.0f / (levelStorageSplitMap.Length-2);


            for (int i = 1; i < levelStorageSplitMap.Length - 1; i++){
                float y = 1.0f - yCoord * i;
                for (int j = 0; j < levelStorageSplitMap[j].Length; j++){
                    x = 1.0f / (levelStorageSplitMap[j].Length - 1);
                    if (textures.ContainsKey(levelStorageSplitMap[i][j])){
                        if (data.PowerUp == levelStorageSplitMap[i][j]){
                            PowerUpBlock block = new PowerUpBlock(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),textures[levelStorageSplitMap[i][j]]);
                            Destroyableblocks.AddEntity(block);
                            PowerUpContainer.AddEntity(block.GetPowerUp());
                        }
                        else if (data.Hardened == levelStorageSplitMap[i][j]){
                            Destroyableblocks.AddEntity(new HardenedBlock(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),textures[levelStorageSplitMap[i][j]]));
                        }
                        else if (data.Unbreakable == levelStorageSplitMap[i][j]){
                            Undestroyableblocks.AddEntity(new UnbreakableBlock(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),textures[levelStorageSplitMap[i][j]]));
                        }
                        else{
                            Destroyableblocks.AddEntity(new Block(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),textures[levelStorageSplitMap[i][j]]));
                        }
                    }
                }
                
                
            }
        }

        public void Render()
        {
            Undestroyableblocks.RenderEntities();
            Destroyableblocks.RenderEntities();
        }

        public EntityContainer<Block> GetUndestroyableBlocks() {
            return Undestroyableblocks;
        }

        public EntityContainer<Block> GetDestroyableBlocks() {
            return Destroyableblocks;
        }

        public EntityContainer<PowerUp.PowerUp> GetPowerUp() {
            return PowerUpContainer;
        }
    }
}