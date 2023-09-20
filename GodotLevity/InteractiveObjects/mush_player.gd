extends CharacterBody2D

# Booleans
var isTeleporting = false
var isUpsideDown = false

# Integers
var activeTeleporterID = -1
var currentDirection = -1
var currentPowerLevel = 0
var currentWallJumpCount = 0
var jumpVelocity = -400.0
var maxWallJumpCount = 1
var maxPowerLevel = 0
var speed = 300.0

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _animate_direction(direction):
	if direction == 1:
		$Sprite2D.flip_h = true
	elif direction == -1:
		$Sprite2D.flip_h = false
	pass

func _change_gravity():
	gravity *= -1
	jumpVelocity *= -1
	scale.y *= -1
	
	if isUpsideDown:
		isUpsideDown = false
		up_direction = Vector2(0, -1)
	else:
		isUpsideDown = true
		up_direction = Vector2(0, 1)
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _physics_process(delta):
	# Add the gravity.
	if !is_on_floor():
		velocity.y += gravity * delta
	else:
		currentWallJumpCount = 0

	# Handle Jump.
	if Input.is_action_just_pressed("ui_accept") && (is_on_floor() || is_on_ceiling()):
		velocity.y = jumpVelocity
	elif Input.is_action_just_pressed("ui_accept") && is_on_wall() && currentWallJumpCount < maxWallJumpCount:
		velocity.y = jumpVelocity
		currentWallJumpCount += 1

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction = Input.get_axis("ui_left", "ui_right")
	if direction:
		velocity.x = direction * speed
		_animate_direction(direction)
	else:
		velocity.x = move_toward(velocity.x, 0, speed)

	move_and_slide()
