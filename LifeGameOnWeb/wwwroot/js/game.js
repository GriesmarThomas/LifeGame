var Game = /** @class */ (function () {
    function Game(canvasElement) {
        this._canvas = document.getElementById(canvasElement);
        this._engine = new BABYLON.Engine(this._canvas, true);
    }
    Game.prototype.createScene = function () {
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
        var squareVectors = [
            [
                new BABYLON.Vector3(-3, 0.5, 0),
                new BABYLON.Vector3(12, 0.5, 5)
            ],
        ];
        // creates an instance of a line system
        var lineSystem = BABYLON.MeshBuilder.CreateLineSystem("lineSystem", { lines: squareVectors, updatable: false }, this._scene);
        lineSystem.color = new BABYLON.Color3(1, 0, 0);
    };
    Game.prototype.doRender = function () {
        var _this = this;
        // Run the render loop.
        this._engine.runRenderLoop(function () {
            _this._scene.render();
        });
        // The canvas/window resize event handler.
        window.addEventListener('resize', function () {
            _this._engine.resize();
        });
    };
    return Game;
}());
window.addEventListener('DOMContentLoaded', function () {
    // Create the game using the 'renderCanvas'.
    var game = new Game('renderCanvas');
    // Create the scene.
    game.createScene();
    // Start render loop.
    game.doRender();
});
