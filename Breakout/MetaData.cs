using System;
using System.IO;

namespace Breakout {
    public class MetaData{
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

        public MetaData(string[] levelStorageSplitMeta){
            Meta(levelStorageSplitMeta);
        }

        private void Meta(String[] levelStorageSplitMeta){
             
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
        }

    }

}