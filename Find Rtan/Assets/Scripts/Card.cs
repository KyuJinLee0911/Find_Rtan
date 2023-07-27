using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void HideCard()
    {
        Invoke("HideCardInvoke", 1.0f);
    }

    void HideCardInvoke()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);

        if(GameManager.Instance().firstCard == null)
            GameManager.Instance().firstCard = gameObject;
        else
        {
            GameManager.Instance().secondCard = gameObject;
            GameManager.Instance().IsMatched();
        }
    }
}
