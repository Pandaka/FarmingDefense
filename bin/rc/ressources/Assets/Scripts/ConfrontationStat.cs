using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfrontationStat : MonoBehaviour {

	[SerializeField]
	Text nbSpd;
	[SerializeField]
	Text nbAtk;
	[SerializeField]
	Text nbTnk;
	[SerializeField]
	Text nbBil;
	[SerializeField]
	Text chrono;
	[SerializeField]
	Image readyB;

	private int billets = 0;
	private int speedSbire = 0;
	private int tankSbire = 0;
	private int attackSbire = 0;

	private float timer = 108000f;
	private bool pret = false;
	private bool commence = false;
	private bool envoie = false;

	public void go()
	{
		commence=true;
	}

	public bool isReady()
	{
		return pret;
	}

	public void giveBillet(int bil)
	{
		billets=bil;
	}

	public int getSpeed()
	{
		return speedSbire;
	}

	public int getAtk()
	{
		return attackSbire;
	}

	public int getTank()
	{
		return tankSbire;
	}


	public void addSS()
	{
		if(billets>=30 && !pret)
		{
			speedSbire++;
			billets-=30;
		}
	}
	public void addTS()
	{
		if(billets>=50 && !pret)
		{
			tankSbire++;
			billets-=50;
		}
	}
	public void addAS()
	{
		if(billets>=50 && !pret)
		{
			attackSbire++;
			billets-=50;
		}
	}

	public void ready()
	{
		if(!pret)
		{
			readyB.color=Color.green;
			networkView.RPC("playerReady",RPCMode.Server,true);
		}
		else
		{
			readyB.color=Color.white;
			networkView.RPC("playerReady",RPCMode.Server,false);
		}
	}

	void Update()
	{
		//pour l'UI
		nbAtk.text=attackSbire.ToString();
		nbSpd.text=speedSbire.ToString();
		nbTnk.text=tankSbire.ToString();
		nbBil.text=billets.ToString();

		//tant qu'on a du temps et que la confrontation commence
		if(timer >= 0 && commence)
		{	
			timer-=Time.realtimeSinceStartup;
			//on réduit le timer et on affiche le chrono
			chrono.text=((int)timer/216000).ToString()+":"+((int)timer/3600).ToString();
		}

		if(pret || timer <= 0 && !envoie)
		{
			networkView.RPC("StatAJour",RPCMode.Server,speedSbire,attackSbire,tankSbire);
			envoie = true ;
		}
	}

	public void raz()
	{
		billets = 0;
		speedSbire = 0;
		tankSbire = 0;
		attackSbire = 0;
		readyB.color=Color.white;
		timer = 108000f;
		pret = false;
		envoie = false;
	}

	[RPC]
	public void playerReady(bool ok)
	{
		pret=ok;
	}

	[RPC]
	public void StatAJour(int smob,int amob,int tmob){
		this.speedSbire=smob;
		this.attackSbire=amob;
		this.tankSbire=tmob;
		this.pret=true;
	}
}
