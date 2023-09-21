extends CharacterBody2D

# Booleans
var isDead = false
var isTeleporting = false
var isUpsideDown = false
var isWalljumping = false

# Integers
var activeTeleporterID = -1
var currentDirection = -1
var currentPowerLevel = 0
var currentRespawnPoint = 0
var currentWalljumpCount = 0
var jumpVelocity = -200.0
var maxWallJumpCount = 1
var maxPowerLevel = 0
var speed = 125.0
var walljumpDirection = 0

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = 525
#ProjectSettings.get_setting("physics/2d/default_gravity")

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

func get_which_wall_collided():
	for i in range(get_slide_collision_count()):
		var collision = get_slide_collision(i)
		if collision.get_normal().x > 0:
			return "left"
		elif collision.get_normal().x < 0:
			return "right"
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _physics_process(delta):
	# Add the gravity.
	if !is_on_floor():
		velocity.y += gravity * delta
		if sign(velocity.y) == sign(gravity):
			isWalljumping = false 
	else:
		currentWalljumpCount = 0
		isWalljumping = false

	# Handle Jump or Walljump.
	if Input.is_action_just_pressed("ui_accept") && (is_on_floor() || is_on_ceiling()):
		velocity.y = jumpVelocity
	elif Input.is_action_just_pressed("ui_accept") && is_on_wall() && currentWalljumpCount < maxWallJumpCount:
		velocity.y = jumpVelocity
		isWalljumping = true
		currentWalljumpCount += 1

	# Get the input direction and handle the movement/deceleration.
	if (!isWalljumping):
		var direction = Input.get_axis("ui_left", "ui_right")
		if direction:
			velocity.x = direction * speed
			_animate_direction(direction)
		else:
			velocity.x = move_toward(velocity.x, 0, speed)
		if get_which_wall_collided() == "right":
			walljumpDirection = -1
		elif get_which_wall_collided() == "left":
			walljumpDirection = 1
	else:
		velocity.x = walljumpDirection * speed
		_animate_direction(walljumpDirection)

	move_and_slide()


func _on_absence_mesh_body_entered(body):
	pass # Replace with function body.
