using Godot;
using System;

public partial class Lamp : Area2D
{
	private bool _isMouseEntered = false;
	private bool _isLampOn = false;

	public Label Lbl { get; set; }

// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var sprite = new Sprite2D();
		sprite.Texture = GD.Load<Texture2D>("res://assets/lamp off.png");
		sprite.Name = "Lamp";
		AddChild(sprite);
		var collision = new CollisionShape2D();
		collision.Shape = new RectangleShape2D();
		((RectangleShape2D)collision.Shape).Size = new Vector2(60.25f, 78.5f);
		collision.Position = new Vector2(16, -24);
		collision.Name = "Collision";
		AddChild(collision);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(this.Lbl is not null) {
			this.Lbl.Position = this.Position + (new Vector2(0, -400));
		}
		ProcessInput();
	}
	
	public void OnInput(Node viewport, InputEvent @event, long shape_idx)
	{
		if (Input.IsActionPressed("mouse1"))
		{
			var sprite = GetNode<Sprite2D>("Lamp");
			Texture2D image;
			if (_isLampOn)
			{
				image = GD.Load<Texture2D>("res://assets/lamp off.png");
				_isLampOn = false;
				this.Lbl.Show();
			}
			else
			{
				image = GD.Load<Texture2D>("res://assets/lamp on.png");
				_isLampOn = true;
				this.Lbl.Hide();
			}
			sprite.Texture = image;
			GD.Print(Lbl.Text);
		}
	}
	
	public void OnMouseEntered()
	{
		_isMouseEntered = true;
		GD.Print("mouse entered");
	}


	public void OnMouseExited()
	{
		_isMouseEntered = false;
		GD.Print("mouse exited");
	}

	private void ProcessInput()
	{

	}
}
