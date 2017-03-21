using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TetrominoPrototype {
	public List<Point> blockPositions;
	public GameObject spritePrototype;
	public bool isLineOrSquare;

	public List<Block> CreateBlocksWithOrigin(Point origin) {
		List<Block> blocks = new List<Block> ();

		foreach (Point relativePosition in blockPositions) {
			Point absolutePosition = origin.Plus(relativePosition);
			Block block = Block.CreateFromPrototype(spritePrototype, absolutePosition);
			blocks.Add(block);
		}

		return blocks;
	}

	public List<Point> GetRotatedPositions() {
		List<Point> rotatedPositions = new List<Point> ();

		foreach (Point blockPosition in blockPositions) {
			Point rotatedPosition = Point.RotateCounterclockwise(blockPosition).Plus(PivotOffset());
			rotatedPositions.Add (rotatedPosition);
		}

		return rotatedPositions;
	}

	public Point PivotOffset() {
		return isLineOrSquare ? Point.Above : Point.Zero;
	}
}
