using System.Collections;
using System.Collections.Generic;

public class BlockMover {
	private BlockField field;
	private Point spawnPoint;
	private Point previewPoint;
	private List<TetrominoPrototype> tetrominos;
	private System.Random random;
	private Tetromino activeTetromino;
	private Tetromino nextTetromino;

	public BlockMover(BlockField field, Point spawnPoint, Point previewPoint, List<TetrominoPrototype> tetrominos) {
		this.field = field;
		this.spawnPoint = spawnPoint;
		this.previewPoint = previewPoint;
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

	public bool SpawnTetromino () {
		activeTetromino = nextTetromino;
		bool success = activeTetromino.Spawn (field, spawnPoint);

		if (success) {
			GetNextTetromino ();
		}

		return success;
	}

	private void GetNextTetromino() {
		TetrominoPrototype prototype = tetrominos [random.Next (tetrominos.Count)];
		nextTetromino = Tetromino.CreateFromPrototype (prototype, previewPoint);
	}

	public bool MoveDown() {
		bool success = activeTetromino.MoveDown (field);

		if (!success) {
			field.AddTetromino (activeTetromino);
		}

		return success;
	}

	public void MoveLeft () {
		activeTetromino.MoveLeft (field);
	}

	public void MoveRight () {
		activeTetromino.MoveRight (field);
	}

	public void RotateAntiClockwise() {
		activeTetromino.RotateAntiClockwise (field);
	}

	public int CountAndRemoveFullRows() {
		int fullRows = field.CountFullRows ();
		field.RemoveFullRows ();
		return fullRows;
	}
}
