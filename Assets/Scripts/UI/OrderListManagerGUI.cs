using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderListManagerGUI : MonoBehaviour
{
    [SerializeField] GameObject verticalLayout;
    [SerializeField] GameObject gfx;
    [SerializeField] OrderSocketGUI orderSocketTemplate;
    List<OrderSocketGUI> orderSocketList = new List<OrderSocketGUI>();
    [SerializeField] private Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        gfx.SetActive(false);
        orderSocketTemplate.gameObject.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }
    public void OpenPanel()
    {
        gfx.SetActive(true);
    }
    public void ClosePanel()
    {
        gfx.SetActive(false);
    }

    public void InitPanel(List<SpecialOrder> specialOrderList)
    {
        int difference = specialOrderList.Count - orderSocketList.Count;
        Debug.Log(difference);
        if(difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                orderSocketList.Add(Instantiate(orderSocketTemplate, verticalLayout.transform));
            }
        }
        for(int i = 0; i < orderSocketList.Count; i++)
        {
            if (i < specialOrderList.Count)
            {
                Debug.Log("INIT");
                orderSocketList[i].InitSocket(specialOrderList[i]);
                orderSocketList[i].gameObject.SetActive(true);
            }
            else
                orderSocketList[i].gameObject.SetActive(false);
        }
    }
}
