using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WagesManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public PlayerController PC;
   // private HealthManager HM;
    private GameplayManager GM;

    public bool iswagebetted;
    public bool _IsWageVisualBet;

    public int Wagetobet, Wagevalue;
    public int maxwage;
    void Start()
    {
        PC = GameObject.FindObjectOfType<PlayerController>();
    }
    void Script_Refresh() {
        if (PlayerController.playerController == null) {
            PlayerController.playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

    }
    private void Update() {
        Wagevisual();
        Betvalues();
    }
    void Betvalues() {

    }

    public void Wagevisual() {
        Script_Refresh();

        if (!iswagebetted && photonView.IsMine) {
            _IsWageVisualBet = true;
            PC._OpponentBetText.SetActive(true);         
        }
        if (iswagebetted && photonView.IsMine) {
            _IsWageVisualBet = false;
        }
        // place ur bet
        if (_IsWageVisualBet && photonView.IsMine) {
            PC.Visual_Txt.GetComponent<TextMeshProUGUI>().text="place your card";
        }

        if (photonView.IsMine) {
            for (int i=0;i<PlayerController.playerController.playerlist.Count;i++) {
                PlayerController.playerController.Visual_Txt.GetComponent<TextMeshProUGUI>().text = "place your card";
            }
        }
        if (photonView.IsMine) {
            for (int i=0;i<PC.playerlist.Count;i++) {
                if (iswagebetted) {

                }
            }
        }
    }
    // Wages to bet
    private void OnMouseDown() {
        Script_Refresh();
        if (gameObject.tag == "Plus") {
          // if (photonView.IsMine) {
                PlayerController.playerController.betadjust++;
                PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
           // }
                if (photonView.IsMine) {
                if (PlayerController.playerController.WagesValue < PlayerController.playerController.MaxWagesValue) {
                    PlayerController.playerController.WagesValue++;
                }
            }
        }
        else if (gameObject.tag == "Minus") {
           // if (photonView.IsMine) {
                PlayerController.playerController.betadjust--;
                PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
          // }
            if (PlayerController.playerController.WagesValue > 0) {
                PlayerController.playerController.WagesValue--;
            }
        }
        else if (gameObject.tag == "Bet") {
            if (!iswagebetted && !PC._NextTurn) {
                print("Check-------bet");
               
               // PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
                iswagebetted = true;

                Wagetobet = Wagevalue;

                PlayerController.playerController.MaxWagesValue = PlayerController.playerController.MaxWagesValue - PlayerController.playerController.betadjust;
                print(" PlayerController.playerController.MaxWagesValue------" + PlayerController.playerController.betadjust);
                maxwage = PlayerController.playerController.MaxWagesValue;
               PC.ChipText.text = maxwage.ToString();
              //  Wagevalue = 0;
                PlayerController.playerController.WagesValue = 0;
                PlayerController.playerController.betadjust = 0;
                PlayerController.playerController.wagevaluetext.text = PlayerController.playerController.betadjust.ToString();
               
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(iswagebetted);
            stream.SendNext(Wagetobet);
            stream.SendNext(Wagevalue);
            stream.SendNext(PC.MaxWagesValue);
            stream.SendNext(PC.WagesValue);
            stream.SendNext(_IsWageVisualBet);
            stream.SendNext(PC.WinName);
        }
        else if (stream.IsReading) {
            iswagebetted = (bool)stream.ReceiveNext();
            Wagetobet = (int)stream.ReceiveNext();
            Wagevalue = (int)stream.ReceiveNext();
            PC.MaxWagesValue = (int)stream.ReceiveNext();
            PC.WagesValue = (int)stream.ReceiveNext();
            _IsWageVisualBet = (bool)stream.ReceiveNext();
            PC.WinName = (string)stream.ReceiveNext();
        }
    }
}
