using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour
{
    public GameObject testCube;

    private bool activeSelction = false;
    private bool temp = false;
    public GameObject img;
    public CardDatabase database;
    private List<int> availCards = new List<int>();// = { 0, 1, 2, 3, 4 };

    public CardSlot card1;
    public CardSlot card2;
    public CardSlot card3;

    private bool selectionMoveCooldown = true;


    void Start()
    {
        for (int i = 0; i < database.Cards.Length; i++)
        {
            availCards.Add(i);
        }
        //GenerateSelection();
    }


    void Update()
    {

        if (!activeSelction)
        {
            temp = testCube.GetComponent<TestCard>().entered;
        }
        else
        {
            temp = false;
        }
        if (temp)
        {
            img.SetActive(true);
            GenerateSelection();
            activeSelction = true;
        }


        if (Input.GetAxis("Horizontal") > 0 && selectionMoveCooldown)
        {
            StartCoroutine("MoveCooldown");
            if (card1.isActive)
            {
                card1.ToggleActive();
                card2.ToggleActive();
            }
            else if (card2.isActive)
            {
                card2.ToggleActive();
                card3.ToggleActive();
            }
            else if (card3.isActive)
            {
                card3.ToggleActive();
                card1.ToggleActive();
            }
            else
            {
                card1.isActive = true;
            }
            selectionMoveCooldown = false;
        }
        else if (Input.GetAxis("Horizontal") < 0 && selectionMoveCooldown)
        {
            StartCoroutine("MoveCooldown");
            if (card1.isActive)
            {
                card1.ToggleActive();
                card3.ToggleActive();
            }
            else if (card2.isActive)
            {
                card2.ToggleActive();
                card1.ToggleActive();
            }
            else if (card3.isActive)
            {
                card3.ToggleActive();
                card2.ToggleActive();
            }
            else
            {
                card1.isActive = true;
            }
            selectionMoveCooldown = false;
        }

    }

    IEnumerator MoveCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        selectionMoveCooldown = true;
    }


    public void Clicked()
    {
        // card1.UpdateCard(database.Cards[availCards[0]].CardID);
        // card2.UpdateCard(database.Cards[availCards[1]].CardID);
        // card3.UpdateCard(database.Cards[availCards[2]].CardID);
        img.SetActive(false);
        activeSelction = false;
    }
    private static void Shuffle(List<int> arr, int a, int b)
    {
        int temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
    private void GenerateSelection()
    {
        for (int i = 0; i < database.Cards.Length; i++)
        {
            availCards.Add(i);
        }



        int n = availCards.Count;
        // int rand = new Random();
        for (int i = 0; i < n; i++)
        {
            Shuffle(availCards, i, Random.Range(0, (n)));
        }

        //Shuffle();

        // Card[] cardArray = new Card[database.Cards.Length];

        // database.Cards.CopyTo(cardArray, 0);

        // for (int i = 0; i < database.Cards.Length; i++)
        // {
        //     int rnd = Random.Range(i, cardArray.Length);
        //     database.Cards[0] = cardArray[rnd];
        //     cardArray[rnd] = cardArray[i];
        //     cardArray[i] = database.Cards[0];
        //     Debug.Log(cardArray[i].CardID);
        // }
        // Random random = new Random();
        //    cardArray =cardArray.OrderBy(x => random.Next()).ToArray();



        card1.UpdateCard(availCards[0]);
        card2.UpdateCard(availCards[1]);
        card3.UpdateCard(availCards[2]);





        // int a = Random.Range(0, database.Cards.Length);
        // int b = Random.Range(0, database.Cards.Length);
        // int c = Random.Range(0, database.Cards.Length);

        // while (a == b)
        //     b = Random.Range(0, database.Cards.Length);
        // while (b == c || c == a)
        //     c = Random.Range(0, database.Cards.Length);

        // availCards[0] = a;
        // availCards[1] = b;
        // availCards[2] = c;
        // // Debug.Log(availCards);
        // for (int i = 0; i < availCards.Length; i++)
        // {
        //     //Debug.Log(availCards[i]);
        // }



    }

}
