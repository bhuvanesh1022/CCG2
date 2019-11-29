using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;



public class SpecialManager : MonoBehaviourPunCallbacks
{
    public PlayerController PC;
    public SpecialManager SPl;
    public HealthManager HM;

    public enum Special { IWins, LifeSteel };
    public Special spl;
    
    public bool _SpecialCardApplied,_SpecialCardActive;
    public bool _ISSameAndSpecial,_IsSpecialCalculating,_IsspecialApplied,_IsSpecialVisual;



    void Start()
    {
        PC = GameObject.FindObjectOfType<PlayerController>();
        SPl = GameObject.FindObjectOfType<SpecialManager>();
        HM = GameObject.FindObjectOfType<HealthManager>();

       // PC._SpecialBtn.GetComponent<Button>().onClick.AddListener(OnClick_Specialbtn);
    }

   void Update()
    {
        // Special_CardFun();
       
    }
    void _SpecialActive() {


    }
    //click special _Btn
    public void OnClick_Specialbtn() {
        if (photonView.IsMine) {
            _SpecialCardActive = true;
            PC._SpecialBtn.GetComponent<Button>().interactable = false;
        }
    }
  
    public void Special_CardFun() {
        // if (_IsSpecialCalculating) {
        switch (spl) 
        {
            case Special.IWins:

                for (int i = 0; i < PC.playerlist.Count; i++) {
                    if (PC.playerlist[i].GetComponent<SpecialManager>()._SpecialCardApplied && !photonView.IsMine) {//
                        for (int j = 0; j < PC.playerlist.Count; j++) {
                            if (PC.playerlist[j].GetComponent<SpecialManager>().photonView.IsMine) {
                                PC.playerlist[j].GetComponent<HealthManager>().health -= PC.playerlist[j].GetComponent<HealthManager>().Opponentbetted + 1;
                                PC.playerlist[j].GetComponent<GameplayManager>().Healthtxt.text = PC.playerlist[j].GetComponent<HealthManager>().health.ToString();
                            }
                        }
                    }
                }
                break;

            case Special.LifeSteel:
                break;

            default:
                break;
        }

        
           // _IsSpecialCalculating = false;
        }

   
}
