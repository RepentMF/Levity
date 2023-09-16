extends Area2D

var DESTINATION = get_meta("Destination")

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _get_node_list():
	pass

func _on_body_entered(body):
	# If this teleporter is colliding with the player
	if body.name == "MUSH_Player":
		# If the player is not already teleporting
		# and if the player's active teleporter ID is not this teleporter's ID
		if !body.isTeleporting && body.activeTeleporterID != get_meta("ID"):
			# If this telporter is a gravity trigger
			if get_meta("GRAVITY_TRIGGER"):
				# Change the player's gravity
				body.gravity *= -1
			
			# Set the player's position to be the partner teleporter's position
			# Set the player's active teleporter ID to be the partner teleporter's ID
			# Set the player to be teleporting
			body.position = DESTINATION.position
			
			#body.activeTeleporterID = get_meta("Destination").get_meta("ID")
			#body.isTeleporting = true
	pass # Replace with function body.
