using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PlayerInst : MonoBehaviourPunCallbacks,IPunObservable {
    public static PlayerInst playerInst;
    public string userName;

   // private GameObject TempObject;
    private void Awake() {
        Script_Refresh();
    }
    void Start() {
        photonView.RPC("AddPlayers", RpcTarget.AllBuffered, null);
        PlayerDetails();
    }

    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    //Adding players
    [PunRPC]
    public void AddPlayers() {
       // Script_Refresh();
        if (PlayerController.playerController.playerlist.Contains(gameObject)) {
            return;
        }
        PlayerController.playerController.playerlist.Add(gameObject);

        print("playerlist." + PlayerController.playerController.playerlist.Count);

    }

    public void PlayerDetails() {

        if (!PlayerController.playerController.PlayerNames.Contains(userName)) {
            PlayerController.playerController.PlayerNames.Add(userName);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.IsWriting) {
            stream.SendNext(userName);
        }
        else if (stream.IsReading) {
            userName = (string)stream.ReceiveNext();
        }
    }
}
