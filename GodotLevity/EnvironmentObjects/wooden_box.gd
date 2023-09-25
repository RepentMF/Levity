extends CharacterBody2D

#Booleans
var canMove
var DISPLAY_INFO
var isGrounded

# Integers
var GRAVITY_DIRECTION
var GRAVITY_VALUE
var PUSHBOX1
var PUSHBOX2
var gravity
var playerVelocityX

func _ready():
	DISPLAY_INFO = get_meta("DISPLAY_INFO")
	GRAVITY_DIRECTION = get_meta("DIRECTION")
	GRAVITY_VALUE = get_meta("GRAVITY_VALUE")
	canMove = false
	gravity = GRAVITY_VALUE * GRAVITY_DIRECTION
	playerVelocityX = 0
	pass

func _process(delta):
	if DISPLAY_INFO:
		print(isGrounded)
	pass

func _physics_process(delta):
	if is_on_floor() || is_on_ceiling():
		isGrounded = true
	else:
		isGrounded = false
	
	if !isGrounded:
		canMove = true
	
	if canMove:
		gravity = GRAVITY_VALUE * GRAVITY_DIRECTION
		velocity.y += gravity * delta
		if scale.x < 2:
			velocity.x = 0.25 * playerVelocityX
		move_and_slide()
	pass

func _on_box_mover_body_entered(body):
	PUSHBOX1 = get_node("box_mover").get_meta("PUSHBOX1")
	PUSHBOX2 = get_node("box_mover2").get_meta("PUSHBOX2")
	if body.name == "MUSH_Player" && Input.get_axis("ui_left", "ui_right") == PUSHBOX1:
		canMove = true
		playerVelocityX = PUSHBOX1 * 125
		print("enter")
	elif body.name == "MUSH_Player" && Input.get_axis("ui_left", "ui_right") == PUSHBOX2:
		canMove = true
		playerVelocityX = PUSHBOX2 * 125
		print("enter")
	pass # Replace with function body.


func _on_box_mover_body_exited(body):
	if body.name == "MUSH_Player":
		playerVelocityX = 0
		print("exit")
	pass # Replace with function body.
