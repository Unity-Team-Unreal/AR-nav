using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Type type;

    [Header("AR 체험존 UI요소")]
    public TMP_Text contentsname;
    public TMP_Text Description;
    public TMP_Text Guide;
    public Image image;


    void Start()
    {
        // DataManager에서 데이터 가져오기
        ContentsData contentsData = DataManager.Instance.contentsData;

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
