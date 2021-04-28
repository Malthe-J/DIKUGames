using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using System.IO;

namespace Breakout {
    public class Level{
        public Level(string FilePath){
            ReadFile(FilePath);

        }
        private void ReadFile(string FilePath){
            try
            {
                string levelStorage = File.ReadAllText(FilePath);
                string[] levelStorageSplit = levelStorage.Split('/');


            
            }
            catch (System.Exception)
            {
                Console.WriteLine("Jeg er fra Ballerup din lorteluder");
            }

        }
    





    }
}