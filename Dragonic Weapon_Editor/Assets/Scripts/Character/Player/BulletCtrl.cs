using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public GameObject Left_firePos, Center_firePos, Right_firePos;
    public GameObject bullet;

    public SE_List PlayerSFX;

    public float delayTime = 2.0f;

    [Range(1, 10)]
    public int weaponPower = 1;

    public int MaxweaponPower = 10;

    public static BulletCtrl instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            PlayerSFX.SoundPlay(0);
            Fire();
        }
    }

    void Fire()
    {
        switch (weaponPower)
        {
            case 1:
            case 2:
            case 3:
                CreateShot(bullet, Center_firePos.transform.position, Vector3.zero);
                break;
            case 4:
            case 5:
            case 6:
                CreateShot(bullet, Left_firePos.transform.position, Vector3.zero);
                CreateShot(bullet, Right_firePos.transform.position, Vector3.zero);
                break;
            case 7:
            case 8:
            case 9:
                CreateShot(bullet, Left_firePos.transform.position, new Vector3(0, 0, 5));
                CreateShot(bullet, Center_firePos.transform.position, Vector3.zero);
                CreateShot(bullet, Right_firePos.transform.position, new Vector3(0, 0, -5));
                break;
            case 10:
                CreateShot(bullet, Left_firePos.transform.position, new Vector3(0, 0, 5));
                CreateShot(bullet, Left_firePos.transform.position, new Vector3(0, 0, 15));
                CreateShot(bullet, Center_firePos.transform.position, Vector3.zero);
                CreateShot(bullet, Right_firePos.transform.position, new Vector3(0, 0, -5));
                CreateShot(bullet, Right_firePos.transform.position, new Vector3(0, 0, -15));
                break;
        }
        
    }

    void CreateShot(GameObject shot, Vector3 pos, Vector3 rot)
    {
        Instantiate(shot, pos, Quaternion.Euler(rot));
    }
}
