"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var LifeGameTable = /** @class */ (function () {
    function LifeGameTable(abcisse, ordonnee) {
        this.Abscisse = abcisse;
        this.Ordonnee = ordonnee;
    }
    LifeGameTable.prototype.InitializeTable = function () {
        var gameTableQueue;
        for (var i = 0; i < this.Abscisse; i++) {
            for (var j = 0; j < this.Ordonnee; j++) {
                gameTableQueue.push(new Case(i, j, false));
            }
        }
        return gameTableQueue;
    };
    return LifeGameTable;
}());
exports.LifeGameTable = LifeGameTable;
var Case = /** @class */ (function () {
    function Case(coordX, coordY, caseIsAlive) {
        this._x = coordX;
        this._y = coordY;
        this._isAlive = caseIsAlive;
    }
    Object.defineProperty(Case.prototype, "x", {
        get: function () {
            return this._x;
        },
        set: function (value) {
            this._x = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Case.prototype, "y", {
        get: function () {
            return this._y;
        },
        set: function (value) {
            this._y = value;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Case.prototype, "isAlive", {
        get: function () {
            return this._isAlive;
        },
        set: function (value) {
            this._isAlive = value;
        },
        enumerable: true,
        configurable: true
    });
    ;
    return Case;
}());
exports.Case = Case;
//# sourceMappingURL=LifeGameTable.js.map