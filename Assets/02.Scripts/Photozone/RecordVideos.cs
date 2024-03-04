using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;
#endif


[RequireComponent(typeof(ARSession))]
public class RecordVideos : MonoBehaviour
{
    [SerializeField] private Button StartRecordBtn;
    [SerializeField] private Button EndRecordBtn;

    ARSession m_Session;

#if UNITY_ANDROID
    ArStatus? m_SetMp4DatasetResult;
    ArPlaybackStatus m_PlaybackStatus = (ArPlaybackStatus)(-1);
    ArRecordingStatus m_RecordingStatus = (ArRecordingStatus)(-1);
#endif

    string m_Mp4Path;

    void Awake()
    {
        m_Session = GetComponent<ARSession>();
    }

    //ȸ�� ���� ��������
    static int GetRotation() => Screen.orientation switch
    {
        ScreenOrientation.Portrait => 0,
        ScreenOrientation.LandscapeLeft => 90,
        ScreenOrientation.PortraitUpsideDown => 180,
        ScreenOrientation.LandscapeRight => 270,
        _ => 0
    };

    public void StartRecordingSession()
    {
        StartRecordBtn.gameObject.SetActive(false);
        EndRecordBtn.gameObject.SetActive(true);

#if UNITY_ANDROID
        if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
        {
            //����, ��� ����, ��ȭ����
            var session = subsystem.session;
            if (session == null)
                return;

            var playbackStatus = subsystem.playbackStatus;
            var recordingStatus = subsystem.recordingStatus;

            //����̳� ��ȭ�� ���� ������ ���� ���
            if (!playbackStatus.Playing() && !recordingStatus.Recording())
            {
                //ARRecordingConfig ��ü ����
                using (var config = new ArRecordingConfig(session))
                {
                    //������ �̸�, ��� ����
                    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "ar-video.mp4";
                    m_Mp4Path = Path.Combine("/storage/emulated/0/DCIM/AR-Nav", fileName);
                    config.SetMp4DatasetFilePath(session, m_Mp4Path);

                    //ȸ�� ����
                    config.SetRecordingRotation(session, GetRotation());

                    //��ȭ ����
                    subsystem.StartRecording(config);
                }
            }
        }
#endif
    }

    public void StopRecordingSession()
    {
        StartRecordBtn.gameObject.SetActive(true);
        EndRecordBtn.gameObject.SetActive(false);

#if UNITY_ANDROID
        if (m_Session.subsystem is ARCoreSessionSubsystem subsystem)
        {
            //��ȭ ����
            var recordingStatus = subsystem.recordingStatus;
            
            //��ȭ ���̸� ��ȭ ����
            if (recordingStatus.Recording())
            {
                subsystem.StopRecording();
                int[] poi = new int[1];
            }
        }
#endif
    }
}