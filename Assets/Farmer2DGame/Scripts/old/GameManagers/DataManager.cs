using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager
{
    public ConstCollection.ManagerStatus status {  get; private set; }

    private string _fileName;


    public void Startup()
    {
        Debug.Log("Data manger starting...");


        _fileName = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ConstCollection.ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("_inventory", Managers.Inventory.GetData());
        gamestate.Add("health", Managers.Player.health);
        gamestate.Add("maxHealth", Managers.Player.maxHealth);
        gamestate.Add("curLevel", Managers.Mission.ÑurLevel);
        

        FileStream stream = File.Create( _fileName );
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize( stream, gamestate );
        stream.Close();
    }

    public void LoadGameState()
    {
        if (!File.Exists(_fileName))
        {
            Debug.Log("No saved game");
            return;
        }
        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_fileName, FileMode.Open);
        gamestate = formatter.Deserialize( stream ) as Dictionary<string,object>;
        stream.Close();

        Managers.Inventory.UpdateData((Dictionary<string,int>)gamestate["_inventory"]);
        Managers.Player.UpdateData((int)gamestate["health"],(int)gamestate["maxHealth"]);
        Managers.Mission.UpdateData(((string)gamestate["curLevel"]));
        Managers.Mission.RestartCurrent();
    }
}
