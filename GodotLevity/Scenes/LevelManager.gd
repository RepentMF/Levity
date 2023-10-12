extends Node2D

# Booleans
var done
var shouldStartCount

# Integers
var count

# ScenePacks
var level

# Strings
var LEVEL_PREFAB


func _delete_room():
	self.get_child(0).queue_free()
	pass
	
func _reset_room():
	_delete_room()
	shouldStartCount = true
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	count = 0
	done = false
	shouldStartCount = false
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if !done:
		LEVEL_PREFAB = str("res://Scenes/", get_child(0).name, ".tscn")
		#print(name, ", ", LEVEL_PREFAB)
		level = load(LEVEL_PREFAB)
		done = true
	
	if shouldStartCount:
		count += 1
		if count > 5:
			count = 0
			shouldStartCount = false
			var level_instance = level.instantiate()
			self.add_child(level_instance)
	pass
