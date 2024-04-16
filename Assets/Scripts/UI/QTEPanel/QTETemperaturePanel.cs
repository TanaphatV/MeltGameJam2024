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

    [SerializeField] private GameObject greatCellSegment;
    [SerializeField] private GameObject softCellSegment;
    [SerializeField] private GameObject missCellSegment;
    [SerializeField] private GameObject horizontalLayout;

    [SerializeField] private int greatCellCount = 3;
    [SerializeField] private int softCellCount = 2;
    private bool isStartMinigame = false;
    private float stickMoveSpeed = 1.0f;
    private List<GameObject> cellList = new List<GameObject>();
    private int randomCellArea;



    private ProcessingStation station;

    public IEnumerator InitQTEProcess(ProcessingStation _ps)
    {
        station = _ps;
        isStartMinigame = false;
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        Debug.Log("QTETempProcessing");

        GenerateCell(greatCellCount, softCellCount);

        yield return StartCoroutine(StickMove(-215, 215));
        yield return new WaitForSeconds(1.0f);
        isStartMinigame = true;
        yield return new WaitForEndOfFrame();

    }

    public void GenerateCell(int greatCellCount, int softCellCount)
    {
        foreach (GameObject cell in cellList)
        {
            Destroy(cell.gameObject);
        }
        cellList.Clear();

        randomCellArea = Random.Range(5, 24);
        for (int i = 0; i < 29; i++)
        {
            if(i == randomCellArea && cellList.Count < 29)
            {
                for (int j = 0; j < softCellCount; j++)
                {
                    GameObject newSoftCell = Instantiate(softCellSegment, horizontalLayout.transform);
                    cellList.Add(newSoftCell);
                }
                for (int k= 0; k < greatCellCount; k++)
                {
                    GameObject newGreatCell = Instantiate(greatCellSegment, horizontalLayout.transform);
                    cellList.Add(newGreatCell);
                }
                for (int l = 0; l < softCellCount; l++)
                {
                    if(cellList.Count < 29)
                    {
                        GameObject newSoftCell = Instantiate(softCellSegment, horizontalLayout.transform);
                        cellList.Add(newSoftCell);
                    }
                }
            }
            else if(cellList.Count < 29)
            {
                GameObject newCell = Instantiate(missCellSegment, horizontalLayout.transform);
                cellList.Add(newCell);
            }
        }

        foreach (var item in cellList)
        {
            item.gameObject.SetActive(true);
        }

    }

    public void CheckStickGrade(int grade)
    {
        if(grade > randomCellArea*15 && grade < (randomCellArea+ (softCellCount*2)+greatCellCount)*15)
        {
            if(grade > (randomCellArea+softCellCount)*15 && grade < (randomCellArea + softCellCount + greatCellCount + 1) * 15)
            {
                station.currentItemCraftingStatus = CraftingStatus.Completed;
                Debug.Log("Perfect!");
                StartCoroutine(SuccessSequence());
            }
            else
            {
                station.currentItemCraftingStatus = CraftingStatus.Completed;
                StartCoroutine(SuccessSequence());
                Debug.Log("Good");
            }
        }
        else
        {
            station.currentItemCraftingStatus = CraftingStatus.Failed;
            StartCoroutine(FailSequence());
            Debug.Log("Fail");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            CheckStickGrade(Mathf.FloorToInt(movingStick.GetComponent<Image>().rectTransform.anchoredPosition.x));
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

    private IEnumerator FailSequence()
    {
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        resultPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Fail!";
        station.currentItemCraftingStatus = CraftingStatus.Failed;

        yield return new WaitForSeconds(2.0f);

        gfx.SetActive(false);
        resultPanel.SetActive(false);
        gameObject.SetActive(false);
    }


    public void SuccessCreatingWeapon()
    {
        StartCoroutine(SuccessSequence());
    }

    private IEnumerator StickMove(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < stickMoveSpeed) // Remove the semicolon here
        {
            float t = elapsedTime / stickMoveSpeed; // Normalize elapsedTime to be between 0 and 1
            float posX = Mathf.Lerp(start, end, t);
            movingStick.GetComponent<Image>().rectTransform.localPosition = 
                new Vector3(posX, movingStick.GetComponent<Image>().rectTransform.localPosition.y, 
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        movingStick.GetComponent<Image>().rectTransform.localPosition =
                new Vector3(end, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
        yield return StickMoveBack(215, -215);
    }

    private IEnumerator StickMoveBack(float start, float end)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < stickMoveSpeed) // Remove the semicolon here
        {
            float t = elapsedTime / stickMoveSpeed; // Normalize elapsedTime to be between 0 and 1
            float posX = Mathf.Lerp(start, end, t);
            movingStick.GetComponent<Image>().rectTransform.localPosition =
                new Vector3(posX, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        movingStick.GetComponent<Image>().rectTransform.localPosition =
                new Vector3(end, movingStick.GetComponent<Image>().rectTransform.localPosition.y,
                movingStick.GetComponent<Image>().rectTransform.localPosition.z);
        yield return StickMove(-215, 215);
    }
}
