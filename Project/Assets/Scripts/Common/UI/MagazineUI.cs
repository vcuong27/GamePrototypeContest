using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineUI : MonoBehaviour
{
    public float remainingAmmo;
    SpriteRenderer[] bullets;
    // Start is called before the first frame update
    void Start()
    {
        bullets = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int remaining = Mathf.FloorToInt(remainingAmmo * bullets.Length);
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i+1 <= remaining)
            {
                bullets[i].gameObject.SetActive(true);
            }
            else
            {
                bullets[i].gameObject.SetActive(false);
            }
        }
    }
}
