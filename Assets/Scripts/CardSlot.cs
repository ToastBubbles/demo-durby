using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlot : MonoBehaviour
{
    private int id;
    public Image img;
    public CardDatabase database;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    private Button butt;
    public CardInventory inv;
    private Card currentCard;
    public CardSelection parent;

    void Start()
    {
        butt = GetComponent<Button>();
        butt.onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        Debug.Log("You have clicked the button!");
        inv.Equip(currentCard);
        parent.Clicked();
    }


    void Update()
    {
       // Debug.Log(id);
    }

    public void UpdateCard(int cardID)
    {
        id = cardID;
        SetCard();
    }

    private void SetCard()
    {
        for (int i = 0; i < database.Cards.Length; i++)
        {
            if(database.Cards[i].CardID == id)
            {
                
                currentCard = database.Cards[i];

                img.sprite = currentCard.Icon;
                title.text = currentCard.CardName;
                desc.text = currentCard.Description;

            }
        }
    }


}
