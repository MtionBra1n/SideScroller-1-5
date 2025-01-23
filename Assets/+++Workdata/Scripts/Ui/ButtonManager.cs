
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuContainer;
    [SerializeField] private GameObject optionsMenuContainer;

    [SerializeField] private Animator anim;
    
    public void Button_OpenOptionMenu()
    {
        mainMenuContainer.SetActive(false);
        optionsMenuContainer.SetActive(true);
    }
    
    public void Button_OpenMainMenu()
    {
        mainMenuContainer.SetActive(true);
        optionsMenuContainer.SetActive(false);
    }
    
    public void Button_NewGame()
    {
        StartCoroutine(FadeInLoadScene());
    }

    IEnumerator FadeInLoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        anim.Play("FadePanel_fade in");
        yield return new WaitForSeconds(1);

        yield return new WaitUntil(() => asyncOperation.progress >= 0.9f);
        
        asyncOperation.allowSceneActivation = true;
    }
}

