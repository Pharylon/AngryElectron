#Project Angry Electron

##Overview
Angry Electron is the project name for a stoichiometry calculator. The User of this stoichiometry calculator will be able to enter in the molecules of a chemical equation and the calculator will balance the equation for the user, telling them how many of each molecule should be used for the equation to be balanced.

##Breakdown
- Parsing - Interpreting user's typed inputs to domain objects.
	- Equation is made up of two Sides.
		- One side is Reactants.
		- One side is Products
	- A Side of a reaction is made up of molecules. The parser should be flexible enough though that in a later implementation there may be other things included that arent molecules (for example electrons).
		- A Molecule may be preceded by a coefficient.
		- A coeffiecient must precede a molecule.
	- A Molecule is made up of Complexes and Elements.
	- A Complex is made up of either more complexes or Elements.
	- A complex or an Element may have a subscript.
	- The subscript designates the number of atoms or the number of complexes of any given element or complex, respectively. 
	- The lack of a subscript or coefficient implys that it would be the number one if it were present.
- Domain Calculations - Manipulating sets of domain objects to arrive at solution to equation.
	- total for a number of atoms of a particular element on one side must equal the total for the number of atoms for that particular element on the other side.
	- coefficients are the only thing that may be changed in order to for the total number of atoms on one side to balance.
- Presentation - Showing the results of the domain calculation in an attractive manner. May include real-time error checking, or showing the results immediately (without requiring calculate to be pressed).

##Parsing
- Side Start
	- Molecule Start
		- Coefficient - is a number either at the front of the molecule or not specified. If its not specified its assumed to be one. The number of atoms of each element is multiplied by the coefficient.
		- Complex Start - May be either parentheses or square brackets. 
			- Complex
			- Element
			- Complex End - Designated by the closing parenthesis or square brackets, depending on which it was began with. The complex end may also have a subscript immediately afterwards. If the next character is anything other than a number than the number of atoms for each element within it is multipliplied by one, otherwise it is multiplied by the number immediately after it.
		- Element Start
			- Element second Letter (lowercase)
			- Element subscript (number)
			- Element End
		- Molecule End
	- Space, plus or electron* (*in a later version) 
	
	
	