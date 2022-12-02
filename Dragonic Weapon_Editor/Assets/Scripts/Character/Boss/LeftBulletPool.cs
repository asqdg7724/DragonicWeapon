using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBulletPool : MonoBehaviour
{
    public static LeftBulletPool leftBulletPoolInstanse;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> Leftbullets;

    void Awake()
    {
        leftBulletPoolInstanse = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Leftbullets = new List<GameObject>();
    }

    public GameObject GetLeftBullet()
    {
        if (Leftbullets.Count > 0)
        {
            for (int i = 0; i < Leftbullets.Count; i++)
            {
                if (!Leftbullets[i].activeInHierarchy)
                {
                    return Leftbullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            Leftbullets.Add(bul);
            return bul;
        }

        return null;
    }
}
