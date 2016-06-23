using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI.Xaml.Shapes;
using GalaSoft.MvvmLight;
using MolarMassCalculator.Models;
using MolarMassCalculator.Common;
using System.Linq;
using System;

namespace MolarMassCalculator.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region [ Constructor ]

        public MainViewModel()
        {
            SelectedMolecule = new Molecule();
            InitializeCommands();
        }

        #endregion

        #region [ Commands ]

        private void InitializeCommands()
        {
            CalculateCmd = new RelayCommand(OnCalculateCmdExecute, OnCalculateCmdCanExecute);
            SelectedSliceCmd = new RelayCommand<object>(OnSelectedSliceCmdCmd, OnSelectedSliceCmdCanExecute);
        }

        public RelayCommand CalculateCmd { get; private set; }
        public RelayCommand<object> SelectedSliceCmd { get; private set; }

        private bool OnCalculateCmdCanExecute()
        {
            return !IsBusy;
        }

        private void OnCalculateCmdExecute()
        {
            Status = string.Empty;
            string errorMsg = string.Empty;
            int retVal = Constants.Failure;

            if (string.IsNullOrEmpty(SelectedMolecule.Formula))
            {
                Status = "Please enter a formula to analyze.";
            }
            else
            {
                IsBusy = true;
                Status = "Processing formule.";

                retVal = SelectedMolecule.TryParseFormula(out errorMsg);
                if (retVal == Constants.Success)
                {
                    retVal = SelectedMolecule.CalculateComposition(out errorMsg);
                }

                if (retVal == Constants.Success)
                {
                    retVal = InitializeChartPalette(out errorMsg);
                }

                if (retVal == Constants.Success)
                {
                    Status = "Successfully processed formule.";
                }
                else
                {
                    Status = errorMsg;
                }

                IsBusy = false;
            }
        }

        private bool OnSelectedSliceCmdCanExecute(object prm)
        {
            return !IsBusy;
        }

        private void OnSelectedSliceCmdCmd(object prm)
        {
            SelectionChangedEventArgs args = (SelectionChangedEventArgs)prm;

            if (args.AddedItems.Any())
            {
                Status = string.Format("{0} ({1:0.00}%)",
                    ((Element)args.AddedItems[0]).element,
                    ((Element)args.AddedItems[0]).relativeMassContent);
            }
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the Status property. This property displays
        /// the status message.
        /// </summary>
        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            private set { this.Set(ref this.status, value); }
        }

        private Molecule selectedMolecule = null;
        public Molecule SelectedMolecule
        {
            get { return selectedMolecule; }
            private set { this.Set(ref this.selectedMolecule, value); }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            private set { this.Set(ref this.isBusy, value); }
        }

        ResourceDictionaryCollection paletteCollection = null;
        public ResourceDictionaryCollection PaletteCollection
        {
            get { return paletteCollection; }
            private set { this.Set(ref this.paletteCollection, value); }
        }

        #endregion

        #region [ Methods ]
        private int InitializeChartPalette(out string errorMsg)
        {
            int retVal = Constants.Failure;
            errorMsg = string.Empty;

            try
            {
                ResourceDictionaryCollection rdCollection = new ResourceDictionaryCollection();
                if (PaletteCollection != null)
                {
                    PaletteCollection.Clear();
                    PaletteCollection = null;
                }

                // Create a color palette entry for each data point
                foreach (Element el in SelectedMolecule.Composition)
                {
                    ResourceDictionary rd = new ResourceDictionary();
                    Style dataPointStyle = new Style(typeof(Control));
                    dataPointStyle.Setters.Add(new Setter(Control.BackgroundProperty, el.Brush));
                    dataPointStyle.Setters.Add(new Setter(Control.TemplateProperty, App.Current.Resources["PieDataPointControlTemplate"] as ControlTemplate));
                    dataPointStyle.Setters.Add(new Setter(DataPoint.DependentValueStringFormatProperty, "{0:0.00}"));
                    rd.Add("DataPointStyle", dataPointStyle);
                    Style dataShapeStyle = new Style(typeof(Shape));
                    dataShapeStyle.Setters.Add(new Setter(Shape.StrokeProperty, el.Brush));
                    dataShapeStyle.Setters.Add(new Setter(Shape.StrokeThicknessProperty, 2));
                    dataShapeStyle.Setters.Add(new Setter(Shape.StrokeMiterLimitProperty, 1));
                    dataShapeStyle.Setters.Add(new Setter(Shape.FillProperty, el.Brush));
                    rd.Add("DataShapeStyle", dataShapeStyle);
                    rdCollection.Add(rd);
                }
                PaletteCollection = rdCollection;
                retVal = Constants.Success;
            }
            catch (Exception ex)
            {
                retVal = Constants.Failure;
                errorMsg = string.Format("An unexpected error occurred: {0}", ex.Message);
            }

            return retVal;
        }

        #endregion
    }
}
