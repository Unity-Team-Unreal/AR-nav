using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    POIPrefabs poiprefabs;

    [Header("AR ü���� UI���")]
    public TMP_Text contentsname;
    public TMP_Text Description;
    public TMP_Text Guide;
    public Image image;

    void Start()
    {
        // DataManager���� ������ ��������
        ContentsData contentsData = DataManager.Instance.contentsData;

        // �����͸� ȭ�鿡 ǥ��
        contentsname.text = contentsData.contentsname;
        Description.text = contentsData.description;
        Guide.text = contentsData.guide;
        ChangeImage(image, contentsData.Image);
    }

    public void ChangeImage(Image imageComponent, Texture2D newTexture)
    {
        // Texture2D�� Sprite�� ��ȯ
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

        // Image ������Ʈ�� sprite�� ���ο� Sprite�� ����
        imageComponent.sprite = newSprite;
    }



}
