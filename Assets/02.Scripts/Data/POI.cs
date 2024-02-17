using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class POI : ScriptableObject //ScriptableObject를 상속
{
    public ContentsData[] contentsdata;
}

[System.Serializable]//클래스를 직렬화하여 데이터를 저장할 수 있게 
public class ContentsData
{
    public string contentsname;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

