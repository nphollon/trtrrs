using System.Collections;
using System.Collections.Generic;

public class BlockMover {
	private BlockField field;
	private Point previewPoint;
	private Point spawnDisplacement;
	private List<TetrominoPrototype> tetrominos;
	private System.Random random;
	private Tetromino activeTetromino;
	private Tetromino nextTetromino;

	public BlockMover(BlockField field, Point spawnPoint, Point previewPoint, List<TetrominoPrototype> tetrominos) {
		this.field = field;
		this.previewPoint = previewPoint;
		this.spawnDisplacement = Point.Subtract (spawnPoint, previewPoint);
		this.tetrominos = tetrominos;
		this.random = new System.Random ();
		activeTetromino = Tetromino.CreateEmpty ();
		nextTetromino = Tetromino.CreateEmpty ();
	}

	public void Reset() {
		field.Destroy ();
		activeTetromino.Destroy ();
		nextTetromino.Destroy ();
		GetNextTetromino ();
		SpawnTetromino ();
	}

	public bool SpawnTetromino() {
		activeTetromino = nextTetromino;
		bool canSpawn = TryToDisplace(spawnDisplacement);

		if (canSpawn) {
			GetNextTetromino ();
		}

		return canSpawn;
	}

	private void GetNextTetromino() {
		TetrominoPrototype prototype = tetrominos [random.Next (tetrominos.Count)];
		nextTetromino = Tetromino.CreateFromPrototype (prototype, previewPoint);
	}

	public bool MoveDown() {
		return TryToDisplace(new Point (0, -1));
	}

	public bool MoveLeft() {
		return TryToDisplace (new Point (-1, 0));
	}

	public bool MoveRight() {
		return TryToDisplace (new Point (1, 0));
	}

	private bool TryToDisplace(Point displacement) {
		List<Point> destination = activeTetromino.GetDisplacedPoints(displacement);

		bool canDisplace = field.AreCellsEmpty (destination);

		if (canDisplace) {
			activeTetromino.Displace (displacement);
		}

		return canDisplace;
	}

	public bool RotateAntiClockwise() {
		List<Point> destination = activeTetromino.GetRotatedPoints ();

		bool canRotate = field.AreCellsEmpty (destination);

		if (canRotate) {
			activeTetromino.Rotate ();
		}

		return canRotate;
	}

	public int CountAndRemoveFullRows() {
		FreezeTetromino ();
		int fullRows = field.CountFullRows ();
		field.RemoveFullRows ();
		return fullRows;
	}

	private void FreezeTetromino() {
		foreach (Block block in activeTetromino.Blocks) {
			field.InsertBlock (block);
		}

		activeTetromino = Tetromino.CreateEmpty ();
	}
}
