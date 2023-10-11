extends StaticBody2D

#AnimatedSprite2D
var animatedSprite2D

# Booleans
var DISPLAY_VALUE
var UNMARKED
var closedFlag
var openFlag

# Floats
var initPosY
var initScale
var previousFrame

# Strings
var DOOR_TYPE
var currentState

func _change_state(newState):
	if currentState != newState:
		currentState = newState
	pass

func _handle_state():
	match currentState:
		"locked":
			animatedSprite2D.play("door_locked")
			pass
		"unlocked":
			if UNMARKED:
				animatedSprite2D.play("door_unlocked_unmarked")
			else:
				animatedSprite2D.play("door_unlocked")
			if !closedFlag:
				get_node("CollisionShape2D").position.y = initPosY
				get_node("CollisionShape2D").scale = initScale
			closedFlag = true
			openFlag = false
			pass
		"opening":
			if animatedSprite2D.frame == 10:
				_change_state("opened")
			else:
				if UNMARKED:
					animatedSprite2D.play("door_opening_unmarked")
				else:
					animatedSprite2D.play("door_opening")
				if previousFrame != -1:
					animatedSprite2D.set_frame(previousFrame)
					previousFrame = -1
			pass
		"closing":
			if animatedSprite2D.frame == 10:
				_change_state("unlocked")
			else:
				if UNMARKED:
					animatedSprite2D.play("door_closing_unmarked")
				else:
					animatedSprite2D.play("door_closing")
				if previousFrame != -1:
					animatedSprite2D.set_frame(previousFrame)
					previousFrame = -1
			pass
		"opened":
			animatedSprite2D.play("door_opened")
			if !openFlag:
				get_node("CollisionShape2D").position.y = initPosY + 20
				get_node("CollisionShape2D").scale = Vector2(0, 0)
			closedFlag = false
			openFlag = true
			pass
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	DISPLAY_VALUE = get_meta("DISPLAY_VALUE")
	UNMARKED = get_meta("UNMARKED")
	closedFlag = false
	openFlag = false
	initScale = get_node("CollisionShape2D").scale
	initPosY = get_node("CollisionShape2D").position.y
	previousFrame = -1
	DOOR_TYPE = get_meta("DOOR_TYPE")
	animatedSprite2D = $AnimatedSprite2D
	
	_change_state(DOOR_TYPE)
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_handle_state()
	if DISPLAY_VALUE:
		print(get_node("CollisionShape2D").position.y)
	pass

func _on_body_entered(body):
	if body.name == "MUSH_Player":
		if currentState == "locked" && body.hasKey:
			_change_state("unlocked")
		if currentState != "locked":
			if currentState == "closing":
				previousFrame = 10 - animatedSprite2D.frame
			_change_state("opening")
	pass

func _on_body_exited(body):
	if body.name == "MUSH_Player":
		if currentState != "locked":
			if currentState == "opening":
				previousFrame = 10 - animatedSprite2D.frame
			_change_state("closing")
	pass