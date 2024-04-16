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
    [SerializeField] private GameObject movingStick;
    private bool isStartMinigame = false;

    private ProcessingStation station;

    public IEnumerator InitQTEProcess(ProcessingStation _ps)
    {
        station = _ps;
        isStartMinigame = false;
        successButton.onClick.AddListener(SuccessCreatingWeapon);
        failButton.onClick.AddListener(FailCreatingWeapon);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        Debug.Log("QTETempProcessing");
        
        

        yield return StartCoroutine(StickMove(-215, 215));
        yield return new WaitForSeconds(1.0f);
        isStartMinigame = true;



        yield return new WaitForEndOfFrame();

    }

    private void Update()
    {
        if (isStartMinigame)
        {
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Wow");
        }
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
    private IEnumerator StickMove(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.7f) // Remove the semicolon here
        {
            float t = elapsedTime / 0.7f; // Normalize elapsedTime to be between 0 and 1
            float posX = Mathf.Lerp(start, end, t);
            movingStick.GetComponent<Image>().rectTransform.localPosition = 
                new Vector3(posX, movingStick.GetComponent<Image>().rectTransform.localPosition.y, 
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //movingStick.GetComponent<Image>().rectTransform.localPosition =
        //        new Vector3(end, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
        //        movingStick.GetComponent<Image>().rectTransform.localPosition.z);
        yield return StickMoveBack(215, -215);
    }

    private IEnumerator StickMoveBack(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.7f) // Remove the semicolon here
        {
            float t = elapsedTime / 0.7f; // Normalize elapsedTime to be between 0 and 1
            float posX = Mathf.Lerp(start, end, t);
            movingStick.GetComponent<Image>().rectTransform.localPosition =
                new Vector3(posX, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //movingStick.GetComponent<Image>().rectTransform.localPosition =
        //        new Vector3(end, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
        //        movingStick.GetComponent<Image>().rectTransform.localPosition.z);
        yield return StickMove(-215, 215);
    }
}
