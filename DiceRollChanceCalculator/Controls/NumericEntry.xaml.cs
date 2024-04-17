namespace DiceRollChanceCalculator.Controls;

public partial class NumericEntry : ContentView
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
           nameof(Value),
           typeof(decimal),
           typeof(NumericEntry),
           default(decimal));

    public NumericEntry()
	{
		InitializeComponent();
	}

    public decimal Value
    {
        get { return (decimal)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    private void IncreaseValue(object sender, EventArgs e)
    {
        Value++;
        numericEntry.Text = Value.ToString();
    }

    private void DecreaseValue(object sender, EventArgs e)
    {
        if (Value > 0)
        {
            Value--;
            numericEntry.Text = Value.ToString();
        }
    }

    private void NumericEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse(e.NewTextValue, out decimal newValue))
        {
            Value = newValue;
        }
    }
}