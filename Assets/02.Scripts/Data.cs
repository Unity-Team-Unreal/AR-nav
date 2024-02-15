using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using UnityEngine.Networking;

public class Data : MonoBehaviour
{
    //1. �����ؿ� �ּҿ��� ���� �մ� "edit?usp=sharing" ����
    //2. ���� �� �ּҿ� ������ �߰��Ѵ� "exprot?format=tsv&range=A2:E6"

    string sheetData;
    public TextMeshProUGUI displayTexty;
    const string ARcontentsurl = "https://docs.google.com/spreadsheets/d/1IvrflSuhz0SyUppKPaiRQAeIQcxmbsudvWExyd3tOec/export?format=tsv&range=A1:E6";


    IEnumerator Start()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(ARcontentsurl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) // �� ��û�� ���������� �Ϸ�Ǿ����� Ȯ��
            {
                sheetData = www.downloadHandler.text;
            }
            else
            {
                Debug.Log("Web request failed: " + www.result); // �� ��û�� ������ ���, ���� ������ ���
            }
        }

        DisplayText();
    }


    private void DisplayText()
    {
        string[] rows = sheetData.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);  // Split by newline and remove empty entries
        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');
            for (int j = 0; j < columns.Length; j++)
            {
                displayTexty.text += columns[j];
                if (j < columns.Length - 1)  // Only add a tab character if this is not the last column
                {
                    displayTexty.text += "\t";
                }
            }
            displayTexty.text += "\n";
        }

        Debug.Log(displayTexty.text);
    }





}
