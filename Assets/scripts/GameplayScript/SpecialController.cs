using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class SpecialController : MonoBehaviourPunCallbacks
{
    public SpecialController SPC;
    public PlayerController PC;
    public WagesManager WM;
    // public SpecialManager SPl;
    public int SplNum;
    public bool IsSpecialNotVal;
    public bool IsClicked;

    public bool _SpecialCardApplied, _SpecialCardActive;
    public bool _ISSameAndSpecial, _IsSpecialCalculating, _IsspecialApplied, _IsSpecialVisual;
    public bool _ISdiffspecialActive;
    void Start()
    {
        // SPl = GameObject.FindObjectOfType<SpecialManager>();
        PC = GameObject.FindObjectOfType<PlayerController>();
        WM = GameObject.FindObjectOfType<WagesManager>();
        SPC = GameObject.FindObjectOfType<SpecialController>();
        print("SplNum-----"+ SplNum);
        PC._Specials[SplNum].GetComponent<Button>().onClick.AddListener(delegate { OnClick_Specialbtn(SplNum); });
    }

    private void OnMouseDown() {
      // SPl. Special_CardFun();
    }


    private void Update() {
       // _SpecialActive();
        print("SplNum------"+ SplNum);
      // PC._Specials[SplNum].GetComponent<Button>().onClick.AddListener(delegate { OnClick_Specialbtn(SplNum); });
    }
   public void OnClick_Specialbtn(int SpecialType) {
        print("but----------");
        switch (SpecialType) {
           
            // win tie
            case 0:
                if (photonView.IsMine) {
                    print("case----------->1");
                    _IsspecialApplied = true;
                    PC._Specials[SplNum].GetComponent<Button>().interactable=false;
                    IsClicked = true;
                }
                break;
                // life steel
            case 1:
                if (photonView.IsMine) {
                    print("case----------->2");
                    _ISdiffspecialActive = true;
                    PC._Specials[SplNum].GetComponent<Button>().interactable = false;
                    IsClicked = true;
                }
                break;
        }
       
    }
    void _SpecialActive() {
        for (int i = 0; i < PC.playerlist.Count; i++) {
            if (PC.OpponentSpecial_Num == SplNum && photonView.IsMine) {
                SplNum = Random.Range(0, PC.MaxSpecialCount);
            }
            else if(PC.OpponentSpecial_Num !=SplNum){
                IsSpecialNotVal = true;
            }
            if (PC.playerlist[i] && photonView.IsMine && IsSpecialNotVal) {
                PC._Specials[PC.playerlist[i].GetComponent<SpecialController>().SplNum].SetActive(true);
                if ((!WM.iswagebetted) && !IsClicked) {
                    PC._Specials[PC.playerlist[i].GetComponent<SpecialController>().SplNum].GetComponent<Button>().interactable = true;
                }
                if (IsClicked) {
                    PC._Specials[PC.playerlist[i].GetComponent<SpecialController>().SplNum].GetComponent<Button>().interactable = false;
                }
            }
            else {
                //PC._Specials[PC.playerlist[i].GetComponent<SpecialController>().SplNum].SetActive(false);
                print("[PC.playerlist[i]----"+PC.playerlist[i]);
            }

        }
        }
}
