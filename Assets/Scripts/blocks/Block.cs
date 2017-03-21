using UnityEngine;

public class Block {
	private Point position;
	private GameObject sprite;

	public static Block CreateFromPrototype(GameObject spritePrototype, Point position) {
		GameObject sprite = MonoBehaviour.Instantiate(spritePrototype, position.ToVector(), Quaternion.identity);
		return new Block (position, sprite);
	}

	public static Block CreateEmpty(Point position) {
		return new Block(position, null);
	}

	private Block(Point position, GameObject sprite) {
		this.position = position;
		this.sprite = sprite;
	}

	public void NudgeDown() {
		Position = Position.Below();
	}

	public void MoveToGoal() {
		Position = Goal;
	}

	public void Destroy() {
		MonoBehaviour.Destroy (Sprite);
		sprite = null;
	}


	public int X { 
		get { return Position.x; } 
	}

	public int Y { 
		get { return Position.y; } 
	}

	public Point Position {
		get { return position; }
	
		private set {
			position = value;
			RepositionSprite ();
		}
	}

	private void RepositionSprite() {
		if (HasSprite ()) {
			Sprite.transform.position = Position.ToVector ();
		}
	}

	public bool HasSprite() {
		return Sprite != null;
	}

	public GameObject Sprite {
		get { return sprite; }
	}

	public Point Goal { get; set; }
}