using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ARcontents Page UI ��� ����
/// </summary>
public class ContentsDetaileUImanager : MonoBehaviour
{
    //contents POI Ÿ��
    [SerializeField] private Type type;

    [Header("AR ü���� UI���")]
    [SerializeField] private TMP_Text contentsname;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Guide;
    [SerializeField] private Image image;

    void Start()
    {
        // DataManager���� ������ ��������
        ContentsData contentsData = ContentsDataManager.Instance.contentsData;

        // �����͸� ȭ�鿡 ǥ��
        if (contentsData != null)
        {
            contentsname.text = contentsData.contentsname;
            Description.text = contentsData.description;
            Guide.text = contentsData.guide;
            ChangeImage(image, contentsData.Image);
        }
        else
        {
            Debug.LogError("ContentsData�� null�Դϴ�.");
        }
    }

    /// <summary>
    /// �̹��� ��ȯ �ڵ�
    /// </summary>
    /// <param name="imageComponent">������ �̹��� ������Ʈ</param>
    /// <param name="newTexture">���ο� �ؽ���</param>
    public void ChangeImage(Image imageComponent, Texture2D newTexture)
    {
        if (imageComponent != null && newTexture != null)
        {
            // Texture2D�� Sprite�� ��ȯ
            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

            // Image ������Ʈ�� sprite�� ���ο� Sprite�� ����
            imageComponent.sprite = newSprite;

            // Preserve Aspect�� Ȱ��ȭ
            imageComponent.preserveAspect = true;
        }
        else
        {
            Debug.LogError("Image ������Ʈ �Ǵ� Texture2D�� null�Դϴ�.");
        }
    }
}
