using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public static LobbyController lobbycontroller;

    [SerializeField] private  Datacontroller Datas;
    public InputField PlayerNameInput,RoomNameInput;
    public GameObject Connect_btn,RoomCreateBtn;
    public GameObject MainPanel, LobbyPanel;
    [SerializeField]
    private Transform Container;
    [SerializeField] private GameObject RoomListingPrefab;
    private List<RoomInfo> roomlisting;

    private int Roomsize = 2;
    private string RoomName;
    void Start()
    {
        lobbycontroller = this;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update() {
        if (PlayerNameInput.text.Length >= 2) {
            Connect_btn.GetComponent<Button>().interactable = true;
        }
        else {
            Connect_btn.GetComponent<Button>().interactable = false;
        }
        if (RoomNameInput.text.Length >= 2) {
            RoomCreateBtn.GetComponent<Button>().interactable = true;
        }
        else {
            RoomCreateBtn.GetComponent<Button>().interactable = false;
        }

    }

   
    // Callbacks
    public override void OnConnectedToMaster() {
        print("connect network");
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect_btn.SetActive(true);
        roomlisting = new List<RoomInfo>();
        PlayerNameInput.text = PhotonNetwork.NickName;

    }
    public override void OnDisconnected(DisconnectCause cause) {
        print("Disconnect network");
    }

    public void Player_nameinput(string input) { // inputfield
        PhotonNetwork.NickName = input;
       // Datas.Name = input;
    }
    public void Lobby_nameinputChange(string namein) { // inputfield lobby
        RoomName = namein;
   }

    // Click connect btn
    public void Click_JoinLobby() {
        MainPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        int tempindex;
        foreach (RoomInfo room in roomList) {
            if (roomlisting != null) {
                tempindex = roomlisting.FindIndex(ByName(room.Name));
            }
            else {
                tempindex = -1;
            }
            if (tempindex!=-1) {
                roomlisting.RemoveAt(tempindex);
                Destroy(Container.GetChild(tempindex).gameObject);
            }
            if (room.PlayerCount>0) {
                roomlisting.Add(room);
                Listroom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name) {
        return delegate (RoomInfo room) {
            return room.Name == name;
        };
    }
    void Listroom(RoomInfo room) {
        GameObject TempListing = Instantiate(RoomListingPrefab,Container);
        Roombutton roombtn = TempListing.GetComponent<Roombutton>();
        roombtn.SetRoom(room.Name,room.MaxPlayers,room.PlayerCount);
    }
    public void CreateRoom() {
        print("Create room");
        RoomOptions roomOptions = new RoomOptions() { IsVisible=true,IsOpen=true,MaxPlayers=(byte)Roomsize};
        PhotonNetwork.CreateRoom(RoomName,roomOptions);
    }
    public override void OnCreateRoomFailed(short returnCode, string message) {// failed callback
        print("room failed");
    }
    }
