using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button startProductionButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private Button redoButton;
    [SerializeField] private Text generationCounterText;
    [SerializeField] private Button terraformingUpButton;
    [SerializeField] private Button terraformingDownButton;
    [SerializeField] private Text terraformingCounterText;

    public FactoryView[] FactoryViews
    {
        get
        {
            return this.transform.GetComponentsInChildren<FactoryView>();
        }
    }

    public UnityEvent OnProductionButtonPressed => startProductionButton.onClick;

    public void Register(Game game, UnityAction onProductionButtonPressed)
    {
        this.startProductionButton.onClick.AddListener(onProductionButtonPressed);
        this.terraformingUpButton.onClick.AddListener(() => game.TerraformingValue++);
        this.terraformingDownButton.onClick.AddListener(() => game.TerraformingValue--);
        this.undoButton.onClick.AddListener(this.OnUndoButtonClicked);
        this.redoButton.onClick.AddListener(this.OnRedoButtonClicked);

        game.OnTerraformingValueChanged += this.OnTerraformingValueChanged;
        game.OnGenerationCounterChanged += this.OnGenerationCounterChanged;
    }

    public void Unregister(Game game)
    {
        this.terraformingUpButton.onClick.RemoveAllListeners();
        this.terraformingDownButton.onClick.RemoveAllListeners();
        this.undoButton.onClick.RemoveListener(this.OnUndoButtonClicked);
        this.redoButton.onClick.RemoveListener(this.OnRedoButtonClicked);

        game.OnTerraformingValueChanged -= this.OnTerraformingValueChanged;
        game.OnGenerationCounterChanged -= this.OnGenerationCounterChanged;
    }

    private void OnTerraformingValueChanged(int obj)
    {
        this.terraformingCounterText.text = obj.ToString();
    }

    private void OnGenerationCounterChanged(int obj)
    {
        this.generationCounterText.text = obj.ToString();
    }

    private void OnRedoButtonClicked()
    {
        throw new NotImplementedException();
    }

    private void OnUndoButtonClicked()
    {
        throw new NotImplementedException();
    }
}