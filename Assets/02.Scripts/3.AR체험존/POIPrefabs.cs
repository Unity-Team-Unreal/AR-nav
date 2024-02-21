using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class POIPrefabs : MonoBehaviour
{
    public TMP_Text contentsNameText; // UI ���
    public TMP_Text descriptionText; // UI ���
    public Image poiImage; // UI ���

    private ContentsData _contentsData; // �������� ������ �ִ� ������

    public void Init(ContentsData contentsData)
    {
        _contentsData = contentsData;

        // UI ��ҿ� ������ �Ҵ�
        contentsNameText.text = _contentsData.contentsname;
        descriptionText.text = _contentsData.description;
        ChangeImage(poiImage, _contentsData.Image);
    }

    public void ChangeImage(Image imageComponent, Texture2D newTexture)
    {
        // Texture2D�� Sprite�� ��ȯ
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

        // Image ������Ʈ�� sprite�� ���ο� Sprite�� ����
        imageComponent.sprite = newSprite;
    }

    // ������ Ŭ�� �� �̺�Ʈ
    public void OnClick()
    {
        // Ŭ���� �����͸� DataManager�� ����
        DataManager.Instance.contentsData = _contentsData;

        Debug.Log("Ŭ����");
        SceneManager.LoadScene("3-1.AR ü���� ��������");
    }
}
