using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesImageScript : MonoBehaviour
{
    /// <summary>
    /// �󼼼��� �̹����� ���� ��ũ��Ʈ
    /// </summary>


     RawImage DesBoxImage;  //�󼼼��� �̹���


    void Awake()
    {
        DesBoxImage = GetComponent<RawImage>();

        DesBoxImage.enabled = false;    //���۽� ��Ȱ��ȭ
    }

   public void desBoxEneable(POIData data)  //�󼼼��� �� ������ �������̳� ����Ʈ�� ���
    {
        string a;
        a = $"������{data.Number()}";  //������ �̸��� '������{POI�ѹ�}'�� ��츦 ����. �ʿ�� ����.
        DesBoxImage.enabled = true;
        DesBoxImage.color = new Color(1, 1, 1, 1);
        //�󼼼��� �̹��� Ȱ��ȭ
        DesBoxImage.texture = Resources.Load<Texture2D>($"Photoimage/{a}"); //�̹����� Resources �������� �޾ƿ� ����
    }

    public void desBoxEneableButNoImage()   //�󼼼��� �� �������̳� ����Ʈ�� �ƴ϶� ��� �ʿ䰡 ���� ���
    {
        DesBoxImage.enabled = true;
        DesBoxImage.color = new Color(1,1,1,0);
        //���� �̹����� Ȱ��ȭ�Ͽ� ������ �ٸ� ��ư�� ������ ���� ����
    }

    public  void desBoxDisable()    //�󼼼��� �̹����� ��Ȱ��ȭ
    {
        DesBoxImage.enabled = false;
    }

}
