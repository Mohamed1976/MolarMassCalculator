#pragma once

#include <iostream>
#include <stdio.h>

#define FORMULA_LENGTH_EXCEEDS_LIMIT	6
#define UNKNOWN_ELEMENT_IN_FORMULA		5
#define INVALID_BRACKETS				4
#define ERROR_WHILE_INITIALIZING_REGEX	3
#define NO_FORMULA_SPECIFIED			2
#define GENERAL_ERROR					1
#define SUCCESS							0
#define MAX_FORMULA_LENGTH				255

using namespace std;
using namespace Platform;
using namespace Platform::Collections;
using namespace Windows::Foundation::Collections;

namespace FormulaParserUWPComponent
{
struct ltstr
{
	bool operator()(const char* s1, const char* s2) const
	{
		return strcmp(s1, s2) < 0;
	}
};

public ref class FormulaParser sealed
{
	public:
		FormulaParser();
		String^ GetLastError();
		IMap<String^, double>^ ParseElements(int *errorCode);
		int SetFormula(String^ moleculeFormula, String^* formulaSummary, double* moleMass);
	private:
		static map<const char*, double, ltstr> periodicTableOfElements;
		int AscIICharsFromString(String^ source, char** destination);
		String^ StringFromAscIIChars(const char* chars);
		void SetLastError(String^ error);
		double calculatedMoleMass;
		String^ lastError;
		char* formula;
};
}