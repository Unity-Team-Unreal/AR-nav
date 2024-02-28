using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ũ���ͺ� �������Ϳ� ����Ǵ� ������
/// </summary>
[System.Serializable]
public class ContentsData
{
    //������ POI ������ 
    public string number; //������ ���� �ѹ�
    public string contentsname; //
    public string description;
    public string latitude;
    public string longitude;
    public string guide;
    public Texture2D Image;
}

/// <summary>
/// ����Ʈ,������ POI �����͸� �����ϴ� POI
/// </summary>
public enum Type { Photo, Docent}; 
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class Contents_POI : ScriptableObject //ScriptableObject�� ���
{
    public Type type;
    public ContentsData[] contentsdata;
}

