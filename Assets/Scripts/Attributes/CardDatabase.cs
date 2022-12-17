using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CardDatabase : ScriptableObject, ISerializationCallbackReceiver
{

    public Card[] Cards;

    public void UpdateID()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i].CardID != i)
                Cards[i].CardID = i;

            //GetItem.Add(i, Items[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, ItemObject>();
    }
}
