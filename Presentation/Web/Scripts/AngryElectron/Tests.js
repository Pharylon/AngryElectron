/// <reference path="~/Scripts/AngryElectron/ChemicalEquation.js" />
/// <reference path="~/Scripts/jquery.js" />
var chemicals = ["A", "B", "E"];
reactants = new ChemicalExpression(chemicals);
products = "C + D";
myEquation = new ChemicalEquation(reactants, products);
$(".test-paragraph").html(reactants.toHtml());

