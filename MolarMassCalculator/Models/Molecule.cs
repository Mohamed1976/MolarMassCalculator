using System;
using System.Collections.Generic;
using MolarMassCalculator.Common;
using Windows.UI;
using Windows.UI.Xaml.Media;
using FormulaParserUWPComponent;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;

namespace MolarMassCalculator.Models
{
    public class Molecule : ViewModelBase
    {
        #region [ Constructor ]

        public Molecule()
        {
            Composition = new List<Element>();
            Parser = new FormulaParser();
        }

        #endregion

        #region [ Properties ]

        private FormulaParser parser = null;
        public FormulaParser Parser
        {
            get { return parser; }
            set { Set(ref parser, value); }
        }

        private string formula = string.Empty;
        public string Formula
        {
            get { return formula; }
            set { Set(ref formula, value); }
        }

        private string formulaSummary = string.Empty;
        public string FormulaSummary
        {
            get { return formulaSummary; }
            set { Set(ref formulaSummary, value); }
        }

        private double molarMass = 0;
        public double MolarMass
        {
            get { return molarMass; }
            set { Set(ref molarMass, value); }
        }

        private List<Element> composition = null;
        public List<Element> Composition
        {
            get { return composition; }
            set { Set(ref composition, value); }
        }

        #endregion

        #region [ Methods ]

        public int TryParseFormula(out string errorMsg)
        {
            string formulaSummary = string.Empty; ;
            int retVal = Constants.Success;
            FormulaSummary = string.Empty;
            errorMsg = string.Empty;
            double molarMass = 0;
            MolarMass = 0;

            try
            {

                if (!Regex.IsMatch(formula, allowedElements,
                    RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Singleline))
                {
                    retVal = Constants.Failure;
                    errorMsg = "Check formula, only elements and parentheses () allowed.";
                }

                if (retVal == Constants.Success)
                {
                    retVal = parser.SetFormula(Formula, out formulaSummary, out molarMass);

                    if (retVal == Constants.Success)
                    {
                        MolarMass = molarMass;
                        FormulaSummary = formulaSummary;
                    }
                    else
                    {
                        errorMsg = parser.GetLastError();
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = Constants.Failure;
                errorMsg = string.Format("An unexpected error occurred: {0}", ex.Message);
            }

            return retVal;
        }

        public int CalculateComposition(out string errorMsg)
        {
            int retVal = Constants.Failure;
            errorMsg = string.Empty;

            try
            {
                Composition.Clear();
                IDictionary<string, double> calculatedComposition = parser.ParseElements(out retVal);
                if (retVal == Constants.Success)
                {
                    foreach (var element in calculatedComposition)
                    {
                        string elementName = element.Key;
                        double massContent = element.Value;

                        Composition.Add(new Element()
                        {
                            element = elementName,
                            relativeMassContent = massContent,
                            Brush = elementColors[elementName]
                        });
                    }
                }
                else
                {
                    errorMsg = parser.GetLastError();
                }
            }
            catch (Exception ex)
            {
                retVal = Constants.Failure;
                errorMsg = string.Format("An unexpected error occurred: {0}", ex.Message);
            }

            return retVal;
        }

        #endregion

        #region [ Defines ]

        private static Dictionary<string, SolidColorBrush> elementColors = new Dictionary<string, SolidColorBrush>
        {
            { "H", new SolidColorBrush(Colors.Green) },
            { "D", new SolidColorBrush(Colors.AntiqueWhite) },
            { "T", new SolidColorBrush(Colors.Aqua) },
            { "He", new SolidColorBrush(Colors.Aquamarine) },
            { "Li", new SolidColorBrush(Colors.Azure) },
            { "Be", new SolidColorBrush(Colors.Beige) },
            { "B", new SolidColorBrush(Colors.Bisque) },
            { "C", new SolidColorBrush(Colors.Red) },
            { "N", new SolidColorBrush(Colors.Black) },
            { "O", new SolidColorBrush(Colors.Blue) },
            { "F", new SolidColorBrush(Colors.BlueViolet) },
            { "Ne", new SolidColorBrush(Colors.Brown) },
            { "Na", new SolidColorBrush(Colors.BurlyWood) },
            { "Mg", new SolidColorBrush(Colors.CadetBlue) },
            { "Al", new SolidColorBrush(Colors.Chartreuse) },
            { "Si", new SolidColorBrush(Colors.Chocolate) },
            { "P", new SolidColorBrush(Colors.Coral) },
            { "S", new SolidColorBrush(Colors.CornflowerBlue) },
            { "Cl", new SolidColorBrush(Colors.Cornsilk) },
            { "Ar", new SolidColorBrush(Colors.Crimson) },
            { "K", new SolidColorBrush(Colors.Cyan) },
            { "Ca", new SolidColorBrush(Colors.DarkBlue) },
            { "Sc", new SolidColorBrush(Colors.DarkCyan) },
            { "Ti", new SolidColorBrush(Colors.DarkGoldenrod) },
            { "V", new SolidColorBrush(Colors.DarkGray) },
            { "Cr", new SolidColorBrush(Colors.DarkGreen) },
            { "Mn", new SolidColorBrush(Colors.DarkKhaki) },
            { "Fe", new SolidColorBrush(Colors.DarkMagenta) },
            { "Co", new SolidColorBrush(Colors.DarkOliveGreen) },
            { "Ni", new SolidColorBrush(Colors.DarkOrange) },
            { "Cu", new SolidColorBrush(Colors.DarkOrchid) },
            { "Zn", new SolidColorBrush(Colors.DarkRed) },
            { "Ga", new SolidColorBrush(Colors.DarkSalmon) },
            { "Ge", new SolidColorBrush(Colors.DarkSeaGreen) },
            { "As", new SolidColorBrush(Colors.DarkSlateBlue) },
            { "Se", new SolidColorBrush(Colors.DarkSlateGray) },
            { "Br", new SolidColorBrush(Colors.DarkTurquoise) },
            { "Kr", new SolidColorBrush(Colors.DarkViolet) },
            { "Rb", new SolidColorBrush(Colors.DeepPink) },
            { "Sr", new SolidColorBrush(Colors.DeepSkyBlue) },
            { "Y", new SolidColorBrush(Colors.DimGray) },
            { "Zr", new SolidColorBrush(Colors.DodgerBlue) },
            { "Nb", new SolidColorBrush(Colors.Firebrick) },
            { "Mo", new SolidColorBrush(Colors.FloralWhite) },
            { "Tc", new SolidColorBrush(Colors.ForestGreen) },
            { "Ru", new SolidColorBrush(Colors.Fuchsia) },
            { "Rh", new SolidColorBrush(Colors.Gainsboro) },
            { "Pd", new SolidColorBrush(Colors.GhostWhite) },
            { "Ag", new SolidColorBrush(Colors.Gold) },
            { "Cd", new SolidColorBrush(Colors.Goldenrod) },
            { "In", new SolidColorBrush(Colors.Gray)},
            { "Sn", new SolidColorBrush(Colors.AliceBlue) },
            { "Sb", new SolidColorBrush(Colors.GreenYellow) },
            { "Te", new SolidColorBrush(Colors.Honeydew) },
            { "I", new SolidColorBrush(Colors.HotPink) },
            { "Xe", new SolidColorBrush(Colors.IndianRed) },
            { "Cs", new SolidColorBrush(Colors.Indigo) },
            { "Ba", new SolidColorBrush(Colors.Ivory) },
            { "La", new SolidColorBrush(Colors.Khaki) },
            { "Ce", new SolidColorBrush(Colors.Lavender) },
            { "Pr", new SolidColorBrush(Colors.LavenderBlush) },
            { "Nd", new SolidColorBrush(Colors.LawnGreen)},
            { "Pm", new SolidColorBrush(Colors.LemonChiffon) },
            { "Sm", new SolidColorBrush(Colors.LightBlue) },
            { "Eu", new SolidColorBrush(Colors.LightCoral) },
            { "Gd", new SolidColorBrush(Colors.LightCyan) },
            { "Tb", new SolidColorBrush(Colors.LightGoldenrodYellow) },
            { "Dy", new SolidColorBrush(Colors.LightGray) },
            { "Ho", new SolidColorBrush(Colors.LightGreen) },
            { "Er", new SolidColorBrush(Colors.LightPink) },
            { "Tm", new SolidColorBrush(Colors.LightSalmon) },
            { "Yb", new SolidColorBrush(Colors.LightSeaGreen) },
            { "Lu", new SolidColorBrush(Colors.LightSkyBlue) },
            { "Hf", new SolidColorBrush(Colors.LightSlateGray) },
            { "Ta", new SolidColorBrush(Colors.LightSteelBlue) },
            { "W", new SolidColorBrush(Colors.LightYellow) },
            { "Re", new SolidColorBrush(Colors.Lime) },
            { "Os", new SolidColorBrush(Colors.LimeGreen) },
            { "Ir", new SolidColorBrush(Colors.Linen) },
            { "Pt", new SolidColorBrush(Colors.Magenta) },
            { "Au", new SolidColorBrush(Colors.Maroon) },
            { "Hg", new SolidColorBrush(Colors.MediumAquamarine) },
            { "Tl", new SolidColorBrush(Colors.MediumBlue) },
            { "Pb", new SolidColorBrush(Colors.MediumOrchid) },
            { "Bi", new SolidColorBrush(Colors.MediumPurple) },
            { "Po", new SolidColorBrush(Colors.MediumSeaGreen) },
            { "At", new SolidColorBrush(Colors.MediumSlateBlue) },
            { "Rn", new SolidColorBrush(Colors.MediumSpringGreen)  },
            { "Fr", new SolidColorBrush(Colors.MediumTurquoise)  },
            { "Ra", new SolidColorBrush(Colors.MediumVioletRed)  },
            { "Ac", new SolidColorBrush(Colors.MidnightBlue)  },
            { "Th", new SolidColorBrush(Colors.MintCream)  },
            { "Pa", new SolidColorBrush(Colors.MistyRose)  },
            { "U", new SolidColorBrush(Colors.Moccasin)  },
            { "Np", new SolidColorBrush(Colors.NavajoWhite)  },
            { "Pu", new SolidColorBrush(Colors.Navy)  },
            { "Am", new SolidColorBrush(Colors.OldLace)  },
            { "Cm", new SolidColorBrush(Colors.Olive)  },
            { "Bk", new SolidColorBrush(Colors.OliveDrab)  },
            { "Cf", new SolidColorBrush(Colors.Orange)  },
            { "Es", new SolidColorBrush(Colors.OrangeRed)  },
            { "Fm", new SolidColorBrush(Colors.Orchid)  },
            { "Md", new SolidColorBrush(Colors.PaleGoldenrod)  },
            { "No", new SolidColorBrush(Colors.PaleGreen)  },
            { "Lr", new SolidColorBrush(Colors.PaleTurquoise)  },
            { "Rf", new SolidColorBrush(Colors.PaleVioletRed)  },
            { "Db", new SolidColorBrush(Colors.PapayaWhip)  },
            { "Sg", new SolidColorBrush(Colors.PeachPuff)  },
            { "Bh", new SolidColorBrush(Colors.Peru)  },
            { "Hs", new SolidColorBrush(Colors.Pink)  },
            { "Mt", new SolidColorBrush(Colors.Plum)  },
            { "Uun", new SolidColorBrush(Colors.PowderBlue)  },
            { "Uuu", new SolidColorBrush(Colors.Purple)  }
        };

        private static readonly string allowedElements
            = @"^(\(|\)|[0-9]|H|D|T|He|Li|Be|B|C|N|O|F|Ne|Na|Mg|Al|Si|P|S|Cl|Ar|K|Ca|Sc|Ti|V|Cr|Mn|Fe|Co|Ni|Cu|Zn|Ga|Ge|As|Se|Br|Kr|Rb|Sr|Y|Zr|Nb|Mo|Tc|Ru|Rh|Pd|Ag|Cd|In|Sn|Sb|Te|I|Xe|Cs|Ba|La|Ce|Pr|Nd|Pm|Sm|Eu|Gd|Tb|Dy|Ho|Er|Tm|Yb|Lu|Hf|Ta|W|Re|Os|Ir|Pt|Au|Hg|Tl|Pb|Bi|Po|At|Rn|Fr|Ra|Ac|Th|Pa|U|Np|Pu|Am|Cm|Bk|Cf|Es|Fm|Md|No|Lr|Rf|Db|Sg|Bh|Hs|Mt|Uun|Uuu)*$";

        #endregion
    }
}
