using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class POIPrefabs : MonoBehaviour
{
    public TMP_Text contentsNameText; // UI 요소
    public TMP_Text descriptionText; // UI 요소
    public Image poiImage; // UI 요소

    private ContentsData _contentsData; // 프리팹이 가지고 있는 데이터

    public void Init(ContentsData contentsData)
    {
        _contentsData = contentsData;

        // UI 요소에 데이터 할당
        contentsNameText.text = _contentsData.contentsname;
        descriptionText.text = _contentsData.description;
        ChangeImage(poiImage, _contentsData.Image);
    }

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
        DataManager.Instance.contentsData = _contentsData;

        Debug.Log("클릭됨");
        SceneManager.LoadScene("3-1.AR 체험존 상세페이지");
    }
}
