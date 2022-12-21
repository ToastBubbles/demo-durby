using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlot : MonoBehaviour
{
    public bool isActive;
    private int id;
    public Image img;
    public CardDatabase database;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    private Button butt;
    public CardInventory inv;
    private Card currentCard;

    private Color dark = new Color(56, 56, 56);
    public CardSelection parent;

    void Start()
    {
        if (isActive)
        {
            img.color = Color.white;
        }
        else
        {

            img.color = new Color32(56, 56, 56, 100);
        }
        butt = GetComponent<Button>();
        butt.onClick.AddListener(Clicked);
    }
    public void ToggleActive()
    {
        if (isActive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }

    void Clicked()
    {
        //Debug.Log("You have clicked the button!");
        //Debug.Log(currentCard);
        inv.Equip(currentCard);
        parent.Clicked();
    }


    void Update()
    {
        // Debug.Log(id);
        if (isActive)
        {
            img.color = Color.white;
            if (Input.GetButtonDown("A_xbox"))
            {
                //Debug.Log("FIRE");
                Clicked();
            }
        }
        else
        {

            img.color = new Color32(56, 56, 56, 100);
        }
    }

    public void UpdateCard(int cardID)
    {
        id = cardID;
        //Debug.Log(id);
        SetCard(id);
    }

    private void SetCard(int ident)
    {
        // for (int i = 0; i < database.Cards.Length; i++)
        // {
        //     if (database.Cards[i].CardID == id)
        //     {

        currentCard = database.Cards[ident];
        //this.gameObject.transform.GetChild(0).GetComponent<Image>() = currentCard.Icon;
        img.sprite = currentCard.Icon;
        title.text = currentCard.CardName;
        desc.text = currentCard.Description;

        //         }
        //     }
    }


}
