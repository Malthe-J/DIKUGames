using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;
using System.Collections.Generic;

namespace Breakout {
    public class Level{
        private Dictionary<char, IBaseImage> textures;
        private MetaData metaData;

        private LegendData legend;


        public MetaData MetaDat{
            get{return metaData;}
        }
    


        public Level(string FilePath){
            textures = new Dictionary<char, IBaseImage>();
            blocks = new EntityContainer<Block>();
            ReadFile(FilePath);
        }
        private void ReadFile(string FilePath){
            try
            {
                //SKal ind i loading filen
                string levelStorage = File.ReadAllText(FilePath);
                string[] levelStorageSplit = levelStorage.Split('/');
                string[] levelStorageSplitMeta = levelStorageSplit[1].Split('\n');
                string[] levelStorageSplitLegend = levelStorageSplit[2].Split('\n');
                string[] levelStorageSplitMap = levelStorageSplit[0].Split('\n');

                legend = new LegendData(levelStorageSplitLegend);
                textures = legend.GetDic();
                
            }
            catch (FileNotFoundException)
            {
                ReadFile(Path.Combine("Assets", "Levels", "wall.txt"));
            }
        

        }
    
        public void render(){
            blocks.RenderEntities();
        }





    }
}