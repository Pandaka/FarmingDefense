using UnityEngine;
using System.Collections;

public class SpawnButton : MonoBehaviour {

	[SerializeField]
	SpawnScript[] spawns;
	[SerializeField]
	int type;

	// Quand on clic on fait un superspawn
	//rapel des types : 1 = SpeedMob, 2 = TankMob, 3 = AttackMob
	void OnClick () {
		foreach(SpawnScript spawn in spawns)
			spawn.networkView.RPC("superSpawn",RPCMode.Server,type);
	}
}
