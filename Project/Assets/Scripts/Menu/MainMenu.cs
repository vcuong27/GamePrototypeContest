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
        StartCoroutine(FadeIn());
        SceneManager.LoadScene("MainScene");
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }


    public IEnumerator FadeOut()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
        //yield return new WaitUntil(() => blackIMG.color.a <= 0.1);
    }


    public IEnumerator FadeIn()
    {
        anim.SetBool("Fad", false);
        yield return new WaitForSeconds(1.5f);
        //yield return new WaitUntil(() => blackIMG.color.a >= 0.9);
    }
}