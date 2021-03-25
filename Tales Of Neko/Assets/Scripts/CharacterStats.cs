using Tales_of_Neko;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Text  StrengthText;
    public Text ConstitutionText;
    public Text DexterityText;
    public Text IntelligenceText;
    public Text WisdomText;
    public Text AvailablePoints;

    public GameObject StrengthPlus;
    public GameObject ConstitutionPlus;
    public GameObject DexterityPlus;
    public GameObject IntelligencePlus;
    public GameObject WisdomPlus;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();

    }

    private void UpdateStats()
    {
        Stats stats = GameManager.Instance.player.Stats;
        StrengthText.text = "Strength : " + stats.Strength;
        ConstitutionText.text = "Constitution : " + stats.Constitution;
        DexterityText.text = "Dexterity : " + stats.Dexterity;
        IntelligenceText.text = "Intelligence : " + stats.Intelligence;
        WisdomText.text = "Wisdom : " + stats.Wisdom;
        AvailablePoints.text = ""+GameManager.Instance.player.levelStatsUp;
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameManager.Instance.player;
        UpdateStats();
        if (player.levelStatsUp>0)
        {
            StrengthPlus.SetActive(true);
            ConstitutionPlus.SetActive(true);
            DexterityPlus.SetActive(true);
            IntelligencePlus.SetActive(true);
            WisdomPlus.SetActive(true);
        }
    }

    public void AddPointStrength()
    {
        Stats stats=new Stats();
        stats.Strength = 1;

        GameManager.Instance.player.Stats += stats;


        GameManager.Instance.player.levelStatsUp--;
        if (GameManager.Instance.player.levelStatsUp<=0)
        {
            StrengthPlus.SetActive(false);
            ConstitutionPlus.SetActive(false);
            DexterityPlus.SetActive(false);
            IntelligencePlus.SetActive(false);
            WisdomPlus.SetActive(false);
        }

    }
    public void AddPointConstitution()
    {
        Stats stats=new Stats();
        stats.Constitution = 1;

        GameManager.Instance.player.Stats += stats;
        GameManager.Instance.player.levelStatsUp--;
        
        if (GameManager.Instance.player.levelStatsUp<=0)
        {
            StrengthPlus.SetActive(false);
            ConstitutionPlus.SetActive(false);
            DexterityPlus.SetActive(false);
            IntelligencePlus.SetActive(false);
            WisdomPlus.SetActive(false);
        }
        
        
    }
    public void AddPointDexterity()
    {
        Stats stats=new Stats();
        stats.Dexterity = 1;

        GameManager.Instance.player.Stats += stats;
        GameManager.Instance.player.levelStatsUp--;
        
        if (GameManager.Instance.player.levelStatsUp<=0)
        {
            StrengthPlus.SetActive(false);
            ConstitutionPlus.SetActive(false);
            DexterityPlus.SetActive(false);
            IntelligencePlus.SetActive(false);
            WisdomPlus.SetActive(false);
        }
        
    }
    public void AddPointIntelligence()
    {
        Stats stats=new Stats();
        stats.Intelligence = 1;

        GameManager.Instance.player.Stats += stats;
        GameManager.Instance.player.levelStatsUp--;
        
        if (GameManager.Instance.player.levelStatsUp<=0)
        {
            StrengthPlus.SetActive(false);
            ConstitutionPlus.SetActive(false);
            DexterityPlus.SetActive(false);
            IntelligencePlus.SetActive(false);
            WisdomPlus.SetActive(false);
        }
        
        
    }
    public void AddPointWisdom()
    {
        Stats stats=new Stats();
        stats.Wisdom = 1;

        GameManager.Instance.player.Stats += stats;
        GameManager.Instance.player.levelStatsUp--;
        
        if (GameManager.Instance.player.levelStatsUp<=0)
        {
            StrengthPlus.SetActive(false);
            ConstitutionPlus.SetActive(false);
            DexterityPlus.SetActive(false);
            IntelligencePlus.SetActive(false);
            WisdomPlus.SetActive(false);
        }
        
    }
}
