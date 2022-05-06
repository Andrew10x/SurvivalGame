
using UnityEngine;

public abstract class MyWeapon
{
    public int[] weaponDamage;
    public int[] weaponCount;
    public MyWeapon(int[] wd, int[] wc)
    {
        weaponDamage = wd;
        weaponCount = wc;
    }
    public abstract int getDamage();
}

public class Axe : MyWeapon {
    static int[] wd = { 25, 30, 40, 50 };
    static int[] wc = { 10, 20, 35, 45 };
    public Axe():base(wd, wc)
    {
    }

    override public int getDamage()
    {
        int pos = PlayerPrefs.GetInt("Axe");
        if (pos == -1) return 0;
        return weaponDamage[pos];
    }
}

public class Revolver : MyWeapon
{
    static int[] wd = {30, 40, 50, 65 };
    static int[] wc = { 15, 25, 35, 45 };
    public Revolver() : base(wd, wc)
    {
    }

    override public int getDamage()
    {
        int pos = PlayerPrefs.GetInt("Revolver");
        if (pos == -1) return 0;
        return weaponDamage[pos];
    }
}

public class Shotgun : MyWeapon
{
    static int[] wd = { 40, 55, 80, 100 };
    static int[] wc = { 20, 30, 35, 50 };
    public Shotgun() : base(wd, wc)
    {
    }

    override public int getDamage()
    {
        int pos = PlayerPrefs.GetInt("Shotgun");
        if (pos == -1) return 0;
        return weaponDamage[pos];
    }
}

public class AsRiffle : MyWeapon
{
    static int[] wd = { 15, 20, 25, 35 };
    static int[] wc = { 40, 50, 60, 70 };
    public AsRiffle() : base(wd, wc)
    {
    }

    override public int getDamage()
    {
        int pos = PlayerPrefs.GetInt("AsRiffle");
        if (pos == -1) return 0;
        return weaponDamage[pos];
    }
}


