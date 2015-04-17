using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerCreation : MonoBehaviour {

	[SerializeField]
	List<Transform> tour_j1;
	[SerializeField]
	List<Transform> tour_j2;
	[SerializeField]
	GameObject[] player;
	[SerializeField]
	Camera player1_cam;
	[SerializeField]
	Camera player2_cam;
	[SerializeField]
	Transform map;

	Ray ray;
	Ray ray2;
	RaycastHit hit;

	private PlayerScript player1_script;
	private PlayerScript player2_script;

	void Start () 
	{
		player1_script=player[0].GetComponent<PlayerScript>();
		player2_script=player[1].GetComponent<PlayerScript>();

	}

	void Update () {
		int qui=0;
		//si on fait clic gauche
		if (Input.GetMouseButtonDown(0)) {
			
			//on capture la position du clic sur la map, On prend la caméra en fonction du joueur qui clic
			Debug.Log("Joueur qui clic : "+Network.player);
			if(Network.player ==player1_script.Player)
			{	
				ray = player1_cam.ScreenPointToRay (Input.mousePosition);
				qui=1;
				//Debug.Log(ray);
			}
			else if(Network.player==player2_script.Player)
			{	
				qui=2;
				ray2 = player2_cam.ScreenPointToRay (Input.mousePosition);
				//Debug.Log(ray2);
			}
			Debug.Log(qui);
			//vérification qu'on touche la map
			if (qui==1 && Physics.Raycast(ray, out hit) && hit.collider.gameObject == map.gameObject) {		
				Debug.Log("hit x:"+hit.point.x+" y:"+hit.point.y+" z"+hit.point.z );

				//on arrange x et z pour une postion plus propre des tours
				float x = Mathf.Round(hit.point.x);
				float z = Mathf.Round(hit.point.z);

				//on met les coordées dans un vecteur 3
				Vector3 tmp = new Vector3 (x, 1.4f, z);

				//envoie du rpc pour créer la tour
				Debug.Log("Player ?" + Network.player + " ok?"+ (player2_script.Player==Network.player)
				          + " dans zone X2?" + (tmp.x<0f && tmp.x>-45f) + " x1?"+ (tmp.x>0f && tmp.x<45f)
				          + "Dans zone z ?"+ (tmp.z>-25f && tmp.z<25f));
				networkView.RPC("PlaceTower", RPCMode.Server, tmp, 1);
			}
			else if(qui==2 && Physics.Raycast(ray2, out hit) && hit.collider.gameObject == map.gameObject)
			{
				Debug.Log("hit x:"+hit.point.x+" y:"+hit.point.y+" z"+hit.point.z );
				
				//on arrange x et z pour une postion plus propre des tours
				float x = Mathf.Round(hit.point.x);
				float z = Mathf.Round(hit.point.z);
				
				//on met les coordées dans un vecteur 3
				Vector3 tmp = new Vector3 (x, 1.4f, z);
				
				//envoie du rpc pour créer la tour
				Debug.Log("Player ?" + Network.player + " ok?"+ (player2_script.Player==Network.player)
				          + " dans zone X2?" + (tmp.x<0f && tmp.x>-45f) + " x1?"+ (tmp.x>0f && tmp.x<45f)
				          + "Dans zone z ?"+ (tmp.z>-25f && tmp.z<25f));
				networkView.RPC("PlaceTower", RPCMode.Server, tmp, 2);
			}
		}			
	}

	[RPC]
	void PlaceTower(Vector3 pos,int builder)
	{
		Debug.Log("Who ?"+builder);
		Debug.Log(" dans zone X2?" + (pos.x<0f && pos.x>-45f) + " x1?"+ (pos.x>0f && pos.x<45f)
		          + "Dans zone z ?"+ (pos.z>-25f && pos.z<25f)
		          + "Encore des tours2 ?"+ (player2_script.cmp_tower()>0) + " j1?" + (player1_script.cmp_tower()>0));

		//si le joueur1 est dans sa partie de terrain et qu'il peut encore poser des tours
		if(builder==1 && 
		   (pos.x>0f && pos.x<45f) && 
		   (pos.z>-25f && pos.z<25f) && 
		   player1_script.cmp_tower()>0)
		{
			// on détecte les obstacles autour de la position ou on veut placer la tour
			var collidedObs = Physics.OverlapSphere(pos, 1f, 1 << LayerMask.NameToLayer("obstacle"));

			if (collidedObs == null || collidedObs.Length == 0) {
				//si le joueur a assez de billet pour la créer
				if(player1_script.combien_billet()>=100){
					networkView.RPC("CreateTowerJ1",RPCMode.All,pos);
				}
			}
		}
		//si le joueur2 est dans sa partie de terrain et qu'il peut encore poser des tours
		else if(builder==2 && 
		        (pos.x>-45f && pos.x<0f) && 
		        (pos.z>-25f && pos.z<25f) && 
		        player2_script.cmp_tower()>0)
		{
			// on détecte les obstacles autour de la position ou on veut placer la tour
			var collidedObs = Physics.OverlapSphere(pos, 1f, 1 << LayerMask.NameToLayer("obstacle"));
			
			if (collidedObs == null || collidedObs.Length == 0) {
				//si le joueur a assez de billet pour la créer
				if(player2_script.combien_billet()>=100){
					networkView.RPC("CreateTowerJ2",RPCMode.All,pos);
				}
			}
		}
	}

	[RPC]
	void CreateTowerJ1 (Vector3 pos) {
	//on recherche une tour désactivée
		foreach(Transform go in tour_j1)
		{
			//si il y a une tour désactivée
			if(!go.gameObject.activeSelf)
			{
				//on la positionne puis on active la barre de vie et la tour ensuite
				go.position = pos;
				StatTower tower = go.gameObject.GetComponent<StatTower>();
				tower.lifeBarInitiate();
				go.gameObject.SetActive(true);

				//ajout d'une tour au compteur et on enlève les billets
				player1_script.modif_nbTour(1);
				player1_script.modif_Billet(-100);
				break;
			}
		}
	}	

	[RPC]
	void CreateTowerJ2 (Vector3 pos) {
		
		foreach(Transform go in tour_j2)
		{
			if(!go.gameObject.activeSelf)
			{
				go.position = pos;
				StatTower tower = go.gameObject.GetComponent<StatTower>();
				tower.lifeBarInitiate();
				go.gameObject.SetActive(true);
				
				//ajout d'une tour au compteur et on enlève des billets
				player2_script.modif_nbTour(1);
				player2_script.modif_Billet(-100);

				break;
			}
		}
	}
}