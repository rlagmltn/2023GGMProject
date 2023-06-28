using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] mainTutos;
    [SerializeField] private GameObject[] trainingTutos;
    [SerializeField] private GameObject[] adventureTutos;

    public void ActiveNextMainTuto(int i=0)
    {
        mainTutos[i].SetActive(true);
        mainTutos[i - 1].SetActive(false);
    }

    public void FinalMainTuto()
    {
        SaveManager.Instance.TutoData.mainTuto = true;
        SaveManager.Instance.TutoDataSave();
        mainTutos[mainTutos.Length - 1].SetActive(false);
    }

    public void ActiveNextTrainingTuto(int i = 0)
    {
        trainingTutos[i].SetActive(true);
        trainingTutos[i - 1].SetActive(false);
    }

    public void FinalTrainingTuto()
    {
        SaveManager.Instance.TutoData.trainingTuto = true;
        SaveManager.Instance.TutoDataSave();
        trainingTutos[trainingTutos.Length - 1].SetActive(false);
    }

    public void ActiveNextAdventureTuto(int i = 0)
    {
        adventureTutos[i].SetActive(true);
        adventureTutos[i - 1].SetActive(false);
    }

    public void FinalAdventureTuto()
    {
        SaveManager.Instance.TutoData.adventrueTuto = true;
        SaveManager.Instance.TutoDataSave();
        adventureTutos[adventureTutos.Length - 1].SetActive(false);
    }
}
