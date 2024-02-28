using System;
using UnityEngine;
using UnityEngine.UI;

public enum Type { Photo, Docent};
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class Contents_POI : ScriptableObject //ScriptableObject�� ���
{
    public Type type;
    public ContentsData[] contentsdata;
}

[System.Serializable]//Ŭ������ ����ȭ�Ͽ� �����͸� ������ �� �ְ� 

public class ContentsData
{
    public string number;
    public string contentsname;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

