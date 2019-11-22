using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameplayManager : MonoBehaviourPunCallbacks
{
    public static GameplayManager gameplaymanager;

    private GameObject TempObject;
    void Start()
    {
        Player_Instanciate();
        //PlayerDetails();
        PlayerPostion();
        //photonView.RPC("AddPlayers",RpcTarget.AllBuffered,null);
    }
   
    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    // Instantiate player
    void Player_Instanciate() {
        Script_Refresh();
        TempObject = PhotonNetwork.Instantiate(PlayerController.playerController.playeref.name,new Vector3(0,0,0),Quaternion.identity);
        TempObject.GetComponent<PlayerInst>().userName = PhotonNetwork.NickName;
       // TempObject.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = TempObject.GetComponent<PlayerInst>().userName;
    }
   public  void PlayerPostion() { // Set PlayerPosition
        Script_Refresh();
        if (photonView.IsMine) {
            print("1-------");
            TempObject.transform.position = PlayerController.playerController.playerposition[0].transform.position;
        }
        else {
            print("2------");
            TempObject.transform.position = PlayerController.playerController.playerposition[1].transform.position;

        }
    }

    //Adding players
    //[PunRPC]
    //public void AddPlayers() {
    //    Script_Refresh();
    //    if (PlayerController.playerController.playerlist.Contains(TempObject)) {
    //        return;
    //    }
       
    //    PlayerController.playerController.playerlist.Add(TempObject);
    //    print("playerlist." + PlayerController.playerController.playerlist.Count);

    //}

    // PlayerDetails
    

    
}
