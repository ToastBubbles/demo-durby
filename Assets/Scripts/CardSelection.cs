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
    private int[] availCards = { 0, 1, 2 };

    public CardSlot card1;
    public CardSlot card2;
    public CardSlot card3;


    void Start()
    {
        
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
            SelectCard();
            activeSelction = true;
        }


        
    }

    private void SelectCard()
    {

        img.SetActive(true);
        GenerateSelection();

        
        //     card1.sprite = database.Cards[availCards[0]].Icon;
        ///   card2.sprite = database.Cards[availCards[1]].Icon;
        //card3.sprite = database.Cards[availCards[2]].Icon;




    }
    public void Clicked()
    {
        card1.UpdateCard(database.Cards[availCards[0]].CardID);
        card2.UpdateCard(database.Cards[availCards[1]].CardID);
        card3.UpdateCard(database.Cards[availCards[2]].CardID);
        img.SetActive(false);
    }

    private void GenerateSelection()
    {

        int a = Random.Range(0, database.Cards.Length);
        int b = Random.Range(0, database.Cards.Length);
        int c = Random.Range(0, database.Cards.Length);

        while (a == b)
            b = Random.Range(0, database.Cards.Length);
        while (b == c || c == a)
            c = Random.Range(0, database.Cards.Length);

        availCards[0] = a;
        availCards[1] = b;
        availCards[2] = c;
        for (int i = 0; i < availCards.Length; i++)
        {
            //Debug.Log(availCards[i]);
        }



    }

}
