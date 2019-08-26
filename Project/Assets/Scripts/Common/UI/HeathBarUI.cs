using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarUI : MonoBehaviour
{
    [SerializeField]
    private RawImage heathBar;
    public Color FullHeathColor;//"#36992DFF";
    public Color LowHeathColor; // "#9B2D2DFF";
    private float HEATH_BAR_CHANGE_DURATION = 0.7f;
    [SerializeField]
    private float lowthreshold = 0.5f;
    [SerializeField]
    private bool horizontal = false;

    public float percentage = 1;
    private float currentPercentage = 1;

    float displayHP = 1;
    public float Percentage
    {
        get { return percentage; }
        set
        {
            if (value < 0)
            {
                percentage = 0;
            }
            else if (value > 1.0f)
            {
                percentage = 1.0f;
            }
            else
            {
                percentage = value;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        heathBar.color = FullHeathColor;

        heathBar.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (percentage <= lowthreshold)
        {
            heathBar.color = LowHeathColor;
        }
        else
        {
            heathBar.color = FullHeathColor;
        }

        if (currentPercentage != percentage)
        {
            float dtHP = currentPercentage - percentage;
            displayHP -= dtHP * dt / HEATH_BAR_CHANGE_DURATION;
            if (displayHP < percentage) displayHP = percentage;
            if (displayHP == percentage)
            {
                currentPercentage = percentage;
            }
        }
        if (displayHP < 0) displayHP = 0;
        if (displayHP > 1) displayHP = 1;
        heathBar.transform.localScale = new Vector3(horizontal ? displayHP : 1, !horizontal ? displayHP : 1, 1);
    }
}
