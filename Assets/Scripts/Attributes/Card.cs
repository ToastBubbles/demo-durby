using UnityEngine;
//public class Character
//{
//    public ChacterStat FireRate;
//}
public enum CardType
{
    Stat,
    Ability,
    AbilityStat,
    Effect,
}
[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string CardName;
    public int CardID;
    public Sprite Icon;
    public string Description;
    // public Color _col;
    // public Rarity rarity;
    [Space]
    public CardType CardType;
    [Space]
    public string DamageType = "default";
    public int JumpFactor; //0 = no jump yet, 1 = jump, 2 = double jump, etc
    [Space]
    public int DriveSpeed;
    public int FireRate;
    public int Damage;
    [Space]
    public float FireRatePercent;
    public float DamagePercent;
    public float BulletSize;
    public float BulletSpeed;

    public void Equip(CardInventory c)
    {
        if (DriveSpeed != 0)
            c.DriveSpeed.AddModifier(new StatModifier(DriveSpeed, StatModType.Flat, this));
        if (FireRate != 0)
            c.FireRate.AddModifier(new StatModifier(FireRate, StatModType.Flat, this));
        if (Damage != 0)
            c.Damage.AddModifier(new StatModifier(Damage, StatModType.Flat, this));
        if (JumpFactor != 0)
            c.JumpFactor.AddModifier(new StatModifier(JumpFactor, StatModType.Flat, this));

        if (FireRatePercent != 0)
            c.FireRate.AddModifier(new StatModifier(FireRatePercent, StatModType.PercentMult, this));
        if (DamagePercent != 0)
            c.Damage.AddModifier(new StatModifier(DamagePercent, StatModType.PercentMult, this));
        if (BulletSpeed != 0)
            c.BulletSpeed.AddModifier(new StatModifier(BulletSpeed, StatModType.Flat, this));
        if (DamageType != "default")
        {
            c.DamageEffects.Add(DamageType);
        }

        //if (BulletSize != 0)
        //   c.FireRate.AddModifier(new StatModifier(FireRatePercent, StatModType.PercentMult, this));
        Debug.Log("equiped");

    }
    public void UnEquip(CardInventory c)
    {
        c.DriveSpeed.RemoveAllModifiersFromSource(this);
        c.FireRate.RemoveAllModifiersFromSource(this);
        c.BulletSpeed.RemoveAllModifiersFromSource(this);
        c.Damage.RemoveAllModifiersFromSource(this);
        c.JumpFactor.RemoveAllModifiersFromSource(this);
    }







    /*
    public void Equip(Character c)
    {
        c.FireRate.AddModifier(new StatModifier(10, StatModType.Flat, this));
        c.FireRate.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.FireRate.RemoveAllModifiersFromSource(this);
    }*/
}
