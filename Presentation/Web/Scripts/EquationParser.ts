// Module
module EquationParser {

    // Class
    class ChemicalEquation {
        reactant: moleculeSequence;
        product: moleculeSequence;
    }
    
    class moleculeSequence {
        molecule: molecule;
        moleculeSequence: moleculeSequence;
    }
    class molecule {
        group: group;
        molecule: molecule;
    }
    class group {
        unitGroup: unitGroup;
        coefficientNumber: number;
    }
    class unitGroup {
        chemicalElement: chemicalElement;
        molecule: molecule;
    }
    class coefficientNumber {

    }
    class  chemicalElement{
        symbol: string;
    }
}