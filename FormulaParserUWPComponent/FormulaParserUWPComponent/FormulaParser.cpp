#include "pch.h"

#include "FormulaParser.h"
extern "C" {
#include "Parser.h"
}

using namespace FormulaParserUWPComponent;

FormulaParser::FormulaParser()
{
	formula = NULL;
	calculatedMoleMass = 0;
}

int FormulaParser::SetFormula(String^ moleculeFormula, String^* elementSummary, double* moleMass)
{	
	int retVal = SUCCESS;
	char tmpBuffer[32 + 1];
	char tmpBufferErrorMsg[255 + 1];
	char eSummary[MAX_FORMULA_LENGTH + 1];
	calculatedMoleMass = 0;
	eSummary[0] = '\0';


	try
	{ 
		if (moleculeFormula->Length() == 0 || moleculeFormula->Length() > MAX_FORMULA_LENGTH)
		{
			retVal = FORMULA_LENGTH_EXCEEDS_LIMIT;
			SetLastError("The entered molecule formula must have a length between 0 and 255 chars.");
		}

		if (retVal == SUCCESS)
		{
			AscIICharsFromString(moleculeFormula, &formula);
			if (verify_brackets(formula) == 0)
			{
				retVal = INVALID_BRACKETS;
				SetLastError("Make sure that all the brackets in the formula are opened and closed correctly.");
			}
		}

		if (retVal == SUCCESS)
		{
			Atom_count *iterator = parse_formula_c(formula);

			while (iterator != NULL && retVal == SUCCESS)
			{
				std::map<const char*, double, ltstr>::iterator search =
					periodicTableOfElements.find(iterator->element_symbol);
				if (search == periodicTableOfElements.end())
				{
					retVal = UNKNOWN_ELEMENT_IN_FORMULA;
					sprintf(tmpBufferErrorMsg, "Unknown element in formula [%s].", iterator->element_symbol);
					SetLastError(StringFromAscIIChars(tmpBufferErrorMsg));
				}

				if (retVal == SUCCESS)
				{
					calculatedMoleMass += search->second * iterator->count;
					sprintf(tmpBuffer, "%s%d", iterator->element_symbol, iterator->count);
					strcat(eSummary, tmpBuffer);
					iterator = iterator->next;
				}
			}
		}

		if (retVal != SUCCESS && formula != NULL)
		{
			delete[] formula;
			formula = NULL;
		}

		*moleMass = calculatedMoleMass;
		*elementSummary = StringFromAscIIChars(eSummary);
	}
	catch (Exception^ ex)
	{
		retVal = GENERAL_ERROR;
		SetLastError(ex->Message);
	}

	return retVal;
}

void FormulaParser::SetLastError(String^ error)
{
	lastError = error;
}

String^ FormulaParser::GetLastError()
{
	return lastError;
}

IMap<String^, double>^ FormulaParser::ParseElements(int *errorCode)
{
	double elementComposition = 0;
	int retVal = SUCCESS;
	map<String^, double> stlMap;

	try
	{
		//Make sure that the formula and the calculatedMoleMass are set  
		if (formula != NULL && calculatedMoleMass > .5)
		{
			Atom_count *iterator = parse_formula_c(formula);

			while (iterator != NULL)
			{
				std::map<const char*, double, ltstr>::iterator search =
					periodicTableOfElements.find(iterator->element_symbol);
				elementComposition = (((search->second * iterator->count) / calculatedMoleMass)  * 100) ;
				stlMap.insert(std::pair<String^, double>(StringFromAscIIChars(iterator->element_symbol), elementComposition));
				iterator = iterator->next;
			}
		}
		else
		{
			retVal = NO_FORMULA_SPECIFIED;
			SetLastError("Please specify a formula using the method SetFormula(String^ moleculeFormula).");
		}
	}
	catch (Exception^ ex)
	{
		retVal = GENERAL_ERROR;
		SetLastError(ex->Message);
	}

	return ref new Map<String^, double>(stlMap);
}

int FormulaParser::AscIICharsFromString(String^ source, char** destination)
{
	*destination = NULL;
	size_t ssLen = 0;

	const wchar_t* wcsource = source->Data();
	errno_t retss = wcstombs_s(&ssLen, NULL, 0, wcsource, wcslen(wcsource));

	if (retss == 0)
	{
		*destination = new char[ssLen];
		retss = wcstombs_s(&ssLen, *destination, ssLen, wcsource, ssLen);
	}

	if (retss && *destination != NULL)
	{
		delete[] * destination;
		*destination = NULL;
	}

	return retss;
}

String^ FormulaParser::StringFromAscIIChars(const char* chars)
{
	size_t convertedChars = 0;

	size_t newsize = strlen(chars) + 1;
	wchar_t * wcstring = new wchar_t[newsize];
	mbstowcs_s(&convertedChars, wcstring, newsize, chars, _TRUNCATE);
	String^ str = ref new String(wcstring);
	delete[] wcstring;
	return str;
}

//Initialization of the static member periodicTableOfElements
std::map<const char*, double, ltstr> FormulaParser::periodicTableOfElements =
{
	{ "H", 1.00794 },
	{ "D", 2.014101 },
	{ "T", 3.016049 },
	{ "He", 4.002602 },
	{ "Li", 6.941 },
	{ "Be", 9.012182 },
	{ "B", 10.811 },
	{ "C", 12.0107 },
	{ "N", 14.00674 },
	{ "O", 15.9994 },
	{ "F", 18.9984032 },
	{ "Ne", 20.1797 },
	{ "Na", 22.989770 },
	{ "Mg", 24.3050 },
	{ "Al", 26.981538 },
	{ "Si", 28.0855 },
	{ "P", 30.973761 },
	{ "S", 32.066 },
	{ "Cl", 35.4527 },
	{ "Ar", 39.948 },
	{ "K", 39.0983 },
	{ "Ca", 40.078 },
	{ "Sc", 44.955910 },
	{ "Ti", 47.867 },
	{ "V", 50.9415 },
	{ "Cr", 51.9961 },
	{ "Mn", 54.938049 },
	{ "Fe", 55.845 },
	{ "Co", 58.933200 },
	{ "Ni", 58.6934 },
	{ "Cu", 63.546 },
	{ "Zn", 65.39 },
	{ "Ga", 69.723 },
	{ "Ge", 72.61 },
	{ "As", 74.92160 },
	{ "Se", 78.96 },
	{ "Br", 79.904 },
	{ "Kr", 83.80 },
	{ "Rb", 85.4678 },
	{ "Sr", 87.62 },
	{ "Y", 88.90585 },
	{ "Zr", 91.224 },
	{ "Nb", 92.90638 },
	{ "Mo", 95.94 },
	{ "Tc", 98 },
	{ "Ru", 101.07 },
	{ "Rh", 102.90550 },
	{ "Pd", 106.42 },
	{ "Ag", 107.8682 },
	{ "Cd", 112.411 },
	{ "In", 114.818 },
	{ "Sn", 118.710 },
	{ "Sb", 121.760 },
	{ "Te", 127.60 },
	{ "I", 126.90447 },
	{ "Xe", 131.29 },
	{ "Cs", 132.90545 },
	{ "Ba", 137.327 },
	{ "La", 138.9055 },
	{ "Ce", 140.116 },
	{ "Pr", 140.90765 },
	{ "Nd", 144.24 },
	{ "Pm", 145 },
	{ "Sm", 150.36 },
	{ "Eu", 151.964 },
	{ "Gd", 157.25 },
	{ "Tb", 158.92534 },
	{ "Dy", 162.50 },
	{ "Ho", 164.93032 },
	{ "Er", 167.26 },
	{ "Tm", 168.93421 },
	{ "Yb", 173.04 },
	{ "Lu", 174.967 },
	{ "Hf", 178.49 },
	{ "Ta", 180.9479 },
	{ "W", 183.84 },
	{ "Re", 186.207 },
	{ "Os", 190.23 },
	{ "Ir", 192.217 },
	{ "Pt", 195.078 },
	{ "Au", 196.96655 },
	{ "Hg", 200.59 },
	{ "Tl", 204.3833 },
	{ "Pb", 207.2 },
	{ "Bi", 208.98038 },
	{ "Po", 209 },
	{ "At", 210 },
	{ "Rn", 222 },
	{ "Fr", 223 },
	{ "Ra", 226 },
	{ "Ac", 227 },
	{ "Th", 232.038 },
	{ "Pa", 231.03588 },
	{ "U", 238.0289 },
	{ "Np", 237 },
	{ "Pu", 244 },
	{ "Am", 243 },
	{ "Cm", 247 },
	{ "Bk", 247 },
	{ "Cf", 251 },
	{ "Es", 252 },
	{ "Fm", 257 },
	{ "Md", 258 },
	{ "No", 259 },
	{ "Lr", 262 },
	{ "Rf", 261 },
	{ "Db", 262 },
	{ "Sg", 266 },
	{ "Bh", 264 },
	{ "Hs", 269 },
	{ "Mt", 268 },
	{ "Uun", 271 },
	{ "Uuu", 272 }
};