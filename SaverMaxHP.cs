using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaverMaxHP : MonoBehaviour
{
    public int MaxHP;

    [Multiline(20)]
    public string data;

    public void Start()
    {
        load();

        if (Player.MaxhealthStatic == 0)
        {
            Player.MaxhealthStatic = 20;
        }
        else
        {
            Player.MaxhealthStatic = MaxHP;
        }
    }

    //public void Update()
    //{
    //    MaxHP = Player.MaxhealthStatic;
    //}
    public void CollectInfo()
    {
        MaxHP = Player.MaxhealthStatic;
    }

    public void SetInfo()
    {
        Player.MaxhealthStatic = MaxHP;
    }

    public void save()
    {
            CollectInfo();
            data = "" + MaxHP;
            File.WriteAllText(Application.persistentDataPath + "/SaveMaxHP"+ Application.version + ".txt", data);
    }
    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveMaxHP" + Application.version + ".txt"))
        {
            data = File.ReadAllText(Application.persistentDataPath + "/SaveMaxHP" + Application.version + ".txt");
            MaxHP = int.Parse(data);
            SetInfo();
        }
    }
}
