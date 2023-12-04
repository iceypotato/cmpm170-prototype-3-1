using Godot;
using System;

public partial class Lamps : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		for (var i = 0; i < 10; i++)
		{
			var lamp = new Lamp();
			lamp.Position = new Vector2(150 + i * 100, 580);
			lamp.InputEvent += lamp.OnInput;
			lamp.Name = "Lamp" + i;
			AddChild(lamp);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
