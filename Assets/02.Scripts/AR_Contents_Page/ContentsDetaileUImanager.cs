using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ARcontents Page UI 요소 연결
/// </summary>
public class ContentsDetaileUImanager : MonoBehaviour
{
    //contents POI 타입
    [SerializeField] private Type type;

    [Header("AR 체험존 UI요소")]
    [SerializeField] private TMP_Text contentsname;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Guide;
    [SerializeField] private Image image;

    void Start()
    {
        // DataManager에서 데이터 가져오기
        ContentsData contentsData = ContentsDataManager.Instance.contentsData;

        // 데이터를 화면에 표시
        if (contentsData != null)
        {
            contentsname.text = contentsData.contentsname;
            Description.text = contentsData.description;
            Guide.text = contentsData.guide;
            ChangeImage(image, contentsData.Image);
        }
        else
        {
            Debug.LogError("ContentsData가 null입니다.");
        }
    }

    /// <summary>
    /// 이미지 변환 코드
    /// </summary>
    /// <param name="imageComponent">변경할 이미지 컴포넌트</param>
    /// <param name="newTexture">새로운 텍스쳐</param>
    public void ChangeImage(Image imageComponent, Texture2D newTexture)
    {
        if (imageComponent != null && newTexture != null)
        {
            // Texture2D를 Sprite로 변환
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

            // Image 컴포넌트의 sprite를 새로운 Sprite로 변경
            imageComponent.sprite = newSprite;

            // Preserve Aspect를 활성화
            imageComponent.preserveAspect = true;
        }
        else
        {
            Debug.LogError("Image 컴포넌트 또는 Texture2D가 null입니다.");
        }
    }
}
