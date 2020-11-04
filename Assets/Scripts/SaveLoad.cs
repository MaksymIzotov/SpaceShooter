using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance;

    const string adress = "data.xml";

    public List<SaveVariables> obj;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (isExist)
        {
            Load();
            ButtonsManager.Instance.UpdateLastLevel();
        }
    }

    public bool isExist => File.Exists(adress);


    public void Load()
    {
        Stream stream = new FileStream(adress, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
        BinaryFormatter formatter;

        formatter = new BinaryFormatter();


        obj = (List<SaveVariables>)formatter.Deserialize(stream);
        stream.Close();

    }

    public void Save(List<SaveVariables> obj)
    {

        Stream stream = File.Open(adress, FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();


        formatter.Serialize(stream, obj);
        stream.Close();
    }
}
