using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WagesManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public bool iswagebetted;

    public int Wagetobet, Wagevalue;
    public int maxwage;
    void Start()
    {
    }
    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    private void Update() {
        Wagevisual();
    }

    public void Wagevisual() {
        Script_Refresh();

        if (photonView.IsMine) {
            for (int i=0;i<PlayerController.playerController.playerlist.Count;i++) {
                PlayerController.playerController.Visual_Txt.GetComponent<TextMeshProUGUI>().text = "place your card";
            }
        }
    }
    // Wages to bet
    private void OnMouseDown() {
        Script_Refresh();
        if (gameObject.tag == "Plus") {
           if (photonView.IsMine) {
                PlayerController.playerController.betadjust++;
                PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
            }
                if (photonView.IsMine) {
                if (PlayerController.playerController.WagesValue < PlayerController.playerController.MaxWagesValue) {
                    PlayerController.playerController.WagesValue++;
                }
            }
        }
        else if (gameObject.tag == "Minus") {
            if (photonView.IsMine) {
                PlayerController.playerController.betadjust--;
                PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
           }
            if (PlayerController.playerController.WagesValue > 0) {
                PlayerController.playerController.WagesValue--;
            }
        }
        else if (gameObject.tag == "Bet") {
            print("Check-------bet");
            PlayerController.playerController.betadjust = 0;
            PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
            iswagebetted = true;

            Wagetobet = Wagevalue;

            PlayerController.playerController.MaxWagesValue = PlayerController.playerController.MaxWagesValue - PlayerController.playerController.WagesValue;
            maxwage = PlayerController.playerController.MaxWagesValue;
            Wagevalue = 0;
            PlayerController.playerController.WagesValue = 0;
            PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
            PlayerController.playerController.ChipText.text = PlayerController.playerController.MaxWagesValue.ToString();

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(iswagebetted);
        }
        else if (stream.IsReading) {
            iswagebetted = (bool)stream.ReceiveNext();
        }
    }
}
