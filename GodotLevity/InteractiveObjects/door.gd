extends StaticBody2D

#AnimatedSprite2D
var animatedSprite2D

# Booleans
var isLocked
var isOpen
var shouldClose
var shouldOpen

# Floats
var initScale

# Strings
var DOOR_TYPE

func _animate_door():
	# Door is not locked
	if !isLocked:
		# Door is open
		if isOpen && !shouldClose && !shouldOpen:
			animatedSprite2D.play("door_opened")
			animatedSprite2D.stop()
		elif !isOpen && !shouldClose && !shouldOpen:
			animatedSprite2D.play("door_unlocked")
			animatedSprite2D.stop()
		# Door is opening or closing
		elif shouldOpen:
			if !animatedSprite2D.is_playing() && animatedSprite2D.frame < 10:
				animatedSprite2D.play("door_opening")
		elif shouldClose:
			if !animatedSprite2D.is_playing() && animatedSprite2D.frame < 10:
				animatedSprite2D.play("door_closing")
			# Door finished opening or closing
		if animatedSprite2D.frame == 10:
			if shouldOpen:
				isOpen = true
			else:
				isOpen = false
			shouldOpen = false
			shouldClose = false
	# Door is closed and locked
	else:
		animatedSprite2D.play("door_locked")
		animatedSprite2D.stop()

# Called when the node enters the scene tree for the first time.
func _ready():
	DOOR_TYPE = get_meta("DOOR_TYPE")
	animatedSprite2D = $AnimatedSprite2D
	isOpen = false
	shouldClose = false
	shouldOpen = false
	initScale = get_node("CollisionShape2D").scale
	
	match DOOR_TYPE:
		"locked":
			isLocked = true
			pass
		"unlocked":
			isLocked = false
			pass
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_animate_door()
	if isOpen:
		get_node("CollisionShape2D").scale = Vector2(0, 0)
		get_node("CollisionShape2D").position.y += 20
	else:
		get_node("CollisionShape2D").scale = initScale
		get_node("CollisionShape2D").position.y -= 20
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		if !isLocked:
			if shouldClose:
				var currentFrame = animatedSprite2D.get_frame()
				var currentProgress = animatedSprite2D.get_frame_progress()
				animatedSprite2D.play_backwards("door_closing")
				animatedSprite2D.set_frame_and_progress(currentFrame, currentProgress)
			shouldOpen = true
			shouldClose = false
	pass

func _on_body_exited(body):
	if body.name == "MUSH_Player":
		if shouldOpen:
			var currentFrame = animatedSprite2D.get_frame()
			var currentProgress = animatedSprite2D.get_frame_progress()
			animatedSprite2D.play_backwards("door_opening")
			animatedSprite2D.set_frame_and_progress(currentFrame, currentProgress)
		if !isLocked:
			shouldOpen = false
			shouldClose = true
	pass
