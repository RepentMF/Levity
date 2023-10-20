extends Area2D

# Arrays
var objects_array

# Booleans
var changeNeeded

# Strings
var boxes_affected

# Called when the node enters the scene tree for the first time.
func _ready():
	objects_array = []
	changeNeeded = false
	boxes_affected = ""
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if objects_array != null && changeNeeded:
		for n in objects_array:
			if n.name.contains(boxes_affected) || boxes_affected == "all":
				n.GRAVITY_DIRECTION *= -1
		changeNeeded = false
	pass

func _on_body_entered(body):
	if body.name.contains("box"):
		objects_array.append(body)
	pass # Replace with function body.
