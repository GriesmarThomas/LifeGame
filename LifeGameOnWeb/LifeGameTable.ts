export class LifeGameTable {
    private Abscisse: Number;
    private Ordonnee: Number;

    constructor(abcisse: Number, ordonnee: Number) {
        this.Abscisse = abcisse;
        this.Ordonnee = ordonnee;
    }

    InitializeTable(): Array<Case> {

        let gameTableQueue: Array<Case>;

        for (var i = 0; i < this.Abscisse ; i++)
        {
            for (var j = 0; j < this.Ordonnee; j++)
            {
                gameTableQueue.push(new Case(i, j, false));
            }
        }
        return gameTableQueue;
    }

}

export class Case {

    private _x: number;
    get x(): number {
        return this._x;
    }
    set x(value: number) {
        this._x = value;
    }
    private _y: number;
    get y(): number {
        return this._y;
    }
    set y(value: number) {
        this._y = value;
    }
    private _isAlive: boolean;
    get isAlive(): boolean {
        return this._isAlive;
    }
    set isAlive(value: boolean) {
        this._isAlive = value;
    }

    constructor(coordX: number, coordY: number, caseIsAlive: boolean) {
        this._x = coordX;
        this._y = coordY;
        this._isAlive = caseIsAlive;
    };
}