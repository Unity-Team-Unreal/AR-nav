using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesImageScript : MonoBehaviour
{
    /// <summary>
    /// 상세설명 이미지를 띄우는 스크립트
    /// </summary>


     RawImage DesBoxImage;  //상세설명 이미지


    void Awake()
    {
        DesBoxImage = GetComponent<RawImage>();

        DesBoxImage.enabled = false;    //시작시 비활성화
    }

   public void desBoxEneable(POIData data)  //상세설명 중 내용이 포토존이나 도슨트일 경우
    {
        string a;
        a = $"포토존{data.Number()}";  //파일의 이름이 '포토존{POI넘버}'인 경우를 상정. 필요시 변경.
        DesBoxImage.enabled = true;
        DesBoxImage.color = new Color(1, 1, 1, 1);
        //상세설명 이미지 활성화
        DesBoxImage.texture = Resources.Load<Texture2D>($"Photoimage/{a}"); //이미지를 Resources 폴더에서 받아와 지정
    }

    public void desBoxEneableButNoImage()   //상세설명 중 포토존이나 도슨트가 아니라 띄울 필요가 없을 경우
    {
        DesBoxImage.enabled = true;
        DesBoxImage.color = new Color(1,1,1,0);
        //투명 이미지를 활성화하여 설명중 다른 버튼이 누르는 것을 방지
    }

    public  void desBoxDisable()    //상세설명 이미지를 비활성화
    {
        DesBoxImage.enabled = false;
    }

}
