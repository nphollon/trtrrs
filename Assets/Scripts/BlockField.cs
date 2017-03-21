﻿using System;

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

		for (int column = 0; column < width; column++) {
			for (int row = 0; row < height; row++) {
				InsertEmptyBlock (column, row);
			}
		}
	}

	private void InsertEmptyBlock(int column, int row) {
		Point position = new Point (column, row);
		InsertBlock(Block.CreateEmpty (position));
	}

	private void InsertBlock(Block block) {
		try {
			field[block.X, block.Y] = block;
		} catch (IndexOutOfRangeException) {
			block.Destroy ();
		}
	}

	public void Destroy() {
		for (int column = 0; column < width; column++) {
			for (int row = 0; row < height; row++) {
				DestroyBlock (column, row);
			}
		}
	}

	private void DestroyBlock(int column, int row) {
		RemoveBlock (column, row).Destroy ();
	}

	private Block RemoveBlock(int column, int row) {
		Block yankedBlock = field [column, row];
		InsertEmptyBlock (column, row);
		return yankedBlock;
	}

	//TODO move out of class!
	public void AddTetromino(Tetromino tetromino) {
		foreach (Block block in tetromino.Blocks) {
			InsertBlock (block);
		}
	}

	public bool IsCellEmpty(Point point) {
		return IsCellEmpty (point.x, point.y);
	}
		
	private bool IsCellEmpty(int column, int row) {
		try {
			return !field [column, row].HasSprite();
		} catch (IndexOutOfRangeException) {
			return false;
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

	public void RemoveFullRows() {
		for (int row = 0; row < height; row++) {
			while (IsRowFull (row)) {
				DestroyRow (row);
				LowerRowsAbove (row);
			}
		}
	}

	private void DestroyRow(int row) {
		for (int column = 0; column < width; column++) {
			DestroyBlock (column, row);
		}
	}

	private void LowerRowsAbove(int bottomRow) {
		for (int column = 0; column < width; column++) {
			for (int row = bottomRow + 1; row < height; row++) {
				LowerBlock (column, row);
			}
		}
	}

	private void LowerBlock(int column, int row) {
		Block block = RemoveBlock (column, row);
		block.NudgeDown ();

		InsertBlock (block);
	}
}
