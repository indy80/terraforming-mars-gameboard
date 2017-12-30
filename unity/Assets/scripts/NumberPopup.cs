using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NumberPopup : MonoBehaviour
{
    public static NumberPopup Instance => instance;
    private static NumberPopup instance;

    [SerializeField] private Transform numbersRoot;
    [SerializeField] private Text previewText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    private int currentNumber;
    private int maxNumber;
    private TaskCompletionSource<bool> taskCompletion;

    public void Awake()
    {
        instance = this;

        var buttons = this.numbersRoot.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            var digit = buttons.Length - i - 1;
            button.GetComponentInChildren<Text>().text = digit.ToString();
            button.onClick.AddListener(() => this.OnNumberPressed(digit));
        }

        this.okButton.onClick.AddListener(this.OnOkButtonPressed);
        this.cancelButton.onClick.AddListener(this.OnCancelButtonPressed);

        this.gameObject.SetActive(false);
    }
    
    private void OnNumberPressed(int number)
    {
        this.currentNumber *= 10;
        this.currentNumber += number;

        this.currentNumber = Math.Min(this.currentNumber, this.maxNumber);
        this.previewText.text = this.currentNumber.ToString();
    }

    private void OnOkButtonPressed()
    {
        this.taskCompletion.SetResult(true);
    }

    private void OnCancelButtonPressed()
    {
        this.taskCompletion.SetResult(false);
    }

    public async Task<int> Run(int maxNumber)
    {
        this.maxNumber = maxNumber;
        this.currentNumber = 0;
        this.previewText.text = this.currentNumber.ToString();

        this.taskCompletion = new TaskCompletionSource<bool>();

        this.gameObject.SetActive(true);

        await this.taskCompletion.Task;

        this.gameObject.SetActive(false);

        if (!this.taskCompletion.Task.Result)
        {
            return 0;
        }
        else
        {
            return this.currentNumber;
        }
    }
}
