using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData {
    public int jumps_left;
    public int levels_cleared;
    

    public GameData(int jumpsLeftInt, int levelsInt) {
        jumps_left = jumpsLeftInt;
        levels_cleared = levelsInt;
    }
}

namespace SaveGame {
    
    public class Save_game {
        //default values
        int jumps_left = 2;
        int levels_cleared = 0;
        string file_path = Application.persistentDataPath + "/save_game.dat";

        void Start() {
            // SaveFile(jumps_left);
            LoadFile();
        }

        public void SaveFile(int jumps, int levels) {
            FileStream file;

            if(File.Exists(file_path)) file = File.OpenWrite(file_path);
            else file = File.Create(file_path);

            GameData data = new GameData(jumps, levels);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        public void LoadFile() {
            FileStream file;

            if(File.Exists(file_path)) file = File.OpenRead(file_path);
            else {
                // Debug.LogError("File not found");
                // return;
                SaveFile(jumps_left, levels_cleared);
                file = File.OpenRead(file_path);
            }

            BinaryFormatter bf = new BinaryFormatter();
            GameData data = (GameData) bf.Deserialize(file);
            file.Close();

            jumps_left = data.jumps_left;
            levels_cleared = data.levels_cleared;
            // Debug.Log(data.jumps_left);
            // Debug.Log(data.levels_cleared);

            // return jumps_left;
        }

        public int get_jumps() {
            // return 6;   //  cheat code
            return jumps_left;
        }
        public int get_levels_cleared() {
            // return 20;  //  cheat code
            return levels_cleared;
        }


    }
}
