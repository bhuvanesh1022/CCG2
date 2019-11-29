using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
public class HealthManager : MonoBehaviourPunCallbacks,IPunObservable
{
    private PlayerController PC;
    private HealthManager HM;
    private GameplayManager GM;
    private WagesManager WM;

    public static HealthManager healthManager;
    public bool Iscalculating;
    public int health;
    public int Opponentbetted;


    // 
    public bool _IsvalueChangedToPlayer;
    public bool _IsValueChanged;
    public bool _IsNothingChanged,_IsNotChanged;

    //
    public bool _IsWin;


    void Start()
    {
        PC = GameObject.FindObjectOfType<PlayerController>();
        HM = GameObject.FindObjectOfType<HealthManager>();
        GM = GameObject.FindObjectOfType<GameplayManager>();
        WM = GameObject.FindObjectOfType<WagesManager>();
        print("s-------------------");
    }
         
    void Update()
    {
        Health_Call();
        HealthValue_Show();
        StartCoroutine("WaitToCall");
    }
    IEnumerator WaitToCall() {
      
        if (_IsvalueChangedToPlayer) {
            yield return new WaitForSeconds(1f);
            _IsvalueChangedToPlayer = false;
            _IsValueChanged = true;
        }

        if (_IsValueChanged) {
            _IsValueChanged = false;
            for (int i = 0; i < PC.playerlist.Count; i++) {
               // PC.playerlist[i].GetComponent<PlayerController>()._NextTurn = true;
            }
        }
        if (_IsNotChanged) {
            _IsNotChanged = false;
        }
        if (_IsNothingChanged) {
            _IsNotChanged = true;
            _IsNothingChanged = false;
        }
        if (PC._NextTurn) {
            WM.iswagebetted = false;
        }
    }
    //
    void Health_Call() {

        for (int i = 0; i < PC.placedcardlist.Count; i++) {
            for (int j = i + 1; j < PC.placedcardlist.Count; j++) {
                if (PC.placedcardlist[i].GetComponent<Card>().CardValue == 1 && PC.placedcardlist[j].GetComponent<Card>().CardValue == 3) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("attack beats--------");
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            // now change--
                            if (!PC.playerlist[a].GetComponent<SpecialController>()._ISdiffspecialActive && PC.playerlist[a].GetComponent<SpecialController>()._ISdiffspecialActive && !_IsWin) {
                                PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                                PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                            }
                            else {
                                PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                            }
                        }
                        if (PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<HealthManager>()._IsWin = true;
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                //
                else if (PC.placedcardlist[j].GetComponent<Card>().CardValue == 1 && PC.placedcardlist[i].GetComponent<Card>().CardValue == 3) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("attack beats--------");
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;
                            PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                        }
                        if (PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                // 2.Defend beats-----------
                else if (PC.placedcardlist[i].GetComponent<Card>().CardValue == 1 && PC.placedcardlist[j].GetComponent<Card>().CardValue == 2) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("Defend beats--------");
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                            PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                        }
                        if (PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                //
                else if (PC.placedcardlist[j].GetComponent<Card>().CardValue == 1 && PC.placedcardlist[i].GetComponent<Card>().CardValue == 2) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("Defend beats--------");
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                            PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                        }
                        if (PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                // 3.Throw beats----------
                else if (PC.placedcardlist[i].GetComponent<Card>().CardValue == 2 && PC.placedcardlist[j].GetComponent<Card>().CardValue == 3) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("Throw beats--------");
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                            PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                        }
                        if (PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[j].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[j].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                //
                else if (PC.placedcardlist[j].GetComponent<Card>().CardValue == 2 && PC.placedcardlist[i].GetComponent<Card>().CardValue == 3) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                        print("Throw beats--------");
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine && PC.playerlist[a].GetComponent<GameplayManager>().photonView.IsMine) {
                            print(PC.playerlist[a].GetComponent<GameplayManager>().UserName + "reduces" + "--------");
                            PC.playerlist[a].GetComponent<HealthManager>().health -= PC.playerlist[a].GetComponent<HealthManager>().Opponentbetted + 1;

                            PC.playerlist[a].GetComponent<HealthManager>()._IsvalueChangedToPlayer = true;
                        }
                        if (PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                        if (!PC.placedcardlist[i].GetComponent<Card>().photonView.IsMine) {
                            PC.WinName = PC.playerlist[PC.placedcardlist[i].GetComponent<Card>().Whichplayer].GetComponent<GameplayManager>().UserName;
                        }
                    }

                }
                //
                else if (PC.placedcardlist[j].GetComponent<Card>().CardValue==PC.placedcardlist[i].GetComponent<Card>().CardValue) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                       // if (PC.playerlist[a].GetComponent<SpecialManager>()._SpecialCardActive) {
                            PC.playerlist[a].GetComponent<HealthManager>()._IsNothingChanged = true;
                        //}
                        //else {
                        //    PC.playerlist[a].GetComponent<SpecialManager>()._IsspecialApplied = true;
                        //}
                    }
                }
                else if (PC.placedcardlist[i].GetComponent<Card>().CardValue == PC.placedcardlist[j].GetComponent<Card>().CardValue) {
                    for (int a = 0; a < PC.playerlist.Count; a++) {
                       // if (PC.playerlist[a].GetComponent<SpecialManager>()._SpecialCardActive) {
                            PC.playerlist[a].GetComponent<HealthManager>()._IsNothingChanged = true;
                        //}
                        //else {
                        //    PC.playerlist[a].GetComponent<SpecialManager>()._IsspecialApplied = true;
                        //}
                    }
                }


            }
        }
               
       // PlayerController.playerController.placedcardlist = new List<GameObject>();
    }
    // display health value
    void HealthValue_Show() {

        for (int i=0;i<PC.playerlist.Count;i++) {
            if (PC.playerlist[i].GetComponent<HealthManager>()._IsvalueChangedToPlayer && photonView.IsMine) {
                PC.Visual_Txt.GetComponent<TextMeshProUGUI>().text = PC.WinName + " Win";
            }
            if (PC.playerlist[i].GetComponent<HealthManager>()._IsValueChanged && photonView.IsMine) {
                print("he---yy--");
                PC.playerlist[i].GetComponent<GameplayManager>().Healthtxt.text = PC.playerlist[i].GetComponent<HealthManager>().health.ToString();
                PC.Visual_Txt.GetComponent<TextMeshProUGUI>().text = PC.playerlist[i].GetComponent<GameplayManager>().UserName + "Health reduces to"+PC.playerlist[i].GetComponent<HealthManager>().health.ToString();
            }
            if (PC.playerlist[i].GetComponent<HealthManager>()._IsNotChanged && photonView.IsMine) {
                print("55555-----");
                PC.Visual_Txt.GetComponent<TextMeshProUGUI>().text = "Same Cards";
            }

            if (PC.playerlist[i].GetComponent<HealthManager>().PC._NextTurn) {
                PC.placedcardlist = new List<GameObject>();
                PC.Visual_Txt.GetComponent<TextMeshProUGUI>().text = " Next turn";
            }

            }

        }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            stream.SendNext(_IsvalueChangedToPlayer);
            stream.SendNext(_IsValueChanged);
            stream.SendNext(_IsNotChanged);
            stream.SendNext(_IsNothingChanged);
            stream.SendNext(PC._NextTurn);
        }
        else if (stream.IsReading) {
            _IsvalueChangedToPlayer = (bool)stream.ReceiveNext();
            _IsValueChanged = (bool)stream.ReceiveNext();
            _IsNotChanged = (bool)stream.ReceiveNext();
            _IsNothingChanged = (bool)stream.ReceiveNext();
            PC._NextTurn = (bool)stream.ReceiveNext();
        }
    }
}
