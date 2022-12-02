using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCtrl : MonoBehaviour
{
    public float time = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
