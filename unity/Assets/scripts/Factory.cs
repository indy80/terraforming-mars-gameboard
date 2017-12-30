using System;

public class Factory
{
    public readonly FactoryConfig Config;

    public Action<int> OnStockChanged;
    public Action<int> OnLevelChanged;

    public int Stock
    {
        get
        {
            return this.stock;
        }

        set
        {
            if (this.stock == value)
            {
                return;
            }

            this.stock = value;

            if (this.OnStockChanged != null)
            {
                this.OnStockChanged.Invoke(this.stock);
            }
        }
    }

    public int Level
    {
        get
        {
            return this.level;
        }

        set
        {
            if (this.level == value)
            {
                return;
            }

            this.level = value;
            this.level = Math.Max(this.level, this.Config.MinLevel);

            if (this.OnLevelChanged != null)
            {
                this.OnLevelChanged.Invoke(this.level);
            }
        }
    }

    private int level = int.MaxValue;
    private int stock = int.MaxValue;

    public Factory(FactoryConfig config)
    {
        this.Config = config;
    }

    public void Init()
    {
        this.Level = 0;
        this.Stock = 0;
    }

    public void Produce(int terraformingValue)
    {
        var newStock = this.Stock + this.level;

        if (this.Config.UseTerraformingValueAsLevel)
        {
            newStock += terraformingValue;
        }

        this.Stock = newStock;
    }
}
