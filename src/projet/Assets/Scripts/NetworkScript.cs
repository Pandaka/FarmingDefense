using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkScript : MonoBehaviour{

	[SerializeField]
	private GameObject _spawn;
	
	[SerializeField]
	private bool _isBuildingServer = true;

	[SerializeField]
	private List<GameObject> players;
	
	void Start (){
		Application.runInBackground = true;
		if (_isBuildingServer){
			Network.InitializeSecurity();
			Network.InitializeServer(1, 9090, true);
		}
		else{
			Network.Connect("127.0.0.1", 9090);
		}
	}

	void OnPlayerConnected(NetworkPlayer player){
		foreach(GameObject go in players){
			if(!go.activeSelf)
			networkView.RPC("StartGame", RPCMode.All);
		}
	}

	[RPC]
	void StartGame(){
		_spawn.gameObject.SetActive(true);
	}

	void Update (){}

}