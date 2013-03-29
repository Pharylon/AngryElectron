var moleculeSequence;
(function (moleculeSequence) {
    var Point = (function () {
        function Point(x, y) {
            this.x = x;
            this.y = y;
        }
        Point.prototype.getDist = function () {
            return Math.sqrt(this.x * this.x + this.y * this.y);
        };
        Point.origin = new Point(0, 0);
        return Point;
    })();
    moleculeSequence.Point = Point;    
})(moleculeSequence || (moleculeSequence = {}));
