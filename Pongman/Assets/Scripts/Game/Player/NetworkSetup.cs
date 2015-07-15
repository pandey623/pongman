﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkSetup : NetworkBehaviour {

	public void translateToSpawn(){
		/*if (GameManager.userName == "stawrocek") {
			transform.position = GameObject.Find("SpawnSouth").transform.position;
		}*/
		if (!isLocalPlayer)
			return;

		if (netId.Value == 1) {
			transform.position = GameObject.Find("SpawnEast").transform.position;
		}
		if (netId.Value == 2) {
			transform.position = GameObject.Find("SpawnWest").transform.position;
		}
		if (netId.Value == 3) {
			transform.position = GameObject.Find("SpawnSouth").transform.position;
		}
		if (netId.Value == 4) {
			transform.position = GameObject.Find("SpawnNorth").transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("NetworkBehaviour::Start");
		//Debug.Log ("start");
		//Debug.Log (isLocalPlayer);
		//ClientScene.Ready(ClientScene.
		//ClientScene.AddPlayer (0);
		//Debug.Log("client known as " + GameManager.userName + " added to local scene with id=0");
		if (isLocalPlayer) {
			//Debug.Log ("attach script");
			GetComponent<MovePlayer>().enabled = true;
			GetComponent<GameStates>().enabled = true;
			GetComponent<PlayerSyncName>().changeRealName(GameManager.userName);

			//tutaj
			translateToSpawn();
		}
	}

	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		if(GameManager.maximumNumberOfPlayersConnected==GameManager.connectedPlayers)
		translateToSpawn ();

		Debug.Log ("Courutine!!!");
	}

	public override void OnStartClient(){
		Debug.Log ("NetworkBehaviour::OnSTartClient");
		//Debug.Log ("on start client " + isLocalPlayer);
		/*if (isLocalPlayer) {
			Debug.Log ("exec");
			StartCoroutine (ExecuteAfterTime (0.2f));
		}*/
		base.OnStartClient ();
	}

	public override void OnStartLocalPlayer ()
	{
		Debug.Log ("NetworkBehaviour::OnSTartLocalPlayer");
		//Debug.Log ("on start local player " + isLocalPlayer);
		if (isLocalPlayer) {
			Debug.Log ("exec courutine");
			StartCoroutine (ExecuteAfterTime (0.5f));
		}
		base.OnStartLocalPlayer ();
	}

	public void dispNetId(){
		Debug.Log ("NetId for user " + GetComponent<PlayerSyncName>().getPlayerName() + " = " + netId+
		           " ,localPlayer= " + isLocalPlayer );

	}

}
