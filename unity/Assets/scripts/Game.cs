using System;
using System.Collections.Generic;

public class Game
{
    private readonly GameView gameView;

    public Action<int> OnTerraformingValueChanged;
    public Action<int> OnGenerationCounterChanged;

    public int TerraformingValue
    {
        get
        {
            return this.terraformingValue;
        }

        set
        {
            if (this.terraformingValue == value)
            {
                return;
            }

            this.terraformingValue = value;

            if (this.OnTerraformingValueChanged != null)
            {
                this.OnTerraformingValueChanged(this.terraformingValue);
            }
        }
    }

    public int GenerationCounter
    {
        get
        {
            return this.generationCounter;
        }

        set
        {
            if (this.generationCounter == value)
            {
                return;
            }

            this.generationCounter = value;

            if (this.OnGenerationCounterChanged != null)
            {
                this.OnGenerationCounterChanged(this.generationCounter);
            }
        }
    }

    public List<Factory> Factories = new List<Factory>();

    private int terraformingValue = int.MinValue;
    private int generationCounter = int.MinValue;

    public Game(GameView gameView)
    {
        this.gameView = gameView;

        this.gameView.Register(this, this.OnProductionButtonPressed);

        var factoryViews = this.gameView.FactoryViews;
        foreach (var factoryView in factoryViews)
        {
            var factory = new Factory(factoryView.factoryConfig);
            this.Factories.Add(factory);

            factoryView.Register(factory);

            factory.Init();
        }

        this.GenerationCounter = 0;
        this.TerraformingValue = 35;
    }

    private void OnProductionButtonPressed()
    {
        this.GenerationCounter++;

        // transfer stock
        foreach (var factory in this.Factories)
        {
            if (!string.IsNullOrEmpty(factory.Config.TransferStockToOtherFactory))
            {
                var targetFactory = this.Factories.Find((f) => f.Config.Name == factory.Config.TransferStockToOtherFactory);
                targetFactory.Stock += factory.Stock;
                factory.Stock = 0;
            }
        }

        // produce new stock
        foreach (var factory in this.Factories)
        {
            factory.Produce(this.TerraformingValue);
        }
    }
}

