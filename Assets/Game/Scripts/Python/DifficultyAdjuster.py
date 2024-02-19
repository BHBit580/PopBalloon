import time
import random

class DifficultyAdjuster:

    minBalloonPerSpawn = 1
    maxBalloonPerSpawn = 5

    minSpeed = 1.0
    maxSpeed = 5.0

    def __init__(self):
        self.last_time = time.time()


    def getBalloonSpawn(self):
        current_time = time.time()
        if current_time - self.last_time >= 3:
            # If 3 or more seconds have passed, increase the minimum number of balloons to spawn
            DifficultyAdjuster.minBalloonPerSpawn = min(DifficultyAdjuster.minBalloonPerSpawn + 1, DifficultyAdjuster.maxBalloonPerSpawn)
            self.last_time = current_time

        if(DifficultyAdjuster.minBalloonPerSpawn == DifficultyAdjuster.maxBalloonPerSpawn): return DifficultyAdjuster.maxBalloonPerSpawn
        return random.randrange(DifficultyAdjuster.minBalloonPerSpawn, DifficultyAdjuster.maxBalloonPerSpawn)


    def balloonSpeed(self):                                
        current_time = time.time()
        if current_time - self.last_time >= 3:
            DifficultyAdjuster.minSpeed = min(DifficultyAdjuster.minSpeed + 0.5, DifficultyAdjuster.maxSpeed)
            DifficultyAdjuster.maxSpeed = min(DifficultyAdjuster.maxSpeed + 1, 10)  # Assuming 10 as the maximum speed limit
            self.last_time = current_time
        
        if(DifficultyAdjuster.minSpeed == DifficultyAdjuster.maxSpeed): return DifficultyAdjuster.maxSpeed
        return random.uniform(DifficultyAdjuster.minSpeed , DifficultyAdjuster.maxSpeed)
