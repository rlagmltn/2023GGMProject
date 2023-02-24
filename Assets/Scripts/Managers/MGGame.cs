using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGGame : MonoBehaviour
{
    // public MGTeam gTeamManager;
    // public MGStage gStageManager;

    void Awake()
    {
        GameSceneClass.gMGGame = this;

        InitCamera();

        // gTeamManager = new MGTeam();
        // gStageManager = new MGStage();

        Global.gameState = eGameState.Playing;
    }

    private void Update()
    {
        if (Global.gameState == eGameState.Playing)
        {
            // 각 기능 클래스들의 업데이트를 여기 집중관리해줌
        }
    }

    void InitCamera()
    {
        Global.gMainCamTrm = FindObjectOfType<Camera>().transform;

        if (Global.gMainCamTrm == null)
        {
            Global.gMainCamTrm = ((GameObject.Instantiate(Global.prefabsDic[ePrefabs.MainCamera])) as GameObject).transform;
        }

        if (Global.gMainCamTrm != null) //카메라 2개면 오류생길듯?
        {
            Global.mainCam = Global.gMainCamTrm.GetComponent<Camera>();
            if (Global.mainCam == null)
            {
                Debug.LogWarning("Global.mainCam in null");
                return;
            }
        }
    }
}
