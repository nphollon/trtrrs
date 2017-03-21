using System;
using System.Collections;
using System.Collections.Generic;

public class Tetromino {
	private static readonly Tetromino empty = new Tetromino (new List<Block> (), new List<Point> (), new Point (0, 0), new Point (0, 0));

	private List<Block> blocks;
	private Point origin;
	private Point pivotOffset;
	private List<Point> rotatedPositions;

	public static Tetromino CreateFromPrototype(TetrominoPrototype prototype, Point origin) {
		List<Block> blocks = prototype.CreateBlocksWithOrigin (origin);
		List<Point> rotatedPositions = prototype.GetRotatedPositions ();
		return new Tetromino (blocks, rotatedPositions, origin, prototype.PivotOffset ());
	}


	public static Tetromino CreateEmpty() {
		return empty;
	}

	private Tetromino(List<Block> blocks, List<Point> rotatedPositions, Point origin, Point pivotOffset) {
		this.origin = origin;
		this.blocks = blocks;
		this.pivotOffset = pivotOffset;
		this.rotatedPositions = rotatedPositions;
	}

	public void Destroy() {
		foreach (Block block in Blocks) {
			block.Destroy ();
		}
		blocks.Clear ();
	}

	public List<Point> GetDisplacedPoints(Point displacement) {
		List<Point> points = new List<Point> ();

		foreach (Block block in Blocks) {
			points.Add (displacement.Plus (block.Position));
		}

		return points;
	}

	public void Displace(Point displacement) {
		foreach (Block block in blocks) {
			block.Position = displacement.Plus (block.Position);
		}

		origin = Point.Sum (origin, displacement);
	}

	public List<Point> GetRotatedPoints() {
		List<Point> points = new List<Point> ();

		foreach (Point rotatedPosition in rotatedPositions) {
			points.Add (origin.Plus (rotatedPosition));
		}

		return points;
	}

	public void Rotate() {
		for (int i = 0; i < blocks.Count; i++) {
			blocks [i].Position = origin.Plus (rotatedPositions [i]);
			rotatedPositions [i] = Point.RotateCounterclockwise (rotatedPositions [i]).Plus (pivotOffset);
		}
	}
		
	public List<Block> Blocks {
		get { return blocks; }
	}
}