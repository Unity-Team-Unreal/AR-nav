using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectExample", menuName = "ScriptableObject/ScriptableObjectExample")]
public class POI : ScriptableObject //ScriptableObject�� ���
{
    public ContentsData[] ARContentsData;
}

[Serializable]//Ŭ������ ����ȭ�Ͽ� �����͸� ������ �� �ְ� 
public class ContentsData
{
    public string name;
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Sprite Image;
}

