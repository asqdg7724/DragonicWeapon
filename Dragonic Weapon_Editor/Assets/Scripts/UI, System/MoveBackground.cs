using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float offset;
    public float scrollSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
    }
}
