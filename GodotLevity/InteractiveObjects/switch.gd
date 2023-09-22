extends Area2D

# Booleans
var isOn = false

# Node2Ds
var GRAVITY_ZONE

# Called when the node enters the scene tree for the first time.
func _ready():
	GRAVITY_ZONE = get_node(get_meta("GRAVITY_ZONE"))
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		if isOn:
			isOn = false
		else:
			isOn = true
		GRAVITY_ZONE.changeNeeded = true
	pass # Replace with function body.
