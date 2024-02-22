using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class POIPrefabs : MonoBehaviour
{
    public TMP_Text contentsNameText; // UI ���
    public TMP_Text descriptionText; // UI ���
    public Image poiImage; // UI ���
    private ContentsData _contentsData; // �������� ������ �ִ� ������
    
    


    public void Init(ContentsData contentsData)
    {
        _contentsData = contentsData;
        //Type = _contentsPOI.type;

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
        
        if (_contentsData.number == 2)
        {
            SceneManager.LoadScene("3_1.AR Contents Detail Page");
        }
        else
        {
            SceneManager.LoadScene("3_2.AR Contents Detail Page");
        }

    }
}
