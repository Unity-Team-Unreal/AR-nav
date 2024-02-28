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
    private static Contents_POI_Info instance;
    public static Contents_POI_Info Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindAnyObjectByType<Contents_POI_Info>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    return null;
                }
            }
            return instance;
        }
    } 


   
    [Header("POI ������ ��ũ")]
    [SerializeField] private string PhotozonewebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A2:F6"; // ������ POI �����͸�ũ
    [SerializeField] private string DocentPOIwebURL = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&gid=1564857787&range=A2:F6"; // ����Ʈ POI �����͸�ũ

    //������ �迭
    [SerializeField] private string[] rowsData;  // �� �����͸� �����ϴ� �迭
    [SerializeField] private string[] columnsData;   // �� �����͸� �����ϴ� �迭
       
    [Header("������ �̹��� �迭")]
    [SerializeField] private Texture2D[] Photozoneimage; 
    [SerializeField] private Texture2D[] Docentimage;

    [Header("������ �̹��� ���")]
    [SerializeField] private string Photo_floder = "Photoimage";
    [SerializeField] private string Docent_floder = "Docentimage";

    [Header("POI ������")]
    [SerializeField] private Contents_POI photozoneData;
    [SerializeField] private Contents_POI DocentData;

    [Header("������ ���� ������Ʈ")]
    [SerializeField] private GameObject contentsDataPfb;

    [Header("������ ���� ��ġ")]
    [SerializeField] private Transform Photoscrolltransform;
    [SerializeField] private Transform Docentscrolltransform;


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
        Photozoneimage = Resources.LoadAll<Texture2D>(Photo_floder);
        Docentimage = Resources.LoadAll<Texture2D>(Docent_floder);

        //������ �ҷ����� ����
        StartCoroutine(requestCoroutine(photozoneData, PhotozonewebURL, Photozoneimage, Photoscrolltransform)); // �ڷ�ƾ ���� 
        StartCoroutine(requestCoroutine(DocentData, DocentPOIwebURL, Docentimage, Docentscrolltransform));

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
            case UnityWebRequest.Result.Success:
                //���� : ������ ó��
                if (WebData.isDone) // �� ��û�� �Ϸ�Ǿ����� Ȯ��
                {
                    POIDB(WebData.downloadHandler.text, contentspoi, texture2D, transform);
                }
                break;
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.ProtocolError: 
            case UnityWebRequest.Result.DataProcessingError:
                //���� : ���� ó��
                Debug.LogError(WebData.error);
                break;
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
        
        // POI ������ ����ü �Ҵ�
        poi.contentsdata = new ContentsData[rowsData.Length];
        

        for (int i = 0; i < rowsData.Length; i++)
        {
            // �� ������ �и�
            columnsData = rowsData[i].Split('\t'); // �� �����͸� ��('/t') �������� ������ �� �����ͷ� ����

            // POI ������ ����
            GameObject poiPrefabsIntance = Instantiate(contentsDataPfb, transform);


            for(int j = 0;  j < columnsData.Length; j++)
            {
                //ContentsData�� �������� ����
                ContentsData contentsData = new ContentsData();

                contentsData.number = columnsData[0];
                contentsData.contentsname = columnsData[1];
                contentsData.description = columnsData[2];
                contentsData.latitude = columnsData[3];
                contentsData.longitude = columnsData[4];
                contentsData.guide = columnsData[5];
                contentsData.Image = texture2D[i];

                //poi.contentsdata �迭�� �߰�
                poi.contentsdata[i] = contentsData;
            }
            
            // POI ������ �ʱ�ȭ
            poiPrefabsIntance.GetComponent<POIPrefabs>().Init(poi.contentsdata[i]);


            //���� �������� ��Ʈ ������ �����
            /*foreach (var column in columnsData)
            {
                Debug.Log("line: " + i + ": " + column);
            }*/
        }
    }

    /// <summary>
    /// �� �ε� �� ó��
    /// </summary>
    /// <param name="scene">�ε��� �� ����</param>
    /// <param name="mode">�� �ε� ����</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //�� �̸��� ���� ���� ������Ʈ Ȱ��ȭ ���� ����
        if (scene.name == "1_3.AR_Contents_Page")
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}

