using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ȭ�� �ϴ� ī�װ� �� �з�
/// </summary>
public enum CategoryState { All, PublicBuilding, Hospital, Photozone }

public static class SellectCategory
{
    /// <summary>
    /// ���� ���õ� ī�װ��� ������ static Ŭ����
    /// </summary>
    public static CategoryState state = CategoryState.All;
}

