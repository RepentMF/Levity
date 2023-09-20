extends Area2D

# Booleans
var GRAVITY_TRIGGER
var LEVEL_DOOR
var MOVING_OBJECT

# Integers
var ID

# Nodes
var DESTINATION

# Called when the node enters the scene tree for the first time.
func _ready():
	GRAVITY_TRIGGER = get_meta("GRAVITY_TRIGGER")
	LEVEL_DOOR = get_meta("LEVEL_DOOR")
	#MOVING_OBJECT = get_meta("MOVING_OBJECT")
	ID = get_meta("ID")
	DESTINATION = get_node(get_meta("DESTINATION"))
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_body_entered(body):
	# If this teleporter is colliding with the player
	if body.name == "MUSH_Player":
		# If the player is not already teleporting
		# and if the player's active teleporter ID is not this teleporter's ID
		if !body.isTeleporting && body.activeTeleporterID != get_meta("ID"):
			# If this telporter is a gravity trigger
			if GRAVITY_TRIGGER:
				# Change the player's gravity
				body._change_gravity()
			
			# Set the player's position to be the partner teleporter's position
			# Set the player's active teleporter ID to be the partner teleporter's ID
			# Set the player to be teleporting
			body.position = DESTINATION.global_position
			body.activeTeleporterID = DESTINATION.ID
			body.isTeleporting = true
			
			if LEVEL_DOOR:
				body.currentPowerLevel = body.maxPowerLevel - 1
	pass # Replace with function body.
	
func _on_body_exited(body):
	if body.name == "MUSH_Player":
		if body.activeTeleporterID == ID:
			body.isTeleporting = false
			body.activeTeleporterID = -1
	pass
