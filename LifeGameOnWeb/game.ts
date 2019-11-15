import { LifeGameTable, Case } from "./LifeGameTable";

class Game {

    private _canvas: HTMLCanvasElement;
    private _engine: BABYLON.Engine;
    private _scene: BABYLON.Scene;
    private _camera: BABYLON.FreeCamera;
    private _light: BABYLON.Light;

    constructor(canvasElement: string) {
        this._canvas = document.getElementById(canvasElement) as HTMLCanvasElement;
        this._engine = new BABYLON.Engine(this._canvas, true);
    }

    createScene(gameTable: Array<Case>): void {
        
        // This creates a basic Babylon Scene object (non-mesh)
        this._scene = new BABYLON.Scene(this._engine);

        // This creates and positions a free camera (non-mesh)
        var camera = new BABYLON.FreeCamera("camera1", new BABYLON.Vector3(0, 5, -10), this._scene);

        // This targets the camera to scene origin
        camera.setTarget(BABYLON.Vector3.Zero());

        // This attaches the camera to the canvas
        camera.attachControl(this._canvas, true);

        // This creates a light, aiming 0,1,0 - to the sky (non-mesh)
        var light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(0, 1, 0), this._scene);

        // Default intensity is 1. Let's dim the light a small amount
        light.intensity = 0.7;

        var pat = BABYLON.Mesh.FLIP_TILE;
        var av = BABYLON.Mesh.TOP;
        var ah = BABYLON.Mesh.LEFT;
        
        var options = {
            sideOrientation: BABYLON.Mesh.DOUBLESIDE,
            pattern: pat,
            alignVertical: av,
            alignHorizontal: ah,
            width: 10,
            height: 0.5,
            depth: 10,
            tileSize: 1,
            tileWidth: 3
        };
        
        BABYLON.MeshBuilder.CreateTiledBox("", options, this._scene);

        //Table
        var squareVectors: BABYLON.Vector3[][] = [
            //Vertical
            [
                new BABYLON.Vector3(-1, 0.30, 5),
                new BABYLON.Vector3(-1, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(-2, 0.30, 5),
                new BABYLON.Vector3(-2, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(-3, 0.30, 5),
                new BABYLON.Vector3(-3, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(-4, 0.30, 5),
                new BABYLON.Vector3(-4, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 5),
                new BABYLON.Vector3(-5, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(0, 0.30, 5),
                new BABYLON.Vector3(0, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(1, 0.30, 5),
                new BABYLON.Vector3(1, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(2, 0.30, 5),
                new BABYLON.Vector3(2, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(3, 0.34, 5),
                new BABYLON.Vector3(3, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(4, 0.30, 5),
                new BABYLON.Vector3(4, 0.30, -5)
            ],
            [
                new BABYLON.Vector3(5, 0.30, 5),
                new BABYLON.Vector3(5, 0.30, -5)
            ],
            //Horizontal
            [
                new BABYLON.Vector3(-5, 0.30, 5),
                new BABYLON.Vector3(5, 0.30, 5)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 4),
                new BABYLON.Vector3(5, 0.30, 4)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 3),
                new BABYLON.Vector3(5, 0.30, 3)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 2),
                new BABYLON.Vector3(5, 0.30, 2)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 1),
                new BABYLON.Vector3(5, 0.30, 1)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, 0),
                new BABYLON.Vector3(5, 0.30, 0)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, -1),
                new BABYLON.Vector3(5, 0.30, -1)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, -2),
                new BABYLON.Vector3(5, 0.30, -2)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, -3),
                new BABYLON.Vector3(5, 0.30, -3)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, -4),
                new BABYLON.Vector3(5, 0.30, -4)
            ],
            [
                new BABYLON.Vector3(-5, 0.30, -5),
                new BABYLON.Vector3(5, 0.30, -5)
            ],
        ];

        //One life
        //var sphere = BABYLON.Mesh.CreateSphere("sphere1", 16, 1, this._scene);
        //sphere.position.y = 1.3;
        //sphere.position = new BABYLON.Vector3(0.5, 1, 0.5);

        //for (var index in gameTable) {
        //    let currentCase = gameTable[index];
        //    var sphere = BABYLON.Mesh.CreateSphere("sphere1", 16, 1, this._scene);
        //    sphere.position = new BABYLON.Vector3(currentCase.x, 1, currentCase.y);
        //}


        // creates an instance of a line system
        var lineSystem = BABYLON.MeshBuilder.CreateLineSystem("lineSystem", {lines: squareVectors, updatable: false}, this._scene);
        lineSystem.color = new BABYLON.Color3(1, 0, 0);
    }

    doRender(): void {
            // Run the render loop.
    this._engine.runRenderLoop(() => {
        this._scene.render();
    });

    // The canvas/window resize event handler.
    window.addEventListener('resize', () => {
        this._engine.resize();
    });
    }
}

window.addEventListener('DOMContentLoaded', () => {
    // Create the game using the 'renderCanvas'.
    let game = new Game('renderCanvas');

    let table = new LifeGameTable(10, 10);
    let myGameTable = table.InitializeTable();
    
    // Create the scene.
    game.createScene(myGameTable);

    // Start render loop.
    game.doRender();
});