extends Area2D

# Booleans
var GRAVITY_TRIGGER
var MOVE_OBJECT

# Floats
var DIRECTION
var INIT_MOVE_TIMER
var MOVE_TIMER
var SPEED

# Integers
var BOUNCE_SPEED

func _move_object(delta):
	if MOVE_OBJECT:
		if MOVE_TIMER > 0:
			position.x += (SPEED.x * DIRECTION.x)
			position.y += (SPEED.y * DIRECTION.y)
			MOVE_TIMER -= delta
		elif MOVE_TIMER <= 0:
			DIRECTION *= -1
			MOVE_TIMER = INIT_MOVE_TIMER
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	GRAVITY_TRIGGER = get_meta("GRAVITY_TRIGGER")
	MOVE_OBJECT = get_meta("MOVE_OBJECT")
	DIRECTION = get_meta("DIRECTION")
	INIT_MOVE_TIMER = get_meta("MOVE_TIMER")
	MOVE_TIMER = get_meta("MOVE_TIMER")
	SPEED = get_meta("SPEED")
	BOUNCE_SPEED = get_meta("BOUNCE_SPEED")
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_move_object(delta)
	pass
	
func _on_area_entered(area):
	# If this teleporter is colliding with the player
	if area.owner.name == "MUSH_Player":
		if area.owner.isUpsideDown:
			area.owner.velocity.y = -BOUNCE_SPEED
		else:
			area.owner.velocity.y = BOUNCE_SPEED
			
		# If this telporter is a gravity trigger
		if GRAVITY_TRIGGER:
			# Change the player's gravity
			area.owner._change_gravity()
	pass
