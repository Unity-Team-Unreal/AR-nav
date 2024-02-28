using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스크립터블 오브젝터에 저장되는 데이터
/// </summary>
[System.Serializable]
public class ContentsData
{
    //컨텐츠 POI 데이터 
    public string number; //컨텐츠 종류 넘버
    public string contentsname; //
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

/// <summary>
/// 도슨트,포토존 POI 데이터를 저장하는 POI
/// </summary>
public enum Type { Photo, Docent}; 
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class Contents_POI : ScriptableObject //ScriptableObject를 상속
{
    public Type type;
    public ContentsData[] contentsdata;
}

