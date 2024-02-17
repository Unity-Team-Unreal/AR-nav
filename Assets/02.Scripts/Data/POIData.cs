using System;
using UnityEngine;


public class POI : MonoBehaviour //ScriptableObject를 상속
{
    POI_Info poi_Info;
    public ContentsData contentsData;

    public void CreatPOIScriptableObject()
    {
        contentsData = ScriptableObject.CreateInstance<ContentsData>();

    }
    
 

    /*private ContentsData[] FeatchData()
    {
        string[] columnsData = poi_Info.columnsData;

        for (int i = 0; i < contentsData.Length; i++)
        {
            contentsData[i] = new(columnsData[0], columnsData[1], columnsData[2], columnsData[3], columnsData[4]);
        }
    }*/
}

[Serializable]//클래스를 직렬화하여 데이터를 저장할 수 있게 
public class ContentsData : ScriptableObject
{
    [SerializeField] private string contentsname;
    [SerializeField] private string description;
    [SerializeField] private string latitude;
    [SerializeField] private string longitude;
    [SerializeField] private string guide;
    [SerializeField] private Sprite Image;

    public string Name
    {
        get { return contentsname; } set {  contentsname = value; }
    }

    public string Description
    {
        get { return description; } set { description = value; }
    }

    public string Latitude
    {
        get {  return latitude; } set { latitude = value;}
    }

    public string Longitude
    {
        get { return longitude; } set { longitude = value;}
    }

    public string Guide
    {
        get { return guide; } set {  guide = value; }
    }


    public ContentsData(string name, string description, string latitude, string longtitude, string guide)
    {
        this.contentsname = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longtitude;
        this.guide = guide;
    }
}

