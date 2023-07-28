using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Text timeText;
    float time = 0.0f;
    public GameObject card;
    public Transform cardParentTransform;

    public GameObject firstCard;
    public GameObject secondCard;

    public AudioClip match;
    public AudioSource audioSource;

    int maxCardCount = 16;
    public GameObject endTxt;

    public static GameManager Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            if (instance == null)
                Debug.Log("No Singleton Object");
        }
        return instance;
    }

    void Awake()
    {
        if (!instance)
            instance = this;

        if (instance != this)
            Destroy(gameObject);

    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.SetParent(cardParentTransform);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<Image>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            time = 30.0f;
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void IsMatched()
    {
        string firstImage = firstCard.transform.GetChild(0).GetComponent<Image>().sprite.name;
        string secondImage = secondCard.transform.GetChild(0).GetComponent<Image>().sprite.name;

        if (firstImage == secondImage)
        {
            audioSource.PlayOneShot(match);
            maxCardCount -= 2;
            firstCard.GetComponent<Card>().HideCard();
            secondCard.GetComponent<Card>().HideCard();

            if (maxCardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
