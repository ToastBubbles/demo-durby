using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public ChacterStat FireRate;
    public ChacterStat Damage;
    public ChacterStat DriveSpeed;
    public ChacterStat JumpFactor;

    [SerializeField] List<Card> cards;
    [SerializeField] CardDatabase inventory;

    private int maxCardsAllowed = 100;

    void Start()
    {

    }


    void Update()
    {
        Debug.Log(DriveSpeed.Value + " is Speed");
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

    }
}
