using UnityEngine;
using System.IO;

namespace OABaseGameSystem
{
    public class SaveManager
    {


        public SaveManager()
        {
            CurrentSave = Load();
            if (CurrentSave == null) CurrentSave = new();
        }

        private string _savePath => Application.persistentDataPath + "/save.json";
        public SaveData CurrentSave;

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_savePath, json);
            Debug.Log("Game Saved!");
        }

        public SaveData Load()
        {
            if (!File.Exists(_savePath))
            {
                Debug.LogWarning("No save file found");
                return null;
            }

            string json = File.ReadAllText(_savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        public bool HasSave()
        {
            return File.Exists(_savePath);
        }

        public void DeleteSave()
        {
            if (File.Exists(_savePath))
                File.Delete(_savePath);
        }
    }

    public class SaveData
    {
        public int[] characterStats;
        /*
         * прокачка 
         * параметры персонажа (хп, оличество здоровья, предметов, монеток как секретных и обычных)
         * 
        */

    }
}