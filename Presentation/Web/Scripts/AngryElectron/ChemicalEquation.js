var ChemicalEquation = function(reactants, products){
    return {
        reactants: reactants,
        products: products,
        toString: function () {
            return this.reactants.toString() + " --> " + this.products.toString();
        },
        toHtml: function() {
            return this.reactants.toHtml() + " &rarr; " + this.products.toHtml();
        }
    }
}

var ChemicalExpression = function (chemicals) {
    expressionChemicals;
    return {
        chemicals: chemicals,
        toString: function () {
            var expression = "";
            var isAdditional = false;
            for (var chemical in chemicals) {
                if (isAdditional) { expression += " + "; }
                expression += chemical.toString();
                isAdditional = true;
            }
            return expression;
        },
        toHtml: function() {
            var expression = "";
            var isAdditional = false;
            for (var i ; i < chemicals.count ; i++) {
                if (isAdditional) { expression += " + "; }
                expression += chemical[i].toString();
                isAdditional = true;
            }
            return expression;
        }
    }
}

var Chemical = function (statement) {
    
}