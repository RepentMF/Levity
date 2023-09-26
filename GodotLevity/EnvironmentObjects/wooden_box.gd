extends RigidBody2D

#Booleans
var DISPLAY_VALUE

# Integers
var GRAVITY_DIRECTION
var SIZE
var gravity

func _ready():
	DISPLAY_VALUE = get_meta("DISPLAY_VALUE")
	GRAVITY_DIRECTION = get_meta("DIRECTION")
	if get_meta("SIZE") > 1:
		SIZE = get_meta("SIZE")
		get_node("CollisionShape2D").scale.x = SIZE
		get_node("CollisionShape2D").scale.y = SIZE
		$Sprite2D.scale.x = SIZE
		$Sprite2D.scale.y = SIZE
		mass = pow(SIZE, 3)
	pass

func _process(delta):
	if DISPLAY_VALUE:
		pass
	pass

func _physics_process(delta):
	gravity_scale = GRAVITY_DIRECTION
	pass
