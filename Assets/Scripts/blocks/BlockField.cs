using System;
using System.Collections.Generic;

public class BlockField {
	private readonly int width;
	private readonly int height;
	private Block[,] field;

	public BlockField(int width, int height) {
		this.width = width;
		this.height = height;
		CreateEmptyField ();
	}

	private void CreateEmptyField() {
		field = new Block[width,height];
		map (InsertEmptyBlock);
	}

	private void InsertEmptyBlock(int column, int row) {
		Block emptyBlock = Block.CreateEmpty(new Point (column, row));
		InsertBlock(emptyBlock);
	}

	public void InsertBlock(Block block) {
		try {
			field[block.X, block.Y] = block;
		} catch (IndexOutOfRangeException) {
			block.Destroy ();
		}
	}

	public void Destroy() {
		map (DestroyBlock);
	}

	private void DestroyBlock(int column, int row) {
		YankBlock (column, row).Destroy ();
	}

	private Block YankBlock(int column, int row) {
		Block yankedBlock = field [column, row];
		InsertEmptyBlock (column, row);
		return yankedBlock;
	}

	public bool AreCellsEmpty(List<Point> points) {
		foreach (Point point in points) {
			if (IsCellOccupied (point.x, point.y)) {
				return false;
			}
		}

		return true;
	}

	private bool IsCellOccupied(int column, int row) {
		try {
			return field [column, row].HasSprite();
		} catch (IndexOutOfRangeException) {
			return true;
		}
	}
		
	public int CountFullRows() {
		int fullRows = 0;

		for (int row = 0; row < height; row++) {
			if (IsRowFull (row)) {
				fullRows = fullRows + 1;
			}
		}

		return fullRows;
	}

	private bool IsRowFull(int row) {
		for (int column = 0; column < width; column++) {
			if (IsCellEmpty(column, row)) {
				return false;
			}
		}
		return true;
	}

	private bool IsCellEmpty(int column, int row) {
		return !IsCellOccupied (column, row);
	}

	public void RemoveFullRows() {
		for (int row = 0; row < height; row++) {
			while (IsRowFull (row)) {
				DestroyRow (row);
				mapAboveRow (row + 1, LowerBlock);
			}
		}
	}

	private void DestroyRow(int row) {
		for (int column = 0; column < width; column++) {
			DestroyBlock (column, row);
		}
	}

	private void LowerBlock(int column, int row) {
		Block block = YankBlock (column, row);
		block.Displace (Point.Below);
		InsertBlock (block);
	}

	private delegate void BlockDelegate(int column, int row);

	private void map(BlockDelegate blockFunction) {
		mapAboveRow (0, blockFunction);
	}

	private void mapAboveRow(int bottomRow, BlockDelegate blockFunction) {
		for (int column = 0; column < width; column++) {
			for (int row = bottomRow; row < height; row++) {
				blockFunction (column, row);
			}
		}
	}
}
