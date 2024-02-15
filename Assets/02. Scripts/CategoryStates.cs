using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CategoryState { All, PublicBuilding, Hospital, Photozone }

public static class SellectCategory    //현재 카테고리 상태를 나타내는 static 클래스
{
    public static CategoryState state = CategoryState.All;
}


public class CategoryStates : MonoBehaviour
{

}
