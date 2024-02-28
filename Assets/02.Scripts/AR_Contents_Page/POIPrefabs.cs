using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// POI 프리팹 스크립트
/// </summary>
public class POIPrefabs : MonoBehaviour
{
    // UI 요소
    public TMP_Text contentsNameText; 
    public TMP_Text descriptionText; 
    public Image poiImage;

    // 프리팹이 가지고 있는 데이터
    private ContentsData _contentsData; 
    

    // 초기화 함수
    public void Init(ContentsData contentsData)
    {
        _contentsData = contentsData;

        // UI 요소에 데이터 할당
        contentsNameText.text = _contentsData.contentsname;
        descriptionText.text = _contentsData.description;

        // 이미지 변경 함수 호출
        ChangeImage(poiImage, _contentsData.Image);
    }

    // 이미지 변경 함수
    public void ChangeImage(Image imageComponent, Texture2D newTexture)
    {
        // Texture2D를 Sprite로 변환
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

        // Image 컴포넌트의 sprite를 새로운 Sprite로 변경
        imageComponent.sprite = newSprite;
    }

    // 프리팹 클릭 시 이벤트
    public void OnClick()
    { 
        // 클릭된 데이터를 DataManager에 저장
        ContentsDataManager.Instance.contentsData = _contentsData;

        // 씬 로딩
        if (int.Parse(_contentsData.number) == 2)
        {
            SceneManager.LoadScene("3_1.AR_Contents_Detail_Page");
        }
        else
        {
            SceneManager.LoadScene("3_2.AR_Contents_Detail_Page");
        }
    }
}
