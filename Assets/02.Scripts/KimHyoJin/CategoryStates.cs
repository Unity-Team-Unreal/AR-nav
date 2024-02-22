using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 화면 하단 카테고리 바 분류
/// </summary>
public enum CategoryState { All, PublicBuilding, Hospital, Photozone }

public static class SellectCategory
{
    /// <summary>
    /// 현재 선택된 카테고리를 정리한 static 클래스
    /// </summary>
    public static CategoryState state = CategoryState.All;
}

