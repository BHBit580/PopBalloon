import time
import random

class DifficultyAdjuster:
    minBalloonPerSpawn = 1
    maxBalloonPerSpawn = 5

    minSpeed = 1.0
    maxSpeed = 5.0

    def __init__(self):
        self.last_time = time.time()
        self.last_values = self.generate_values()

    def generate_values(self):
        return random.randrange(DifficultyAdjuster.minBalloonPerSpawn, DifficultyAdjuster.maxBalloonPerSpawn)

    def getBalloonSpawn(self):
        current_time = time.time()
        if current_time - self.last_time < 3:
            # If less than 3 seconds have passed, return the last values
            return self.last_values
        else:
            # Otherwise, generate new values and update the last values and time
            DifficultyAdjuster.minBalloonPerSpawn = min(DifficultyAdjuster.minBalloonPerSpawn + 1, DifficultyAdjuster.maxBalloonPerSpawn)
            self.last_values = self.generate_values()
            self.last_time = current_time
            return self.last_values

    def balloonSpeed(self):                                
        current_time = time.time()
        if current_time - self.last_time < 3:
            return random.uniform(DifficultyAdjuster.minSpeed , DifficultyAdjuster.maxSpeed)
        else:
            DifficultyAdjuster.minSpeed = min(DifficultyAdjuster.minSpeed + 0.5, DifficultyAdjuster.maxSpeed)
            DifficultyAdjuster.maxSpeed = min(DifficultyAdjuster.maxSpeed + 1, 10)  # Assuming 10 as the maximum speed limit
            self.last_time = current_time
            return random.uniform(DifficultyAdjuster.minSpeed , DifficultyAdjuster.maxSpeed)
