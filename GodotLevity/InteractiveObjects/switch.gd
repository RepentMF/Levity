extends Area2D

#AnimatedSprite2D
var animatedSprite2D

#Booleans
var isPushed

# Node2Ds
var GRAVITY_ZONE

# Strings
var SWITCH_TYPE

func _animate_switch():
	if isPushed:
		animatedSprite2D.play("switch_pushed")
	else:
		animatedSprite2D.play("switch_unpushed")

# Called when the node enters the scene tree for the first time.
func _ready():
	GRAVITY_ZONE = get_node(get_meta("GRAVITY_ZONE"))
	SWITCH_TYPE = get_meta("SWITCH_TYPE")
	animatedSprite2D = $AnimatedSprite2D
	isPushed = false
	
	# Handle different colors
	match SWITCH_TYPE:
		"all":
			# Make it purple
			animatedSprite2D.modulate = Color(1,0,1)
			pass
		"wooden":
			# Make it red
			animatedSprite2D.modulate = Color(1,0,0)
			pass
		"anti":
			# Make it blue
			animatedSprite2D.modulate = Color(0,0,1)
			pass
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_animate_switch()
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player" || body.name.contains("box"):
		isPushed = true
		GRAVITY_ZONE.changeNeeded = true
		GRAVITY_ZONE.boxes_affected = SWITCH_TYPE
	pass # Replace with function body.

func _on_body_exited(body):
	if body.name == "MUSH_Player" || body.name.contains("box"):
		isPushed = false
