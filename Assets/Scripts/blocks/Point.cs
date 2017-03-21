using System;
using UnityEngine;

[Serializable]
public class Point {
	public static readonly Point ToTheLeft = new Point (-1, 0);
	public static readonly Point ToTheRight = new Point (1, 0);
	public static readonly Point Below = new Point (0, -1);
	public static readonly Point Above = new Point (0, 1);
	public static readonly Point Zero = new Point (0, 0);

	public int x;
	public int y;

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Vector3 ToVector() {
		return new Vector3 (x, y, 0);
	}

	public Point Plus(Point addend) {
		return Sum (this, addend);
	}

	public static Point Sum(Point augend, Point addend) {
		return new Point (augend.x + addend.x, augend.y + addend.y);
	}

	public static Point Subtract(Point minuend, Point subtrahend) {
		return new Point (minuend.x - subtrahend.x, minuend.y - subtrahend.y);
	}

	public static Point RotateCounterclockwise(Point point) {
		return new Point (-point.y, point.x);
	}
}
