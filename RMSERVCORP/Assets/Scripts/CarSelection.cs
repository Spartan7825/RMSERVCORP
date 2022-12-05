using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [Header ("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Buy Button")]
    [SerializeField] private Button buy;
    [SerializeField] private Button Select;
    [SerializeField] private Text priceText;

    [Header("car Attributes")]
    [SerializeField] private int[] carPrices;
    private int currentCar;

    private void Start()
    {
        currentCar = SaveManager.instance.currentCar;
        SelectCar(currentCar);
    }

    private void SelectCar(int _index) 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
            if (i == _index) 
            {
                transform.GetChild(i).GetComponent<BuddyColorController>().changeMaterial();
                transform.GetChild(i).gameObject.transform.GetChild(30).GetComponent<FTruckColorController>().changeMaterial();
            }
        }

        
        UpdateUI();
    }

    private void UpdateUI() 
    {
        if (SaveManager.instance.carsUnlocked[currentCar])
        {
            Select.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        else
        {
            Select.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = carPrices[currentCar] + " Brownie Points";


        }
    }

    private void Update()
    {
        //Check if we have enough money
        if (buy.gameObject.activeInHierarchy) 
        {
            buy.interactable = (SaveManager.instance.money >= carPrices[currentCar]);
        }
    }

    public void ChangeCar(int _change) 
    {
        currentCar += _change;

        if (currentCar > transform.childCount - 1)
            currentCar = 0;
        else if (currentCar < 0)
            currentCar = transform.childCount - 1;

        SelectCar(currentCar);
    }

    public void BuyCar() 
    {
        SaveManager.instance.money -= carPrices[currentCar];
        SaveManager.instance.carsUnlocked[currentCar] = true;
        SaveManager.instance.Save();
        UpdateUI();
    }

    public void Thefunction() 
    {
        SaveManager.instance.currentCar = currentCar;
        SaveManager.instance.Save();
    }

    public void Thefunctionthatupdatesselectandbuybuttons() 
    {
        currentCar = SaveManager.instance.currentCar;
        SelectCar(currentCar);
    }
}

