using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour {
	public BlockField blockField;
	public Point previewPoint;
	public Point spawnPoint;
	public List<TetrominoPrototype> tetrominos;

	private Point spawnDisplacement;
	private System.Random random;
	private Tetromino activeTetromino;
	private Tetromino nextTetromino;

	public void Start() {
		spawnDisplacement = Point.Subtract (spawnPoint, previewPoint);
		random = new System.Random ();
		activeTetromino = Tetromino.CreateEmpty ();
		nextTetromino = Tetromino.CreateEmpty ();
	}

	public void Reset() {
		blockField.Destroy ();
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
		return TryToDisplace(Point.Below);
	}

	public bool MoveLeft() {
		return TryToDisplace (Point.ToTheLeft);
	}

	public bool MoveRight() {
		return TryToDisplace (Point.ToTheRight);
	}

	private bool TryToDisplace(Point displacement) {
		List<Point> destination = activeTetromino.GetDisplacedPoints(displacement);

		bool canDisplace = blockField.AreCellsEmpty (destination);

		if (canDisplace) {
			activeTetromino.Displace (displacement);
		}

		return canDisplace;
	}

	public bool RotateAntiClockwise() {
		List<Point> destination = activeTetromino.GetRotatedPoints ();

		bool canRotate = blockField.AreCellsEmpty (destination);

		if (canRotate) {
			activeTetromino.Rotate ();
		}

		return canRotate;
	}

	public int CountAndRemoveFullRows() {
		FreezeTetromino ();
		int fullRows = blockField.CountFullRows ();
		blockField.RemoveFullRows ();
		return fullRows;
	}

	private void FreezeTetromino() {
		foreach (Block block in activeTetromino.Blocks) {
			blockField.InsertBlock (block);
		}

		activeTetromino = Tetromino.CreateEmpty ();
	}
}
