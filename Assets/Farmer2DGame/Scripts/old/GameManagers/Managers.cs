using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]
public class Managers : MonoBehaviour {


	public static PlayerManager Player {get; private set;}
	public static InventoryManager Inventory {get; private set;}
	public static MissionManager Mission {get; private set;}
	public static DataManager Data {get; private set;}
	 
	private List<IGameManager> _startSequence;
	
	 public void Awake() 
	{
        DontDestroyOnLoad(gameObject);

		Data = GetComponent<DataManager>();
        Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Mission = GetComponent<MissionManager>();


		_startSequence = new List<IGameManager>();
		_startSequence.Add(Mission);
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
        _startSequence.Add(Data);

	}

	public void InitManagers()
	{
        StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		foreach (IGameManager manager in _startSequence) {
			manager.Startup();
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;
		
		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			
			foreach (IGameManager manager in _startSequence) {
				if (manager.status == ConstCollection.ManagerStatus.Started) {
					numReady++;
				}
			}

			if (numReady > lastReady)
			{
				Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
			yield return null;
		}
		
		Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}
