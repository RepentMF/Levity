extends Area2D

# Booleans
var DISPLAY
var GRAVITY_TRIGGER
var MOVE_OBJECT
var firstPass

# Floats
var FINAL_POSX
var FINAL_POSY
var INIT_MOVE_TIMER
var INIT_POSX
var INIT_POSY
var MOVE_TIMER

# Integers
var BOUNCE_SPEED
var count = 0

# Vector2s
var DIRECTION
var SPEED

func _move_object(delta):
	if MOVE_OBJECT:
		if MOVE_TIMER > 0 && !firstPass:
			position.x += (SPEED.x * DIRECTION.x)
			position.y += (SPEED.y * DIRECTION.y)
			MOVE_TIMER -= delta
		elif MOVE_TIMER <= 0 && !firstPass:
			DIRECTION *= -1
			firstPass = true
			FINAL_POSX = position.x
			FINAL_POSY = position.y
		if firstPass:
			if position.x > FINAL_POSX && DIRECTION.x == 1:
				DIRECTION *= -1
			elif position.x < INIT_POSX && DIRECTION.x == -1:
				DIRECTION *= -1
			else:
				position.x += (SPEED.x * DIRECTION.x)
				position.y += (SPEED.y * DIRECTION.y)
		if DISPLAY:
			print(sign(SPEED.x), " ", SPEED.x)
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	DISPLAY = get_meta("DISPLAY")
	GRAVITY_TRIGGER = get_meta("GRAVITY_TRIGGER")
	MOVE_OBJECT = get_meta("MOVE_OBJECT")
	DIRECTION = get_meta("DIRECTION")
	INIT_MOVE_TIMER = get_meta("MOVE_TIMER")
	MOVE_TIMER = get_meta("MOVE_TIMER")
	SPEED = get_meta("SPEED")
	BOUNCE_SPEED = get_meta("BOUNCE_SPEED")
	INIT_POSX = position.x
	INIT_POSY = position.y
	
	firstPass = false
	
	if GRAVITY_TRIGGER:
		get_node("small_light").color = Color(0, 1, 0, 1)
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_move_object(delta)
	pass
	
func _on_area_entered(area):
	# If this bounce is colliding with the player
	if area.owner.name == "MUSH_Player":
		if area.owner.isUpsideDown:
			area.owner.velocity.y = -BOUNCE_SPEED
		else:
			area.owner.velocity.y = BOUNCE_SPEED
			
		# If this bounce is a gravity trigger
		if GRAVITY_TRIGGER:
			# Change the player's gravity
			area.owner._change_gravity()
		
		# If the player does not have full wall jump amount
		if area.owner.currentWalljumpCount != 0:
			area.owner.currentWalljumpCount = 0
	pass

func _on_body_entered(body):
	# If this bounce is colliding with a box
	if body.name.contains("box"):
		if body.GRAVITY_DIRECTION == 1:
			body.linear_velocity.y = BOUNCE_SPEED
		elif body.GRAVITY_DIRECTION == -1:
			body.linear_velocity.y = -BOUNCE_SPEED
	pass
