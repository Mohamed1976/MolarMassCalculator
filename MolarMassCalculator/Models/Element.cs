using Windows.UI.Xaml.Media;

namespace MolarMassCalculator.Models
{
    public class Element
    {
        public string element { get; set; }
        public double relativeMassContent { get; set; }
        public SolidColorBrush Brush { get; set; }
    }
}
