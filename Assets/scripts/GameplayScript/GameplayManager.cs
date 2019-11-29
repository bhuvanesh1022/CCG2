using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameplayManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public static GameplayManager gameplaymanager;
    public PlayerController PC;

    public TextMeshProUGUI User_NameTxt;
    public string UserName;

    public TextMeshProUGUI Healthtxt;


    void Start()
    {
        PlayerPostion();
        playerDetails();
        photonView.RPC("AddPlayers",RpcTarget.AllBuffered,null);
    }
   
    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    
   public  void PlayerPostion() { // Set PlayerPosition
        Script_Refresh();
        if (photonView.IsMine) {
            print("1-------");
           transform.position = PlayerController.playerController.playerposition[0].transform.position;
        }
        else {
            print("2------");
         
          transform.position = PlayerController.playerController.playerposition[1].transform.position;

        }
    }

    //Adding players
    [PunRPC]
    public void AddPlayers() {
        Script_Refresh();
        if (PlayerController.playerController.playerlist.Contains(gameObject)) {
            return;
        }
        PlayerController.playerController.playerlist.Add(gameObject);
       // print("playerlist." + PlayerController.playerController.playerlist.Count);
    
    }
    // Playerdetails
    void playerDetails() {
        Script_Refresh();
        if (photonView.IsMine) {
           // User_NameTxt.text = "You";
            UserName = photonView.Owner.NickName;
           this.gameObject.name = photonView.Owner.NickName; // display player name
            User_NameTxt.text = UserName;
            PlayerController.playerController.WagesValue = 0;
            PlayerController.playerController.MaxWagesValue = 10;
            Healthtxt.text = PlayerController.playerController.MaxWagesValue.ToString();


        }
        else {
            UserName = photonView.Owner.NickName;
             this.gameObject.name= photonView.Owner.NickName; // display player name
            User_NameTxt.text = UserName;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(UserName);
        }
        else if (stream.IsReading) {
            UserName = (string)stream.ReceiveNext();
        }
        }
}
