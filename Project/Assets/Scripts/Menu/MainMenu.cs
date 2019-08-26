using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image blackIMG;
    public Animator anim;

    private void Start()
    {
        blackIMG.gameObject.SetActive(true);
    }

    public void Play()
    {
        blackIMG.gameObject.SetActive(true);
        Fading();
        SceneManager.LoadScene("MainScene");
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }


    IEnumerator Fading()
    {
        anim.SetBool("Fade",true);
        yield return new WaitUntil(() => blackIMG.color.a >= 0.9);
    }
}
