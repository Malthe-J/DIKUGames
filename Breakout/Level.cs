using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;

namespace Breakout {
    public class Level{
        private string name;
        private int time;
        private char powerUp;
        private char hardened;
        private char unbreakable;


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
            ReadFile(FilePath);

        }
        private void ReadFile(string FilePath){
            try
            {
                string levelStorage = File.ReadAllText(FilePath);
                string[] levelStorageSplit = levelStorage.Split('/');
                string[] levelStorageSplitMeta = levelStorageSplit[1].Split('\n');

                
                int nameIndex = levelStorageSplitMeta[3].IndexOf("Name: ")+"Name: ".Length;
                name = levelStorageSplitMeta[3].Substring(nameIndex);

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
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    
    





    }
}