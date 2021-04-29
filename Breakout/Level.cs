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
            ReadFile(FilePath);
        }
        private void ReadFile(string FilePath){
            try
            {
                string levelStorage = File.ReadAllText(FilePath);
                string[] levelStorageSplit = levelStorage.Split('/');
                string[] levelStorageSplitMeta = levelStorageSplit[1].Split('\n');
                string[] levelStorageSplitLegend = levelStorageSplit[2].Split('\n');
                
                int nameIndex = levelStorageSplitMeta[3].IndexOf("Name: ")+"Name: ".Length;
                int lastIndex = levelStorageSplitMeta[3].LastIndexOf("\r");
                name = levelStorageSplitMeta[3].Substring(nameIndex, lastIndex - nameIndex);

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

                // Legend Data read from file
                for(int i = 3; i<levelStorageSplitLegend.Length; i++) {
                   if (levelStorageSplitLegend[i].Contains("#) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("#) ")+"#) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('#', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +"# lvl1");
                   } 
                   if (levelStorageSplitLegend[i].Contains("1) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("1) ")+"1) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('1', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +"1 lvl1");
                   } 
                   if (levelStorageSplitLegend[i].Contains("2) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("2) ")+"2) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('2', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +"2 lvl1");
                   }
                   if (levelStorageSplitLegend[i].Contains("q) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("q) ")+"q) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('q', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +"q lvl1");
                   }  
                   if (levelStorageSplitLegend[i].Contains("a) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("a) ")+"a) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('a', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" a lvl2");
                   }
                    if (levelStorageSplitLegend[i].Contains("b) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("b) ")+"b) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('b', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" b lvl2");
                   }
                    if (levelStorageSplitLegend[i].Contains("c) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("c) ")+"c) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('c', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" c lvl2");
                   } 
                    if (levelStorageSplitLegend[i].Contains("d) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("d) ")+"d) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('d', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" d lvl2");
                   }
                   if (levelStorageSplitLegend[i].Contains("v) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("v) ")+"v) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('v', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" v lvl3");
                   }
                   if (levelStorageSplitLegend[i].Contains("%) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("%) ")+"%) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('%', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" % lvl3");
                   }
                   if (levelStorageSplitLegend[i].Contains("X) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("X) ")+"X) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('X', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" X lvl3");
                   }
                   if (levelStorageSplitLegend[i].Contains("p) ")) {
                        int Index = levelStorageSplitLegend[i].IndexOf("p) ")+"p) ".Length;
                        int LastIndex = levelStorageSplitLegend[i].LastIndexOf("\r");
                        string temp = levelStorageSplitLegend[i].Substring(Index, LastIndex - Index);
                        textures.Add('p', new Image(Path.Combine("Assets", "Images", temp)));
                        Console.WriteLine(@temp +" p lvl3");
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