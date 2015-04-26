using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfrontationPhase : MonoBehaviour {

	[SerializeField]
	ConfrontationStat j1;
	[SerializeField]
	ConfrontationStat j2;
	[SerializeField]
	PlayerScript ps_J1;
	[SerializeField]
	PlayerScript ps_J2;
	[SerializeField]
	Text j1Text;
	[SerializeField]
	Text j2Text;
	[SerializeField]
	GameObject ui_j1;
	[SerializeField]
	GameObject ui_j2;
	[SerializeField]
	NetworkScript ownNetworkScript;

	private int sommeAtk_j1 = 0;
	private int sommeAtk_j2 = 0;
	private int sommeVie_j1 = 0;
	private int sommeVie_j2 = 0;
	private int gain = 0;
	private bool fini = false;

	public void PhaseOne(){

		//on enlève la possibilité de créer des tours et on prend les billets des joueurs
		ps_J1.disableCreation();
		ps_J2.disableCreation();
		j1.giveBillet(ps_J1.combien_billet());
		j2.giveBillet(ps_J2.combien_billet());

		//on met le jeu en pause
		Time.timeScale=0;

		//on active l'ui
		ui_j1.SetActive(true);
		ui_j2.SetActive(true);

		//on dit que la confrontation commence
		j1.go();
		j2.go();

	}

	void PhaseTwo(){
		if(j1.isReady())
			j1.ready();
		if(j2.isReady())
			j2.ready();

		sommeAtk_j1 = j1.getSpeed()*2 + j1.getAtk()*6 + j1.getTank()*2;
		sommeVie_j1 = j1.getSpeed()*20 + j1.getAtk()*20 + j1.getTank()*60;
		
		sommeAtk_j2 = j2.getSpeed()*2 + j2.getAtk()*6 + j2.getTank()*2;
		sommeVie_j2 = j2.getSpeed()*20 + j2.getAtk()*20 + j2.getTank()*60;

		if(Network.isServer){
			if((sommeVie_j2-sommeAtk_j1) < (sommeVie_j1-sommeAtk_j2))
			{
				networkView.RPC("StartConfront",RPCMode.AllBuffered,1);
			}
			else if((sommeVie_j2-sommeAtk_j1) > (sommeVie_j1-sommeAtk_j2))
			{
				networkView.RPC("StartConfront",RPCMode.AllBuffered,2);
			}
			else
			{
				networkView.RPC("StartConfront",RPCMode.AllBuffered,3);
			}
		}
	}


	void Update()
	{
		if(j1.isReady() && j2.isReady() && !fini && Network.isServer)
			PhaseTwo();

		if(j1.isReady() && j2.isReady() && fini && Network.isServer)
		{
			networkView.RPC("Finish",RPCMode.All);
		}
	}

	[RPC]
	void Finish()
	{
		j1.raz();
		j2.raz();
		
		j1Text.enabled=false;
		j2Text.enabled=false;
		
		ui_j1.SetActive(false);
		ui_j2.SetActive(false);
		
		Time.timeScale=1;
		ownNetworkScript.resetChrono();
	}

	// début de la confrontation (combat)
	[RPC]
	void StartConfront(int type) {
		j1Text.gameObject.SetActive(true);
		j2Text.gameObject.SetActive(true);

		if(!(j1.isReady()))
			j1.ready();

		if(!(j2.isReady()))
			j2.ready();

		if(type == 1)
		{
			j1Text.text="Victoire du joueur 1!";
			j2Text.text="Victoire du joueur 1!";
			gain = (j1.getSpeed()/2)*30 + (j1.getAtk()/2)*50 + (j1.getTank()/2)*50;
			ps_J1.modif_Billet(gain);


		}
		else if(type == 2)
		{
			j1Text.text="Victoire du joueur 2!";
			j2Text.text="Victoire du joueur 2!";
			gain = (j2.getSpeed()/2)*30 + (j2.getAtk()/2)*50 + (j2.getTank()/2)*50;
			ps_J2.modif_Billet(gain);

		}
		else
		{
			j1Text.text="Egalité !";
			j2Text.text="Egalité !";
		}

		fini = true;
	} 

}
