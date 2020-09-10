using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTiling : MonoBehaviour
{
    Renderer rend;

    public GameObject[] wood;

    // Start is called before the first frame update
    void Start()
    {
        wood = GameObject.FindGameObjectsWithTag("Wood");
        CorrectTiling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CorrectTiling()
    {
        for (int i = 0; i < wood.Length; i++)
        {
            rend = wood[i].GetComponent<Renderer>();
            Vector2 scale = Vector2.one;
            scale.x = wood[i].transform.localScale.x;
            rend.material.mainTextureScale = scale;
        }
    }
}
