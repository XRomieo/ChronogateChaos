using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Extra
    private CapsuleCollider2D player;
    [SerializeField] private BoxCollider2D belowSurface;
    //Fight1
    [SerializeField] GameObject crate1;
    [SerializeField] GameObject enemy11;
    [SerializeField] GameObject enemy12;
    [SerializeField] GameObject enemy13;
    //Fight2
    [SerializeField] GameObject crate2;
    [SerializeField] GameObject enemy21;
    //Fight3
    [SerializeField] GameObject crate3;
    [SerializeField] GameObject enemy31;
    [SerializeField] GameObject enemy32;
    //Fight4
    [SerializeField] GameObject crate4;
    [SerializeField] GameObject enemy41;
    //Fight5
    [SerializeField] GameObject crate5;
    [SerializeField] GameObject enemy51;
    //Fight6
    [SerializeField] GameObject crate6;
    [SerializeField] GameObject enemy61;
    //Fight7
    [SerializeField] GameObject crate7;
    [SerializeField] GameObject enemy71;
    //MachineGun
    [SerializeField] GameObject crate8;
    private bool machineGunAcquired = false;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject machineGun;
    [SerializeField] private GameObject machineGunHolder;
    [SerializeField] private BoxCollider2D machineGunTrigger;
    //BossFight
    [SerializeField] BoxCollider2D bossFightTrigger;
    [SerializeField] GameObject zoroEnemy;

    private void Awake() {
        crate1.GetComponent<ObjectsDamage>().isBreakable = false;
        crate2.GetComponent<ObjectsDamage>().isBreakable = false;
        crate3.GetComponent<ObjectsDamage>().isBreakable = false;
        crate4.GetComponent<ObjectsDamage>().isBreakable = false;
        crate5.GetComponent<ObjectsDamage>().isBreakable = false;
        crate6.GetComponent<ObjectsDamage>().isBreakable = false;
        crate7.GetComponent<ObjectsDamage>().isBreakable = false;
        crate8.GetComponent<ObjectsDamage>().isBreakable = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if(enemy11 == null && enemy12 == null && enemy13 == null) {
            if(crate1 != null) {
                crate1.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy21 == null) {
            if (crate2 != null) {
                crate2.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy31 == null && enemy32 == null) {
            if (crate3 != null) {
                crate3.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy41 == null) {
            if (crate4 != null) {
                crate4.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy51 == null) {
            if (crate5 != null) {
                crate5.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy61 == null) {
            if (crate6 != null) {
                crate6.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (enemy71 == null) {
            if (crate7 != null) {
                crate7.GetComponent<ObjectsDamage>().isBreakable = true;
            }
        }
        if (machineGunAcquired && crate8 != null) {
            crate8.GetComponent<ObjectsDamage>().isBreakable = true;
        }

        if (bossFightTrigger.IsTouching(player) && zoroEnemy) {
            zoroEnemy.SetActive(true);
        }
        if (belowSurface.IsTouching(player)) {
            RestartGameScene();
        }
        if (machineGunTrigger.IsTouching(player)) {
            AquireMachineGun();
        }
        if(zoroEnemy == null) {
            this.GetComponent<SceneRedirector>().OpenStoryBoardEnd();
        }
    }

    private void AquireMachineGun() {
        pistol.SetActive(false);
        machineGun.SetActive(true);
        machineGunAcquired = true;
        machineGunHolder.SetActive(false);
    }

    public void RestartGameScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
