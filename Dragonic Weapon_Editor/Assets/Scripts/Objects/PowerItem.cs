using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (BulletCtrl.instance.weaponPower < BulletCtrl.instance.MaxweaponPower)
            {
                BulletCtrl.instance.weaponPower++;

                GameManager.GM.PowerUpSound();
            }
   
            else {
                GameManager.GM.Power_Score_Up();
            }
            Destroy(gameObject);
        }

        if (col.tag == "Destroyer")
        {
            Destroy(this.gameObject);
        }
    }
}
