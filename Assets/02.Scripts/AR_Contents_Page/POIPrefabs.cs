using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// POI ������ ��ũ��Ʈ
/// </summary>
public class POIPrefabs : MonoBehaviour
{
    // UI ���
    public TMP_Text contentsNameText; 
    public TMP_Text descriptionText; 
    public Image poiImage;

    // �������� ������ �ִ� ������
    private ContentsData _contentsData; 
    

    // �ʱ�ȭ �Լ�
    public void Init(ContentsData contentsData)
    {
        _contentsData = contentsData;

        // UI ��ҿ� ������ �Ҵ�
        contentsNameText.text = _contentsData.contentsname;
        descriptionText.text = _contentsData.description;

        // �̹��� ���� �Լ� ȣ��
        ChangeImage(poiImage, _contentsData.Image);
    }

    // �̹��� ���� �Լ�
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
        ContentsDataManager.Instance.contentsData = _contentsData;

        // �� �ε�
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
