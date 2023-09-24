extends Area2D

# Arrays
var objects_array

# Booleans
var changeNeeded

# Node2Ds
var ONOFF_SWITCH

# Called when the node enters the scene tree for the first time.
func _ready():
	objects_array = []
	changeNeeded = false
	ONOFF_SWITCH = get_node(get_meta("ONOFF_SWITCH"))
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if objects_array != null && changeNeeded:
		if ONOFF_SWITCH.isOn:
			for n in objects_array:
				n.GRAVITY_DIRECTION *= -1
				n.canMove = true
		else:
			for n in objects_array:
				n.GRAVITY_DIRECTION *= -1
				n.canMove = true
		
		var count = 0
		for n in objects_array:
			if n.isGrounded:
				count += 1
			if count == objects_array.size():
				changeNeeded = false
	pass

func _on_body_entered(body):
	if body.name.contains("box"):
		objects_array.append(body)
	pass # Replace with function body.
