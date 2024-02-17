using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class POI : ScriptableObject //ScriptableObject�� ���
{
    public ContentsData[] contentsdata;
}

[System.Serializable]//Ŭ������ ����ȭ�Ͽ� �����͸� ������ �� �ְ� 
public class ContentsData
{
    public string contentsname;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

