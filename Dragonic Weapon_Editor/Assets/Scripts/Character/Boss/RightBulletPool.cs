using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBulletPool : MonoBehaviour
{
    public static RightBulletPool rightBulletPoolInstanse;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool = true;

    private List<GameObject> Rightbullets;

    void Awake()
    {
        rightBulletPoolInstanse = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Rightbullets = new List<GameObject>();
    }

    public GameObject GetRightBullet()
    {
        if (Rightbullets.Count > 0)
        {
            for (int i = 0; i < Rightbullets.Count; i++)
            {
                if (!Rightbullets[i].activeInHierarchy)
                {
                    return Rightbullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            Rightbullets.Add(bul);
            return bul;
        }

        return null;
    }
}
