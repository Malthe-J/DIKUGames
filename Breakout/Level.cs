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
        private string name;
        private int time;
        private char powerUp;
        private char hardened;
        private char unbreakable;
        private Dictionary<char, IBaseImage> textures;
        private EntityContainer<Block> blocks;

        private LegendData legend;


        public string Name{
            get{return name;}
        }

        public int Time{
            get{return time;}
        }

        public char PowerUp{
            get{return powerUp;}
        }

        public char Hardened{
            get{return hardened;}
        }

        public char Unbreakable{
            get{return unbreakable;}
        }



        public Level(string FilePath){
            textures = new Dictionary<char, IBaseImage>();
            blocks = new EntityContainer<Block>();
            ReadFile(FilePath);
        }
        private void ReadFile(string FilePath){
            try
            {
                string levelStorage = File.ReadAllText(FilePath);
                string[] levelStorageSplit = levelStorage.Split('/');
                string[] levelStorageSplitMeta = levelStorageSplit[1].Split('\n');
                string[] levelStorageSplitLegend = levelStorageSplit[2].Split('\n');
                string[] levelStorageSplitMap = levelStorageSplit[0].Split('\n');

                legend = new LegendData(levelStorageSplitLegend);
                textures = legend.GetDic();
                
                int nameIndex = levelStorageSplitMeta[3].IndexOf("Name: ")+"Name: ".Length;
                int lastIndex = levelStorageSplitMeta[3].LastIndexOf("\r");
                name = levelStorageSplitMeta[3].Substring(nameIndex, lastIndex - nameIndex);


                //Meta data read from file
                for (int i = 4; i < levelStorageSplitMeta.Length -1; i++){
                    if (levelStorageSplitMeta[i].Contains("Time")){
                        int timeIndex = levelStorageSplitMeta[i].IndexOf("Time: ")+"Time: ".Length;
                        time = Int32.Parse(levelStorageSplitMeta[i].Substring(timeIndex));
                    }
                    if (levelStorageSplitMeta[i].Contains("Hardened")){
                        int hardenedIndex = levelStorageSplitMeta[i].IndexOf("Hardened: ")+"Hardened: ".Length;
                        char[] characters = levelStorageSplitMeta[i].Substring(hardenedIndex).ToCharArray();
                        hardened = characters[0];
                    }
                    if (levelStorageSplitMeta[i].Contains("PowerUp")){
                        int powerUpIndex = levelStorageSplitMeta[i].IndexOf("PowerUp: ")+"PowerUp: ".Length;
                        char[] characters = levelStorageSplitMeta[i].Substring(powerUpIndex).ToCharArray();
                        powerUp = characters[0];
                    }
                    if (levelStorageSplitMeta[i].Contains("Unbreakable")){
                        int unbreakableIndex = levelStorageSplitMeta[i].IndexOf("Unbreakable: ")+"Unbreakable: ".Length;
                        char[] characters = levelStorageSplitMeta[i].Substring(unbreakableIndex).ToCharArray();
                        unbreakable = characters[0];
                    }                    
                }

                float x = 0.0f;
                float yCoord = 1.0f / (levelStorageSplitMap.Length-2);
                Console.WriteLine(levelStorageSplitMap.Length-2);

                for (int i = 1; i < levelStorageSplitMap.Length - 1; i++){
                    float y = 1.0f - yCoord * i;
                    for (int j = 0; j < levelStorageSplitMap[j].Length; j++){
                        x = 1.0f / (levelStorageSplitMap[j].Length - 1);
                        if (textures.ContainsKey(levelStorageSplitMap[i][j])){
                            IBaseImage image;
                            textures.TryGetValue(levelStorageSplitMap[i][j], out image);
                            blocks.AddEntity(new Block(new StationaryShape(new Vec2F(x*j,y), new Vec2F(x, 0.03f)),image, 2));
                        }
                    }
                    
                    
                }
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