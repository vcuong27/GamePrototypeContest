using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_paralax : MonoBehaviour
{
    public float backGroundSize;
    private Transform cameratranform;
    [SerializeField]
    private Transform[] layers_bp;
    private Transform[] layers;
    private float viewZone = 10;
    private int above;
    private int below;

    private void Start()
    {
        cameratranform = Camera.main.transform;
        layers = new Transform[layers_bp.Length];

        for (int i = 0; i < layers_bp.Length; i++)
        {
            layers[i] = Instantiate(transform, new Vector3(0, backGroundSize * i, 0), Quaternion.identity);
        }
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    layers[i] = transform.GetChild(i);
        //}
        above = 0;
        below = layers.Length - 1;
    }

    private void Update()
    {
        if (cameratranform.position.y > layers[above].position.y)
        {
            ScrollUp();
        }
        if (cameratranform.position.y < layers[below].position.y)
        {
            ScrollDown();
        }
    }

    private void ScrollUp()
    {
        int lastIndex = below;
        layers[below].position = new Vector3(layers[below].position.x, layers[above].position.y + backGroundSize, layers[below].position.z);
        below--;
        above = lastIndex;
        if (below < 0)
        {
            below = layers.Length - 1;
        }
    }

    private void ScrollDown()
    {
        int lastIndex = above;
        layers[above].position = new Vector3(layers[above].position.x, layers[below].position.y - backGroundSize, layers[above].position.z);
        above++;
        below = lastIndex;
        if (above >= layers.Length)
        {
            above = 0;
        }
    }
}
