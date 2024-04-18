using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MainHUD
{
    Material,
    Recipe,
    Money
}

public class MainCharacterHUDManager : MonoBehaviour
{
    [SerializeField] private GameObject moneyHUD;
    [SerializeField] private GameObject materialList;
    [SerializeField] private GameObject hpHorizontal;
    [SerializeField] private Image hpIcon;
    [SerializeField] private Image reputationIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI dayCountText;
    [SerializeField] private TextMeshProUGUI repuScoreRatioText;
    [SerializeField] private Image reputationBar;
    [SerializeField] private List<Sprite> plaqueSpriteList = new List<Sprite>();

    [SerializeField] private PlayerCombat _playerCombat;

    private List<Image> hpIconList = new List<Image>();

    private int currentHp;

    private void Start()
    {
        _playerCombat = FindAnyObjectByType<PlayerCombat>();
        
        _playerCombat.onHpChange += OnHPValueChange;
        hpIcon.gameObject.SetActive(false);
        currentHp = PlayerStats.instance.maxHp;
        for (int i = 0; i < PlayerStats.instance.maxHp; i++)
        {
            Image newHpIcon = Instantiate(hpIcon, hpHorizontal.transform);
            newHpIcon.gameObject.SetActive(true);
            hpIconList.Add(newHpIcon);
        }
    }

    private void Update()
    {
        timerText.text = TimeManager.instance.GetTimerText();
        dayCountText.text = "Day " + TimeManager.instance.DayCount.ToString();
        repuScoreRatioText.text = ReputationManager.instance.GetReputationPoint.ToString() + "/" + ReputationManager.instance.GetRequireCurrentRankReputationAmount().ToString();
        reputationBar.fillAmount = ReputationManager.instance.GetReputationPoint / 1050f;
        reputationIcon.sprite = plaqueSpriteList[ReputationManager.instance.currentRank];
    }

    public void OnHPValueChange(int remainingHP)
    {
        for (int i = 0; i < hpIconList.Count; i++)
        {
            if(i < remainingHP)
            {
                hpIconList[i].gameObject.SetActive(true);
            }
            else
            {
                hpIconList[i].gameObject.SetActive(false);
            }
            
        }
    }

    public void ToggleRecipeListHUD(bool isOpen)
    {
        moneyHUD.SetActive(!isOpen);
    }
}