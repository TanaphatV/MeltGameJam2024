using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTETemperaturePanel : MonoBehaviour
{
    [SerializeField] private Button successButton;
    [SerializeField] private Button failButton;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject gfx;

    private ProcessingStation station;

    public IEnumerator InitQTEProcess(ProcessingStation _ps)
    {
        station = _ps;

        successButton.onClick.AddListener(SuccessCreatingWeapon);
        failButton.onClick.AddListener(FailCreatingWeapon);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        Debug.Log("QTETempProcessing");
        
        yield return new WaitForSeconds(2.0f);

        yield return new WaitForEndOfFrame();

    }

    private IEnumerator SuccessSequence()
    {
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Success!";
        station.currentItemCraftingStatus = CraftingStatus.Completed;

        yield return new WaitForSeconds(2.0f);

        gfx.SetActive(false);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SuccessCreatingWeapon()
    {
        StartCoroutine(SuccessSequence());
    }

    public void FailCreatingWeapon()
    {
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Fail!";
        station.currentItemCraftingStatus = CraftingStatus.Failed;
    }
}
