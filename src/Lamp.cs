using Godot;
using System;

public partial class Lamp : Area2D
{
	private bool _isMouseEntered = false;
	private bool _isLampOn = false;

	public bool IsLampOn
	{
		get => _isLampOn;
		set
		{
			var sprite = GetNode<Sprite2D>("Lamp");
			if (value)
			{
				sprite.Texture = GD.Load<Texture2D>("res://assets/lamp on.png");
				this.Lbl.Hide();
			}
			else
			{
				sprite.Texture = GD.Load<Texture2D>("res://assets/lamp off.png");
				this.Lbl.Show();
			}
			_isLampOn = value;
		}
	}

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
		if (Input.IsActionJustPressed("mouse1"))
		{
			IsLampOn = !IsLampOn;
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
