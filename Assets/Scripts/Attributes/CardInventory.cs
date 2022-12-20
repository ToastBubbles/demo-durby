using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public ChacterStat FireRate;
    public ChacterStat BulletSpeed;
    public ChacterStat Damage;

    public ChacterStat DriveSpeed;
    public ChacterStat JumpFactor;
    private List<Card> cardcheck;

    [SerializeField] List<Card> cards;
    [SerializeField] CardDatabase inventory;

    private int maxCardsAllowed = 100;

    void Start()
    {

    }


    void Update()
    {
        //Debug.Log(DriveSpeed.Value + " is Speed");
    }
    public void Equip(Card card)
    {
        if (card)
        {
            card.Equip(this);
            cards.Add(card);
            RefreshStats();
        }

    }
    public void Unequip(Card card)
    {
        if (card)
        {
            card.UnEquip(this);
            cards.Remove(card);
            RefreshStats();
        }

    }
    public bool AddCard(Card card)
    {
        if (IsFull())
            return false;

        cards.Add(card);

        return true;
    }
    public bool RemoveCard(Card card)
    {
        if (cards.Remove(card))
        {

            return true;
        }
        return false;

    }
    public bool IsFull()
    {
        return cards.Count >= maxCardsAllowed;
    }

    private void RefreshStats()
    {
        //Debug.Log("REFRESHING");
        if (cardcheck != cards)
        {
            // Debug.Log("List Changed!");
            cards.ForEach(card =>
            {

                if (card.CardType == 0)//only applies if card is a stat mod
                {
                    //Debug.Log(card.CardName);

                }
            });
        }
        cardcheck = new List<Card>(cards);
    }

    private void ApplyStat(string type, float val)
    {

    }
}
