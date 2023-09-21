extends Area2D

# Arrays
var objects_array

# Booleans
var isChangeNeeded

# Node2Ds
var ONOFF_SWITCH

# Called when the node enters the scene tree for the first time.
func _ready():
	objects_array = []
	isChangeNeeded = false
	ONOFF_SWITCH = get_node(get_meta("ONOFF_SWITCH"))
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if objects_array != null && isChangeNeeded:
		if ONOFF_SWITCH.isOn:
			for n in objects_array:
				n.gravity = -gravity
				n.canMove = true
		else:
			for n in objects_array:
				n.gravity = gravity
				n.canMove = true
		isChangeNeeded = false
	pass

func _on_body_entered(body):
	if body.name.contains("box"):
		objects_array.append(body)
	pass # Replace with function body.
