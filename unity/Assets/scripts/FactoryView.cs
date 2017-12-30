using System;
using UnityEngine;
using UnityEngine.UI;

public class FactoryView : MonoBehaviour
{
    [SerializeField] public FactoryConfig factoryConfig;
    [SerializeField] private Text levelText;
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button levelDownButton;
    [SerializeField] private Text stockText;
    [SerializeField] private Button stockUpButton;
    [SerializeField] private Button stockDownButton;
    [SerializeField] private Button stockUpXButton;
    [SerializeField] private Button stockDownXButton;

    public void Register(Factory factory)
    {
        factory.OnLevelChanged += this.OnLevelChanged;
        factory.OnStockChanged += this.OnStockChanged;

        this.levelUpButton.onClick.AddListener(() => factory.Level++);
        this.levelDownButton.onClick.AddListener(() => factory.Level--);

        this.stockUpButton.onClick.AddListener(() => factory.Stock++);
        this.stockDownButton.onClick.AddListener(() => factory.Stock--);

        this.stockUpXButton.onClick.AddListener(() => this.OnStockUpXButtonPressed(factory));
        this.stockDownXButton.onClick.AddListener(() => this.OnStockDownXButtonPressed(factory));
    }

    public void Unregister(Factory factory)
    {
        factory.OnLevelChanged -= this.OnLevelChanged;
        factory.OnStockChanged -= this.OnStockChanged;

        this.levelUpButton.onClick.RemoveAllListeners();
        this.levelUpButton.onClick.RemoveAllListeners();
        this.stockUpButton.onClick.RemoveAllListeners();
        this.stockDownButton.onClick.RemoveAllListeners();
        this.stockUpXButton.onClick.RemoveAllListeners();
        this.stockDownXButton.onClick.RemoveAllListeners();
    }

    private async void OnStockUpXButtonPressed(Factory factory)
    {
        int number = await NumberPopup.Instance.Run(int.MaxValue);
        factory.Stock += number;
    }

    private async void OnStockDownXButtonPressed(Factory factory)
    {
        int number = await NumberPopup.Instance.Run(factory.Stock);
        factory.Stock -= number;
    }

    private void OnStockChanged(int obj)
    {
        this.stockText.text = obj.ToString();
    }

    private void OnLevelChanged(int obj)
    {
        this.levelText.text = obj.ToString();
    }


}
