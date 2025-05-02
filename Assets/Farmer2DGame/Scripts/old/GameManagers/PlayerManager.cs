using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ConstCollection.ManagerStatus status {get; private set;}

	public int health {get; private set;}
	public int maxHealth {get; private set;}

	public bool AllowAttack = true;
	public bool AllowMove = true;

    public void Startup() {
		Debug.Log("Player manager starting...");


        // these values could be initialized with saved data
        UpdateData(50, 100);

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        status = ConstCollection.ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth)
	{
		this.health = health;
		this.maxHealth = maxHealth;
	}

    public void ChangeHealth(int value) {
	health += value;
	if (health > maxHealth) {
		health = maxHealth;
	} else if (health < 0) {
		health = 0;
	}
	if(health <= 0)
	{
		Messenger.Broadcast(GameEvent.LEVEL_FAILED);
	}
		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
		Debug.Log("Health: " + health + "/" + maxHealth);
	}
    public void Respawn()
    { 
		 UpdateData(50, 100);
    }
}
