using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Contents_POI_Info : MonoBehaviour
{
    //�̱��� �ν��Ͻ�
    private static Contents_POI_Info instance;
    public static Contents_POI_Info Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<Contents_POI_Info>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<Contents_POI_Info>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }

    //POI ������ ��ũ
    [Header("������ ��ũ")]
    string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:F6"; // POI �����͸� �ҷ��� �� 
    string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:F6"; // POI �����͸� �ҷ��� �� 

    //gid=1564857787

    //������ �迭
    string[] rowsData;  // �� �����͸� �����ϴ� �迭
    string[] columnsData;   // �� �����͸� �����ϴ� �迭
    
    //�̹���
    [Header("������ �̹���")]
    public Texture2D[] Photozoneimage;
    public Texture2D[] Docentimage;

    //�̹��� ���
    [Header("������ �̹��� ���")]
    public string Photo_floder = "Photoimage";
    public string Docent_floder = "Docentimage";

    //POI ������
    [Header("POI ������")]
    public Contents_POI photozone;
    public Contents_POI Docent;

    //������ ������
    [Header("������ ���� ������Ʈ")]
    public GameObject contentsDataPfb; //POI ������

    //������ ���� ��ġ
    [Header("������ ���� ��ġ")]
    public Transform PhotoscrollViewContent; //������ ��ũ�� ���� Contents
    public Transform Docentscrolltransform; //����Ʈ ��ũ�� ���� Contents


    private void Awake()
    {
        gameObject.SetActive(true);

        //�̱��� ����
        var objs = FindObjectsOfType<Contents_POI_Info>();

        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        //�̹��� �ε�
        Photozoneimage = Resources.LoadAll<Texture2D>("Photoimage");
        Docentimage = Resources.LoadAll<Texture2D>("Docentimage");

        //������ �ҷ����� ����
        StartCoroutine(requestCoroutine(photozone, PhotozonewebURL, Photozoneimage, PhotoscrollViewContent)); // �ڷ�ƾ ���� 
        StartCoroutine(requestCoroutine(Docent, DocentPOIwebURL, Docentimage, Docentscrolltransform));

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    /// <summary>
    /// �ڷ�ƾ : �� ������ ��û �� ó��
    /// </summary>
    /// <param name="contentspoi">POI ������ ����ü</param>
    /// <param name="link">�� ������ ��ũ</param>
    /// <param name="texture2D">�̹��� �迭</param>
    /// <param name="transform">��ũ�� ��</param>
    /// <returns></returns>
    IEnumerator requestCoroutine(Contents_POI contentspoi, string link, Texture2D[] texture2D, Transform transform)
    {
        //�� ��û ����
        UnityWebRequest WebData = UnityWebRequest.Get(link); 

        //�� ��û ���� ���
        yield return WebData.SendWebRequest(); 


        // �� ���� ����� ���� ó��
        switch (WebData.result)
        {
            case UnityWebRequest.Result.Success: //����
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError: 
            case UnityWebRequest.Result.DataProcessingError:
                //����: ���� ó��
                Debug.LogError(WebData.error);
                break;
        }

        if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
        {
            POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform); // �� ��û�� ����� ���ڿ���    
        }
    }

    /// <summary>
    /// ������ �Ľ� �� ����
    /// </summary>
    /// <param name="tsv">������ ���ڿ�</param>
    /// <param name="poi">POI ������ ����ü</param>
    /// <param name="texture2D">�̹��� �迭</param>
    /// <param name="transform">��ũ�� ��</param>
    void POIDB(string tsv, Contents_POI poi, Texture2D[] texture2D, Transform transform)
    {
        // �� ������ �и�
        rowsData = tsv.Split('\n');

        // ������ Ȯ��
        int columnSize = rowsData[0].Split("\t").Length;

        poi.contentsdata = new ContentsData[rowsData.Length];
        
        //������ ��ȸ
        for (int i = 0; i < rowsData.Length; i++)
        {
            // �� ������ �и�
            columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

            // POI ������ ����
            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);

            for(int j = 0;  j < columnsData.Length; j++)
            {
                // ������ ���� �� �ʱ�ȭ
                ContentsData contentsData = new ContentsData();

                contentsData.number = columnsData[0];
                contentsData.contentsname = columnsData[1];
                contentsData.description = columnsData[2];
                contentsData.latitude = columnsData[3];
                contentsData.longitude = columnsData[4];
                contentsData.guide = columnsData[5];
                contentsData.Image = texture2D[i];

                poi.contentsdata[i] = contentsData;
            }
            

            poiPrefabsIntance.GetComponent<POIPrefabs>().Init(poi.contentsdata[i]);


            //���� �������� ��Ʈ ������ �����
            /*foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }*/
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���⼭ "My Scene"�� �� ������Ʈ�� Ȱ��ȭ�Ǿ�� �ϴ� ���� �̸��Դϴ�.
        // �� �̸��� �ڽ��� �� �̸����� �����ϼ���.
        if (scene.name == "1_3.AR Contents Page")
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}

