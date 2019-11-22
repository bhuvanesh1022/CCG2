using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class RoomController : MonoBehaviourPunCallbacks
{
    public static RoomController roomController;
    public GameObject LobbyPanel, RoomPanel;
    public Text RoomName_display;
    public GameObject StartBtn, EnterBtn;
    [SerializeField] private Transform PlayerContainer;
    [SerializeField] private GameObject PlayerListingPrefab;

    public PhotonView photon;
    bool Isenter;
    private GameObject Templisting;
    public bool IsEnable;
    void Awake() {
        if (roomController == null) {
            roomController = this;
        }
        else {
            if (roomController != null) {
                Destroy(roomController.gameObject);
                roomController = this;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    //callback
    public override void OnJoinedRoom() {
        print("Joined Room");
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
        RoomName_display.text = PhotonNetwork.CurrentRoom.Name;
        EnterRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) {
        if (!Isenter) {
            EnterBtn.SetActive(true);
        }
        print("Entered-");
    }
    public override void OnPlayerLeftRoom(Player otherPlayer) {
        print("Left-");
        if (Isenter) {

        }
        else {
            EnterBtn.SetActive(true);
        }
    }
    public void EnterRoom() {
        Isenter = true;
        photon.RPC("ClearPlayerListing",RpcTarget.All);
        photon.RPC("ListPlayer", RpcTarget.All);
    }
    [PunRPC]
    void ClearPlayerListing() {
        for (int i= PlayerContainer.childCount - 1;i>=0; i--) {
            Destroy(PlayerContainer.GetChild(i).gameObject);
        }
    }
    [PunRPC]
    void ListPlayer() {
        foreach (Player player in PhotonNetwork.PlayerList) {
            Templisting = Instantiate(PlayerListingPrefab, PlayerContainer);
            Text tem_txt = Templisting.transform.GetChild(0).GetComponent<Text>();
            tem_txt.text = player.NickName;
        }
    }
    //
    public void StartGame() {

        photonView.RPC("TempFun",RpcTarget.AllBuffered,null);
    }
    public void LoadScene() {
        if (IsEnable) {
            PhotonNetwork.LoadLevel(1);
        }
    }
    [PunRPC]
   public void TempFun() {
        IsEnable = true;
        LoadScene();
    }
    
}
