using System.Collections.ObjectModel;
using System.Globalization;

namespace DiceRollChanceCalculator.Converters;

public class CollectionEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<object> collection)
        {
            return collection.Count > 0;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This converter is intended for one-way binding, so ConvertBack can return null or default value.
        return null;
    }
}
