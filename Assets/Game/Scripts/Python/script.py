import json

# Define your game parameters
game_parameters = {
    "balloon_spawn_rate": 5,  # Number of balloons to spawn per second
    "balloon_speed": 2,  # Speed at which balloons float upwards
    # Add more parameters as needed
}

# Write the game parameters to a JSON file
with open('game_parameters.json', 'w') as f:
    json.dump(game_parameters, f)
