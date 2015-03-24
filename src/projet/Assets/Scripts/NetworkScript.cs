using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkScript : MonoBehaviour{

	[SerializeField]
	private List<GameObject> _spawn;
	
	[SerializeField]
	private bool _isBuildingServer = true;

	[SerializeField]
	private List<GameObject> players;

	[SerializeField]
	Camera server_cam;

	void Start (){
		//le serveur reste allumé si on est plus dessus
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
		//on active le "player" pour les joueurs qui se connecte et on lance le jeu
		networkView.RPC("ActivePlayer",RPCMode.Others,0);
		networkView.RPC("StartGame", RPCMode.All);
	}

	[RPC]
	void StartGame(){
		//on active tout les spawns
		foreach (GameObject generateur in _spawn)
			generateur.gameObject.SetActive(true);
	}

	[RPC]
	void ActivePlayer(int nb){
		players[nb].SetActive(true);

	}

}