using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    public Slider hungerSlider;
    public int amountToBeFeed;

    private int currentFedAmount;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToBeFeed;
        hungerSlider.value = 0;
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FeedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;

        if (currentFedAmount >= amountToBeFeed)
        {
            gameManager.AddScore(amountToBeFeed);
            Destroy(gameObject, 0.1f);
        }
    }
}
