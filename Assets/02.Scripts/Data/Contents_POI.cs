using System;
using UnityEngine;
using UnityEngine.UI;

public enum Type { Photo, Docent};
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class Contents_POI : ScriptableObject //ScriptableObject를 상속
{
    public Type type;
    public ContentsData[] contentsdata;
}

[System.Serializable]//클래스를 직렬화하여 데이터를 저장할 수 있게 

public class ContentsData
{
    public int number;
    public string contentsname;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

