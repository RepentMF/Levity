extends Area2D

# Booleans
var MOVE_OBJECT

# Floats
var INIT_MOVE_TIMER
var MOVE_TIMER

# Vector2s
var DIRECTION
var SPEED

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
	MOVE_OBJECT = get_meta("MOVE_OBJECT")
	DIRECTION = get_meta("DIRECTION")
	INIT_MOVE_TIMER = get_meta("MOVE_TIMER")
	MOVE_TIMER = get_meta("MOVE_TIMER")
	SPEED = get_meta("SPEED")
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_move_object(delta)
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		body.isDead = true
		#print(body.isDead)
	pass # Replace with function body.
