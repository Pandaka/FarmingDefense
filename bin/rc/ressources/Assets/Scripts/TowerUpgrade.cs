using UnityEngine;
using System.Collections;

public class TowerUpgrade : MonoBehaviour {

	[SerializeField]
	StatTower ownStat;
	[SerializeField]
	PlayerScript ownPlayer;
	[SerializeField]
	GameObject ownZoneDetection;

	private int lvl_a;
	private int lvl_d;
	private int lvl_p;
	private int lvl_h;

	private bool activ;


	protected bool paused;
	
	void OnPauseGame () 
	{ 
		paused = true; 
	}
	
	void OnResumeGame () 
	{ 
		paused = false; 
	}

	// Use this for initialization
	void Start () {
		this.lvl_a=0;
		this.lvl_d=0;
		this.lvl_p=0;
	}

	public void Reset(){
		this.lvl_a=0;
		this.lvl_d=0;
		this.lvl_p=0;
	}




	void Update(){
		//pour amélioré l'attaque de la tour
		if(!paused)
		{
			if(activ && Network.player == ownPlayer.Player && Input.GetKeyDown(KeyCode.U)){
				if(lvl_a<4 && ownPlayer.combien_billet() > (100 + 50*lvl_a)){
					networkView.RPC("Upgrade",RPCMode.AllBuffered,1);
				}
			}
			//pour amélioré la défense
			if(activ && Network.player == ownPlayer.Player && Input.GetKeyDown(KeyCode.I)){
				if(lvl_d<4 && ownPlayer.combien_billet() > (100 + 50*lvl_d)){
					networkView.RPC("Upgrade",RPCMode.AllBuffered,2);
				}
			}
			//pour améliorét la portée
			if(activ && Network.player == ownPlayer.Player && Input.GetKeyDown(KeyCode.O)){
				if(lvl_p<4 && ownPlayer.combien_billet() > (100 + 50*lvl_p)){
					networkView.RPC("Upgrade",RPCMode.AllBuffered,3);
				}
			}
			//pour soigner la tour
			if(activ && Network.player == ownPlayer.Player && Input.GetKeyDown(KeyCode.H)){
				if(ownPlayer.combien_billet() > (100 + 50*lvl_h)){
					networkView.RPC("Heal",RPCMode.AllBuffered);
				}
			}
		}
	}

	//Fonction quand on clic sur la tour
	void OnMouseDown(){
		if(!activ)
		{
			activ=true;
			ownZoneDetection.SetActive(true);
		}
		else
		{
			activ=false;
			ownZoneDetection.SetActive(false);
		}
		Debug.Log("Clic");
		//faire apparaitre le menu

	}

	[RPC]//type 1 = attack, type 2 = defense, type 3 = portée
	void Heal()
	{
		if(ownStat.Vie+((ownStat.VieMax*3)/4) >= ownStat.VieMax)
			ownStat.Vie=ownStat.VieMax;
		else
			ownStat.Vie+=(ownStat.VieMax*3)/4;
		ownPlayer.modif_Billet(-(100 + 50*lvl_h));
		lvl_h++;
	}

	[RPC]//type 1 = attack, type 2 = defense, type 3 = portée
	void Upgrade(int type)
	{
		if(type == 1)
		{
			switch(lvl_a){
				case 0:
					ownStat.modif_attack(2);
					ownPlayer.modif_Billet(-(100 + 50*lvl_a));
					lvl_a++;
					break;
				case 1:
					ownStat.modif_attack(2);
					ownPlayer.modif_Billet(-(100 + 50*lvl_a));
					lvl_a++;
					break;
				case 2:
					ownStat.modif_attack(3);
					ownPlayer.modif_Billet(-(100 + 50*lvl_a));
					lvl_a++;
					break;
				case 3:
					ownStat.modif_attack(3);
					ownPlayer.modif_Billet(-(100 + 50*lvl_a));
					lvl_a++;
					break;

			}
		}
		else if(type == 2)
		{
			switch(lvl_d){
			case 0:
				ownStat.modif_def(100);
				ownPlayer.modif_Billet(-(100 + 50*lvl_d));
				lvl_d++;
				break;
			case 1:
				ownStat.modif_def(100);
				ownPlayer.modif_Billet(-(100 + 50*lvl_d));
				lvl_d++;
				break;
			case 2:
				ownStat.modif_def(100);
				ownPlayer.modif_Billet(-(100 + 50*lvl_d));
				lvl_d++;
				break;
			case 3:
				ownStat.modif_def(100);
				ownPlayer.modif_Billet(-(100 + 50*lvl_d));
				lvl_d++;
				break;
				
			}
		}
		else if(type == 3)
		{
			switch(lvl_p){
			case 0:
				ownStat.modif_portee(0.1f);
				ownZoneDetection.transform.localScale+=new Vector3(0.2f,0,0.2f);
				ownPlayer.modif_Billet(-(100 + 50*lvl_p));
				lvl_p++;
				break;
			case 1:
				ownStat.modif_portee(0.2f);
				ownZoneDetection.transform.localScale+=new Vector3(0.4f,0,0.4f);
				ownPlayer.modif_Billet(-(100 + 50*lvl_p));
				lvl_p++;
				break;
			case 2:
				ownStat.modif_portee(0.1f);
				ownZoneDetection.transform.localScale+=new Vector3(0.2f,0,0.2f);
				ownPlayer.modif_Billet(-(100 + 50*lvl_p));
				lvl_p++;
				break;
			case 3:
				ownStat.modif_portee(0.2f);
				ownZoneDetection.transform.localScale+=new Vector3(0.4f,0,0.4f);
				ownPlayer.modif_Billet(-(100 + 50*lvl_p));
				lvl_p++;
				break;
			}
		}
	}
}
