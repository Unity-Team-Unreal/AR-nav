using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObject/ScriptableObjectExample")]
public class POI : ScriptableObject //ScriptableObject를 상속
{
    public ContentsData[] ARContentsData;
}

[Serializable]//클래스를 직렬화하여 데이터를 저장할 수 있게 
public class ContentsData
{
    public string name;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Sprite Image;
}

