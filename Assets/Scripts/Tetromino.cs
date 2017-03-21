using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TetrominoPrototype {
	public List<Point> blockPositions;
	public GameObject spritePrototype;
	public bool evenWidth;

	public List<Block> CreateBlocksWithOrigin(Point origin) {
		List<Block> blocks = new List<Block> ();

		foreach (Point relativePosition in blockPositions) {
			Point absolutePosition = Point.Sum(origin, relativePosition);
			Block block = Block.CreateFromPrototype(spritePrototype, absolutePosition);
			blocks.Add(block);
		}

		return blocks;
	}
}

public class Tetromino {
	private List<Block> blocks;
	private Point origin;
	public bool evenWidth;

	public static Tetromino CreateFromPrototype(TetrominoPrototype prototype, Point origin) {
		List<Block> blocks = prototype.CreateBlocksWithOrigin (origin);
		return new Tetromino (blocks, origin, prototype.evenWidth);
	}

	public static Tetromino CreateEmpty() {
		return new Tetromino (new List<Block> (), new Point(0,0), true);
	}

	private Tetromino(List<Block> blocks, Point origin, bool evenWidth) {
		this.origin = origin;
		this.blocks = blocks;
		this.evenWidth = evenWidth;
	}

	public void Destroy() {
		foreach (Block block in Blocks) {
			block.Destroy ();
		}
		blocks.Clear ();
	}

	public bool Spawn(BlockField field, Point spawnPivot) {
		return Displace (field, Point.Subtract (spawnPivot, origin));
	}

	public bool MoveDown(BlockField field) {
		return Displace (field, new Point (0, -1));
	}

	public bool MoveLeft(BlockField field) {
		return Displace (field, new Point (-1, 0));
	}

	public bool MoveRight(BlockField field) {
		return Displace (field, new Point (1, 0));
	}

	public bool RotateAntiClockwise(BlockField field) {
		foreach (Block block in blocks) {
			Point offset = Point.Subtract (block.Position, origin);
			Point rotatedOffset = Point.RotateAntiClockwise (offset);
			block.Goal = Point.Sum (origin, rotatedOffset);

			if (evenWidth) {
				block.Goal = Point.Sum (block.Goal, new Point (0, 1));
			}
		}

		return TryToMove (field);
	}

	private bool Displace(BlockField field, Point displacement) {
		foreach (Block block in blocks) {
			block.Goal = Point.Sum (block.Position, displacement);
		}

		bool success = TryToMove (field);

		if (success) {
			origin = Point.Sum (origin, displacement);
		}

		return success;
	}

	private bool TryToMove (BlockField field) {
		bool canMoveBlocks = true;

		foreach (Block block in blocks) {
			canMoveBlocks = canMoveBlocks && field.IsCellEmpty (block.Goal);
		}

		if (canMoveBlocks) {
			foreach (Block block in blocks) {
				block.MoveToGoal ();
			}
		}

		return canMoveBlocks;
	}
		
	public List<Block> Blocks {
		get { return blocks; }
	}
}