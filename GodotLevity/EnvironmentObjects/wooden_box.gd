extends CharacterBody2D

#Booleans
var canMove

# Integers
var gravity

func _ready():
	canMove = false
	gravity = 0
	pass

func _physics_process(delta):
	if canMove:
		velocity.y += gravity * delta
		move_and_slide()
	pass
