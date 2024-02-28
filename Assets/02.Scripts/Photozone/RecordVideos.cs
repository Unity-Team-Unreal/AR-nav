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

    //회전 각도 가져오기
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
            //세션, 재생 상태, 녹화상태
            var session = subsystem.session;
            if (session == null)
                return;

            var playbackStatus = subsystem.playbackStatus;
            var recordingStatus = subsystem.recordingStatus;

            //재생이나 녹화가 진행 중이지 않은 경우
            if (!playbackStatus.Playing() && !recordingStatus.Recording())
            {
                //ARRecordingConfig 객체 생성
                using (var config = new ArRecordingConfig(session))
                {
                    //동영상 이름, 경로 설정
                    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "ar-video.mp4";
                    m_Mp4Path = Path.Combine("/storage/emulated/0/DCIM/AR-Nav", fileName);
                    config.SetMp4DatasetFilePath(session, m_Mp4Path);

                    //회전 설정
                    config.SetRecordingRotation(session, GetRotation());

                    //녹화 시작
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
            //녹화 상태
            var recordingStatus = subsystem.recordingStatus;
            
            //녹화 중이면 녹화 종료
            if (recordingStatus.Recording())
            {
                subsystem.StopRecording();
                int[] poi = new int[1];
            }
        }
#endif
    }
}
