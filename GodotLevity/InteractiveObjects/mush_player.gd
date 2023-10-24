extends CharacterBody2D

#AnimatedSprite2D
var animatedSprite2D

# Booleans
var botSquish = false
var hasKey = false
var hasReset = false
var isDead = false
var isTeleporting = false
var isUpsideDown = false
var isWalljumping = false
var topSquish = false

# Integers
var PUSH_VALUE
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

func _animate_direction(direction):
	if direction == 1:
		animatedSprite2D.flip_h = true
	elif direction == -1:
		animatedSprite2D.flip_h = false
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

func _get_which_wall_collided():
	for i in range(get_slide_collision_count()):
		var collision = get_slide_collision(i)
		if collision.get_normal().x > 0:
			return "left"
		elif collision.get_normal().x < 0:
			return "right"
	pass

func _ready():
	animatedSprite2D = $Sprite2D
	PUSH_VALUE = get_meta("PUSH_VALUE")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _physics_process(delta):
	#Handle death conditions
	if !isDead || !hasReset:
		# Add the gravity
		if !is_on_floor():
			velocity.y += gravity * delta
			if sign(velocity.y) == sign(gravity):
				isWalljumping = false 
		else:
			currentWalljumpCount = 0
			isWalljumping = false

		# Handle Jump or Walljump
		if Input.is_action_just_pressed("ui_accept") && (is_on_floor() || is_on_ceiling()):
			velocity.y = jumpVelocity
		elif Input.is_action_just_pressed("ui_accept") && is_on_wall() && currentWalljumpCount < maxWallJumpCount:
			velocity.y = jumpVelocity
			isWalljumping = true
			currentWalljumpCount += 1

		# Get the input direction and handle the movement/deceleration
		if (!isWalljumping):
			var direction = Input.get_axis("ui_left", "ui_right")
			if direction:
				velocity.x = direction * speed
				_animate_direction(direction)
			else:
				velocity.x = move_toward(velocity.x, 0, speed)
			
			if _get_which_wall_collided() == "right":
				walljumpDirection = -1
			elif _get_which_wall_collided() == "left":
				walljumpDirection = 1
		else:
			velocity.x = walljumpDirection * speed
			_animate_direction(walljumpDirection)
		
		move_and_slide()
		for n in get_slide_collision_count():
			var i = get_slide_collision(n)
			if i.get_collider() is RigidBody2D && i.get_collider().name.contains("box") && Input.is_action_pressed("ui_cancel"):
				i.get_collider().apply_impulse(Vector2(-i.get_normal().x, 0) * PUSH_VALUE / 1.5)
	
	if (topSquish && botSquish) || isDead:
		isDead = true
	else:
		isDead = false
		
	# Handle resets
	if Input.is_action_just_pressed("ui_select"):
		hasReset = true
	pass
	
func _on_bot_body_entered(body):
	if body.name.contains("floor") || body.name.contains("ceiling") || body.name.contains("box") || body.get_parent().name.contains("switch"):
		botSquish = true
	pass

func _on_bot_body_exited(body):
	if body.name.contains("floor") || body.name.contains("ceiling") || body.name.contains("box") || body.get_parent().name.contains("switch"):
		botSquish = false
	pass
	
func _on_top_body_entered(body):
	if body.name.contains("floor") || body.name.contains("ceiling") || body.name.contains("box") || body.get_parent().name.contains("switch"):
		topSquish = true
	pass

func _on_top_body_exited(body):
	if body.name.contains("floor") || body.name.contains("ceiling") || body.name.contains("box") || body.get_parent().name.contains("switch"):
		topSquish = false
	pass
